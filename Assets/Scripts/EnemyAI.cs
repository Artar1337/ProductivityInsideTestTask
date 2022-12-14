using UnityEngine;
using UnityEngine.AI;
using AbstractFactory;
using Observer;

// handles pathfinding
// enemy's behaviour
// attacking trigger for player and for instance of enemy

public class EnemyAI : MonoBehaviour
{    
    [SerializeField]
    private EEnemy enemy;
    [SerializeField]
    private float pathRecalculateRate = 0.3f;
    [SerializeField]
    private int maxCorners = 50;

    private Transform player, spawnPoint, target;
    private NavMeshAgent agent;
    private Enemy enemyType;
    private Weapon weapon;
    private new MeshRenderer renderer;
    private EnemyObserver observer;
    private int skipUpdate = 0;

    private float currentDistance = float.MaxValue;
    private bool followPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerStats.instance.transform;
        Transform tmp = new GameObject("SpawnPoint").transform;
        spawnPoint = Instantiate(tmp, transform.parent);
        Destroy(tmp.gameObject);
        player = target;
        InvokeRepeating(nameof(GetPathRemainingDistance), 
             Resources.instance.GetRandomFloat(0f, 1f),
             pathRecalculateRate);

        EnemyFactory factory = Extentions.GetFactoryByEnum(enemy);
        observer = new EnemyObserver(transform);
        PlayerStats.instance.RegisterObserver(observer);

        enemyType = factory.CreateEnemy(transform);
        weapon = factory.CreateWeapon(transform);

        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        if (enemyType == null)
            return;

        GetComponent<MeshRenderer>().materials[0].color = enemyType.stats.EnemyColor;
        GetComponent<CapsuleCollider>().radius *= weapon.weapon.Range;
    }

    public void GetPathRemainingDistance()
    {
        if (agent.pathPending ||
            agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            agent.path.corners.Length == 0) 
        { 
            currentDistance = float.MaxValue;
            return;
        }

        if (!float.IsInfinity(agent.remainingDistance) || agent.path.corners.Length > maxCorners)
        {
            currentDistance = agent.remainingDistance;
            return;
        }

        float distance = 0.0f;
        for (int i = 0; i < agent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(agent.path.corners[i], agent.path.corners[i + 1]);
        }

        currentDistance = distance;
    }

    public void FollowPlayer()
    {
        target = player;
        followPlayer = true;
        Color c = renderer.materials[0].color;
        renderer.materials[0].color = new Color(c.r, c.g, c.b, 1f);
    }

    public void AvoidPlayer()
    {
        target = spawnPoint;
        followPlayer = false;
    }

    public void AttackPlayer()
    {
        weapon.Hit();
        PlayerStats.instance.RecieveHit(weapon.weapon.Damage);
        AvoidPlayer();
        // skipping 50 updates
        skipUpdate = 50;
    }

    private void FixedUpdate()
    {
        if (!agent.enabled)
            return;

        agent.SetDestination(target.position);

        if (!followPlayer && Mathf.Abs(agent.remainingDistance) < 0.2f && target == spawnPoint)
        {
            Color c = renderer.materials[0].color;
            renderer.materials[0].color = new Color(c.r, c.g, c.b, 0.05f);
        }
        
        if (skipUpdate > 0) 
        {
            skipUpdate--;
            if (skipUpdate <= 0)
                FollowPlayer();
            return;
        }

        if (currentDistance > enemyType.stats.ChaseDistance) {
            agent.speed = 0f;
            return;
        }
        agent.speed = enemyType.stats.Speed;
    }

    public void StopObserve()
    {
        PlayerStats.instance.RemoveObserver(observer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (PlayerStats.instance.InRageMode)
                enemyType.RecieveHit(PlayerStats.instance.Damage);
            else
                AttackPlayer();
        }
    }
}
