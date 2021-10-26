using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehav : MonoBehaviour
{
 
    GameObject destroyPlane;
    int characterCollect = 1;
    CoinManager CoinManager;
    public ParticleSystem coinParticle;
    public bool followPlayer = false;

    public static CharacterBehav instance;

    private void Awake()
    {
        instance = this;
        destroyPlane = GameObject.FindGameObjectWithTag("DestroyZone");
        CoinManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoinManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer && Vector3.Distance(this.transform.position, destroyPlane.transform.position) >= 10f)
            destroyPlaneFollowPlayer();
        characterCollect = GameManage.GMinstance.playerList.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyZone") || Vector3.Distance(this.transform.position, destroyPlane.transform.position) >= 10f)
        {
            followPlayer = true;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            CoinManager.addCoin(characterCollect);
            coinParticle.Play();
           
            other.gameObject.SetActive(false);
            SoundManager.SMInstance.Play("CollectCoin");
        }
        if (other.gameObject.CompareTag("DestroyZone") && this.gameObject.CompareTag("PlayerClone"))
        {
            this.gameObject.SetActive(false);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Obs1_1") || collision.gameObject.CompareTag("Obstacle2"))
        //{
        //    whenPlayerDead()
        //}
        //if (collision.gameObject.CompareTag("PlayerClone"))
        //{
        //    collision.gameObject.layer = 7;
        //    collision.gameObject.GetComponent<CharacterControl>().enabled = true;
        //    collision.gameObject.GetComponent<CharacterBehav>().enabled = true;
        //}
    }
    public void whenPlayerDead() 
    {
        SoundManager.SMInstance.Play("PlayerHit");
        this.gameObject.layer = 8;
        this.gameObject.tag = "PlayerClone";
        this.gameObject.GetComponent<CharacterControl>().enabled = false;
        this.gameObject.GetComponent<CharacterBehav>().enabled = false;
        this.gameObject.SetActive(false);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("PlayerClone"))
        {
            SoundManager.SMInstance.Play("CollectPlayerClone");
            hit.gameObject.layer = 7;
            hit.gameObject.tag = "Player";
            if (hit.gameObject.GetComponent<CharacterControl>().forwardSpeed != this.gameObject.GetComponent<CharacterControl>().forwardSpeed)
            {
                hit.gameObject.GetComponent<CharacterControl>().forwardSpeed = this.gameObject.GetComponent<CharacterControl>().forwardSpeed;
            }
            hit.gameObject.GetComponent<CharacterControl>().enabled = true;
            hit.gameObject.GetComponent<CharacterBehav>().enabled = true;
        }
    }

    void destroyPlaneFollowPlayer()
    {
        destroyPlane.transform.position = new Vector3(destroyPlane.transform.position.x, GameManage.GMinstance.playerList[0].transform.position.y, GameManage.GMinstance.playerList[0].transform.position.z - 10);
    }

}
