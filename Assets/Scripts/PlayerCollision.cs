using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    bool playerFollow;
    float offset = 0f;
    void Start()
    {
        player = GameObject.Find("/YetiDummy");
        playerFollow = false;
    }

    // Update is called once per frame
    void Update()
    {
        Celda celda = Juego.camino.First.Value;

        if (playerFollow && celda.GetCelda(celda.getColumnaPlayer(player)) == BloquesType.Agua)
        {
   
            
            player.transform.position = new Vector3(
            transform.position.x + offset,
            player.transform.position.y,
            player.transform.position.z);

        }


    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
           
            if (!Juego.getLogroCompleted(Juego.LOGRO_100_TRONCOS))
            {
             /*   Juego.cargarLogros();
                Juego.desbloquearLogro(Juego.LOGRO_100_TRONCOS,
                    Juego.getLogroPercentCompleted(Juego.LOGRO_100_TRONCOS) + 1.0);
                    Juego.updatePercentLogro(Juego.LOGRO_100_TRONCOS, 1.0);*/
            }
            offset = (player.transform.position.x - transform.position.x);
            playerFollow = true;
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Celda c = Juego.camino.First.Value;
            c.recolocarPlayer(player);
            playerFollow = false;
        }
    }
}
