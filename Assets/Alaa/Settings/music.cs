using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class music : MonoBehaviour
{
    public GameObject Music;
    public AudioSource MusicG;
    public GameObject Mute;
    public GameObject BG;
    public static int muteOrNot = 1;

    public EventSystem eve;
    [SerializeField] GameObject closedB;
    public GameObject button;
    public GameObject button2;
    public GameObject button3;



    public void Start()
    {
        BG.SetActive(false);
    }
    public void Play()
    {
        Music.SetActive(false);
        Mute.SetActive(true);
        MusicG.Play();
        MusicG.mute = false;
        muteOrNot = 1;
        eve.SetSelectedGameObject(Mute);
    }


    public void MuteM()
    {
        Music.SetActive(true);
        Mute.SetActive(false);
        MusicG.mute = true;
        muteOrNot = 0;
        eve.SetSelectedGameObject(Music);
    }

    public void MusicButton()
    {
        if (muteOrNot == 1)
        {
            BG.SetActive(true);
            Music.SetActive(false);
            Mute.SetActive(true);
        }
        if (muteOrNot == 0)
        {
            BG.SetActive(true);
            Mute.SetActive(false);
            Music.SetActive(true);
        }

        eve.SetSelectedGameObject(Mute);
        button2.GetComponent<Button>().enabled = false;
        button3.GetComponent<Button>().enabled = false;
        button.GetComponent<Button>().enabled = false;
    }


    public void close()
    {
        BG.SetActive(false);
        eve.SetSelectedGameObject(button);
        button2.GetComponent<Button>().enabled = true;
        button3.GetComponent<Button>().enabled = true;
        button.GetComponent<Button>().enabled = true;
    }
}
