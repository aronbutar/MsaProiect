using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class UIController : MonoBehaviour
{
    Player player;
    Text distanceText;

    GameObject results;
    Text finalDistanceText;
    public Text highScoreText;


    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        results = GameObject.Find("Results");
        results.SetActive(false);
       

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";
            if (PlayerPrefs.GetInt("score") < distance)
            {
                PlayerPrefs.SetInt("score", distance);
                StartCoroutine(setScore(PlayerPrefs.GetString("username"), distance));
            }
            highScoreText.text = ""+PlayerPrefs.GetInt("score");
            //Debug.Log(PlayerPrefs.GetInt("score"));
        }
        
        
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public IEnumerator setScore(string username, int score)
    {

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginScore", score);
        using (UnityWebRequest www = UnityWebRequest.Post("http://79.113.85.34/UnityBackend/setScore.php", form))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {

                Debug.Log("Error:\n" + www.error);
            }

        }
    }
}
