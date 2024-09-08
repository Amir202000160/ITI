using Unity.VisualScripting;
using UnityEngine;

public class music : MonoBehaviour
{
    public GameObject Music;
    public AudioSource MusicG;
    public GameObject Mute;
    public GameObject BG;
    public static int muteOrNot = 1;


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
    }



    public void MuteM()
    {
        Music.SetActive(true);
        Mute.SetActive(false);
        MusicG.mute = true;
        muteOrNot = 0;
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
    }

    public void close()
    {
        BG.SetActive(false);
    }
}
