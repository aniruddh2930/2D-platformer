using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private GameObject pauseScreen;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySound(gameOver);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
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
            }
        }
}

    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
    }
}
