using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && transform.position.z == other.gameObject.transform.position.z){
            GameObject[] puntuaciones = GameObject.FindGameObjectsWithTag("Canvas");
            if(puntuaciones.Length > 0){
                int puntuacion = int.Parse(puntuaciones[0].GetComponent<HUD>().puntuacionLabel.text);
                puntuaciones[0].GetComponent<HUD>().puntuacionLabel.text = string.Format ("{0:0000}", puntuacion + 25);
            }
            Destroy(gameObject);
        }
    }
}
