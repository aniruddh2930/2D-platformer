using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    private AudioSource musicSource;
    private string currentUsername;
    public static AudioManager instance;
    public float currentVolume { get;private set; }
    public float currentMusic { get; private set; }
    [SerializeField] [Range(0.0f, 1.0f)] private float changeVolume ;
    [SerializeField] [Range(0.0f, 1.0f)] private float changeMusic;
    [SerializeField] [Range(0.0f, 1.0f)] public float maxVolume;
    [SerializeField] [Range(0.0f, 1.0f)] public float maxMusic;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        source = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        changeMusic = changeMusic*maxMusic;
        changeVolume = changeVolume*maxVolume;
    }

    public void SetSound()
    {
        currentUsername = Accounts.instance.currentUsername;
        currentMusic = PlayerPrefs.GetFloat(Accounts.instance.GetMusicID(currentUsername), maxMusic);
        currentVolume = PlayerPrefs.GetFloat(Accounts.instance.GetVolumeID(currentUsername), maxVolume);
        musicSource.volume = currentMusic;
        source.volume = currentVolume;
    }

    public void PlaySound(AudioClip clip)
    {

        source.PlayOneShot(clip);
    }

    public void ChangeMusic()
    {
        // music from 0 -0.3
        currentMusic += changeMusic;
        if (currentMusic > 0.3f)
        {
            currentMusic = 0f;
        }
        musicSource.volume = currentMusic;
        currentUsername = Accounts.instance.currentUsername;
        PlayerPrefs.SetFloat(Accounts.instance.GetMusicID(currentUsername), currentMusic);
    }

    public void ChangeVolume()
    {
        // volume from 0 -1.0
        currentVolume += changeVolume;
        if (currentVolume > 1f)
        {
            currentVolume = 0f;
        }
        source.volume = currentVolume;
        currentUsername = Accounts.instance.currentUsername;
        PlayerPrefs.SetFloat(Accounts.instance.GetVolumeID(currentUsername), currentVolume);
    }
}
