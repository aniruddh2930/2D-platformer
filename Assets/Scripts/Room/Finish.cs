using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject finishScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0;
            finishScreen.SetActive(true);
        }
    }
}
