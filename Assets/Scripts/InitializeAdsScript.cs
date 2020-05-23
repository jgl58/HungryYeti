using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour { 

    #if UNITY_IOS
    private string gameId = "3616359";
    #elif UNITY_ANDROID
    private string gameId = "3616358";
    #endif

    public static string placementId = "GameOver";
    public static string placementRewardedId = "rewardedVideo";
    bool testMode = true;

    void Start () {
        Advertisement.Initialize(gameId, testMode);
    }

    public static bool hasAds(){
        if (PlayerPrefs.HasKey("Ads"))
        {
            return PlayerPrefs.GetInt("Ads") == 1;
        }
        return true;
    }
}