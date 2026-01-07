using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject controlsScreen;
    // Update is called once per frame
    public void Resume()
    {
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        gameObject.SetActive(false);
        controlsScreen.SetActive(true);
    }

    
}

