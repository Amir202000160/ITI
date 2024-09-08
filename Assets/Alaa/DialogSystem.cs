using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{

   // public GameObject dustPrefab;
    public AudioClip typingSound;
    public Canvas canvas;
    public Font font;
    public List<DialogData> dialogs;
    private int currentDialogIndex = 0;

    private GameObject currentDialog;

    Text messageText;
    private AudioSource audioSource;

    private AudioSource dialogAudioSource;

     public UnityEvent onDialogStarted;
    public UnityEvent onDialogEnded;

    // public List<AudioClip> dialogAudioClips;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = typingSound;
    }

    public void CreateDialog(DialogData dialogData)
    {
        if (currentDialog != null)
        {
            Destroy(currentDialog);
        }

        // Create a new GameObject for the dialog
        GameObject dialog = new GameObject("Dialog");
        dialog.transform.SetParent(canvas.transform, false);
        currentDialog = dialog;

        // Add an Image component for the background
        Image bgImage = dialog.AddComponent<Image>();
        bgImage.sprite = dialogData.dialogBackgroundSprite;
        //bgImage.material = backgroundMaterial; 
        // Set the size and position of the dialog
        RectTransform dialogRect = dialog.GetComponent<RectTransform>();
        dialogRect.sizeDelta = new Vector2(600, 200);
        dialogRect.anchorMin = new Vector2(0.5f, 0);
        dialogRect.anchorMax = new Vector2(0.5f, 0);
        dialogRect.pivot = new Vector2(0.5f, 0);
        dialogRect.anchoredPosition = new Vector2(0, 0);

        GameObject characterImageObj = new GameObject("CharacterImage");
        characterImageObj.transform.SetParent(dialog.transform, false);

        // Add an Image component for the character image
        Image characterImage = characterImageObj.AddComponent<Image>();
        characterImage.sprite = dialogData.characterSprite;
        //  characterImage.material = backgroundMaterial; 
        // Set the size and position of the character image
        RectTransform characterImageRect = characterImageObj.GetComponent<RectTransform>();
        characterImageRect.sizeDelta = new Vector2(100, 100);
        characterImageRect.anchorMin = new Vector2(0f, 1f);
        characterImageRect.anchorMax = new Vector2(0f, 1f);
        characterImageRect.pivot = new Vector2(0f, 1f);
        characterImageRect.anchoredPosition = new Vector2(20, 70);

        // Create a new GameObject for the title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(dialog.transform, false);

        dialogAudioSource = dialog.AddComponent<AudioSource>();
        dialogAudioSource.clip = dialogData.soundEffect;

        // Add a Text component for the title
        Text titleText = titleObj.AddComponent<Text>();
        titleText.text = dialogData.title;
        titleText.font = font;
        titleText.fontSize = 26;
        titleText.color = Color.white;
        titleText.alignment = TextAnchor.MiddleCenter;

        // Set the size and position of the title
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.sizeDelta = new Vector2(600, 30);
        titleRect.anchorMin = new Vector2(0f, 1);
        titleRect.anchorMax = new Vector2(0f, 1);
        titleRect.pivot = new Vector2(0f, 1);
        titleRect.anchoredPosition = new Vector2(110, -20);

        // Create a new GameObject for the message
        GameObject messageObj = new GameObject("Message");
        messageObj.transform.SetParent(dialog.transform, false);

        // Add a Text component for the message
        messageText = messageObj.AddComponent<Text>();
        messageText.text = dialogData.message;
        messageText.font = font;
        messageText.fontSize = 28;
        messageText.color = Color.white;
        messageText.alignment = TextAnchor.MiddleCenter;

        // Set the size and position of the message
        RectTransform messageRect = messageObj.GetComponent<RectTransform>();
        messageRect.sizeDelta = new Vector2(560, 100);
        messageRect.anchorMin = new Vector2(0.5f, 0.5f);
        messageRect.anchorMax = new Vector2(0.5f, 0.5f);
        messageRect.pivot = new Vector2(0.5f, 0.5f);
        messageRect.anchoredPosition = new Vector2(0, 0);




        StartCoroutine(DisplayTextLetterByLetter(dialogData.message));
        soundEffectPlay();

        // Create a new GameObject for the "Skip" button
        GameObject skipButtonObj = new GameObject("SkipButton");
        skipButtonObj.transform.SetParent(dialog.transform, false);

        // Add an Image component for the button background
        Image skipButtonImage = skipButtonObj.AddComponent<Image>();
        skipButtonImage.color = Color.black;

        // Add a Button component
        Button skipButton = skipButtonObj.AddComponent<Button>();

        // Add a listener to the button to destroy the dialog when clicked
        skipButton.onClick.AddListener(() => Destroy(dialog));

        // Create a new GameObject for the button text
        GameObject skipButtonTextObj = new GameObject("SkipButtonText");
        skipButtonTextObj.transform.SetParent(skipButtonObj.transform, false);

        // Add a Text component for the button text
        Text skipButtonText = skipButtonTextObj.AddComponent<Text>();
        skipButtonText.text = "SKIP>>";
        skipButtonText.font = font;
        skipButtonText.fontSize = 18;
        skipButtonText.color = Color.white;
        skipButtonText.alignment = TextAnchor.MiddleCenter;

        // Set the size and position of the button text
        RectTransform skipButtonTextRect = skipButtonTextObj.GetComponent<RectTransform>();
        skipButtonTextRect.sizeDelta = new Vector2(80, 30);
        skipButtonTextRect.anchorMin = new Vector2(0.5f, 0.5f);
        skipButtonTextRect.anchorMax = new Vector2(0.5f, 0.5f);
        skipButtonTextRect.pivot = new Vector2(0.5f, 0.5f);
        skipButtonTextRect.anchoredPosition = Vector2.zero;

        // Set the size and position of the button
        RectTransform skipButtonRect = skipButtonObj.GetComponent<RectTransform>();
        skipButtonRect.sizeDelta = new Vector2(100, 50);
        skipButtonRect.anchorMin = new Vector2(0.5f, 0);
        skipButtonRect.anchorMax = new Vector2(0.5f, 0);
        skipButtonRect.pivot = new Vector2(0.5f, 0);
        skipButtonRect.anchoredPosition = new Vector2(-290, 10);

        // Create a new GameObject for the "Continue" button
        GameObject continueButtonObj = new GameObject("ContinueButton");
        continueButtonObj.transform.SetParent(dialog.transform, false);

        // Add an Image component for the button background
        Image continueButtonImage = continueButtonObj.AddComponent<Image>();
        continueButtonImage.color = Color.black;

        // Add a Button component
        Button continueButton = continueButtonObj.AddComponent<Button>();

        // Add a listener to the button to create a new dialog or end the sequence
        if (currentDialogIndex < dialogs.Count - 1)
        {
            continueButton.onClick.AddListener(NextDialog);
        }
        else
        {
            continueButton.onClick.AddListener(() => Destroy(dialog));
        }

        // Create a new GameObject for the button text
        GameObject continueButtonTextObj = new GameObject("ContinueButtonText");
        continueButtonTextObj.transform.SetParent(continueButtonObj.transform, false);

        // Add a Text component for the button text
        Text continueButtonText = continueButtonTextObj.AddComponent<Text>();
        continueButtonText.text = "CONTINUE>>";
        continueButtonText.font = font;
        continueButtonText.fontSize = 18;
        continueButtonText.color = Color.white;
        continueButtonText.alignment = TextAnchor.MiddleCenter;

        // Set the size and position of the button text
        RectTransform continueButtonTextRect = continueButtonTextObj.GetComponent<RectTransform>();
        continueButtonTextRect.sizeDelta = new Vector2(120, 30);
        continueButtonTextRect.anchorMin = new Vector2(0.5f, 0.5f);
        continueButtonTextRect.anchorMax = new Vector2(0.5f, 0.5f);
        continueButtonTextRect.pivot = new Vector2(0.5f, 0.5f);
        continueButtonTextRect.anchoredPosition = Vector2.zero;

        // Set the size and position of the button
        RectTransform continueButtonRect = continueButtonObj.GetComponent<RectTransform>();
        continueButtonRect.sizeDelta = new Vector2(100, 50);
        continueButtonRect.anchorMin = new Vector2(0.5f, 0);
        continueButtonRect.anchorMax = new Vector2(0.5f, 0);
        continueButtonRect.pivot = new Vector2(-2f, 0);
        continueButtonRect.anchoredPosition = new Vector2(50, 10);
    }

    void NextDialog()
    {
        currentDialogIndex++;
        if (currentDialogIndex < dialogs.Count)
        {
            CreateDialog(dialogs[currentDialogIndex]);
        }

    }

    IEnumerator DisplayTextLetterByLetter(string text)
    {
        messageText.text = "";
        foreach (char letter in text.ToCharArray())
        {

            messageText.text += letter;
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void soundEffectPlay()
    {
        dialogAudioSource.Play();
    }

     public void TriggerDialogStart()
    {
        if (dialogs != null && dialogs.Count > 0)
        {
            onDialogStarted?.Invoke();
            CreateDialog(dialogs[currentDialogIndex]);
        }
    }

    public void TriggerDialogEnd()
    {
        onDialogEnded?.Invoke();
    }
}