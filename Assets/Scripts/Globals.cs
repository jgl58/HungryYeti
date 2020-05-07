using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public enum gameState {
        menu,
        jugando,
        perdido
    }
 
    public static gameState estado = gameState.jugando;
 
    // Start is called before the first frame update
    void Start()
    {
        estado = gameState.menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
