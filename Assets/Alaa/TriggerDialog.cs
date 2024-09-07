using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
 public DialogSystem dialogSystem;

    void Update()
    {
        // Check for the "A" key press
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Trigger the dialog start
            if (dialogSystem != null)
            {
                dialogSystem.TriggerDialogStart();
            }
        }
    }
    }
