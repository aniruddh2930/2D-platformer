using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip arrowSound;

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
        coolDown -= Time.deltaTime;
        if (coolDown <= 0)
        {
              Attack();
        }
    }
}
