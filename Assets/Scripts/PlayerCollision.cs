using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    bool playerFollow;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFollow)
        {
         this.player.transform.position = new Vector3(
            this.transform.position.x,
            player.transform.position.y,
            player.transform.position.z);
        }
       
        
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            playerFollow = true;
            
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Celda c = GeneraSuelo.camino.First.Value;
            c.recolocarPlayer(player);

            playerFollow = false;
        }

    }
}
