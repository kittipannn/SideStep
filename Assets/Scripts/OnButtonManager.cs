using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnButtonManager : MonoBehaviour
{
    public Spawner spawner;
    
    public GameObject /*joyStick,*/ Ui,startGame;
    [SerializeField] Score score;
    [SerializeField] GameObject panelShop , panelPause ,panelOption,panelMode;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Camera mainCamera, shopCamera;
    [SerializeField] private Button munuButtonResult;
    [SerializeField] private Button munuButtonPause;
    [SerializeField] private Button rewardAdsButton;
    [SerializeField] private Button noThanksButton;
    [SerializeField] private Button removeAds;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionToMenuButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;
    [SerializeField] private Button boosterButton;
    [SerializeField] private Button magnetButton;
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button menustageResultButton;
    [SerializeField] private Button modeButton;
    [SerializeField] private Button modeToMenu;
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;
    public GameObject shop;
    public UIManager uIManager;
    string currentMode;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        
        //joyStick.SetActive(false);
        Ui.SetActive(false);
        shop.SetActive(false);
        startGameButton.onClick.AddListener(() => { OnStartGame(); });
        shopButton.onClick.AddListener(() => { OnOpenShop(); });
        backButton.onClick.AddListener(() => { shoptoMenu(); });
        pauseButton.onClick.AddListener(() => { OnPauseButton(); });
        resumeButton.onClick.AddListener(() => { resumeGame(); });
        noThanksButton.onClick.AddListener(() => { GameManage.GMinstance.noThanks(); });
        rewardAdsButton.onClick.AddListener(() => { GameManage.GMinstance.WatchAds(); });
        munuButtonResult.onClick.AddListener(() => { menuButtonInGame(); });
        munuButtonPause.onClick.AddListener(() => { menuButtonInGame(); });
        optionButton.onClick.AddListener(() => { OnOption(); });
        optionToMenuButton.onClick.AddListener(() => { optionToMenu(); });
        soundOnButton.onClick.AddListener(() => { SoundManager.SMInstance.OnButtonSoundControl(); });
        soundOffButton.onClick.AddListener(() => { SoundManager.SMInstance.OnButtonSoundControl(); });
        boosterButton.onClick.AddListener(() => { Item.itemInstance.onClickBooster(); });
        magnetButton.onClick.AddListener(() => { Item.itemInstance.onClickMagnet(); });
        shieldButton.onClick.AddListener(() => { Item.itemInstance.onClickShield(); });
        tryAgainButton.onClick.AddListener(() => { menuButtonInGame(); });
        menustageResultButton.onClick.AddListener(() => { menuButtonInGame(); });
        modeButton.onClick.AddListener(() => { OnMode(); });
        modeToMenu.onClick.AddListener(() => { modePanelToMenu(); });
    }
    
    public void OnStartGame()
    {
        Animator startgameUI = startGame.GetComponent<Animator>();
        startgameUI.SetTrigger("disableUI"); 
    }

    public void OnOpenShop()
    {

        startGame.SetActive(false);
        shop.SetActive(true);
        panelShop.SetActive(true);
        mainCamera.enabled = false;
        shopCamera.enabled = true;

    }
    public void shoptoMenu() 
    {
        shop.SetActive(false);
        panelShop.SetActive(false);
        startGame.SetActive(true);
        mainCamera.enabled = true;
        shopCamera.enabled = false;
    }
    public void OnPauseButton() 
    {
        //uIManager.showScoreInPausePanel();
        panelPause.SetActive(true);
        //joyStick.SetActive(false);
        Ui.SetActive(false);
        Time.timeScale = 0;
    }
    public void resumeGame() 
    {
        Animator panelPauseAnim = panelPause.GetComponent<Animator>();
        panelPauseAnim.SetTrigger("enablePanel");
    }
    public void menuButtonInGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnOption() 
    {
        startGame.SetActive(false);
        panelOption.SetActive(true);
    }
    public void OnMode()
    {
        startGame.SetActive(false);
        panelMode.SetActive(true);
    }
    public void modePanelToMenu()
    {
        Animator panelModeAnim = panelMode.GetComponent<Animator>();
        panelModeAnim.SetTrigger("closeMode");

    }
    public void optionToMenu() 
    {
        Animator panelOptionAnim = panelOption.GetComponent<Animator>();
        panelOptionAnim.SetTrigger("closeOption");
        
    }

    public void OnRemoveAds() 
    {
        AdMobScript.AdMobInstance.removeAds();
        PlayerPrefs.SetInt("ads", 0);
    }


}
