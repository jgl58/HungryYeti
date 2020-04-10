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
        for(int i = -8; i <= 60; i += 2)
        {
            
            int pongoPremio = Random.Range(0, 3);
            if (pongoPremio == 1)
            {
                Instantiate(bloque, new Vector3(0, 0, i), new Quaternion()).transform.parent = suelo.transform;
            }
            else
            {

                Instantiate(nieve, new Vector3(0, 0, i), new Quaternion()).transform.parent = suelo.transform;
            }

        }

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
