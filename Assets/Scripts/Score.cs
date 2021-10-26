
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    float currentScore = 0;
    int highScore = 0;
    GameObject player;
    float scoreBooster = 2;
    //public bool spawnPlayer = false;
    bool countScore = false;
    public int score { get; set; }
    public int HighScore { get; set; }
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore",0);
        HighScore = highScore;
        score = 0;
    }

    void Update()
    {
        if (GameManage.GMinstance.gameStart)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            countScore = true;
        }
        if (GameManage.GMinstance.mode == "endless")
        {
            if (countScore)
            {
                if (GameManage.GMinstance.playerList.Count != 0)
                {

                    if (Item.itemInstance.boosterScore)
                    {
                        currentScore += Time.deltaTime * scoreBooster;
                    }
                    else
                    {
                        currentScore += Time.deltaTime;
                    }
                    score = Mathf.RoundToInt(currentScore);
                }
            }
            saveHighScore(Mathf.RoundToInt(currentScore));
        }
        
    }
    void saveHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            HighScore = highScore;
        }
    }
    
    
}
