using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Linq;

public class UIControllerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIControllerMenu instance;
    public Web web;
    public GameObject userProfile;

    private string jsonArray;
    private bool finished = false;
    

    GameObject leaderboard;
    GameObject close;
    public Transform entryContainer;
    public Transform entryTemplate;
    
    private void Awake()
    {
        //entryContainer = transform.Find("Panel/Leaderboard/highscoreEntryContainer");
        //entryTemplate = transform.Find("Panel/Leaderboard/highscoreEntryTemplate");
        leaderboard = GameObject.Find("Leaderboard");
        close = GameObject.Find("Exit");
        leaderboard.SetActive(false);
        entryTemplate.gameObject.SetActive(false);
        close.SetActive(false);
       
        
        

    }


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        instance = this;
        web = GetComponent<Web>();
        Debug.Log(PlayerPrefs.GetString("username") + PlayerPrefs.GetInt("score"));

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    public void Leaderboard()
    {
        
        leaderboard.SetActive(true);
        close.SetActive(true);
        entryContainer.gameObject.SetActive(true);
        StartCoroutine(getUserList());
        
    }
    public void CloseLead()
    {
        leaderboard.SetActive(false);
        close.SetActive(false);
    }


    public IEnumerator getUserList()
    {
        finished = false;
        using (UnityWebRequest www = UnityWebRequest.Get("http://79.113.85.34/UnityBackend/getUsers.php"))
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
                    jsonArray = www.downloadHandler.text;
                    List<UsersJSON> userList = JsonConvert.DeserializeObject<List<UsersJSON>>(jsonArray);
                    userList = userList.OrderByDescending(user => user.score).ToList();
                    int i = 0;
                    float templateHeight = 20f;
                    foreach (UsersJSON user in userList)
                    {
                        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

                        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                        entryTransform.gameObject.SetActive(true);

                        int rank = i + 1;
                        string rankString;
                        switch (rank)
                        {
                            default:
                                rankString = rank + "TH"; break;
                            case 1: rankString = "1ST"; break;
                            case 2: rankString = "2ND"; break;
                            case 3: rankString = "3RD"; break;
                        }
                        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
                        entryTransform.Find("posScore").GetComponent<Text>().text = user.score;
                        entryTransform.Find("posName").GetComponent<Text>().text = user.username;
                        i++;
                    }

                }

            }
        }

    }

    
}
public class UsersJSON
{
    public string username;
    public string score;

}

