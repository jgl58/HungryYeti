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
    public static GameObject imagenTiempoDown;
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

    private static Vector3 inicioCamera;
    private static Vector3 finCamera;

    private static Vector3 rotationInicioCamera;
    private static Vector3 rotationFinCamera;

    public static Juego instance;

    // Start is called before the first frame update
    void Start()
    {
        Movement.puntuacionLabel = puntuacionLabel;
        camino = new LinkedList<Celda>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");


        inicioCamera = new Vector3(-0.8f, 6.5f, -6f);
        finCamera = new Vector3(0f,13f,-9f);

        rotationInicioCamera = new Vector3(51f,31f,0f);
        rotationFinCamera = new Vector3(53f,0f,0f);

        instance = this;

        firstTime = true;

        BloquesFactory.inicio = -10;
        BloquesFactory.generateInit();
        BloquesFactory.generateSuelo(25);
        estado = gameState.menu;
        MenuPrincipal = GameObject.Find("/MenuPrincipal");
        Transform[] trs = MenuPrincipal.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == "ImageTiempoDown")
            {
                imagenTiempoDown = t.gameObject;
            }
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
            player.SetActive(false);
            menuButton.gameObject.SetActive(true);
        }
    }

    public static void start()
    {
        if (estado != gameState.jugando)
        {
            MenuPrincipal.GetComponent<HUD>().reset();
            imagenTiempoDown.SetActive(true);
            tituloImagen.SetActive(false);
            gameOver.gameObject.SetActive(false);
            yourButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            removeAdsButton.gameObject.SetActive(false);
            imagenTiempo.gameObject.SetActive(true);
            imagenPuntuacion.gameObject.SetActive(true);

            player.SetActive(true);

            print(firstTime);

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

                mainCamera.transform.position = inicioCamera;

                HUD.lastCheckPos = player.transform.position;

                camino.Clear();
                BloquesFactory.inicio = -10;
                BloquesFactory.generateInit();
                BloquesFactory.generateSuelo(25);

            }
            else
            { 
                firstTime = false;
            }
            instance.StartCoroutine(AnimationCamera());
            
            estoyTronco = false;
        }
    }


    public static IEnumerator AnimationCamera()
    {
        // mainCamera.transform.position = inicioCamera;
        float t = 0.0f;
        while (t < 1.0f)
        {
            mainCamera.transform.position = Vector3.Lerp(inicioCamera, finCamera, t);
            mainCamera.transform.eulerAngles = Vector3.Lerp(rotationInicioCamera, rotationFinCamera, t);
            t += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = finCamera;
        mainCamera.transform.eulerAngles = rotationFinCamera;
        estado = gameState.jugando;
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
        imagenTiempoDown.SetActive(false);
        GameObject suelo = GameObject.Find("Suelo");
        foreach (Transform child in suelo.transform)
        {
            Destroy(child.gameObject);
        }

        Vector3 position = player.transform.position;
        position.z = -3;
        position.x = 1;
        player.transform.position = position;

        player.SetActive(true);

        mainCamera.transform.position = inicioCamera;
        mainCamera.transform.eulerAngles = rotationInicioCamera;


        camino.Clear();
        BloquesFactory.inicio = -10;
        BloquesFactory.generateInit();
        BloquesFactory.generateSuelo(25);
    }
}
