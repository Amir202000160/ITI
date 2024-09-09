using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange;
    [SerializeField] private float speed = 10.0f;

    public bool isChasing = false;
    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        Vector3 direction = (target.position - transform.position).normalized;
        if (distanceToTarget < chaseRange)
        {
            EnemyDirection(direction);
            transform.position += direction * speed * Time.deltaTime;
            //animation play
            Debug.Log("Start Chaseing");
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }
    private void EnemyDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
