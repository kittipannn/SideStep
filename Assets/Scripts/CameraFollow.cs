using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 lookOffset = new Vector3(0, 1, 0);
    public float distance = 10;
    public float height = 5;
    public Camera mainCamera;
    public float rotSpeed = 10;
    GameObject player;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameManage.GMinstance.playerList.Count != 0 )
        {
            player = GameManage.GMinstance.playerList[0];
            
        }
    }
    
    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 lookPosition = player.transform.position + lookOffset;
            //Vector3 relativePos = lookPosition - mainCamera.transform.position;
            //Quaternion rot = Quaternion.LookRotation(relativePos);
            //mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);
            //Vector3 targetPos = player.transform.position + player.transform.up * height - player.transform.forward * distance;

            Vector3 targetPos = new Vector3 (player.transform.position.x,player.transform.position.y + height, player.transform.position.z - distance);
            mainCamera.transform.position = targetPos;
        }
    }
}
