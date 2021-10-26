using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEventUi : MonoBehaviour
{
    UIManager uIManager;
    public GameObject /*joyStick,*/ Ui, startGame, panelPause, panelOption,panelMode;
    
    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }
    public void disableStartUi() 
    {
        startGame.SetActive(false);
        //joyStick.SetActive(true);
        Ui.SetActive(true);
        GameManage.GMinstance.gameStart = true;

    }
    public void closePausePanel() 
    {
        panelPause.SetActive(false);
        //joyStick.SetActive(true);
        Ui.SetActive(true);
        Time.timeScale = 1;
    }
    public void closeOptionPanel()
    {
        panelOption.SetActive(false);
        startGame.SetActive(true);

    }
    public void closeModePanel() 
    {
        panelMode.SetActive(false);
        startGame.SetActive(true);
        if (GameManage.GMinstance.currentMode != GameManage.GMinstance.mode)
        {
            SceneManager.LoadScene(0);
        }
    }

}
