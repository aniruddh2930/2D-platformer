using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameAndPassword : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;

    [Header("Warnings")]
    [SerializeField] private TMP_Text usernameWarning;
    [SerializeField] private TMP_Text passwordWarning;

    [Header("Texts")]
    [SerializeField] private TMP_Text login;
    [SerializeField] private TMP_Text createAccount;

    [Header("Login Screen")]
    [SerializeField] private GameObject loginScreen;

    [Header("Sound Manager")]
    [SerializeField] private GameObject soundManager;

    [Header("Main Menu")]
    [SerializeField] private Behaviour selectingText;
    [SerializeField] private Image background;
    [SerializeField] private GameObject continueText;
    [SerializeField] private GameObject controlsText;
    [SerializeField] private float transitionTime;


    private bool isUsernameValid=false;
    private bool isPasswordValid=false;
    private bool isLogingin=true;

    public void UsernameAuthentication(string username)
    {
        if (username.Length == 0)
        {
            return;
        }
        if (isLogingin)
        { 
            if (username.Length < 3)
            {
                usernameWarning.text = "Username must be at least 3 characters long.";
            }
            else
            {
                if (Accounts.instance.CheckUsername(username))
                {
                    if (isPasswordValid)
                    {
                        OpenMainMenu();
                        Debug.Log("Login successful!");
                    }
                    else
                    {
                        Debug.Log("Username is valid.");
                        usernameWarning.text = "";
                        isUsernameValid = true;
                        passwordInput.ActivateInputField();
                    }
                }
                else
                {
                    usernameWarning.text = "Username does not exist. Please create an account.";
                    isUsernameValid = false;
                }
            }
        }
        else
        {
            if (username.Length < 3)
            {
                usernameWarning.text = "Username must be at least 3 characters long.";
            }
            else
            {
                if (!Accounts.instance.CheckUsername(username))
                {
                    if (isPasswordValid)
                    {
                        usernameWarning.text = "";
                        Accounts.instance.CreateAccount(username, passwordInput.text);
                        OpenMainMenu();
                        Debug.Log("Login successful!");
                    }
                    else
                    {
                        Debug.Log("Username is valid.");
                        isUsernameValid = true;
                        usernameWarning.text = "";
                        passwordInput.ActivateInputField();
                    }
                }
                else
                {
                    usernameWarning.text = "Username does exist. Please try another name.";
                    isUsernameValid = false;
                }
            }
        }
    }

    public void PasswordAuthentication(string password)
    {
        if (password.Length == 0)
        {
            return;
        }
        if (password.Length < 8)
        {
            passwordWarning.text="Password must be at least 8 characters long.";
        }
        else
        {
            if (isUsernameValid)
            {
                if (!isLogingin)
                {
                    isPasswordValid = true;
                    passwordWarning.text = "";
                    if (isUsernameValid)
                    {
                        Accounts.instance.CreateAccount(usernameInput.text, password);
                        OpenMainMenu();
                    }
                }
                else
                {
                    if (isUsernameValid)
                    {
                        if (PlayerPrefs.GetString(usernameInput.text) == password)
                        {
                            OpenMainMenu();
                            Debug.Log("Login successful!");
                        }
                        else
                        {
                            passwordWarning.text = "Incorrect password. Please try again.";
                            isPasswordValid = false;
                        }
                    }
                }
            }
            else
            {
                usernameWarning.text = "Please enter a valid username.";
                isPasswordValid = false;
            }
        }
    }

    public void CreateNewAccount()
    {
        if (isLogingin)
        {
            isLogingin = false;
            login.text = "Register";
            createAccount.text = "Back to Login";
        }
        else
        {
            isLogingin = true;
            login.text = "Login";
            createAccount.text = "Register New Account";
        }
    }

    private void OpenMainMenu()
    {
        //setting username of current player in memory so it can be used later
        Accounts.instance.currentUsername = usernameInput.text;
        Debug.Log("Current username: " + Accounts.instance.currentUsername);

        //disabling text components part of login process
        loginScreen.SetActive(false);

        //enabling sound manager
        soundManager.SetActive(true);
        AudioManager.instance.SetSound();

        //enabling main menu components
        continueText.SetActive(true);
        controlsText.SetActive(true);
        selectingText.enabled=true;
        StartCoroutine(TransitionColor());
    }

    private  IEnumerator TransitionColor()
    {
        float diff= (1-background.color.a)/100;
        for (int i=0;i<100; i++)
        {
            Color tempColor = background.color;
            tempColor.a += diff;
            background.color = tempColor;
            yield return new WaitForSeconds(transitionTime/100);
        }
    }
}
