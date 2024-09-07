// Import necessary libraries
using TMPro;
using UnityEngine;

// Declare the Dialog Content class
public class DialogContent : MonoBehaviour {

    // Text
    [Header("Text:")]
    [Tooltip("TextMeshPro text component for displaying dialog content")]
    [SerializeField] private TMP_Text Text; // Reference to the TextMeshPro text component for displaying dialog content

    // Set up the dialog text with the provided content
    public void Setup(string Dialog) {
        Text.text = Dialog; // Assign the provided text content to the TextMeshPro component
    }
}