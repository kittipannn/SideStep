using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gacha : MonoBehaviour
{
    Shopdata shopData;
    bool unlockAllSkin = false;
    List<int> indexCharacter = new List<int>();

    public GameObject SkinInGacha;
    GameObject player;

    public GameObject gachaLand;
    private void Start()
    {
        player = GameManage.GMinstance.playerList[0];
        shopData = Shopdata.shopInstance;
    }

    public void randomCharacter()
    {
        if (!unlockAllSkin)
        {

            for (int i = 0; i < shopData.shopCharacters.Length; i++)
            {
                if (!shopData.shopCharacters[i].isUnlolcked)
                {
                    indexCharacter.Add(i);
                }
            }
            if (indexCharacter.Count > 0)
            {
                int characterIndex = Random.Range(0, indexCharacter.Count);
                characterIndex = indexCharacter[characterIndex];
                shopData.shopCharacters[characterIndex].isUnlolcked = true;
                string keyName = shopData.shopCharacters[characterIndex].character.name + "Unlock";
                PlayerPrefs.SetInt(keyName, shopData.shopCharacters[characterIndex].isUnlolcked ? 1 : 0);
                PlayerPrefs.SetInt("GachaIndex", characterIndex);
                PlayerPrefs.Save();
            }
            else
            {
                unlockAllSkin = true;
            }
        }
    }


    public void showSkin() 
    {
        gachaLand.SetActive(true);
        int gachaIndex = PlayerPrefs.GetInt("GachaIndex");
        for (int i = 0; i < SkinInGacha.transform.childCount; i++)
        {
            if (i == gachaIndex)
            {
                SkinInGacha.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                SkinInGacha.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

}
