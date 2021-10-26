using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishPointBehav : MonoBehaviour
{
    [SerializeField] stageMode stageData;
    private void OnEnable()
    {
        stageData = GameObject.FindGameObjectWithTag("GameController").GetComponent<stageMode>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stageData.finishStage = true;
            SoundManager.SMInstance.Play("Victory");
            StartCoroutine("delaySetFalse");

        }
    }
    IEnumerator delaySetFalse() 
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
