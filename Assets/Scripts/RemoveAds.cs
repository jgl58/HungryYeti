using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour
{
    public Button removeAdsButton;
    public Text buttonText;
    public void RemoveAdsOK (){
        //0 = no ads
        //1 = ads
        Debug.Log("You removed ads!");
        PlayerPrefs.SetInt("Ads", 0);
        removeAdsButton.interactable = false;
        buttonText.text = "Purchased";
    } 

    public void RemoveAdsFailure (){
        
        Debug.Log("A problem removing ads!");
    } 
}
