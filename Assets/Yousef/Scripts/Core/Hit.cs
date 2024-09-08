// Import necessary libraries
using UnityEngine;

// Declare the Hit class
public class Hit : MonoBehaviour {
    [Header("Damage:")]
    [Tooltip("Amount of damage dealt by the hit")]
    [SerializeField] private float Damage; // Amount of damage dealt by the hit

    // Called when another Collider2D enters the trigger (2D physics only)
    public virtual void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision involves an enemy and the hit object is Player Weapon
        if (collision.tag == "Enemy" && gameObject.tag == "Player Weapon") {
            // Check if the enemy is not dead
            /*if (collision.GetComponent<Enemy>().Health > 0) {
                // Apply the damage to the enemy
                collision.GetComponent<Enemy>().TakeDamage(Damage);
            }*/
        }

        // Check if the collision involves a player and the hit object is Enemy Weapon
        if (collision.tag == "Player" && gameObject.tag == "Enemy Weapon") {
            // Check if the Player is not dead
            if (collision.GetComponent<PlayerMovement>().Health > 0) {
                // Apply the damage to the player
                collision.GetComponent<PlayerMovement>().TakeDamage(Damage);
            }
        }
    }
}