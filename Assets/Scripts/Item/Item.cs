using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item itemInstance;
    public GameDataScriptable gameData;
    public CoinManager coinManager;
    int stageIndex;
    GameObject player;
    //booster
    int distance;
    float speed;
    float boostSpeed;
    float timeBooster;
    bool useBooster = false;
    [SerializeField] int costBooster = 500;
    public GameObject BoosterBtn;
    public GameObject boosterParticle;
    public bool boosterScore = false;

    //magnet
    public float timeMagnet;
    public float speedMove = 10f;
    bool useMagnet = false;
    GameObject coinDetector; //ตอนเช้าทำต่อด้วย
    [SerializeField] int costMagnet = 500;
    public GameObject MagnetBtn;
    public GameObject magnetParticle;
    //shield
    GameObject shieldObj;
    public bool useShield = false;
    [SerializeField] int costShield = 500;
    public GameObject ShieldBtn;
    void Awake()
    {
        if (itemInstance == null)
        {
            itemInstance = this;
        }
        stageIndex = PlayerPrefs.GetInt("StageLevel");
        BoosterBtn.SetActive(false);
        MagnetBtn.SetActive(false);
        ShieldBtn.SetActive(false);

    }
    void Start()
    {
        //Booster
        speed = GameManage.GMinstance.forwardSpeedPlayer;
        boostSpeed = speed + (speed * 100) / 100;
        distance = gameData.stageLevel[stageIndex].checkpointDistance;
        timeBooster = (distance * 0.2f) / 100;
        boosterParticle.SetActive(false);

        if (PlayerPrefs.GetInt("booster") == 1)
            useBooster = true;
        //Magnet
        coinDetector = GameManage.GMinstance.playerList[0].transform.GetChild(0).gameObject;
        magnetParticle = GameObject.FindGameObjectWithTag("magnetParticle");
        magnetParticle.SetActive(false);
        coinDetector.SetActive(false);
        timeMagnet = distance / 100;

        if (PlayerPrefs.GetInt("magnet") == 1)
            useMagnet = true;

        //shield
        shieldObj = GameManage.GMinstance.playerList[0].transform.GetChild(1).gameObject;
        shieldObj.SetActive(false);

        if (PlayerPrefs.GetInt("shield") == 1)
            useShield = true;

        player = GameManage.GMinstance.playerList[0];
    }

    // Update is called once per frame
    void Update()
    {
        magnetParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1.5f, player.transform.position.z-0.5f);
        boosterParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z - 0.5f);
        updateUI();
        if (GameManage.GMinstance.playerList.Count != 0)
        {
            coinDetector = GameManage.GMinstance.playerList[0].transform.GetChild(0).gameObject;
            shieldObj = GameManage.GMinstance.playerList[0].transform.GetChild(1).gameObject;
        }
        if (GameManage.GMinstance.gameStart && GameManage.GMinstance.mode == "stage")
        {
            if (useBooster)
                OnBooster();
            if (useMagnet)
                OnMagnet();
            if (useShield)
                OnShield();

            PlayerPrefs.SetInt("booster", 0);
            PlayerPrefs.SetInt("magnet", 0);
            PlayerPrefs.SetInt("shield", 0);
        }
        if (GameManage.GMinstance.playerList.Count == 0)
        {
            magnetParticle.SetActive(false);
            boosterParticle.SetActive(false);
        }

    }
    void updateUI() 
    {
        if (PlayerPrefs.GetInt("booster") == 1)
            BoosterBtn.SetActive(true);
        else
            BoosterBtn.SetActive(false);

        if (PlayerPrefs.GetInt("magnet") == 1)
            MagnetBtn.SetActive(true);
        else
            MagnetBtn.SetActive(false);

        if (PlayerPrefs.GetInt("shield") == 1)
            ShieldBtn.SetActive(true);
        else
            ShieldBtn.SetActive(false);

    }
    public void onClickBooster() 
    {
        if (coinManager.CoinPoint >= costBooster)
        {
            useBooster = true;
            PlayerPrefs.SetInt("booster", 1);
            BoosterBtn.SetActive(true);
            coinManager.useCoin(costBooster);
        }
    }
    public void OnBooster() 
    {
        boosterScore = true;
        GameManage.GMinstance.forwardSpeedPlayer = boostSpeed;
        boosterParticle.SetActive(true);
        StartCoroutine("activeBooster");
        if (GameManage.GMinstance.mode == "endless")
        {
            timeBooster = 5;
        }
    }
    IEnumerator activeBooster() 
    {
        yield return new WaitForSeconds(timeBooster);
        GameManage.GMinstance.forwardSpeedPlayer = speed;
        if (GameManage.GMinstance.mode == "stage")
        {
            PlayerPrefs.SetInt("booster", 0);
        }
        useBooster = false;
        boosterScore = false;
        boosterParticle.SetActive(false);
    }
    public void onClickMagnet()
    {
        if (coinManager.CoinPoint >= costMagnet)
        {
            useMagnet = true;
            PlayerPrefs.SetInt("magnet", 1);
            MagnetBtn.SetActive(true);
            coinManager.useCoin(costMagnet);
        }
    }

    public void OnMagnet() 
    {
        coinDetector.SetActive(true);
        magnetParticle.SetActive(true);
        StartCoroutine("activeMagnet");
        if (GameManage.GMinstance.mode == "endless")
        {
            timeMagnet = 5;
        }

    }
    IEnumerator activeMagnet()
    {
        yield return new WaitForSeconds(timeMagnet);
        coinDetector.SetActive(false);
        if (GameManage.GMinstance.mode == "stage")
        {
            PlayerPrefs.SetInt("magnet", 0);
        }
        useMagnet = false;
        magnetParticle.SetActive(false);
    }
    public void onClickShield()
    {
        if (coinManager.CoinPoint >= costMagnet)
        {
            useShield = true;
            PlayerPrefs.SetInt("shield", 1);
            ShieldBtn.SetActive(true);
            coinManager.useCoin(costShield);
        }
    }
    public void OnShield() 
    {
        shieldObj.SetActive(true);
    }

}
