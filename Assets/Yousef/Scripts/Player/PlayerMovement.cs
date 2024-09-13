// Import necessary libraries
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Declare the Player Movement class
public class PlayerMovement : MonoBehaviour
{
    // Player Input
    private PlayerInputActions InputAction;

    // Player Movement and related components
    [Header("Player Movement:")]
    [Tooltip("Movement speed of the player")]
    public float Speed; // Movement speed of the player
    [Tooltip("Animator component for player animations")]
    [SerializeField] private Animator animator; // Reference to the animator component for player animations
    [Tooltip("Rigidbody2D component for physics simulation")]
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component for physics simulation
    [Tooltip("CollideDetection script for Detections")]
    [SerializeField] private CollideDetection Detection; // Reference to Collide Detection script for Detections
    private Vector2 Movement; // Current movement direction of the player

    // Player Jump and related components
    [Header("Player Jump:")]
    [Tooltip("Jump force of the player")]
    [SerializeField] private float JumpForce; // Jump force of the player
    [Tooltip("Time for the player coyote")]
    [SerializeField] private float CoyoteTime; // Time for the player cyote
    private float CoyoteCounter; // Counter to know how long the player stayed in Coyote mode

    // Player Health and related components
    [Header("Player Health:")]
    [Tooltip("Current health of the player")]
    public float Health; // Current health of the player
    [SerializeField] private float MaxHealth;

    // Dialog UI elements
    [Header("Dialog UI:")]
    [Tooltip("Parent object for the entire dialog system")]
    [SerializeField] private GameObject DialogSystem; // Parent object for the entire dialog system
    // Called when the script is first initialized

    private void Start()
    {
        // Check for player input
        InputAction = new PlayerInputActions();
        InputAction.Player.Enable();
        InputAction.Player.Jump.started += context => Jump(context);
        InputAction.Player.Jump.canceled += context => EndJump(context);
    }

    // Called every frame
    private void Update()
    {
        if (DialogSystem.activeInHierarchy)
        {
            InputAction.Player.Disable();
        }
        else
        {
            InputAction.Player.Enable();
        }

        // Get player movement input
        Movement = InputAction.Player.Move.ReadValue<Vector2>();

        // Update movement and animations
        UpdateMove();
        UpdateAnimations();

        // If the player is grounded
        if (Detection.Grounded())
        {
            // Reset the coyote counter to the coyote time
            CoyoteCounter = CoyoteTime;
        }
        else
        {
            // Decrement the coyote counter by the time passed
            CoyoteCounter -= Time.deltaTime;
        }

        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
    }

    // Update the player's position based on movement input
    private void UpdateMove()
    {
        // Update horizontal movement velocity based on input and speed
        rb.linearVelocity = new Vector2(Movement.x * Speed, rb.linearVelocity.y);

        // Flip character sprite based on movement direction
        if (Movement.x > 0)
        {
            transform.localScale = new Vector2(1, 1); // Facing right
        }
        else if (Movement.x < 0)
        {
            transform.localScale = new Vector2(-1, 1); // Facing left
        }
    }


    // Update player animations based on movement
    private void UpdateAnimations()
    {
        if (Movement != Vector2.zero && Detection.Grounded())
        {
            animator.SetBool("Move", true);
            animator.SetFloat("JumpFall", 0);
        }
        else if (Movement == Vector2.zero && Detection.Grounded())
        {
            animator.SetBool("Move", false);
            animator.SetFloat("JumpFall", 0);
        }
        else if (!Detection.OnWall() && !Detection.Grounded())
        {
            animator.SetFloat("JumpFall", rb.linearVelocity.y);
        }
        else if (Detection.OnWall() && !Detection.Grounded())
        {
            animator.SetFloat("JumpFall", -1);
        }
    }

    // Handle player jumping input
    private void Jump(InputAction.CallbackContext context)
    {
        // Checking if the jump input is started and the player is grounded or in coyote mode or just jumped
        if (context.started && (CoyoteCounter > 0f))
        {
            // Adding an upward force to the rigidbody for jumping
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }

    // Handle player finished jumping input
    private void EndJump(InputAction.CallbackContext context)
    {
        // Checking if the jump input is canceled
        if (context.canceled)
        {
            // Reset the coyote counter
            CoyoteCounter = 0f;
        }
    }

    // Method to handle player taking damage
    public void TakeDamage(float Damage)
    {
        // Decrease player health by the amount of damage received
        Health -= Damage;

        // Check if player's health is still above zero
        if (Health > 0f)
        {
            // If health is still above zero, trigger the "Hit" animation
            animator.SetTrigger("Hit");
        }
        else
        {
            // If player's health reaches zero or below, set movement to zero to stop player from moving
            Movement = Vector2.zero;

            // Set player speed to zero to prevent further movement
            Speed = 0f;

            // Trigger the "Die" animation since player's health is zero
            animator.SetTrigger("Die");
        }
    }

    // Method to disable the GameObject
    private void Disable()
    {
        // Deactivate the GameObject, effectively hiding it from the scene
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}