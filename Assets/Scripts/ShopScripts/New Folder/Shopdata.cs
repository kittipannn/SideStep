using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopdata : MonoBehaviour
{
    public static Shopdata shopInstance;
    public shopCharacter[] shopCharacters;
    public int currentCharacterModel;
    private void Awake()
    {
        if (shopInstance == null)
            shopInstance = this;
        currentCharacterModel = PlayerPrefs.GetInt("selectCharacter");
        for (int i = 1; i < shopCharacters.Length; i++)
        {
            string keyNameUnlock = shopCharacters[i].character.name + "Unlock";
            string keyNamePurchased = shopCharacters[i].character.name + "Purchased";
            shopCharacters[i].isUnlolcked = PlayerPrefs.GetInt(keyNameUnlock) == 1 ? true : false;
            shopCharacters[i].purchased = PlayerPrefs.GetInt(keyNamePurchased) == 1 ? true : false;
        }
    }
    
    
}
[System.Serializable]
public class shopCharacter
{
    public GameObject character;
    public int unlockCost;
    public bool isUnlolcked;
    public bool purchased;

}
