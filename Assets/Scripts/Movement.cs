using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour
{
    public Text puntuacionLabel;
    // Start is called before the first frame update
    private int refreshCounter = 5;

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.z--;
            this.transform.position = position;
            refreshCounter--;
            if (refreshCounter == 0)
            {
                refreshCounter = 5;
                print("Cargamos nuevos bloques");


                BloquesFactory.generateSuelo(5);
            }
            int puntuacion = Convert.ToInt32(puntuacionLabel.text) + 1; 
            puntuacionLabel.text = string.Format ("{0:0000}", puntuacion);
            
        }

        if(Input.touchCount > 0){
            touch = Input.GetTouch(0);
            
            switch(touch.phase){
                case TouchPhase.Began:
                    beginTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    if(beginTouchPosition == endTouchPosition){
                        Vector3 position = this.transform.position;
                        position.z--;
                        this.transform.position = position;
                        refreshCounter--;
                        if (refreshCounter == 0)
                        {
                            refreshCounter = 5;
                            print("Cargamos nuevos bloques");


                            BloquesFactory.generateSuelo(5);
                        }
                    }
                break;    
            }
        }
    }
}
