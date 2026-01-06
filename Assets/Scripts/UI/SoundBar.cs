using UnityEngine.UI;
using UnityEngine;

public class SoundBar : MonoBehaviour
{
    public RectTransform rectTransform;
    [Header("Sound Bar Settings")]
    [SerializeField] private float maxWidth;
    [SerializeField] private Image soundBar;

    [Header("Transforms")]
    [SerializeField] private Transform volume;
    [SerializeField] private Transform music;


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
