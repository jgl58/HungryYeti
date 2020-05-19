using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Juego : MonoBehaviour
{
    public enum gameState
    {
        menu,
        jugando,
        perdido
    }
    public static LinkedList<Celda> camino;

    public static bool firstTime = true;

    public static GameObject tituloImagen;
    public static GameObject gameOver;
    public static GameObject yourButton;
    public static GameObject player;
    public static GameObject camera;

    private static GameObject hud;

    public static gameState estado = gameState.jugando;

    public static bool estoyTronco = false;

    // Start is called before the first frame update
    void Start()
    {
        camino = new LinkedList<Celda>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        BloquesFactory.inicio = -10;
        BloquesFactory.generateInit();
        BloquesFactory.generateSuelo(25);
        estado = gameState.menu;
        hud = (GameObject)Resources.Load("Prefabs/GameCanvas");
        Transform[] trs = GameObject.Find("/MenuPrincipal").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == "TitleImage")
            {
                tituloImagen = t.gameObject;
            }
            if (t.name == "StartButton")
            {
                yourButton = t.gameObject;
            }
            if (t.name == "GameOver")
            {
                gameOver = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static void die()
    {
        if (estado == gameState.jugando)
        {
            estado = gameState.perdido;
            yourButton.gameObject.SetActive(true);
            gameOver.gameObject.SetActive(true);
        }
    }

    public static void start()
    {
        if (estado != gameState.jugando)
        {
            //hud.GetComponent<HUD>().reset();
            tituloImagen.SetActive(false);
            gameOver.gameObject.SetActive(false);
            yourButton.gameObject.SetActive(false);
            GameObject[] canvases = GameObject.FindGameObjectsWithTag("Canvas");
            foreach (GameObject canvas in canvases)
            {
                Destroy(canvas);
            }
            hud = Instantiate((GameObject)Resources.Load("Prefabs/GameCanvas"), new Vector3(0, 0, 0), Quaternion.identity);
            Movement.puntuacionLabel = hud.GetComponent<HUD>().puntuacionLabel;
            if (!firstTime)
            {
                GameObject suelo = GameObject.Find("Suelo");
                foreach (Transform child in suelo.transform)
                {
                    Destroy(child.gameObject);
                }

                Vector3 position = player.transform.position;
                position.z = -3;
                position.x = 1;
                player.transform.position = position;

                position = camera.transform.position;
                position.z = -8.4f;
                camera.transform.position = position;


                camino.Clear();
                BloquesFactory.inicio = -10;
                BloquesFactory.generateInit();
                BloquesFactory.generateSuelo(25);

            }
            else
            {
                firstTime = false;
            }
            estado = gameState.jugando;
            estoyTronco = false;
        }
    }
}
