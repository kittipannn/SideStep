using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable GameData")]
public class GameDataScriptable : ScriptableObject
{
    public float spawnRateLevel = 1f;
    public int countInterstitialAd = 0;
    public stageLevel[] stageLevel;
    public bool finishStageMode = false;
}

[System.Serializable]
public class stageLevel
{
    public int StageLevel;
    public int checkpointDistance;
    public int platformValue;
    public bool pass;
    

}
