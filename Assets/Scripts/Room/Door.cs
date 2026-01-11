using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private AudioClip cleared;
    [Header("Enemies in current room")]
    [SerializeField] private GameObject[] enemiesDead;
    private int deadEnemies;
    private SpriteRenderer sprite;
    private Vector2 initialPosition;
    private Transform doorTransform;

    private void Start()
    {
        sprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        doorTransform = transform.GetChild(1).transform;
        initialPosition = doorTransform.position;
    }

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
        yield return new WaitForSeconds(3f);
        AudioManager.instance.PlaySound(cleared);
        for (int i = 0; i < 10; i++)
        {
            if(i%2==0)
                cam.transform.Translate(new Vector3(-1,0,0));
            else
                cam.transform.Translate(new Vector3(1,0,0));
            doorTransform.Translate(new Vector3(0,-1,0));
            yield return new WaitForSeconds(0.1f);
        }
        sprite.enabled = false;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<EdgeCollider2D>().enabled = true;
    }

    public void EnableDoor()
    {
        doorTransform.position = initialPosition;
        sprite.enabled = true;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(DoorAppear());

    }

    private IEnumerator DoorAppear()
    {
        float alpha = 0;
        Color temp = sprite.color;
        temp.a = alpha;
        for (int i=0; i<50; i++)
        {
            sprite.color = temp;
            alpha += 1f / 50;
            temp.a=alpha;
            yield return new WaitForSeconds(1f/50);
        }
        sprite.color = temp;
    }
}
