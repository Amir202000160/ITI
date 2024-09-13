// Import necessary libraries
using UnityEngine;

// Declare the Enemy Search class
public class EnemySearch : MonoBehaviour {
    [Tooltip("Flag to indicate whether the player is found")]
    public bool PlayerFound; // Boolean flag to indicate whether the player is found

    // Called when another collider enters the trigger attached to this game object
    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collider has a tag "Player"
        if (collision.tag == ("Player")) {
            // If player is found, set PlayerFound flag to true
            PlayerFound = true;
        }
    }

    // Called when another collider exits the trigger attached to this game object
    private void OnTriggerExit2D(Collider2D collision) {
        // Check if the collider has a tag "Player"
        if (collision.tag == ("Player")) {
            // If player exits, set PlayerFound flag to false
            PlayerFound = false;
        }
    }
}