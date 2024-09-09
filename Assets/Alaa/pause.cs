using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {
      
        menu.SetActive(false);
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
