// Import necessary libraries
using UnityEngine;
using UnityEngine.Events;

// Declare the Dialog Changer class
public class DialogChanger : MonoBehaviour {
    // Dialog elements
    [Header("Dialog:")]
    [Tooltip("Reference to the dialog system")]
    [SerializeField] private Dialog Dialog; // Reference to the dialog system
    [Tooltip("Text Asset to hold the dialog content")]
    [SerializeField] private TextAsset DialogValue; // Text Asset to hold the dialog content

    public bool HasDialogEffect;
    public UnityEvent @DialogEffectEvent;
    public bool HasAfterDialogEffect;
    public UnityEvent @AfterDialogEffectEvent;

    // Called when another Collider2D enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Dialog.DialogValue = DialogValue;
            Dialog.EnableSystem();
            if (HasDialogEffect) {
                @DialogEffectEvent.Invoke();
            }
            if (HasAfterDialogEffect) {
                Dialog.HasAfterDialogEffect = true;
                Dialog.@AfterDialogEffectEvent = @AfterDialogEffectEvent;
            }
            else {
                Dialog.HasAfterDialogEffect = false;
                Dialog.@AfterDialogEffectEvent = null;
            }
        }
    }
}