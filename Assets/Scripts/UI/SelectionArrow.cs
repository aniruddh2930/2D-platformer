using System;
using UnityEngine.UI;
using UnityEngine;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentIndex = 0;
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip interactSound;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //move
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Change(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Change(-1);
        }

        //interact
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            interact();
        }
    }

    private void interact()
    {
        AudioManager.instance.PlaySound(interactSound);
        options[currentIndex].GetComponent<Button>().onClick.Invoke();
    }

    private void Change(int change)
    {
        currentIndex += change;
        currentIndex= (currentIndex%options.Length+options.Length)%options.Length;

        if (change!= 0)
            AudioManager.instance.PlaySound(move);

        rect.position= new Vector2(rect.position.x, options[currentIndex].position.y);
    }
}
