using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PlayAgainWithAd : MonoBehaviour, IUnityAdsListener
{
    public Button yourButton;
    private string myPlacementId;
	void Start () {
        myPlacementId = InitializeAdsScript.placementRewardedId;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(ShowRewardedVideo);
        Advertisement.AddListener (this);
	}

	void ShowRewardedVideo(){
        Advertisement.Show (myPlacementId);
	}

    public void OnUnityAdsReady (string placementId) {
        if (placementId == myPlacementId) {        
            yourButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        gameObject.SetActive(false);
        if (showResult == ShowResult.Finished) {
            print("todo guay");
            Juego.start(false);
        } else if (showResult == ShowResult.Skipped) {
            //nada
            gameObject.SetActive(false);
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error");
        }
    }

    public void OnUnityAdsDidError (string message) {
        gameObject.SetActive(false);
        Debug.LogWarning ("The ad did not finish due to an error");
    }

    public void OnUnityAdsDidStart (string placementId) {
        gameObject.SetActive(false);
    } 

}
