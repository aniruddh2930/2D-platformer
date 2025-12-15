using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int StartingHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = StartingHealth;
    }

    private void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth=0;
        }
    }
}


