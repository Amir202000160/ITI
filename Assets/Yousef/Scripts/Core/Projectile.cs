// Import necessary libraries
using UnityEngine;

// Declare the Projectile class
public class Projectile : MonoBehaviour {
    [Header("Projectile:")]
    [Tooltip("Rigidbody2D component for the projectile")]
    public Rigidbody2D rb; // Reference to the Rigidbody2D component for the projectile
    [Tooltip("Projectile speed")]
    public float Speed; // Projectile speed

    // Projectile lifetime parameters
    [Header("Projectile Life Time:")]
    public float LifeTime;
    private float LifeTimeSec;

    // Called when the script is first initialized
    private void Start() {
        // Get the Rigidbody2D component from the game object
        rb = GetComponent<Rigidbody2D>();
        // Initialize the remaining lifetime of the projectile
        LifeTimeSec = LifeTime;
    }

    // Called every frame
    private void Update() {
        // Decrease the remaining lifetime of the projectile over time
        LifeTimeSec -= Time.deltaTime;
        // Destroy the projectile if it exceeds its defined lifetime
        if (LifeTimeSec <= 0) {
            Destroy(this.gameObject);
        }
    }

    // Method to fire the projectile in a specified direction
    public void Fire(int Direction) {
        // Set the velocity of the projectile based on the specified direction and speed
        transform.localScale = new Vector3(transform.localScale.x * Direction, transform.localScale.y, transform.localScale.z);
        rb.linearVelocity = new Vector2(Direction * Speed, rb.linearVelocity.y);
    }

    // Method called when the projectile collides with another collider
    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with an object tagged as "Player" and is set as a trigger
        if (collision.tag == "Player") {
            // Destroy the projectile upon colliding with a player
            Destroy(this.gameObject);
        }
    }
}