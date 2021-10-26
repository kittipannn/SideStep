using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager2 : MonoBehaviour
{
    Shopdata shopData;
    public CoinManager coinManager;

    public int currentCharacterIndex;
    public GameObject[] characterSkin;
    public Button next,pervious;
    public Button selectSkinBtn;
    public Button purchaseBtn; // ปุ่มซื้อสกิน จะขึ้นต่อเมื่อ unlock แล้ว
    public GameObject lockedSkin; // จะขึ้นต่อเมื่อยังไม่ได้ unlock สกิน
    public GameObject noti;
    public GameObject playerSpawnPoint;
    GameObject player;
    public Text Coin;
    public Text price;
    void Start()
    {
        shopData = Shopdata.shopInstance;
        setButton();
        currentCharacterIndex = PlayerPrefs.GetInt("selectCharacter");
       
        characterSkin[currentCharacterIndex].SetActive(true);
    }
    private void OnEnable()
    {
        foreach (GameObject character in characterSkin)
        {
            character.SetActive(false);
        }
        currentCharacterIndex = PlayerPrefs.GetInt("selectCharacter");
        characterSkin[currentCharacterIndex].SetActive(true);
        lockedSkin.SetActive(false);
        Coin.text = coinManager.CoinPoint.ToString();
    }
    private void Update()
    {
        Coin.text = coinManager.CoinPoint.ToString();
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && shopData.shopCharacters[currentCharacterIndex].purchased && shopData.currentCharacterModel == currentCharacterIndex) // Unlock แล้ว ซื้อแล้ว
        {
            selectSkinBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(false);
        }
    }
    void setButton() 
    { 
        next.onClick.AddListener(() => { changeNext(); });
        pervious.onClick.AddListener(() => { changePervious(); });
        selectSkinBtn.onClick.AddListener(() => { selectSkin(); });
        purchaseBtn.onClick.AddListener(() => { purchaseSkin(); });
    }

    void changeNext() 
    {
        lockedSkin.SetActive(false);
        price.text = shopData.shopCharacters[currentCharacterIndex].unlockCost.ToString();
        characterSkin[currentCharacterIndex].SetActive(false);
        currentCharacterIndex++;
        if (currentCharacterIndex == characterSkin.Length)
            currentCharacterIndex = 0;
        characterSkin[currentCharacterIndex].SetActive(true);
        
        if (shopData.currentCharacterModel == currentCharacterIndex)
        {
            selectSkinBtn.gameObject.SetActive(false);
        }
        if (!shopData.shopCharacters[currentCharacterIndex].isUnlolcked) // ถ้ายังไม่ได้ unlock จะขึ้น lockedSkin
        {
            lockedSkin.SetActive(true);
            selectSkinBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(false);
        }
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && !shopData.shopCharacters[currentCharacterIndex].purchased) // Unlock แล้ว แต่ยังไม่ได้ซื้อ
        {
            selectSkinBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
        }
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && shopData.shopCharacters[currentCharacterIndex].purchased && shopData.currentCharacterModel != currentCharacterIndex) // Unlock แล้ว ซื้อแล้ว
        {
            selectSkinBtn.gameObject.SetActive(true);
            purchaseBtn.gameObject.SetActive(false);
        }

    }
    void changePervious()
    {
        lockedSkin.SetActive(false);
        price.text = shopData.shopCharacters[currentCharacterIndex].unlockCost.ToString();
        characterSkin[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;
        if (currentCharacterIndex == -1)
            currentCharacterIndex = characterSkin.Length-1 ;
        characterSkin[currentCharacterIndex].SetActive(true);
        if (shopData.currentCharacterModel == currentCharacterIndex)
        {
            selectSkinBtn.gameObject.SetActive(false);
        }
        if (!shopData.shopCharacters[currentCharacterIndex].isUnlolcked) // ถ้ายังไม่ได้ unlock จะขึ้น lockedSkin
        {
            lockedSkin.SetActive(true);
            selectSkinBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(false);
        }
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && !shopData.shopCharacters[currentCharacterIndex].purchased) // Unlock แล้ว แต่ยังไม่ได้ซื้อ
        {
            selectSkinBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
        }
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && shopData.shopCharacters[currentCharacterIndex].purchased && shopData.currentCharacterModel != currentCharacterIndex) // Unlock แล้ว ซื้อแล้ว
        {
            selectSkinBtn.gameObject.SetActive(true);
            purchaseBtn.gameObject.SetActive(false);
        }

    }
    
    void selectSkin() 
    {
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked && shopData.shopCharacters[currentCharacterIndex].purchased)// unlock == true ต้อง unlock แล้วถึงจะกดเลือกได้
        {
            PlayerPrefs.SetInt("selectCharacter", currentCharacterIndex);
            PlayerPrefs.Save();
            shopData.currentCharacterModel = currentCharacterIndex;
            player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player.transform.GetChild(3).gameObject);
            GameObject model = Instantiate(shopData.shopCharacters[currentCharacterIndex].character, playerSpawnPoint.transform.position, transform.rotation.normalized);
            model.transform.parent = player.transform;
            selectSkinBtn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("faill to select");
        }
    }
    
    void purchaseSkin()
    {

        int coin = PlayerPrefs.GetInt("Coin");
        if (shopData.shopCharacters[currentCharacterIndex].isUnlolcked)
        {
            if (coin >= shopData.shopCharacters[currentCharacterIndex].unlockCost)
            {
 
                lockedSkin.SetActive(false);
                coinManager.useCoin(shopData.shopCharacters[currentCharacterIndex].unlockCost);
                shopData.shopCharacters[currentCharacterIndex].purchased = true;
                string keyName = shopData.shopCharacters[currentCharacterIndex].character.name + "Purchased";
                PlayerPrefs.SetInt(keyName, shopData.shopCharacters[currentCharacterIndex].purchased ? 1 : 0);
                PlayerPrefs.Save();
                selectSkinBtn.gameObject.SetActive(true);
                purchaseBtn.gameObject.SetActive(false);
            }
            else
            {
  
                noti.SetActive(true);
                StartCoroutine("delayNotiToFalse");
            }
        }
    }
    IEnumerator delayNotiToFalse()
    {
        yield return new WaitForSeconds(1f);
        noti.SetActive(false);
    }
}
