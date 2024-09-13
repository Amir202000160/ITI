using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public void Load() {
        SceneManager.LoadSceneAsync(0);
    }
}
