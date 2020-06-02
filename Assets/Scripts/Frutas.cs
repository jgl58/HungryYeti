using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frutas : MonoBehaviour
{

    public Text puntuacionLabel;
    public AudioSource source {get{return GetComponent<AudioSource>();}}
    public AudioClip mordiscoSound;
    public Camera mainCamera;

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
            if (Juego.doublePoints)
            {
                puntuacionLabel.text = string.Format("{0:0000}", puntuacion + 50);
            }
            else{
                puntuacionLabel.text = string.Format("{0:0000}", puntuacion + 25);
            }

            //Destroy(other.gameObject);


            if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_FRUTA))
            {
                Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_FRUTA, 100.0);
            }
            if (!Juego.getLogroCompleted(Juego.LOGRO_100_FRUTAS))
            {
                Juego.desbloquearLogro(Juego.LOGRO_100_FRUTAS,
                    Juego.getLogroPercentCompleted(Juego.LOGRO_100_FRUTAS) + 1.0);
                    Juego.updatePercentLogro(Juego.LOGRO_100_FRUTAS, 1.0);
            }

            Juego.frutasComidas++;
            other.gameObject.LeanMove(mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 1, mainCamera.nearClipPlane + 2)), 1f).setOnComplete(()=>{
                //rotationDirection = rotationState.up;
                Destroy(other.gameObject);
            });
            //StartCoroutine(Patrulla(other.gameObject));
        }
    }


    /*IEnumerator Patrulla(GameObject fruta)
    {
        Vector3 finalPosition = new Vector3(-1,11,fruta.transform.localPosition.z);
        Vector3 origen = fruta.transform.localPosition;
        float t = 0.0f;
        while (t< 3.0f)
        {
            fruta.transform.position = Vector3.Lerp(origen, finalPosition, t);
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(fruta);

    }*/
}
