using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTesst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, 0).normalized;
        this.transform.position += direction * 5 * Time.deltaTime;
    }
}
