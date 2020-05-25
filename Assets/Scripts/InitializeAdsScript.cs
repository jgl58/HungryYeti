using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour { 

    private string gameId = "3616358";
    public static string placementId = "GameOver";
    public static string placementRewardedId = "rewardedVideo";
    bool testMode = true;

    void Start () {
        #if UNITY_IOS
        gameId = "3616359";        
        #endif
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