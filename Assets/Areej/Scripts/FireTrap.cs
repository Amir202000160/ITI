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

    private bool Load;
    private PlayerMovement Player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerd && collision.CompareTag("Player"))
        {
            Load = true;
            StartCoroutine(ActivateFireTrap());
        }
        Player = collision.GetComponent<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Player = null;
    }

    private void Update() {
        if (active && Player != null && Load) {
            Player.TakeDamage(damage);
            Load = false;
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
