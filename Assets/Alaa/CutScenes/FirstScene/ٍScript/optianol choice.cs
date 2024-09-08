using UnityEngine;
using UnityEngine.SceneManagement;
public class optianolchoice : MonoBehaviour
{

    public GameObject Alert;
    public GameObject YesButtonUI;
    public GameObject NoButtonUI;
    public GameObject Alert2;

    public void Start()
    {
        Alert.SetActive(true);
        Alert2.SetActive(false);
        NoButtonUI.SetActive(false);
        YesButtonUI.SetActive(false);
    }
    public void YesButton()
    {
        Alert.SetActive(false);
        NoButtonUI.SetActive(false);
        YesButtonUI.SetActive(true);
        Alert2.SetActive(false);
    }


    public void NoButton()
    {
        Alert.SetActive(false);
        NoButtonUI.SetActive(true);
        YesButtonUI.SetActive(false);
        Alert2.SetActive(false);
    }

    public void YesYes()
    {
        SceneManager.LoadScene("BegininfProfile");
    }

    public void NoNo()
    {
        Alert2.SetActive(true);
        NoButtonUI.SetActive(false);
    }
}
