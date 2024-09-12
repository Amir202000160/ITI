using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMain : MonoBehaviour
{
    public void LoadStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    
    public void LoadHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void loadscene(int x){
     SceneManager.LoadSceneAsync(x);
    }
}
