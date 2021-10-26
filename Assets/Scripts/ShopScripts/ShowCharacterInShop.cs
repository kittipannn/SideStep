using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterInShop : MonoBehaviour
{
    public static ShowCharacterInShop instance;
    [SerializeField]
    float rotationSpeed = 50;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
