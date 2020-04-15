using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Text tiempoLabel;
    public Text puntuacionLabel;
 
    private float time;

    void Start()
    {
        //puntuacionLabel.text = string.Format ("{0:0000}", 1);
    }
 
    void Update() {
        // TIME STUFF
        time += Time.deltaTime;
        var minutes = time / 60;
        var seconds = time % 60;
        var fraction = (time * 100) % 100;
        tiempoLabel.text = string.Format ("{0:00}:{1:00}", minutes, seconds);

        //Globals.estado = Globals.gameState.menu;
        //print(Globals.estado);
    }

}