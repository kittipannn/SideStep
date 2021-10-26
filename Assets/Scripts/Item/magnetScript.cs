using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetScript : MonoBehaviour
{
    [SerializeField] float speedRot = 10;
    GameObject player;
    void Start()
    {
        player = GameManage.GMinstance.playerList[0];
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayer();
    }
    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, speedRot, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Item.itemInstance.OnMagnet();
            this.gameObject.SetActive(false);
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
}
