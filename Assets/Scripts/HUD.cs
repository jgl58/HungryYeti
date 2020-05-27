using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    //COSAS DE MORIR
    public const float maxTime = 6.0f; //MENOS 1, ES DECIR, 6.0f -> 5 segundos;
    public static float lastCheckTime = maxTime;
    public static Vector3 lastCheckPos;
    public GameObject player;

    //OTRAS COSAS
    public Text tiempoLabelDown;
    public Text tiempoLabel;
    public Text puntuacionLabel;

    private float secondsCount;
    private int minuteCount;
    private int hourCount;
 
    private float time;

    void Start()
    {
        //puntuacionLabel.text = string.Format ("{0:0000}", 1);
        lastCheckPos = player.transform.position;
    }
 
    void Update() {
        // TIME STUFF
        if(Juego.estado == Juego.gameState.jugando){
            UpdateTimerUI();
            UpdateDie();
        }
    }

    public void UpdateDie(){
        if (player.transform.position != lastCheckPos){
            lastCheckTime = maxTime;
        }
        if (lastCheckTime < 1)
        {
            tiempoLabelDown.text = "0";
            lastCheckTime = maxTime;
            lastCheckPos = player.transform.position;
            Juego.die(); 
        } else {
            lastCheckTime -= Time.deltaTime;
            tiempoLabelDown.text = ((int)lastCheckTime).ToString("0");
            lastCheckPos = player.transform.position;
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

    public void reset() {
        tiempoLabel.text = "00:00";
        puntuacionLabel.text = "0000";
        secondsCount = 0.0f;
        minuteCount = 0;
        hourCount = 0;
        tiempoLabelDown.text = "5";
        lastCheckTime = maxTime;
    }

}