using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private SoundBar soundBar;
    [SerializeField] private GameObject Bar;

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
        Time.timeScale = 1;
    }

    public void Volume()
    {
        AudioManager.instance.ChangeVolume();
        soundBar.ChangeRect("volume");
    }

    public void Music()
    {
        AudioManager.instance.ChangeMusic();
        soundBar.ChangeRect("music");
    }






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.activeInHierarchy && !gameOverScreen.activeInHierarchy)
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
