using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    public AudioSource source {get{return GetComponent<AudioSource>();}}
    public AudioClip crushSound;
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
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Trineo")
            {
               
                if (Juego.powerUpState == Juego.PowerUpState.escudo)
                {
                    Juego.powerUpState = Juego.PowerUpState.ninguno;

                    Transform escudo = other.transform.Find("Escudo(Clone)");
                    if (escudo != null)
                    {
                        Destroy(escudo.gameObject);
                    }
                }
                else
                {
                    Juego.restartTriggers();
                    other.gameObject.GetComponent<Animator>().SetTrigger("Morir");

                    if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE_TRINEO))
                    {
                        Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE_TRINEO, 100.0);
                    }
                    source.PlayOneShot(crushSound);
                    Juego.die();
                }

            }
            else
            {
                Juego.restartTriggers();
                other.gameObject.GetComponent<Animator>().SetTrigger("Morir");

                if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE))
                {
                    Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE, 100.0);
                }
                else
                {
                    source.PlayOneShot(crushSound);
                    Juego.die();
                }
            }

        }
    }

}
