using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int StartingHealth;
    public float currentHealth { get; private set; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takeDamage(1);
        }
    }
}



