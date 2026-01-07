using UnityEngine;

public class AttackCanvas : MonoBehaviour
{
    [SerializeField] private float playerAttackCooldown;
    private Animator anim;
    private float cooldownTimer = 0.0f;
    // where the projectile will be instantiated
    [SerializeField] private RectTransform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballHit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("grounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K) && cooldownTimer <= 0.0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        AudioManager.instance.PlaySound(fireballHit);
        anim.SetTrigger("attack");
        cooldownTimer = playerAttackCooldown;
        int fireball = GetFireball();
        fireballs[fireball].GetComponent<RectTransform>().position = firePoint.position;
        fireballs[fireball].GetComponent<ProjectileCanvas>().direction = 1;
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


