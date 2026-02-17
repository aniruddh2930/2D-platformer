using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private SoundBar soundBar;
    [SerializeField] private GameObject Bar;
    [SerializeField] private SelectionArrow selectionArrow;

    public static float start = 0.0f;

    private void Start()
    {
        start=Time.time;
    }
    //Game Over Screen Functions
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySound(gameOver);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Shared functions
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    //Pause Screen Functions
    public void Resume()
    {
        PauseGame(false);
        Bar.SetActive(false);
        Time.timeScale = 1;
    }

    public void Volume()
    {
        selectionArrow.SetIndex(1);
        AudioManager.instance.ChangeVolume();
        soundBar.ChangeRect("volume");
    }

    public void Music()
    {
        selectionArrow.SetIndex(2);
        AudioManager.instance.ChangeMusic();
        soundBar.ChangeRect("music");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.activeInHierarchy && !gameOverScreen.activeInHierarchy && !finishScreen.activeInHierarchy)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
                Bar.SetActive(false);
            }
        }
    }

    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if (!status)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

}
