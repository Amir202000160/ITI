using UnityEngine;

public class Potion : Item {
    [SerializeField] private float HealthAmount;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerMovement>().Health += HealthAmount;
            Destroy(gameObject);
        }
    }
}