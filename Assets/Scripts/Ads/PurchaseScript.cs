using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PurchaseScript : MonoBehaviour
{
    public Button removeButton;
    public GameObject purchaseNoAds;
    private void Start()
    {
        if (PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(true);
            removeButton.enabled = false;
        }
        else if (!PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(false);
            removeButton.enabled = true;
        }
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(true);
            removeButton.enabled = false;
        }
        else if (!PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(false);
            removeButton.enabled = true;
        }
    }
    void removeAds() // RemoveAds Button
    {
        if (PlayerPrefs.HasKey("ads") == false)
        {
            purchaseNoAds.SetActive(true);
            removeButton.enabled = false;

        }
    }
}
