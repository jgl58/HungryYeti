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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Celda celda = GeneraSuelo.camino.First.Value;
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
        Celda celda = GeneraSuelo.camino.First.Value;
        if (other.gameObject.tag == "Player")
        {
            print("Entro tronco");
            offset = (player.transform.position.x - transform.position.x);
            playerFollow = true;
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {

        if ((other.gameObject.tag == "Player"))
        {
            print("Salgo tronco");
            Celda c = GeneraSuelo.camino.First.Value;
            c.recolocarPlayer(player);
            playerFollow = false;
        }

    }
}
