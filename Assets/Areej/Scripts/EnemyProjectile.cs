using UnityEngine;

public class EnemyProjectile : Hit
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float resetTime = 5.0f;

    private float arrowLifeTime;

    public void ActivateProjectile()
    {
        arrowLifeTime = 0;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(Vector3.right * movementSpeed);
        arrowLifeTime += Time.deltaTime;
        if (arrowLifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }

}
