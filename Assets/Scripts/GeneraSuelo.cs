using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneraSuelo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bloque;
    public GameObject nieve;

    public GameObject suelo;


    void Start()
    {

        BloquesFactory.generateSuelo(16);

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

}
