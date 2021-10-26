using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBehav : MonoBehaviour
{
    string[] tagObs = {"Obs1_1", "Obs1_2", "Obs1_3", "Obs2_1", "Obs2_2", "Obs2_3" };
    GameObject obs, player;

    private void OnEnable()
    {
        player = GameManage.GMinstance.playerList[0];
        player.layer = 8;
    }
    private void OnDisable()
    {
        if (GameManage.GMinstance.playerList.Count != 0)
        {
            player.layer = 7;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < tagObs.Length; i++)
        {
            if (collision.gameObject.CompareTag(tagObs[i]))
            {
                Item.itemInstance.useShield = false;
                obs = collision.gameObject;
                SoundManager.SMInstance.Play("shieldHit");
                StartCoroutine("disableShield");
            }
        }
    }
    IEnumerator disableShield()
    {
        yield return new WaitForSeconds(0.5f);
        player.layer = 7;
        this.gameObject.SetActive(false);
        obs.SetActive(false);
        if (GameManage.GMinstance.mode == "stage")
        {
            PlayerPrefs.SetInt("shield", 0);
        }

    }
}
