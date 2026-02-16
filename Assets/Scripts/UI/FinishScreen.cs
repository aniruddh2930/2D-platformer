using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour
{
    private int timeElapsed;

    [Header("Times")]
    [SerializeField] private TMP_Text allTimeText;
    [SerializeField] private TMP_Text personalBestTimeText;
    [SerializeField] private TMP_Text currentTimeText;

    private void Start()
    {
       timeElapsed = Mathf.RoundToInt(Time.time - UIManager.start);
       int personalBestTime=PlayerPrefs.GetInt(Accounts.instance.GetTimeID(Accounts.instance.currentUsername), int.MaxValue); 
       int allTimeBestTime=PlayerPrefs.GetInt("allTimeBestTime", int.MaxValue);
       if (timeElapsed < allTimeBestTime)
       {
           PlayerPrefs.SetInt("allTimeBestTime", timeElapsed);
           allTimeBestTime = timeElapsed;
           PlayerPrefs.SetInt(Accounts.instance.GetTimeID(Accounts.instance.currentUsername), timeElapsed);
           personalBestTime = timeElapsed;
       }
       else if (timeElapsed<personalBestTime)
       {
         PlayerPrefs.SetInt(Accounts.instance.GetTimeID(Accounts.instance.currentUsername), timeElapsed);
         personalBestTime = timeElapsed;

       }

       allTimeText.text = FormatTime(allTimeBestTime);
       personalBestTimeText.text = FormatTime(personalBestTime);
       currentTimeText.text = FormatTime(timeElapsed);

    }

    private string FormatTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
