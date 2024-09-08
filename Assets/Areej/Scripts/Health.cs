using UnityEditor.Search;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    public float currentHealth {  get; private set; }

    private Animator anim;

    private bool dead;
    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamade(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        Debug.Log("Player took damage, current health: " + currentHealth);

        if (currentHealth > 0)
        {
            //hurt Animation
        }
        else
        {
            if (!dead)
            {
                //die Animation
                //disable Player Movement
                dead = true;
            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamade(1);
        } 
    }

}
