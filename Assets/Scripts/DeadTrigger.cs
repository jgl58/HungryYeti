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
                Juego.restartTriggers();
                other.gameObject.GetComponent<Animator>().SetTrigger("Morir");
                
                if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE_TRINEO))
                {
                    Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE_TRINEO, 100.0);
                }

            }
            if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE))
            {
                Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE, 100.0);
            }
            source.PlayOneShot(crushSound);
            Juego.die();
            
            

        }
    }

}
