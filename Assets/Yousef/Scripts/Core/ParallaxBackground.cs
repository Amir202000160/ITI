// Import necessary libraries
using UnityEngine;

// Declare the Parallax Background class
public class ParallaxBackground : MonoBehaviour {
    [Header("Camera:")]
    [Tooltip("The main camera")]
    [SerializeField] private Camera Cam; // Reference to the main camera

    [Header("Player:")]
    [Tooltip("The Player")]
    [SerializeField] private Transform Player;// Reference to the player's transform

    // Initial position variables
    private Vector2 StartPosition;
    private float StartYPosition;
    private float StartZPosition;

    // Calculate the distance the camera has moved
    private Vector2 CamMoveDistance => (Vector2)Cam.transform.position - StartPosition;

    // Calculate the distance between the background and the player
    private float BackgroundDistance => transform.position.z - Player.transform.position.z;

    // Calculate the clipping plane distance
    private float ClippingPlane => Cam.transform.position.z + (BackgroundDistance > 0 ? Cam.farClipPlane : Cam.nearClipPlane);

    // Calculate the parallax factor based on background distance and clipping plane
    private float ParallaxFactor => Mathf.Abs(BackgroundDistance) / ClippingPlane;

    // Called when the script is first initialized
    private void Start() {
        // Store the initial position values
        StartPosition = new Vector2(transform.position.x, transform.position.y);
        StartYPosition = transform.position.y;
        StartZPosition = transform.position.z;
    }

    // Called every frame
    private void Update() {
        // Calculate the new position based on the parallax factor
        Vector2 NewPosition = StartPosition + CamMoveDistance * ParallaxFactor;

        // Update the background's position
        transform.position = new Vector3(NewPosition.x, StartYPosition, StartZPosition);
    }
}