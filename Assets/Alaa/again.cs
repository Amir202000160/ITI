using UnityEngine;
using UnityEngine.SceneManagement;

public class Again : MonoBehaviour
{
    public void Replay()
    {


        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
