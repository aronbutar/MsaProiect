using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public bool loginSucces = false;
    public bool registerSucces = false;
    public string status = "";
    public string jsonArray = "";
    public bool finished;

    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate("http://localhost/UnityBackend/test.php"));
        //StartCoroutine(Login("testuser","passwrod123"));
        //StartCoroutine(Register("testuser2","passwrod123","hellomail@mail2.com"));


    }

 

    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator Login(string username,string password)
    {
        
        WWWForm form=new WWWForm();
        form.AddField("loginUser",username);
        form.AddField("loginPass",password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://79.113.85.34/UnityBackend/Login.php", form))
        {
            yield return www.SendWebRequest();
            

            if (www.result != UnityWebRequest.Result.Success)
            {

                Debug.Log("Error:\n"+www.error);
            }
            else
            {
                Debug.Log("Login:"+www.downloadHandler.text);
                if (www.downloadHandler.text == "Login Succes.")
                {
                    loginSucces = true;
                    Debug.Log("Status="+ loginSucces);
                }
                    
            }
        }
        if (loginSucces)
        {
            PlayerPrefs.SetString("username",username);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            getInfo(username);
            SceneManager.LoadScene("Menu");
        }
    }

    public IEnumerator Register(string username,string password,string email)
    {
        Debug.Log("Entered Register\n");
        WWWForm form=new WWWForm();
        form.AddField("loginUser",username);
        form.AddField("loginPass",password);
        form.AddField("loginEmail",email);
        using (UnityWebRequest www = UnityWebRequest.Post("http://79.113.85.34/UnityBackend/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "New Account created successfully")
                {
                    registerSucces = true;
                }
                else status = www.downloadHandler.text;
            }
        }
    }

    public IEnumerator getInfo(string username)
    {

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        using (UnityWebRequest www = UnityWebRequest.Post("http://79.113.85.34/UnityBackend/getUserInfo.php", form))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {

                Debug.Log("Error:\n" + www.error);
            }
            else
            {
                Debug.Log("Login:" + www.downloadHandler.text);
                if (www.downloadHandler.text != "Error")
                {
                    PlayerPrefs.SetInt("score",int.Parse(www.downloadHandler.text));
                }

            }
        }
    }


}
