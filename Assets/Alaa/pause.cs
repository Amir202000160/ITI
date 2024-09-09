using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject menu;
    private PlayerInputActions controls; // Reference to the generated Input Actions class


    private void Awake()
    {
        controls = new PlayerInputActions(); // Initialize the Input Actions
        menu.SetActive(false);
    }

    private void OnEnable()
    {
        controls.Player.Pause.performed += OnPause; // Subscribe to the Pause action
        controls.Enable(); // Enable the Input Actions
    }

    private void OnDisable()
    {
        controls.Player.Pause.performed -= OnPause; // Unsubscribe from the Pause action
        controls.Disable(); // Disable the Input Actions
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }
}
