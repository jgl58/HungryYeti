using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour
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
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Trineo")
            {
                
                if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE_TRINEO))
                {
                    Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE_TRINEO, 100.0);
                }
                if (Juego.powerUpState == Juego.PowerUpState.escudo)
                {
                    Juego.powerUpState = Juego.PowerUpState.ninguno;

                    Transform escudo = other.transform.Find("Escudo(clone)");
                    if (escudo != null)
                    {
                        Destroy(escudo.gameObject);
                    }
                }
                else
                {
                    Juego.die();
                }


            }
            if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE))
            {
                Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE, 100.0);
            }
            else
            {
                Juego.die();
            }
     
        }
    }

}
