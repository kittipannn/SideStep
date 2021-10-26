using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int coinPoint = 0;
    private int currentCoin = 0;
    public int CoinPoint { get; set; }
    public int CurrentPoint { get; set; }

    private void Awake()
    {
        coinPoint = PlayerPrefs.GetInt("Coin",10000);
        CoinPoint = coinPoint;
        
    }
    private void Update()
    {
        CoinPoint = coinPoint;
    }
    public void addCoin (int multiply) 
    {
        currentCoin += 2 * multiply;
        CurrentPoint = currentCoin;
    }
    
    public void useCoin(int value)
    {
        if (coinPoint >= value)
        {
            coinPoint -= value;
            PlayerPrefs.SetInt("Coin", coinPoint);
        }
    }

    public void addCoinPoint() 
    {
        coinPoint += currentCoin;
        CoinPoint = coinPoint;

    }
    
}
