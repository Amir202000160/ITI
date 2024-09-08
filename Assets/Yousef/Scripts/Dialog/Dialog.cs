// Import necessary libraries
using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Declare the Dialog class
public class Dialog : MonoBehaviour {
    // Dialog Prefabs elements
    [Header("Dialog Prefabs:")]
    [Tooltip("Prefab for displaying dialog text")]
    [SerializeField] private GameObject DialogText;   // Prefab for displaying dialog text
    [Tooltip("Text Asset to hold the dialog content")]
    [SerializeField] private TextAsset DialogValue; // Text Asset to hold the dialog content

    // Dialog UI elements
    [Header("Dialog UI:")]
    [Tooltip("Parent object for the entire dialog system")]
    [SerializeField] private GameObject DialogSystem; // Parent object for the entire dialog system
    [Tooltip("Container for dialog text")]
    [SerializeField] private GameObject DialogHolder; // Container for dialog text
    [Tooltip("Text component for displaying speaker's name")]
    [SerializeField] private TMP_Text SpeakerName; // Text component for displaying speaker's name

    // Dialog Story element
    [Header("Dialog Story:")]
    [Tooltip("Ink story instance to manage the dialog content")]
    [SerializeField] private Story Story; // Ink story instance to manage the dialog content

    // Dialog contents elements
    [Header("Dialog Contents:")]
    private DialogContent Content; // Script attached to DialogText prefab to handle dialog text display
    private const string SPEAKER_TAG = "Speaker"; // Tag used in the dialog script to identify the speaker

    // Activates the dialog system and initializes the story
    public void Start() {
        DialogSystem.SetActive(true);
        SetStory();
        RefreshView();
    }

    // Creates a new Ink story based on the provided dialog content
    public void SetStory() {
        if (DialogValue != null) {
            Story = new Story(DialogValue.text);
        }
    }

    // Refreshes the view to display the next part of the dialog
    public void RefreshView() {
        // Continue displaying dialog text until there's no more content
        while (Story.canContinue) {
            CreateDialog(Story.Continue());
            HandleTags(Story.currentTags);
        }

        // If there are no choices deactivate the dialog system
        if (Story.currentChoices.Count <= 0) {
            DialogSystem.SetActive(false);
        }
    }

    // Handles special tags in the dialog content, such as the speaker's name
    private void HandleTags(List<string> Tags) {
        foreach (string tag in Tags) {
            string[] SplitTag = tag.Split(':');
            string TagKey = SplitTag[0].Trim();
            string TagValue = SplitTag[1].Trim();
            if (TagKey == SPEAKER_TAG) {
                // Sets the speaker's name in the UI
                SpeakerName.text = TagValue;
            }
        }
    }

    // Creates a new dialog text object with the provided text
    private void CreateDialog(string Dialog) {
        // Destroy the previous dialog text object if it exists
        if (Content != null) {
            Destroy(Content.gameObject);
        }
        // Instantiate a new dialog text object and set up its content
        Content = Instantiate(DialogText, DialogHolder.transform).GetComponent<DialogContent>();
        Content.Setup(Dialog);
    }

    // Called when the player makes a choice, advances the story, and refreshes the view
    public void ChooseChoice(int choice) {
        Story.ChooseChoiceIndex(choice);
        RefreshView();
    }
}