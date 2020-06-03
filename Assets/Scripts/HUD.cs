using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HUD : MonoBehaviour
{
    //COSAS DE MORIR
    public const float maxTime = 11.0f; //MENOS 1, ES DECIR, 6.0f -> 5 segundos;
    private float auxMaxTime = maxTime;
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
    private string auxText = "";

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
            DecreaseMaxTime();
        }
    }

    

    public void DecreaseMaxTime(){
        int puntuacion = int.Parse(puntuacionLabel.text);
        if(auxMaxTime > 2){
            auxMaxTime = maxTime - Math.Min((int)Math.Truncate((float)(puntuacion/2000)), maxTime - 3);
        }
    }

    public void UpdateDie(){
        if (player.transform.position != lastCheckPos){
            lastCheckTime = auxMaxTime;
        }
        if (lastCheckTime < 1)
        {
            tiempoLabelDown.text = "0";
            lastCheckTime = auxMaxTime;
            lastCheckPos = player.transform.position;
            auxText = "";
            Juego.die(); 
        } else {

            lastCheckTime -= Time.deltaTime;

            tiempoLabelDown.text = ((int)lastCheckTime).ToString("0");
            
            lastCheckPos = player.transform.position;

            if (lastCheckTime < 6 && auxText != tiempoLabelDown.text)
            {
                auxText = tiempoLabelDown.text;
                tiempoLabelDown.gameObject.LeanScale(new Vector3(2,2,1), 0.3f).setOnComplete(() => {
                    tiempoLabelDown.gameObject.LeanScale(new Vector3(1,1,1), 0.3f);
                });
            }
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
        tiempoLabelDown.text = "10";
        lastCheckTime = maxTime;
        auxText = "";
    }

}