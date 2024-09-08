using UnityEngine;

public class hint : MonoBehaviour
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
