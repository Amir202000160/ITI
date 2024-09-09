using System.Collections;
using UnityEngine;

public class DisappearingGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Dissolve());
            Destroy(gameObject);
        }
    }
    private IEnumerator Dissolve()
    {

        yield return new WaitForSeconds(1);
    }
}
