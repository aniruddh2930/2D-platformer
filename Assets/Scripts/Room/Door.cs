    using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private AudioClip cleared;
    [Header("Enemies in current room")]
    [SerializeField] private GameObject[] enemiesDead;
    private int deadEnemies;

    private void Update()
    {
        deadEnemies = 0;
        foreach (GameObject enemy in enemiesDead)
        {
            if (enemy.activeInHierarchy == true)
                return;
            deadEnemies++;
        }

        if (deadEnemies == enemiesDead.Length)
        {
            StartCoroutine(DisableDoor());
            this.enabled = false;
        }
    }
    private System.Collections.IEnumerator DisableDoor()
    {
        AudioManager.instance.PlaySound(cleared);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 10; i++)
        {
            if(i%2==0)
                cam.transform.Translate(new Vector3(-1,0,0));
            else
                cam.transform.Translate(new Vector3(1,0,0));
            transform.GetChild(1).transform.Translate(new Vector3(0,-1,0));
            yield return new WaitForSeconds(0.1f);
        }
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        foreach( Transform child in transform)
        {
            child.GetComponent<BoxCollider2D>().enabled = !child.GetComponent<BoxCollider2D>().enabled;
        }
    }
    

}
