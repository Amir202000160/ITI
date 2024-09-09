using UnityEngine;

public class EnemyAttack : Hit
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [SerializeField] private Transform target;

    private float attackTime;


    private EnemyChasing chasing;

    private void Start()
    {
        chasing = GetComponent<EnemyChasing>();
    }
    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        Vector3 direction = (target.position - transform.position).normalized;

        if (distanceToTarget < attackDelay && chasing.isChasing)
        {
            if (Time.time > attackDelay + attackDelay)
            {
                //animation play
                Debug.Log("Ataackedddddddddddddd");
                attackTime = Time.time;
            }

        }
    }
    private void EnemyDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
