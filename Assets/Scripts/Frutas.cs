using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frutas : MonoBehaviour
{

    public Text puntuacionLabel;
    public AudioSource source {get{return GetComponent<AudioSource>();}}
    public AudioClip mordiscoSound;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fruta"){
            source.PlayOneShot(mordiscoSound);
            int puntuacion = int.Parse(puntuacionLabel.text);
            puntuacionLabel.text = string.Format ("{0:0000}", puntuacion + 25);
            Destroy(other.gameObject);
            Juego.frutasComidas++;
        }
    }
}
