using UnityEngine;

public class EnemyPetrol : MonoBehaviour
{
    [SerializeField] Transform[] petrolPoints;
    [SerializeField] private float speed = 5.0f;

    private Transform currentPetrolPoint;
    private int currentPetrolIndex;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //transform.position = petrolPoints[0].position;
        currentPetrolIndex = 0;
        currentPetrolPoint = petrolPoints[currentPetrolIndex];
    }

    private void Update()
    {
        Vector3 direction = (currentPetrolPoint.position - transform.position).normalized;
        // Move in the direction of the patrol point
        transform.position += direction * speed * Time.deltaTime;
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, currentPetrolPoint.position) < 0.1f)
        {
            if (currentPetrolIndex + 1 < petrolPoints.Length)
            {
                currentPetrolIndex++;
            }
            else
            {
                currentPetrolIndex = 0;
            }
            currentPetrolPoint = petrolPoints[currentPetrolIndex];
            //animation play
        }
        EnemyDirection(direction);
    }
    private void EnemyDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
