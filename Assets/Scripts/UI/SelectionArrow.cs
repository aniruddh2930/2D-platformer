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
    [Header ("Only for Pause Menu")]
    [SerializeField] private GameObject soundBar;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        currentIndex = 0;
        rect.position = new Vector2(rect.position.x, options[currentIndex].position.y);
    }


    private void Update()
    {
        //move
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //show sound bar only on volume and music options
            //only for pause menu
            Change(1);
            CheckIndex();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //show sound bar only on volume and music options
            //only for pause menu
            Change(-1);
            CheckIndex();
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
        AudioManager.instance.PlaySound(move);
        rect.position= new Vector2(rect.position.x, options[currentIndex].position.y);
    }

    private void CheckIndex()
    {
        if (soundBar != null)
        {
            if (currentIndex == 1)
            {
                soundBar.SetActive(true);
                soundBar.GetComponent<SoundBar>().Move("volume");
            }
            else if (currentIndex == 2)
            {
                soundBar.SetActive(true);
                soundBar.GetComponent<SoundBar>().Move("music");
            }
            else
            {
                soundBar.SetActive(false);
            }
        }
    }
}
