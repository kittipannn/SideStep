using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName ="ShopData",menuName = "Scriptable ShopData")]
    public class ShopScriptScriptable : ScriptableObject
    {
        public ShopItem[] shopItems;
        public shopItem2[] shop2;
        public int currentCharacterModel;
    }

    [System.Serializable]
    public class ShopItem 
    {
        public string modelName;
        public bool isUnlolcked;
        public Mesh mesh;
        public int unlockCost;
        
    }
    [System.Serializable]
    public class shopItem2
    {
        public GameObject character;
        public int unlockCost;
        public bool isUnlolcked;
        public bool purchased;
}