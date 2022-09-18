using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{    
    [SerializeField]
    private EEnemy enemy;
    [SerializeField]
    private float reAssignHealth = -0.01f, reAssignChaseDistance = -0.01f;

    private Transform target;
    private NavMeshAgent agent;
    private Enemy enemyType;
    private Weapon weapon;

    private float currentDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        InvokeRepeating(nameof(GetPathRemainingDistance), 1f, 1f);
        EnemyFactory factory;
        if (enemy == EEnemy.Red)
            factory = new RedEnemyFactory();
        else if (enemy == EEnemy.Blue)
            factory = new BlueEnemyFactory();
        else
            return;

        enemyType = factory.CreateEnemy();
        weapon = factory.CreateWeapon();
        if (reAssignChaseDistance > 0f)
            enemyType.ChaseDistance = reAssignChaseDistance;
        if (reAssignHealth > 0f)
            enemyType.Health = reAssignHealth;

        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        if (enemyType == null)
            return;

        GetComponent<MeshRenderer>().materials[0].color = enemyType.EnemyColor;
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

    private void FixedUpdate()
    {
        agent.SetDestination(target.position);
        if (currentDistance > enemyType.ChaseDistance) {
            agent.speed = 0f;
            return;
        }
        agent.speed = 5f;
    }

}
