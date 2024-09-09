using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage = 2.0f;

    [SerializeField] private float activationDelay = 2.0f;
    [SerializeField] private float activationTime = 2.0f;

    private Animator anim;
    private SpriteRenderer Sprite;

    private bool triggerd;
    private bool active;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerd)
        {
            StartCoroutine(ActivateFireTrap());
        }
        if (active)
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        //sprite red
        triggerd = true;
        Sprite.color = Color.red;

        //wait for delay, active trap, animation, sprite normal
        yield return new WaitForSeconds(activationDelay);
        Sprite.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait then reset all
        yield return new WaitForSeconds(activationTime);
        active = false;
        triggerd = false;
        anim.SetBool("activated", false);
    }
}
