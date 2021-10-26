using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameDataScriptable gameData;
    Shopdata shopData;
    public GameObject spawnPoint;
    Vector3 spawn;
    bool start = true;
    GameObject ObstacleSpawn;


    public GameObject[] Obstacletype1Prefab;
    public GameObject[] Obstacletype2Prefab;
    public GameObject[] Obstacletype3Prefab;
    public string[] tagToSpawn;
    public GameObject coinPrefab;
    public GameObject[] ObstacleSpawnPoint;
    public GameObject platformStart;
    public CoinManager coinManager;
    [SerializeField] int levelSpawn = 1;
    private float elapsedTime = 0.0f;
    //public bool StartGame = false;
    [SerializeField] float spawnRate;

    public Score score;
    int checkScore = 0;
    int num;
    [SerializeField] int distanceSpawnPlayerclone = 5;

    //item
    public string[] tagItem = { "booster", "shield", "magnet" };
    int countToSpawnitem;
    [SerializeField] int distanceSpawnItem = 7;

    string[] platform = { "Platform1", "Platform2", "Platform3" };
    string platformTag = "Platform1";
    private void Awake()
    {
        ObstacleSpawn = GameObject.FindGameObjectWithTag("ObstaclePoint");

    }
    private void Start()
    {
        shopData = Shopdata.shopInstance;
        platformStart.SetActive(true);
        spawn = spawnPoint.transform.position;
        stageSpawnPlatformLevel();
        spawnTile();
        

        //InvokeRepeating("spawnObstacle", 0, 1.5f);
    }
    private void Update()
    {
        checkSpawnLevel();
        //setSpawnRate();
        if (GameManage.GMinstance.gameStart)
        {
            if (GameManage.GMinstance.mode == "endless")
            {
                OnSpawnObstacle(GameManage.GMinstance.spawnRate);
            }
            else if (GameManage.GMinstance.mode == "stage" )
            {
                if (!GameManage.GMinstance.finishInStageMode)
                {
                    OnSpawnObstacle(GameManage.GMinstance.spawnRate);
                }
            }
        }
    }

    //void setSpawnRate() 
    //{
    //    if (coinManager.CurrentPoint >= 0 && coinManager.CurrentPoint < 30)
    //    {
    //        spawnRate = gameData.spawnRateLevel;
    //    }
    //    else if (coinManager.CurrentPoint >=30 && coinManager.CurrentPoint <55)
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.1f;
    //    }
    //    else if (coinManager.CurrentPoint >= 55 && coinManager.CurrentPoint < 80)
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.2f;
    //    }
    //    else if (coinManager.CurrentPoint >= 80 && coinManager.CurrentPoint < 105)
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.3f;
    //    }
    //    else if (coinManager.CurrentPoint >= 105 && coinManager.CurrentPoint < 130)
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.4f;
    //    }
    //    else if (coinManager.CurrentPoint >= 130 && coinManager.CurrentPoint < 155)
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.5f;
    //    }
    //    else
    //    {
    //        spawnRate = gameData.spawnRateLevel - 0.4f;
    //    }
    //}

    public void OnSpawnObstacle(float timeToSpawn)
    {
        elapsedTime += Time.deltaTime ;
        if (elapsedTime > timeToSpawn)
        {
            elapsedTime = 0;
            objectpoolerObstacle();
        }

    }
    void stageSpawnPlatformLevel() 
    {
        if (GameManage.GMinstance.mode == "stage")
        {
            
            int stagelevel = PlayerPrefs.GetInt("StageLevel") + 1; //stageã¹ PlayerPref àÃÔèÁ·Õè 0 áµèã¹ scriptàÃÔèÁ·Õè 1 àÅÂµéÍ§ºÇ¡à¢éÒä»
            if (stagelevel >= gameData.stageLevel[0].StageLevel && stagelevel < gameData.stageLevel[5].StageLevel)
            {
                platformTag = platform[0];
                levelSpawn = 1;
            }
            else if (stagelevel >= gameData.stageLevel[5].StageLevel && stagelevel < gameData.stageLevel[11].StageLevel)
            {
                platformTag = platform[1];
                levelSpawn = 1;
            }
            else
            {
                platformTag = platform[2];
                levelSpawn = 1;
            }
        }
    }
    public void spawnTile() 
    {
        GameObject platform = ObjectPooler.SharedInstance.GetPooledObject(platformTag);
        if (platform != null)
        {
            if (GameManage.GMinstance.mode == "endless") //endless
            {
                if (start)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        num++;
                        countToSpawnitem++;
                        GameObject platformstart = ObjectPooler.SharedInstance.GetPooledObject(platformTag);
                        platformstart.transform.position = spawn;
                        spawn = platformstart.transform.GetChild(1).transform.position;
                        ObstacleSpawn.transform.position = new Vector3(spawn.x, spawn.y + 7, spawn.z);
                        platformstart.SetActive(true);
                        if (num == distanceSpawnPlayerclone)
                        {
                            spawnPlayerClone(randomPos(platformstart));
                            num = 0;
                        }
                        if (countToSpawnitem == distanceSpawnItem)
                        {
                            spawnItem(randomPos(platformstart));
                            countToSpawnitem = 0;
                        }
                    }
                    start = false;
                }
                else
                {
                    num++;
                    countToSpawnitem++;
                    platform.transform.position = spawn;
                    spawn = platform.transform.GetChild(1).transform.position;
                    ObstacleSpawn.transform.position = new Vector3(spawn.x, spawn.y + 7, spawn.z);
                    platform.SetActive(true);
                    if (num == distanceSpawnPlayerclone)
                    {
                        spawnPlayerClone(randomPos(platform));
                        num = 0;
                    }
                    if (countToSpawnitem == distanceSpawnItem)
                    {
                        spawnItem(randomPos(platform));
                        countToSpawnitem = 0;
                    }
                }
            }
            else if (GameManage.GMinstance.mode == "stage") // stage
            {
                if (start)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        num++;
                        GameObject platformstart = ObjectPooler.SharedInstance.GetPooledObject(platformTag);
                        platformstart.transform.position = spawn;
                        spawn = platformstart.transform.GetChild(1).transform.position;
                        ObstacleSpawn.transform.position = new Vector3(spawn.x, spawn.y + 7, spawn.z);
                        platformstart.SetActive(true);
                        if (num == distanceSpawnPlayerclone)
                        {
                            spawnPlayerClone(randomPos(platformstart));
                            num = 0;
                        }
                    }
                    start = false;
                }
                else
                {
                    num++;
                    platform.transform.position = spawn;
                    spawn = platform.transform.GetChild(1).transform.position;
                    ObstacleSpawn.transform.position = new Vector3(spawn.x, spawn.y + 7, spawn.z);
                    platform.SetActive(true);
                    if (num == distanceSpawnPlayerclone)
                    {
                        spawnPlayerClone(randomPos(platform));
                        num = 0;
                    }
                }
            }
        }
        //GameObject temp = Instantiate(platformTile, spawn, spawnPoint.transform.rotation);
        //spawn = temp.transform.GetChild(1).transform.position;
    }
    Vector3 randomPos(GameObject gameObj) 
    {
        RaycastHit hit;
        Vector3 pos2;
        Collider coll = gameObj.transform.Find("SpanwerCoin").GetComponent<BoxCollider>();
        Vector3 pos;
        float x;
        float y;
        float z;
        x = Random.Range(coll.bounds.min.x, coll.bounds.max.x);
        y = coll.bounds.max.y;
        z = Random.Range(coll.bounds.min.z, coll.bounds.max.z);
        pos = new Vector3(x, y, z);
        var ray = new Ray(pos, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            pos2 = new Vector3(pos.x, hit.point.y, pos.z);
        }
        else
        {
            pos2 = new Vector3(pos.x, hit.point.y, pos.z);
        }
        return pos2;

    }

    //public void spawnObstacle() 
    //{



    //    //choose point to Spawn the Obstacle
    //    int obstacleSpawnIndex = Random.Range(0, 4); // 4 point
    //    Transform spawnPoint = ObstacleSpawnPoint[obstacleSpawnIndex].transform;


    //    //spawn the obstacle
    //    if (spawnLevel1)//stage1
    //    {
    //        int obstale1Index = Random.Range(0, 4); // 4 obj
    //        Instantiate(Obstacletype1Prefab[obstale1Index], spawnPoint.position, Random.rotation, transform);
    //    }
    //    else if (spawnLevel2)//stage2
    //    {
    //        int obstale2Index = Random.Range(0, 4); //  4 obj
    //        Instantiate(Obstacletype2Prefab[obstale2Index], spawnPoint.position, Random.rotation, transform);
    //    }
    //    else if (spawnLevel3)//stage3
    //    {
    //        int randomPrefabIndex = Random.Range(0, 2);

    //        if (randomPrefabIndex == 0)
    //        {
    //            int obstale3Index = Random.Range(0, 4); //  obj
    //            Instantiate(Obstacletype1Prefab[obstale3Index], spawnPoint.position, Random.rotation, transform);
    //        }
    //        else
    //        {
    //            int obstale3Index = Random.Range(0, 4); //  obj
    //            Instantiate(Obstacletype2Prefab[obstale3Index], spawnPoint.position, Random.rotation, transform);
    //        }
    //    }
    //    //else if (true)//stage4
    //    //{
    //    //    int obstale4Index = Random.Range(0, 0); //  obj
    //    //    Instantiate(Obstacletype2Prefab[obstale4Index], spawnPoint.position, Random.rotation, transform);
    //    //}
    //}
    
    void objectpoolerObstacle() 
    {
        //Object Pooler
        int obstacleSpawnIndex = Random.Range(0, 4); // 4 point
        Transform spawnPoint = ObstacleSpawnPoint[obstacleSpawnIndex].transform;
        int tagIndex;
        if (levelSpawn == 1)
        {
            tagIndex = Random.Range(0, 4);
            
        }
        else if (levelSpawn == 2)
        {
            tagIndex = Random.Range(4, 8);
        }
        else
        {
            tagIndex = Random.Range(0, 8);
        }
        string tagSpawn = tagToSpawn[tagIndex];
        GameObject obstacleType1 = ObjectPooler.SharedInstance.GetPooledObject(tagSpawn);
        if (obstacleType1 != null)
        {
            obstacleType1.SetActive(true);
            obstacleType1.transform.position = spawnPoint.position;

        }
    }
    void checkSpawnLevel() 
    {
        if (GameManage.GMinstance.mode == "endless")
        {
            randomPlat();
            if (score.score > 0 && score.score < 30)
            {
                //platformTag = platform[0];
                levelSpawn = 1;
            }
            else if (score.score >= 30 && score.score < 60)
            {
                //platformTag = platform[1];
                levelSpawn = 2;
            }
            else if (score.score >= 90)
            {
                //platformTag = platform[2];
                levelSpawn = 3;
            }
        }
       
    }

    void randomPlat() 
    {
        platformTag = platform[0];
        if (score.score >= checkScore)
        {
            int tagIndex = Random.Range(0, platform.Length);
            platformTag = platform[tagIndex];
            checkScore += 30;
        }
    }
    public void spawnPlayerClone(Vector3 position)
    {
        GameObject playerClone = ObjectPooler.SharedInstance.GetPooledObject("PlayerClone");
        playerClone.transform.position = position;
        //int characterSKinIndex = Random.Range(0, 20);
        //if (shopData.shopCharacters[characterSKinIndex].isUnlolcked)
        //{
        //    playerClone.transform.GetChild(characterSKinIndex).gameObject.SetActive(true);
        //}
        //else
        //{
        //    playerClone.transform.GetChild(0).gameObject.SetActive(true);
        //}
        playerClone.transform.GetChild(3).gameObject.SetActive(true);
        playerClone.SetActive(true);
    }
    void spawnItem(Vector3 position) 
    {
        position.y = position.y + 1;
        int itemSpawnIndex = Random.Range(0, tagItem.Length);
        string tagItemSpawn = tagItem[itemSpawnIndex];
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(tagItemSpawn);
        item.transform.position = position;
        item.SetActive(true);
    }
}
