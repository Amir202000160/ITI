using UnityEngine;

public class SpikeHead : Hit
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float range = 10.0f;
    [SerializeField] private float checkDelay = 8.0f;
    [SerializeField] private LayerMask playerLayer;

    private float checkTimer;
    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private bool attacking;


    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CanSeePlayer();
            }
        }
    }
    private void CanSeePlayer()
    {
        CalculateDirection();
        //check if spike see player
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        directions[0] = transform.right * range; //right
        directions[1] = -transform.right * range; //left
        directions[2] = transform.up * range; //up
        directions[3] = -transform.up * range; //down
    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
