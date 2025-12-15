using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float playerAttackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 0.0f;
    // where the projectile will be instantiated
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K) && cooldownTimer <= 0.0f && playerMovement.canAttack()) 
        {
            Attack();
        }
    }

    private void Attack() {

        anim.SetTrigger("attack");
        cooldownTimer = playerAttackCooldown;
        int fireball = GetFireball();
        fireballs[fireball].transform.position = firePoint.position;
        fireballs[fireball].GetComponent<Projectile>().direction = transform.localScale.x;
        fireballs[fireball].gameObject.SetActive(true);
    }

    private int GetFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
