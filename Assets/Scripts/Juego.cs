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

    public Text puntuacionLabel;
    public static GameObject MenuPrincipal;
    public static GameObject tituloImagen;
    public static GameObject imagenTiempo;
    public static GameObject imagenPuntuacion;
    public static GameObject gameOver;
    public static GameObject yourButton;
    public static GameObject menuButton;
    public static GameObject removeAdsButton;
    public static GameObject player;
    public static GameObject mainCamera;

    public static gameState estado = gameState.jugando;

    public static bool estoyTronco = false;

    // Start is called before the first frame update
    void Start()
    {
        Movement.puntuacionLabel = puntuacionLabel;
        camino = new LinkedList<Celda>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        BloquesFactory.inicio = -10;
        BloquesFactory.generateInit();
        BloquesFactory.generateSuelo(25);
        estado = gameState.menu;
        MenuPrincipal = GameObject.Find("/MenuPrincipal");
        Transform[] trs = MenuPrincipal.GetComponentsInChildren<Transform>(true);
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
            if (t.name == "ImagePuntuacion")
            {
                imagenPuntuacion = t.gameObject;
            }
            if (t.name == "ImageTiempo")
            {
                imagenTiempo = t.gameObject;
            }
            if (t.name == "BackMenuButton")
            {
                menuButton = t.gameObject;
            }
            if (t.name == "RemoveAdsButton")
            {
                removeAdsButton = t.gameObject;
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
            menuButton.gameObject.SetActive(true);

        }
    }

    public static void start()
    {
        if (estado != gameState.jugando)
        {
            MenuPrincipal.GetComponent<HUD>().reset();
            tituloImagen.SetActive(false);
            gameOver.gameObject.SetActive(false);
            yourButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            removeAdsButton.gameObject.SetActive(false);
            imagenTiempo.gameObject.SetActive(true);
            imagenPuntuacion.gameObject.SetActive(true);
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

                position = mainCamera.transform.position;
                position.z = -8.4f;
                mainCamera.transform.position = position;


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

    public static void backToMenu(){
        firstTime = true;
        estado = gameState.menu;
        tituloImagen.SetActive(true);
        gameOver.gameObject.SetActive(false);
        yourButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        removeAdsButton.gameObject.SetActive(true);
        imagenTiempo.gameObject.SetActive(false);
        imagenPuntuacion.gameObject.SetActive(false);
        GameObject suelo = GameObject.Find("Suelo");
        foreach (Transform child in suelo.transform)
        {
            Destroy(child.gameObject);
        }

        Vector3 position = player.transform.position;
        position.z = -3;
        position.x = 1;
        player.transform.position = position;

        position = mainCamera.transform.position;
        position.z = -8.4f;
        mainCamera.transform.position = position;


        camino.Clear();
        BloquesFactory.inicio = -10;
        BloquesFactory.generateInit();
        BloquesFactory.generateSuelo(25);
    }
}
