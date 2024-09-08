using UnityEngine;

public class Dev : MonoBehaviour
{
    public GameObject BG;
    
    public void  Start()
    {
          BG.SetActive(false);
    }
    public void Open(){
        BG.SetActive(true);
    }
     public void close(){
        BG.SetActive(false);
    }
}
