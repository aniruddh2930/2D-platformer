using UnityEngine.UI;
using UnityEngine;
using System.Dynamic;

public class SoundBar : MonoBehaviour
{
    private RectTransform rectTransform;
    [Header("Sound Bar Settings")]
    [SerializeField] private float maxWidth;
    [SerializeField] private Image soundBar;

    [Header("Transforms")]
    [SerializeField] private Transform volume;
    [SerializeField] private Transform music;

    private void Awake()
    {
       rectTransform = GetComponent<RectTransform>();   
    }


    private void SetVolume()
    {
        if (AudioManager.instance.currentVolume <= (0.5f * AudioManager.instance.maxVolume))
        {
            soundBar.color = Color.white;
        }
        else if (AudioManager.instance.currentVolume <= (0.8f * AudioManager.instance.maxVolume))
        {
            soundBar.color = Color.yellow;
        }
        else
        {
            soundBar.color = Color.red;
        }
    }

    private void SetMusic()
    {
        if (AudioManager.instance.currentMusic <= (0.5f * AudioManager.instance.maxMusic))
        {
            soundBar.color = Color.white;
        }
        else if (AudioManager.instance.currentMusic <= (0.8f * AudioManager.instance.maxMusic))
        {
            soundBar.color = Color.yellow;
        }
        else
        {
            soundBar.color = Color.red;
        }
    }

    //moves the sound bar to the correct position and changes the size of the sound bar based on the current volume or music
    public void Move(string location)
    {
        if (location == "volume")
        {
            rectTransform.position = new Vector2(rectTransform.position.x, volume.position.y);
            ChangeSize(location);
            SetVolume();
        }
        else if (location == "music")
        {
            rectTransform.position = new Vector2(rectTransform.position.x, music.position.y);
            ChangeSize(location);
            SetMusic();
        }
    }

    //used by uimanager changes size and if neccessary color of the sound bar when the volume or music is changed
    public void ChangeRect(string location)
    {
        ChangeSize(location);
        if (location == "volume")
        {
            SetVolume();
        }
        else if (location == "music")
        {
            SetMusic();
        }
    }

    private void ChangeSize(string location)
    {
        if( location == "music")
        {
            float newWidth = (maxWidth / 0.3f) * AudioManager.instance.currentMusic;
            if (newWidth == 0)
            {
                newWidth = 50;
            }
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            return;
        }
        else
        {

            float newWidth = maxWidth * AudioManager.instance.currentVolume;
            if (newWidth == 0)
            {
                newWidth = 50;
            }
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
    }

    
}
