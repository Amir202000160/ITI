// Import necessary libraries
using UnityEngine;

// Declare the Enemy class
public class Enemy : MonoBehaviour {
    // Enemy state and related components
    [Header("Enemy Info:")]
    [Tooltip("Movement speed of the Enemy")]
    [SerializeField] private float Speed; // Movement speed of the Enemy
    [Tooltip("Animator component for enemy animations")]
    [SerializeField] private Animator animator; // Reference to the animator component for enemy animations
    [Tooltip("Rigidbody2D component for physics simulation")]
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component for physics simulation
    [Tooltip("CollideDetection script for Detections")]
    [SerializeField] private CollideDetection Detection; // Reference to Collide Detection script for Detections
    [Tooltip("EnemySearch component")]
    [SerializeField] private EnemySearch Search; // Reference to the enemy search component
    [Tooltip("Time the enemy remains idle before resuming movement")]
    [SerializeField] private float IdleTime; // Time the enemy remains idle before resuming movement
    private bool Move = true; // Flag to indicate whether the enemy can move or not
    private float Direction; // Direction the enemy is facing
    private float IdleTimer; // Timer to track how long the enemy has been idle

    private bool NeedToAdd;
    private bool NeedToSub;
    [SerializeField] private float FlipAmount;

    // Enemy Health and related components
    [Header("Enemy Health:")]
    [Tooltip("Current health of the enemy")]
    public float Health; // Current health of the enemy
    private PlayerMovement PlayerHealth; // Current health of the player

    // Range Enemy and related components
    [Header("Range Enemy")]
    [Tooltip("The projectile GameObject")]
    [SerializeField] private GameObject projectile = null; // Reference to the projectile GameObject
    [Tooltip("The point from which the projectile will be fired")]
    [SerializeField] private GameObject FirePoint = null; // Reference to the point from which the projectile will be fired
    [Tooltip("The delay between consecutive shots fired by the enemy")]
    [SerializeField] private float AttackDelay; // The delay between consecutive shots fired by the enemy
    private float AttackDelaySeconds; // The remaining time until the enemy can fire again
    private bool CanAttack = true; // Flag indicating whether the enemy is currently able to fire

    // Called when the script is first initialized
    private void Start() {
        // Set initial direction based on the local scale of the transform
        Direction = transform.localScale.x;
        if (Direction > 0) {
            NeedToAdd = true;
            NeedToSub = false;
        }
        else if (Direction < 0) { 
            NeedToSub = true;
            NeedToAdd = false;
        }

        // Find the player and set his health
        PlayerHealth = FindAnyObjectByType<PlayerMovement>();
    }

    // Called every frame
    private void Update() {
        // Check if the enemy is on the wall
        if (Detection.OnWall() || (Detection.OnRightCliff() && Speed > 0) || (Detection.OnLeftCliff() && Speed < 0)) {
            // Flip the enemy
            Flip();
        }

        // Update movement and animations
        UpdateMove();
        UpdateAnimations();

        // Check if the enemy cann't fire
        if (!CanAttack) {
            // Decrease the remaining time until the enemy can fire again
            AttackDelaySeconds -= Time.fixedDeltaTime;

            // If the remaining cooldown time has elapsed
            if (AttackDelaySeconds <= 0) {
                // Reset the CanFire flag to true, allowing the enemy to fire again
                CanAttack = true;

                // Reset the cooldown timer to the initial fire delay value
                AttackDelaySeconds = AttackDelay;
            }
        }
    }

    // Update enemy's position
    private void UpdateMove() {
        // If the enemy is allowed to move
        if (Move) {
            // Reset the idle timer
            IdleTimer = 0;

            // Move the enemy horizontally
            rb.linearVelocity = new Vector2(Speed * Vector2.right.x, rb.linearVelocity.y);
        }

        // If the player is found by the search component and his health is bigger than 0
        if (Search.PlayerFound && PlayerHealth.Health > 0) {
            // Stop the enemy's movement
            Move = false;
            rb.linearVelocity = Vector2.zero;
        }
        // If the enemy is not moving and the player is not found
        else if (!Move && !Search.PlayerFound) {
            // Increment the idle timer
            IdleTimer += Time.fixedDeltaTime;

            // If the idle time exceeds the specified threshold
            if (IdleTimer > IdleTime) {
                // Allow the enemy to move again
                Move = true;
            }
        }
    }

    // Update enemy animations based on movement
    private void UpdateAnimations() {
        animator.SetBool("Move", Move);
        if (Search.PlayerFound && PlayerHealth.Health > 0 && CanAttack) {
            animator.SetTrigger("Attack");
            CanAttack = false;
        }
    }


    // Flip the enemy's direction
    private void Flip() {
        // Stop movement
        Move = false;
        rb.linearVelocity = Vector2.zero;

        // Flip character direction
        Direction = -Direction;
        Speed = -Speed;
        transform.localScale = new Vector2(Direction, transform.localScale.y);
        if (NeedToAdd) {
            transform.position = transform.position + new Vector3(Direction * FlipAmount, 0, 0);
        }
        else if (NeedToSub) {
            transform.position = transform.position - new Vector3(Direction * FlipAmount, 0, 0);
        }

        // Reset idle timer
        IdleTimer += Time.deltaTime;
        if (IdleTimer > IdleTime) {
            Move = true;
        }
    }

    // Perform a ranged attack
    private void RangeAttack() {
        // Instantiate a projectile at the FirePoint position with no rotation
        GameObject CurrentProjectile = Instantiate(projectile, FirePoint.transform.position, Quaternion.identity);

        // Fire the projectile in the direction of the enemy's movement using its Fire method
        // Determine the direction of the projectile based on the sign of the enemy's speed
        // This ensures the projectile moves in the same direction as the enemy
        CurrentProjectile.GetComponent<Projectile>().Fire((int)(Speed / Mathf.Abs(Speed)));

        // Prevent the enemy from firing again until the fire delay has elapsed
        CanAttack = false;
    }


    // Method to handle the enemy taking damage
    public void TakeDamage(float Damage) {
        // Decrease enemy health by the amount of damage received
        Health -= Damage;

        // Check if enemy's health is still above zero
        if (Health > 0f) {
            // If health is still above zero, trigger the "Hit" animation
            animator.SetTrigger("Hit");
        }
        else {
            // If enemy's health reaches zero or below, stop the enemy from moving
            Move = false;

            // Trigger the "Die" animation since enemy's health is zero
            animator.SetTrigger("Die");
        }
    }

    // Method to disable the GameObject
    private void Disable() {
        // Deactivate the GameObject, effectively hiding it from the scene
        this.gameObject.SetActive(false);
    }

}