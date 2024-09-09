using UnityEngine;

public class Destructible : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float DestroyDelay;
    [SerializeField] private Item Item;
    // Method to handle player taking damage
    public void TakeDamage(float Damage) {
        animator.SetTrigger("Destroy");
    }

    public void Destroy() {
        Instantiate(Item, transform.position, Quaternion.identity);
        Destroy(gameObject, DestroyDelay);
    }
}