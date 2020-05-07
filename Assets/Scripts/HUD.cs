using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Text tiempoLabel;
    public Text puntuacionLabel;

     private float secondsCount;
     private int minuteCount;
     private int hourCount;
 
    private float time;

    void Start()
    {
        //puntuacionLabel.text = string.Format ("{0:0000}", 1);
    }
 
    void Update() {
        // TIME STUFF
        if(Globals.estado == Globals.gameState.jugando){
            UpdateTimerUI();
        }
    }

     public void UpdateTimerUI(){
         secondsCount += Time.deltaTime;
         tiempoLabel.text = minuteCount.ToString("00") + ":" + ((int)secondsCount).ToString("00");
         if(secondsCount >= 60){
             minuteCount++;
             secondsCount = 0;
         }else if(minuteCount >= 60){
             hourCount++;
             minuteCount = 0;
         }   
  
     }

}