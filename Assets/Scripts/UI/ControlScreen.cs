using UnityEngine;

public class ControlScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainManu;
    [SerializeField] private AudioClip escapeSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlaySound(escapeSound);
            gameObject.SetActive(false);
            mainManu.SetActive(true);
        }
    }
}
