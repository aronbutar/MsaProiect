using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private string username;
    //string hashcode;
    private string score;
    public void SetUsername(string username)
    {
        this.username = username;
    }
    public void SetScore(string score)
    {
        this.score = score;
    }
    public string GetUsername() { return username; }
    public string GetScore() { return score; }
}
