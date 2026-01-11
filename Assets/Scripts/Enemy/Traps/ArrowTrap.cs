using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip arrowSound;
    [Header("lifetime duration")]
    [SerializeField] private Component[] behaviours;
    [SerializeField] private float lifeTime;
    [SerializeField] private Health playerHealth;

    private float coolDown;

    private void Attack()
    {
        coolDown = attackCooldown;
        int arrow = findArrow();
        arrows[arrow].transform.position = firePoint.position;
        arrows[arrow].GetComponent<EnemyProjectile>().Activate();
        AudioManager.instance.PlaySound(arrowSound);

    }

    private int findArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        if (!playerHealth.dead)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
                Deactivate();
        }

        coolDown -= Time.deltaTime;
        if (coolDown <= 0)
        {
              Attack();
        }
    }

    private void Deactivate()
    {
        this.enabled= false;
        foreach (Behaviour component in behaviours)
        {
            component.enabled = false;
        }

    }




}
