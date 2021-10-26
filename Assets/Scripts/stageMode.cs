using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageMode : MonoBehaviour
{
    public CoinManager coinManager;
    public GameDataScriptable gameData;

    public gacha gachaScript;
    public Score scoreScript;
    int stageIndex = 0;
    float currentTime;
    int distance;
    int checkPoint;
    public GameObject Result;
    public GameObject stagePanelResult;
    public ParticleSystem gachaParLeft, gachaParRight;
    bool countScore = true;
    bool spawnFinishLine = false;
    public Slider slider;
    bool checkOnclick = false;
    //UI
    public GameObject selectStage, selectEndless;
    public Button stageBtn, endlessBtn;
    public Text showLevelInGame;
    public Text levelResult;
    public Text coinResult;
    public Button continueBtn;
    public Button unlockSkin;
    public bool finishStage = false;
    public GameObject UiInGame;
    //public GameObject joyStick;
    public GameObject gachaPanel;
    public GameObject allStageClear;
    public Button continueClearBtn;

    float num = 0.2f;
    //camera
    public Camera mainCamera, gachaCamera;
    private void Awake()
    {
        
        stageIndex = PlayerPrefs.GetInt("StageLevel");
        stagePanelResult.SetActive(false);
        Result.SetActive(false);
    }
    void Start()
    {
        num = PlayerPrefs.GetFloat("checkSpawn", 0.2f);
        spawnFinishLine = false;
        checkOnclick = false;
        finishStage = false;
        mainCamera.enabled = true;
        gachaCamera.enabled = false;

        countScore = true;
        //เรียกใช้ค่าของ stagelevel แล้วก็ checkpointDistance
        checkPoint = gameData.stageLevel[stageIndex].checkpointDistance;
        slider.maxValue = gameData.stageLevel[stageIndex].checkpointDistance;

        continueBtn.onClick.AddListener(() => { changeScene(); });
        stageBtn.onClick.AddListener(() => { OnselectStageMode(); });
        endlessBtn.onClick.AddListener(() => { OnselectEndlessMode(); });
        unlockSkin.onClick.AddListener(() => { swicthCameraAndRandomSkin(); });
        continueClearBtn.onClick.AddListener(() => { changeScene(); });
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
        if (GameManage.GMinstance.mode == "stage")
        {
            if (GameManage.GMinstance.gameStart && countScore && GameManage.GMinstance.playerList.Count != 0)
            {
                if (Item.itemInstance.boosterScore)
                {
                    currentTime += Time.deltaTime * 100 * 2;
                }
                else
                {
                    currentTime += Time.deltaTime * 100;
                }
                distance = Mathf.RoundToInt(currentTime);
                showLevelInGame.text = "Level : " + gameData.stageLevel[stageIndex].StageLevel;
                slider.value = currentTime;
                finishPoint(distance);
            }
        }
    }
    void finishPoint(int valueDistance) 
    {
        if (valueDistance >= checkPoint * num && !spawnFinishLine) 
        {
            spawnFinishLine = true;
            GameManage.GMinstance.checkSpawnFinishLine = true;
        }
        if (valueDistance >= checkPoint && finishStage)
        {
            finishStage = false;
            GameManage.GMinstance.finishInStageMode = true;

            updateNum();
            if (stageIndex < gameData.stageLevel.Length -1)
            {
                GameManage.GMinstance.finishInStageMode = true;
                successStage();
                stageIndex++;
                PlayerPrefs.SetInt("StageLevel", stageIndex);
                PlayerPrefs.Save();
            }
            else
            {
                // เคลียด่าน 20
                // set panel ที่บอกว่า coming soon หรือ ว่าจบโหมดนี้แล้ว
                // แล้วก็ถ้าจบแล้วจะกดเปลี่ยนโหมดมา level ไม่ได้ จะขึ้นว่า clear ไปแล้ว เล่นได้แค่ endless

                UiInGame.SetActive(false);
                //joyStick.SetActive(false);
                stagePanelResult.SetActive(true);
                allStageClear.SetActive(true);
               
                PlayerPrefs.SetInt("FinishStageMode", GameManage.GMinstance.finishStageMode ? 1 : 0);
                GameManage.GMinstance.mode = "endless";

                PlayerPrefs.SetString("Mode", "endless");
                PlayerPrefs.Save();

            }
            countScore = false;
        }
        
    }
    void updateNum() 
    {
        if (stageIndex < 9)
            num += 0.1f;
        else if (stageIndex > 9 && stageIndex < 12)
            num = 0.7f;
        else if (stageIndex > 12 && stageIndex < 18)
            num = 0.8f;
        else if (stageIndex > 18)
            num = 0.9f;
        PlayerPrefs.SetFloat("checkSpawn", num);
    }
    void successStage() 
    {
        stagePanelResult.SetActive(true);
        Result.SetActive(true);
        levelResult.text = "Level : " + gameData.stageLevel[stageIndex].StageLevel;
        coinManager.addCoinPoint();
        PlayerPrefs.SetInt("Coin", coinManager.CoinPoint);
        PlayerPrefs.Save();
        coinResult.text = "" + coinManager.CurrentPoint.ToString();
        gachaScript.gachaLand.SetActive(false);
    }
    public void OnselectStageMode() 
    {
        GameManage.GMinstance.mode = "stage";
        PlayerPrefs.SetString("Mode", "stage");
    }
    public void OnselectEndlessMode()
    {
        GameManage.GMinstance.mode = "endless";
        PlayerPrefs.SetString("Mode", "endless");

    }
    void updateUI() 
    {
        if (GameManage.GMinstance.mode == "stage")
        {
            selectEndless.SetActive(false);
            selectStage.SetActive(true);
        }
        else
        {
            selectStage.SetActive(false);
            selectEndless.SetActive(true);
        }
        if (GameManage.GMinstance.finishStageMode)
        {
            selectStage.SetActive(false);
            selectEndless.SetActive(true);
            stageBtn.gameObject.SetActive(false);
        }
    }
    public void changeScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void swicthCameraAndRandomSkin() 
    {
        
        gachaScript.randomCharacter();
        gachaScript.showSkin();
        stagePanelResult.SetActive(false);
        UiInGame.SetActive(false);
        //joyStick.SetActive(false);
        mainCamera.enabled = false;
        gachaCamera.enabled = true;
        gachaParLeft.Play();
        gachaParRight.Play();
        gachaPanel.SetActive(true);
    }
}
