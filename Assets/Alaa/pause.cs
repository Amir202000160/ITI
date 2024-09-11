using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menu;
    private bool isPaused = false; 

    public GameObject dialog;

    public void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        
        // Check for Esc key press
        if (Input.GetKeyDown(KeyCode.Escape) && !dialog.activeInHierarchy)
        {
            if (isPaused)
            {
                ResumeGame(); // If currently paused, resume the game
            }
            else
            {
                PauseGame(); // If not paused, pause the game
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        isPaused = true; // Update pause state
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        isPaused = false; // Update pause state
    }
}
