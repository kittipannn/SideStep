using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentCharacterIndex;
    public ShopScriptScriptable shopData;
    public Mesh[] mesh;
    public CoinManager coinManager;
    [SerializeField] private Button sniperButton, assasinButton, banditButton, knightButton, vikingButton, supergirlButton, businessButton, mageButton, samuraiButton;
    [SerializeField] private Button[] unlockButton;
    public ShowCharacterInShop ShowCharacter;
    public Text Coin;
    public GameObject cantBuy;
    void Start()
    {
        cantBuy.SetActive(false);
        sniperButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[0].mesh, 0); });
        assasinButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[1].mesh, 1); });
        banditButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[2].mesh, 2); });
        knightButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[3].mesh, 3); });
        vikingButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[4].mesh, 4); });
        supergirlButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[5].mesh, 5); });
        businessButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[6].mesh, 6); });
        mageButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[7].mesh, 7); });
        samuraiButton.onClick.AddListener(() => { SelectSkin(shopData.shopItems[8].mesh, 8); });

        //for (int i = 0; i < unlockButton.Length; i++)
        //{
        //    unlockButton[i].onClick.AddListener(() => { unlockCharacter(i); });
        //    Debug.Log(unlockButton[i].name);
        //}
        unlockButton[0].onClick.AddListener(() => { unlockCharacter(0); });
        unlockButton[1].onClick.AddListener(() => { unlockCharacter(1); });
        unlockButton[2].onClick.AddListener(() => { unlockCharacter(2); });
        unlockButton[3].onClick.AddListener(() => { unlockCharacter(3); });
        unlockButton[4].onClick.AddListener(() => { unlockCharacter(4); });
        unlockButton[4].onClick.AddListener(() => { unlockCharacter(5); });
        unlockButton[4].onClick.AddListener(() => { unlockCharacter(5); });
        unlockButton[4].onClick.AddListener(() => { unlockCharacter(7); });

        //CharacterBehav.instance.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = shopData.shopItems[shopData.currentCharacterModel].mesh;
        //ShowCharacter.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = shopData.shopItems[shopData.currentCharacterModel].mesh;
    }
    public void SelectSkin(Mesh mesh,int index)
    {
        CharacterBehav.instance.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = mesh;
        ShowCharacterInShop.instance.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = mesh;
        shopData.currentCharacterModel = index;
    }

    private void Update()
    {
        updateUI();
        Coin.text = coinManager.CoinPoint.ToString();
    }
    void updateUI() 
    {
        for (int i = 1; i <= unlockButton.Length; i++)
        {
            if (shopData.shopItems[i].isUnlolcked)
            {
                unlockButton[i-1].gameObject.SetActive(false);
            }
            else
            {
                unlockButton[i-1].gameObject.SetActive(true);
            }
        }
    }
    void unlockCharacter(int index) 
    {
        int coin = PlayerPrefs.GetInt("Coin");
        Debug.Log(index);
        Debug.Log(shopData.shopItems[index + 1].isUnlolcked);
        if (!shopData.shopItems[index+1].isUnlolcked)
        {
            if (coin >= shopData.shopItems[index+1].unlockCost)
            {
                coinManager.useCoin(shopData.shopItems[index+1].unlockCost);
                shopData.shopItems[index+1].isUnlolcked = true;
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
                cantBuy.SetActive(true);
                StartCoroutine("delayCantBuyToFalse");
            }
        }
    }
    IEnumerator delayCantBuyToFalse() 
    {
        yield return new WaitForSeconds(1f);
        cantBuy.SetActive(false);
    }

}

