using UnityEngine;
using UnityEngine.AI;
using AbstractFactory;
using Observer;

public class EnemyAI : MonoBehaviour
{    
    [SerializeField]
    private EEnemy enemy;

    private Transform player, spawnPoint, target;
    private NavMeshAgent agent;
    private Enemy enemyType;
    private Weapon weapon;
    private EnemyObserver observer;

    private float currentDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerStats.instance.transform;
        spawnPoint = Instantiate(new GameObject("SpawnPoint").transform, transform.parent);
        player = target;
        InvokeRepeating(nameof(GetPathRemainingDistance), 1f, 1f);

        EnemyFactory factory = Extentions.GetFactoryByEnum(enemy);
        observer = new EnemyObserver(transform, PlayerStats.instance);
        PlayerStats.instance.RegisterObserver(observer);

        enemyType = factory.CreateEnemy();
        weapon = factory.CreateWeapon(transform);

        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        if (enemyType == null)
            return;

        GetComponent<MeshRenderer>().materials[0].color = enemyType.stats.EnemyColor;
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

        if (!float.IsInfinity(agent.remainingDistance))
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
    }

    public void AvoidPlayer()
    {
        target = spawnPoint;
    }

    private void FixedUpdate()
    {
        agent.SetDestination(target.position);
        if (currentDistance > enemyType.stats.ChaseDistance) {
            agent.speed = 0f;
            return;
        }
        agent.speed = enemyType.stats.Speed;
    }

}
