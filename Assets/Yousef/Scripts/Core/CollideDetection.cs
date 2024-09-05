// Import necessary libraries
using UnityEngine;

// Declare the Collide Detection class
public class CollideDetection : MonoBehaviour {
    [Header("Character:")]
    [Tooltip("Collider2D component for physics simulation")]
    [SerializeField] private BoxCollider2D Collider; // Reference to the Collider2D component for physics simulation

    [Header("Layers:")]
    [Tooltip("The ground layer")]
    [SerializeField] private LayerMask GroundLayer; // Reference to the ground layer
    [Tooltip("The wall layer")]
    [SerializeField] private LayerMask WallLayer; // Reference to the wall layer

    // Check if the player is grounded
    public bool Grounded() {
        // Grounded check using overlap box
        return Physics2D.OverlapBox(Collider.bounds.center, Collider.bounds.size + new Vector3(0, 1, 0), 0, GroundLayer);
    }

    // Check if the player is on a wall
    public bool OnWall() {
        // Wall check using overlap box
        return Physics2D.OverlapBox(Collider.bounds.center, Collider.bounds.size + new Vector3(1, 0, 0), 0, WallLayer);
    }

    // Check if the player is on a right cliff
    public bool OnRightCliff() {
        // Cliff check using overlap box
        return !Physics2D.OverlapBox(Collider.bounds.center + new Vector3(1f, -2f, 0), new Vector3(0.5f, 2.5f, 0), 0, GroundLayer);
    }

    // Check if the player is on a left cliff
    public bool OnLeftCliff() {
        // Cliff check using overlap box
        return !Physics2D.OverlapBox(Collider.bounds.center + new Vector3(-1f, -2f, 0), new Vector3(0.5f, 2.5f, 0), 0, GroundLayer);
    }
}