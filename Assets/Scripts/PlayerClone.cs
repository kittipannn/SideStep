using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.gameObject.CompareTag("PlayerClone") )
        {
            
            collision.gameObject.layer = 7;
            collision.gameObject.GetComponent<CharacterControl>().enabled = true;
            collision.gameObject.GetComponent<CharacterBehav>().enabled = true;
            collision.gameObject.GetComponent<PlayerClone>().enabled = true;
            collision.gameObject.transform.Find("CheckPlayerClone").GetComponent<BoxCollider>().enabled = true;
        }
    }
}
