// Import necessary libraries
using UnityEngine;
using UnityEngine.InputSystem;

// Declare the Player Attack class
public class PlayerAttack : MonoBehaviour {
    // Player Input
    private PlayerInputActions InputAction;

    // Player Attack and related components
    [Header("Player Attack:")]
    [Tooltip("Animator component for player animations")]
    [SerializeField] private Animator animator; // Reference to the animator component for player animations
    [Tooltip("PlayerMovement component for player movement control")]
    [SerializeField] private PlayerMovement Movement; // Reference to the PlayerMovement component for player movement control
    [Tooltip("CollideDetection script for Detections")]
    [SerializeField] private CollideDetection Detection; // Reference to CollideDetection script for Detections
    private int AttackCount; // Number of consecutive attacks performed by the player
    private bool Combo = true; // Flag to indicat whether the player can perform a combo attack
    private float NormalSpeed;

    // Called when the script is first initialized
    private void Start() {
        // Check for player input
        InputAction = new PlayerInputActions();
        InputAction.Player.Enable();
        InputAction.Player.Attack.started += context => Attack(context);
        NormalSpeed = Movement.Speed;
    }

    // This method handles the player's attack action
    private void Attack(InputAction.CallbackContext context) {
        // Checking if the attack action is started, combo is allowed, attack count is less than 3, and the player is grounded
        if (context.started && Combo && AttackCount < 3 && Detection.Grounded()) {
            // Disable the player movement
            Movement.Speed = 0f;
            
            // increace the attack counter by 1
            AttackCount++;

            // Set Combo to false
            Combo = false;

            // Triggering different attack animations based on attack count
            switch (AttackCount) {
                case 1:
                    animator.SetTrigger("ATK_1");
                    break;
                case 2:
                    animator.SetTrigger("ATK_2");
                    break;
                case 3:
                    animator.SetTrigger("ATK_3");
                    break;
                default:
                    break;
            }
        }
    }

    // Method to enable combo
    private void CanCombo() {
        Combo = true;
    }

    // Method to disable combo
    private void CanNotCombo() {
        Combo = false;
    }

    // Method to reset combo status and attack count
    private void ResetCombo() {
        // Enable the player movement
        Movement.Speed = NormalSpeed;

        // Set combo to true
        Combo = true;

        // Set Attack Count to 0
        AttackCount = 0;
    }
}