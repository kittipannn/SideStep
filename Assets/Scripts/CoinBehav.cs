using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinBehav : MonoBehaviour
{
    [SerializeField] float speedRot = 10;
    [SerializeField] float speedMove = 50;
    GameObject player;
    bool onMagnet = false;
 
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, speedRot, 0));
    }
    private void OnDisable()
    {
        onMagnet = false;
    }
    private void Update()
    {
        if (GameManage.GMinstance.playerList.Count != 0)
        {
            player = GameManage.GMinstance.playerList[0];
        }
        checkPlayer();
        if (onMagnet)
        {
            activeMagnet();
        }
    }
    void checkPlayer() 
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (player != null)
            {
                if (this.transform.position.z < player.transform.position.z - 5)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                player = GameManage.GMinstance.playerList[0];
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin Detector"))
        {
            onMagnet = true;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            onMagnet = false;
        }
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin Detector"))
        {
            onMagnet = false;
        }
    }
    void activeMagnet() 
    {
        if (GameManage.GMinstance.gameStart)
        {
            Debug.Log("Magnet");
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedMove * Time.deltaTime);
        }
    }
}
