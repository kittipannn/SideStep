using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPShop : MonoBehaviour
{
    private string removeAds = "com.varisoft.sidestep.removeads";
    public GameObject restorePurchaseBtn;
    private void Awake()
    {
        disableRestorePurchaseBtn();
    }
    public void OnPurchaseComplete(Product product) 
    {
        if (product.definition.id == removeAds)
        {
            Debug.Log("Purchase Complete");
            AdMobScript.AdMobInstance.removeAds();
            PlayerPrefs.SetInt("ads", 0);
        }
    }
    public void OnPurchaseFailed(Product product , PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }
    private void disableRestorePurchaseBtn()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restorePurchaseBtn.SetActive(false);
        }
    }
}
