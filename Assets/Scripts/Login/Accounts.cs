using Unity.VisualScripting;
using UnityEngine;

public class Accounts : MonoBehaviour
{
    public static Accounts instance;
    public string currentUsername;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CheckUsername(string username)
    {
        if (PlayerPrefs.HasKey(username))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CreateAccount(string username, string password)
    {
        PlayerPrefs.SetString(username, password);
        Debug.Log("Account created successfully!");
    }

    public string GetVolumeID(string username)
    {
        return username + "_volume";
    }

    public string GetMusicID(string username)
    {
        return username + "_music";
    }

    public string GetTimeID(string username)
    {
        return username + "_time";
    }
}
