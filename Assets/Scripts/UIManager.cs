using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameDataScriptable gamedata;
    //Text
    public Score scoreManager;
    public Text textScore;
    public Text textHighScore;
    public Text levelStage;
    public Text levelStageInPause;
    public Text coinInStageResult;
    //public Text textCurrntCoin;
    //public Text textHighScoreCoin;
    //Coin
    public CoinManager coinManager;
    public Text Coin;
    public Text CoinPoint;
    public Score score;
    //panelWhenDead
    public Text currentScore;
    public Text highScore;

    //PausePanel
    //public Text currentScoreInPause;

    //UI
    public GameObject endlessPanel, stagePanel;
    bool checkSetActivePanel = false;
    public GameObject stageUI;

    private void Start()
    {
        int stageIndex = PlayerPrefs.GetInt("StageLevel");
        checkSetActivePanel = false;
        if (GameManage.GMinstance.mode == "stage")
        {
            stageUI.SetActive(true);
        }
        else
        {
            stageUI.SetActive(false);
        }
        
    }
    void Update()
    {
        updateScoreAndCoin();
        if (GameManage.GMinstance.gameStart && GameManage.GMinstance.mode == "stage" && !checkSetActivePanel)
        {
            endlessPanel.SetActive(false);
            stagePanel.SetActive(true);
            checkSetActivePanel = true;
        }
        else if (GameManage.GMinstance.gameStart && GameManage.GMinstance.mode == "endless" && !checkSetActivePanel)
        {
            stagePanel.SetActive(false);
            endlessPanel.SetActive(true);
            checkSetActivePanel = true;
        }
    }
    void updateScoreAndCoin()
    {
        if (GameManage.GMinstance.mode == "endless")
        {
            stageUI.SetActive(false);
            textScore.text = "" + scoreManager.score.ToString();
            textHighScore.text = "" + scoreManager.HighScore.ToString();
            //textCurrntCoin.text = "Your Score : " + coinManager.CurrentPoint.ToString();
            //textHighScoreCoin.text = "Highscore : " + coinManager.HighCoinPoint.ToString();
            Coin.text = coinManager.CurrentPoint.ToString();
        }
        else if (GameManage.GMinstance.mode == "stage")
        {
            coinInStageResult.text = coinManager.CurrentPoint.ToString();
            stageUI.SetActive(true);
        }

    }
    public void showScoreWhenPlayerDead()
    {
        if (GameManage.GMinstance.mode == "endless")
        {
            currentScore.text = "" + score.score.ToString();
            highScore.text = "" + score.HighScore.ToString();
            CoinPoint.text = "" + coinManager.CurrentPoint.ToString();
        }
        if (GameManage.GMinstance.mode == "stage")
        {
            int stageIndex = PlayerPrefs.GetInt("StageLevel");
            levelStage.text = "Level : " + gamedata.stageLevel[stageIndex].StageLevel;
        }
    }
    //public void showScoreInPausePanel() 
    //{
    //    if (GameManage.GMinstance.mode == "endless")
    //    {
    //        currentScoreInPause.text = "" + score.score.ToString();
    //    }
    //    if (GameManage.GMinstance.mode == "stage")
    //    {
    //        int stageIndex = PlayerPrefs.GetInt("StageLevel");
    //        levelStageInPause.text = "Level : " + gamedata.stageLevel[stageIndex].StageLevel;
    //    }
    //}
}
