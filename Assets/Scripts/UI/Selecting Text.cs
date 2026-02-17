using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SelectingText : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject[] options;
    [SerializeField] private TMP_FontAsset noGlow;
    [SerializeField] private TMP_FontAsset glow;

    [Header("Sounds")]
    [SerializeField] private AudioClip navigateSound;
    [SerializeField] private AudioClip clickSound;

    private int currentIndex = 0;
    private int prevIndex = 0;
    // Update is called once per frame
    private void Start()
    {
        UpdateText();
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D))
       {
           AudioManager.instance.PlaySound(navigateSound);
           ChangeIndex(1);
           UpdateText();
       }
       else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
       {
           AudioManager.instance.PlaySound(navigateSound);
           ChangeIndex(-1);
           UpdateText();
       }

       if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
       {
           AudioManager.instance.PlaySound(clickSound);
           options[currentIndex].GetComponent<Button>().onClick.Invoke();
       }

    }

    private int ChangeIndex(int change)
    {
        prevIndex = currentIndex;
        currentIndex += change;
        currentIndex = ((currentIndex%options.Length)+options.Length)%options.Length;
        return currentIndex;
    }

    private void UpdateText()
    {
        options[prevIndex].GetComponent<TMP_Text>().font = noGlow;
        options[currentIndex].GetComponent<TMP_Text>().font = glow;
    }
}
