using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    RaycastHit hit;
    public Collider coll;

    // Update is called once per frame
    private void OnEnable()
    {
        onSpawnCoin();
    }
    void onSpawnCoin()
    {
 
        int amountOfCoin = Random.Range(1, 5);
        for (int i = 0; i < amountOfCoin; i++)
        {
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
                if (hit.collider.gameObject.CompareTag("Plat"))
                {
                    Vector3 pos2 = new Vector3(pos.x, hit.point.y + 1f, pos.z);
                    spawnCoin(pos2);
                }
                else
                {

                    x = Random.Range(coll.bounds.min.x, coll.bounds.max.x);
                    y = coll.bounds.max.y;
                    z = Random.Range(coll.bounds.min.z, coll.bounds.max.z);
                    pos = new Vector3(x, y, z);
                    ray = new Ray(pos, Vector3.down);
                    Vector3 pos2 = new Vector3(pos.x, hit.point.y + 1f, pos.z);
                    spawnCoin(pos2);
                }
            }

        }
    }

    public void spawnCoin(Vector3 position)
    {
        GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Coin");
        coin.transform.position = position;
        coin.SetActive(true);
    }

}
