using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneraSuelo : MonoBehaviour
{
    // Start is called before the first frame update
    public static LinkedList<Celda> camino;

    void Start()
    {
        camino = new LinkedList<Celda>();
        BloquesFactory.generateSuelo(45);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

}
