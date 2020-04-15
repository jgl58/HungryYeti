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
    }
}
