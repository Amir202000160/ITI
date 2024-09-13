using UnityEngine;

public class EnemyApper : MonoBehaviour
{
    [SerializeField] private GameObject Enemies;
    private void OnTriggerEnter2D(Collider2D collision) {
        Enemies.SetActive(true);
        Destroy(gameObject);
    }
}