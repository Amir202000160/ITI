using UnityEngine;
using UnityEngine.EventSystems;

public class Dev : MonoBehaviour
{
    public GameObject BG;
    public EventSystem eve;

    public GameObject button;
    [SerializeField] GameObject closedB;
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
