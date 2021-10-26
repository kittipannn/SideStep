using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehav : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 3f;
    [SerializeField] float startSpeed = 50f;
    bool startGame = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        startGame = true;
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (GameManage.GMinstance.finishInStageMode == true)
        {
            this.gameObject.SetActive(false);
        }
 
    }
    private void FixedUpdate()
    {
        if (startGame)
        {
            rb.AddForce(Vector3.back * startSpeed, ForceMode.Impulse);
            StartCoroutine(delay());
        }
        else
        {
            rb.AddForce(Vector3.back * speed, ForceMode.Impulse);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterBehav>().whenPlayerDead();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            this.gameObject.SetActive(false);
        }

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        startGame = false;
    }

}
