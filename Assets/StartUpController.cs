using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUpController : MonoBehaviour
{

    public InputField UsernameInput;
    public InputField RegUsernameInput;
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField RegPasswordInput;
    public InputField RegPasswordInput2;
    public Button LoginButton;

    Text status;
    // Start is called before the first frame update
    public Web web;
    GameObject register;
    GameObject login;
    private void Awake()
    {
        web = GameObject.Find("Canvas").GetComponent<Web> ();
        register = GameObject.Find("Register");
        login = GameObject.Find("Login");
        status = GameObject.Find("StatusMessage").GetComponent<Text>();
        login.SetActive(true);
        register.SetActive(false);
    }
    void Start(){
        
        
    }


    public void Login()
    {
        StartCoroutine(web.Login(UsernameInput.text, PasswordInput.text));
        status.text = "Login/Password Incorrect";

    }
    public void Register()
    {

        if (RegPasswordInput.text == RegPasswordInput2.text)
        {
             StartCoroutine(tryRegister());
        }
        else status.text="Password missmatch";

    }
    IEnumerator tryRegister()
    {
        Debug.Log("TryRegister");
        StartCoroutine(web.Register(RegUsernameInput.text, RegPasswordInput.text, EmailInput.text));
        yield return new WaitForSecondsRealtime(1);
        Debug.Log(web.registerSucces);
        if (web.registerSucces)
            goBack();
        else status.text = web.status;
    }

    public void showRegister()
    {
        status.text = "";
        register.SetActive(true);
        login.SetActive(false);
    }
    public void goBack()
    {
        status.text = "";
        register.SetActive(false);
        login.SetActive(true);
    }


}
