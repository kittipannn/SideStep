using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehav : MonoBehaviour
{
    Spawner spawner;
    GameObject finishLine;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        finishLine = this.transform.GetChild(2).gameObject;

    }
    private void OnEnable()
    {
        if (GameManage.GMinstance.checkSpawnFinishLine)
        {
            finishLine.SetActive(true);
            GameManage.GMinstance.checkSpawnFinishLine = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManage.GMinstance.countToSpawn++;
            spawner.spawnTile();
            this.gameObject.SetActive(false);
        }

    }
    
    //void onSpawnCoin() 
    //{
    //    int amountOfCoin = Random.Range(1, 4);
    //    for (int i = 0; i < amountOfCoin; i++)
    //    {
    //        Vector3 pos;
    //        float x;
    //        float y;
    //        float z;
    //        x = Random.Range(coll.bounds.min.x, coll.bounds.max.x);
    //        y = coll.bounds.max.y;
    //        z = Random.Range(coll.bounds.min.z, coll.bounds.max.z);
    //        pos = new Vector3(x, y, z);
    //        var ray = new Ray(pos, Vector3.down);
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //        {
    //            Vector3 pos2 = new Vector3(pos.x, hit.point.y + 1f, pos.z);
    //            Debug.Log(pos2);
    //            spawnCoin(pos2);
    //        }

    //    }
    //}

    //public void spawnCoin(Vector3 position)
    //{
    //    GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Coin");
    //    coin.SetActive(true);
    //    coin.transform.position = position;
    //}
    //void randomPos() 
    //{
    //    for (int i = 0; i < pos.Length; i++)
    //    {
    //        pos[i] = Random.Range(0, spawnPoint.Length);
    //    }
    //    for (int i = 0; i < pos.Length; i++)
    //    {
    //        for (int j = 0; j < pos.Length; j++)
    //        {
    //            if (i != j)
    //            {
    //                if (pos[i] == pos[j])
    //                {
    //                    pos[j] = Random.Range(0, spawnPoint.Length);
    //                }
    //            }
    //        }
    //    }
    //}
}
