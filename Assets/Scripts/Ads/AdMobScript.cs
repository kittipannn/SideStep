using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class AdMobScript : MonoBehaviour
{

//#if UNITY_ANDROID
//    string App_ID = "ca-app-pub-7318907042461228~4996902854";
//    string Banner_Ad_ID = "ca-app-pub-7318907042461228/1994037824";
//    string Interstitial_Ad_ID = "ca-app-pub-7318907042461228/7617065449";
//    string Video_Ad_ID = "ca-app-pub-7318907042461228/8914669397";
//#elif UNITY_IPHONE
//    string App_ID = "";
//    string Banner_Ad_ID = "";
//    string Interstitial_Ad_ID = "";
//    string Video_Ad_ID = "";
//#else
//            string adUnitId = "unexpected_platform";
//#endif
    string Banner_Ad_ID = "ca-app-pub-7318907042461228/1994037824";
    string Interstitial_Ad_ID = "ca-app-pub-7318907042461228/7617065449";
    string Video_Ad_ID = "ca-app-pub-7318907042461228/8914669397";



    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd gameOverRewardedAds;
    public static AdMobScript AdMobInstance;
    private void Awake()
    {
        if (AdMobInstance == null)
        {
            AdMobInstance = this;
        }
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestInterstitial();
        showBannerAds();

        gameOverRewardedAds = CreateAndLoadRewardedAds(Video_Ad_ID);

    }
   

    private void RequestBanner()
    {



        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;


       
    }
    public void showBannerAds() // run Banner
    {
        if (PlayerPrefs.HasKey("ads") == false)
        {
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerView.LoadAd(request);
        }
    }
    public void RequestInterstitial()
    {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);


        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void showInterstitialAds() // run Interstitial
    {
        if (this.interstitial.IsLoaded())
        {
            if (PlayerPrefs.HasKey("ads") == false)
            {
                this.interstitial.Show();
            }
        }
    }

    public RewardedAd CreateAndLoadRewardedAds(string adUnituId) 
    {

        RewardedAd rewardedAd = new RewardedAd(adUnituId);


        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }


    public void UserChoseToWatchGameOverRewardAds() // run GameOver Reward Ads
    {
        if (gameOverRewardedAds.IsLoaded())
        {
            gameOverRewardedAds.Show();
        }
    }
    //public void UserChoseToWatchShoprRewardAds()
    //{
    //    if (shopRewardedAds.IsLoaded())
    //    {
    //        shopRewardedAds.Show();
    //    }

    //}



    //Banner and Interstitial Ads
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ads Loaded");
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Ads Failed to Load");
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpened event received");
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClosed event received");

    }
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeavingApplication event received");
    }

    //RewardAds
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("RewardedAdOpening");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("RewardedAdClosed");  
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Revive");
        GameManage.GMinstance.rewardedWhenUserWatch();
    }

    public void removeAds() 
    {
        bannerView.Destroy();
        interstitial.Destroy();
    }
}
