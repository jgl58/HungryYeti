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

    public static GameObject hud;
 
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

    public static void die(){
        estado = gameState.menu;
        GameObject tituloLabel = null;
        GameObject tituloImagen = null;
        GameObject yourButton = null;
        Transform[] trs= GameObject.Find("/MenuPrincipal").GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == "TitleImage"){
                tituloImagen = t.gameObject;
            }
            if(t.name == "Titulo"){
                tituloLabel = t.gameObject;
            }
            if(t.name == "StartButton"){
                yourButton = t.gameObject;
            }
        }
        tituloImagen.gameObject.SetActive(true);
        tituloLabel.gameObject.SetActive(true);
        yourButton.gameObject.SetActive(true);
        //Destroy(GameObject.Find("GameCanvas"), 1);
    }

}
