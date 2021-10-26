using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManage : MonoBehaviour
{
    public GameDataScriptable gameData;
    public List<GameObject> playerList;
    public GameObject panelWhenPlayerDead;
    public static GameManage GMinstance;
    UIManager uIManager;
    public bool gameStart = false;
    bool endGame = true;
    public CoinManager coinManager;
    public int countToSpawn;
    public GameObject uiInGame;
    public GameObject rewardAds;
    bool watchAds = false;
    public GameObject NothanksButton;

    //stagePanel Result
    public GameObject tryagainPanel;
    public GameObject resultStagePanel;


    Vector3 lastPosition;
    GameObject player;

    //forwardSpeedForPlayer
    public Score score;
    [SerializeField] int checkScore = 20;
    [SerializeField] int increaseSpeedRate = 2;
    [SerializeField] int increaseScoreRate = 20;
    public float forwardSpeedPlayer = 20;

    //spawnRateForObstacle
    [SerializeField] int TimeToIncreaseSpawnRate = 20;
    [SerializeField] float increaseSpawnRate = 0.1f;
    [SerializeField] int increaseTimeRate = 20;
    public float spawnRate = 1;
    public float obsSpawn = 1;
    public float speedObstacle;

    public Text coinInMenu;

    //player spawn
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;
    Shopdata shopData;

    [SerializeField] GameObject soundOn, soundOff;

    //mode 
    public string[] gameMode;
    public string mode = "stage";
    public bool finishInStageMode = false;
    public bool finishStageMode = false;
    public string currentMode;
    public bool checkSpawnFinishLine = false;
    //ads
    int countInterstitialAd = 0;

    private void Awake()
    {
        if (GMinstance == null)
        {
            GMinstance = this;
        }
        if (!finishStageMode)
        {
            mode = PlayerPrefs.GetString("Mode", "stage");
            currentMode = mode;
        }
        else
        {
            mode = "endless";
        }
        finishStageMode = PlayerPrefs.GetInt("FinishStageMode" , 0) == 1 ? true : false; 
        uIManager = this.GetComponent<UIManager>();
        gameStart = false;
        int indexOfCharacter = PlayerPrefs.GetInt("selectCharacter");
        shopData = Shopdata.shopInstance;
        GameObject character = Instantiate(playerPrefab, playerSpawnPoint.transform.position, transform.rotation.normalized);
        GameObject model = Instantiate(shopData.shopCharacters[indexOfCharacter].character, playerSpawnPoint.transform.position, transform.rotation.normalized);
        model.transform.parent = character.transform;
        finishInStageMode = false;
        
    }
    void Start()
    {
        panelWhenPlayerDead.SetActive(false);
        player = playerList[0];
        countInterstitialAd = PlayerPrefs.GetInt("countInterstitialAd");
        
    }
    private void Update()
    {
        coinInMenu.text = coinManager.CoinPoint.ToString();
        if (playerList.Count != 0)
        {
            playerList[0].transform.tag = "Player";
            lastPosition = playerList[0].transform.position;
        }
        if (playerList.Count == 0 && endGame)
        {
            Invoke("whenDead", 0);
        }
        increasePlayerSpeed();
        setSpawnRateObstacle();
    }
    public void whenDead()
    {
        endGame = false;
        uiInGame.SetActive(false);
        if (!watchAds)
        {
            rewardAds.SetActive(true);
            StartCoroutine("delayNothanksButton");
            watchAds = true;
        }
        else
        {
            showPanelwhenPlayerDead();
        }
    }
    public void WatchAds()
    {
        rewardAds.SetActive(false);
        rewardedWhenUserWatch();
        AdMobScript.AdMobInstance.UserChoseToWatchGameOverRewardAds();
    }
    public void noThanks()
    {
        rewardAds.SetActive(false);
        showPanelwhenPlayerDead();
    }
    IEnumerator delayNothanksButton()
    {
        yield return new WaitForSeconds(1.5f);
        NothanksButton.SetActive(true);
    }

    public void rewardedWhenUserWatch()
    {
        player.transform.position = lastPosition;
        player.gameObject.GetComponent<CharacterBehav>().enabled = true;
        player.gameObject.GetComponent<CharacterControl>().enabled = true;
        StartCoroutine("delayToSwitchLayer");
        endGame = true;
        uiInGame.SetActive(true);
        player.SetActive(true);
    }
    IEnumerator delayToSwitchLayer() 
    {
        yield return new WaitForSeconds(3);
        player.gameObject.layer = 7;
    }
    void showPanelwhenPlayerDead() 
    {
        countInterstitialAd++;
        PlayerPrefs.SetInt("countInterstitialAd", countInterstitialAd);
        PlayerPrefs.Save();
        if (countInterstitialAd >= 3)
        {
            countInterstitialAd = 0;
            AdMobScript.AdMobInstance.showInterstitialAds();
            PlayerPrefs.SetInt("countInterstitialAd", countInterstitialAd);
            PlayerPrefs.Save();
        }
        if (mode == "endless")
        {
            PlayerPrefs.SetInt("HighScore", score.HighScore);
            coinManager.addCoinPoint();
            PlayerPrefs.SetInt("Coin", coinManager.CoinPoint);
            panelWhenPlayerDead.SetActive(true);
        }
        if (mode == "stage")
        {
            resultStagePanel.SetActive(true);
            tryagainPanel.SetActive(true);
            coinManager.addCoinPoint();
            PlayerPrefs.SetInt("Coin", coinManager.CoinPoint);
        }
        uIManager.showScoreWhenPlayerDead();
    }

    
    void increasePlayerSpeed()
    {
        if (mode == "endless")
        {
            if (score.score > checkScore)
            {
                checkScore += increaseScoreRate;
                forwardSpeedPlayer += increaseSpeedRate;
            }
        }
        //if (mode == "stage") //เพิ่ม speed player ใน mode stage
        //{
        //    int stagelevel = PlayerPrefs.GetInt("StageLevel") + 1; //stageใน PlayerPref เริ่มที่ 0 แต่ใน scriptเริ่มที่ 1 เลยต้องบวกเข้าไป
        //    int stageIndex = 1;
        //    if (stagelevel == gameData.stageLevel[stageIndex].StageLevel && gameData.stageLevel[stageIndex].StageLevel <= 10)
        //    {
        //        forwardSpeedPlayer += increaseSpeedRate;
        //        stageIndex += 2;
        //    }
        //}
    }

    
    void setSpawnRateObstacle() 
    {
        if (mode == "endless")
        {
            if (score.score > TimeToIncreaseSpawnRate)
            {
                TimeToIncreaseSpawnRate += increaseTimeRate;
                //spawnRate -= increaseSpawnRate;
                switch (TimeToIncreaseSpawnRate - 10)
                {
                    case 10: obsSpawn = 1;  spawnRate = 1; break;
                    case 20: obsSpawn = 1;  spawnRate = 1; break;
                    case 30: obsSpawn = 2;  spawnRate = 1; break;
                    case 40: obsSpawn = 2;  spawnRate = 1; break;
                    case 50: obsSpawn = 3;  spawnRate = 1; break;
                    case 80: obsSpawn = 3;  spawnRate = 1; break;
                    case 110: obsSpawn = 4;  spawnRate = 1; break;
                    case 140: obsSpawn = 4;  spawnRate = 1; break;
                    case 170: obsSpawn = 6; spawnRate = 1; break;
                    default: obsSpawn = 3;  spawnRate = 1; break;
                }
                spawnRate = spawnRate / obsSpawn;
            }
        }
        if (mode == "stage")
        {
            int stagelevel = PlayerPrefs.GetInt("StageLevel") + 1; //stageใน PlayerPref เริ่มที่ 0 แต่ใน scriptเริ่มที่ 1 เลยต้องบวกเข้าไป
            if (stagelevel >= gameData.stageLevel[0].StageLevel && stagelevel < gameData.stageLevel[4].StageLevel)
            {
                spawnRate = 1;
                obsSpawn = 1;
                spawnRate = spawnRate / obsSpawn;
            }
            else if (stagelevel >= gameData.stageLevel[4].StageLevel && stagelevel < gameData.stageLevel[9].StageLevel)
            {
                spawnRate = 1;
                obsSpawn = 1.5f;
                spawnRate = spawnRate / obsSpawn;
            }
            else if (stagelevel >= gameData.stageLevel[9].StageLevel && stagelevel < gameData.stageLevel[14].StageLevel)
            {
                spawnRate = 1;
                obsSpawn = 2;
                spawnRate = spawnRate / obsSpawn;
            }
            else
            {
                spawnRate = 1;
                obsSpawn = 2;
                spawnRate = spawnRate / obsSpawn;
            }

            
        }
    }

    public void changeButtonSoundControl(bool muted) 
    {
        if (!muted)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
}
