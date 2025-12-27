using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; } = 3;
    private Animator anim;
    private bool dead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            currentHealth=0;
            anim.SetTrigger("Die");
            GetComponent<PlayerMovement>().enabled = false;
            dead = true;
        }
        anim.SetTrigger("Hurt");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}



