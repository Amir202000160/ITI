using UnityEngine;
using UnityEngine.EventSystems;

public class hint : MonoBehaviour
{
    public GameObject BG;
    public EventSystem eve;
    [SerializeField] GameObject closedB;
      public GameObject button;
    public void Start()
    {
        BG.SetActive(false);
    }
    public void Open()
    {
        BG.SetActive(true);
        eve.SetSelectedGameObject(closedB);
    }
    public void close()
    {
        BG.SetActive(false);
        eve.SetSelectedGameObject(button);
    }

}
