using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class Juego : MonoBehaviour
{

    public static string LOGRO_PRIMERA_FRUTA = "CgkIpMLIoLcZEAIQAQ";
    public static string LOGRO_PRIMERA_MUERTE = "CgkIpMLIoLcZEAIQAg";
    public static string LOGRO_PRIMERA_MUERTE_COCHE = "CgkIpMLIoLcZEAIQAw";
    public static string LOGRO_PRIMERA_MUERTE_AGUA = "CgkIpMLIoLcZEAIQBA";
    public static string LOGRO_PRIMERA_MUERTE_TRINEO = "CgkIpMLIoLcZEAIQBQ";
    public static string LOGRO_100_FRUTAS = "CgkIpMLIoLcZEAIQBg";
    public static string LOGRO_100_TRONCOS = "CgkIpMLIoLcZEAIQBw";

    public static List<IAchievement> logros = new List<IAchievement>();

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
    public static GameObject playAgainWithAd;
    public static GameObject player;
    public static GameObject mainCamera;
    public static GameObject logrosButton;

    public static gameState estado = gameState.jugando;

    public static bool estoyTronco = false;

    private static Vector3 inicioCamera;
    private static Vector3 finCamera;

    private static Vector3 rotationInicioCamera;
    private static Vector3 rotationFinCamera;

    public static Juego instance;

    public static int frutasComidas = 0;

    // Start is called before the first frame update
    void Start()
    {
    #if UNITY_ANDROID
            PlayGamesPlatform.DebugLogEnabled = false;
            PlayGamesPlatform.Activate();
    #endif

        Social.localUser.Authenticate(ProcessAuthentication);



        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Ads", 1);
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
            if (t.name == "PlayAgainButton")
            {
                playAgainWithAd = t.gameObject;
            }
            if (t.name == "LogrosButton")
            {
                logrosButton = t.gameObject;
            }
        }

        if (PlayerPrefs.HasKey("Ads"))
        {
            int hasAds = PlayerPrefs.GetInt("Ads");
            removeAdsButton.SetActive(hasAds == 1);
        }else{
            removeAdsButton.SetActive(true);
        }

        
    }

    public static void cargarLogros()
    {
        Social.LoadAchievements(achievements => {
            if (achievements.Length > 0)
            {
                foreach (IAchievement achievement in achievements)
                {
                    logros.Add(achievement);
                }
            }
            else
                Debug.Log("No achievements returned");
        });
    }

    public static bool getLogroCompleted(string id)
    {
        foreach (IAchievement achievement in logros)
        {
            if (achievement.id == id)
            {
                return achievement.completed;
            }
        }
        return false;

    }
    public static double getLogroPercentCompleted(string id)
    {
        foreach (IAchievement achievement in logros)
        {
            if (achievement.id == id)
            {
                return achievement.percentCompleted;
            }
        }
        return 0.0;
    }

        public void verLogros()
    {
        Social.ShowAchievementsUI();
    }


    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            cargarLogros();
            Debug.Log("Authenticated, checking achievements");
           
        }
        else
            Debug.Log("Failed to authenticate");
    }

    public static void desbloquearLogro(string idLogro, double completo)
    {
        if (Social.localUser.authenticated)
        { 

            Social.ReportProgress(idLogro, completo, (bool result) => {
                if (result)
                { 
                    Debug.Log("Logro desbloqueado con exito");
                }
                else
                {
                    Debug.Log("Error al desbloquear logro");
              }
            });

            
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
            if (!Juego.getLogroCompleted(Juego.LOGRO_PRIMERA_MUERTE))
            {
                Juego.desbloquearLogro(Juego.LOGRO_PRIMERA_MUERTE, 100.0);
            }
            if (frutasComidas >= 5){
                playAgainWithAd.SetActive(true);
            }
        }
    }

    public static void start(bool resetPuntuacion = true)
    {
        if (estado != gameState.jugando)
        {
            if(resetPuntuacion){
                MenuPrincipal.GetComponent<HUD>().reset();
            }
            playAgainWithAd.SetActive(false);
            imagenTiempoDown.SetActive(true);
            tituloImagen.SetActive(false);
            gameOver.gameObject.SetActive(false);
            yourButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            removeAdsButton.gameObject.SetActive(false);
            imagenTiempo.gameObject.SetActive(true);
            imagenPuntuacion.gameObject.SetActive(true);
            logrosButton.gameObject.SetActive(false);

            frutasComidas = 0;

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
        if(InitializeAdsScript.hasAds()){
            instance.StartCoroutine(ShowAdWhenReady()); // PUBLICIDAD INTERSTICIAL NORMAL
        }
        firstTime = true;
        estado = gameState.menu;
        playAgainWithAd.SetActive(false);
        tituloImagen.SetActive(true);
        gameOver.gameObject.SetActive(false);
        yourButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(false);
        logrosButton.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("Ads"))
        {
            int hasAds = PlayerPrefs.GetInt("Ads");
            removeAdsButton.SetActive(hasAds == 1);
        }else{
            removeAdsButton.SetActive(true);
        }
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

    public static IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(InitializeAdsScript.placementId))
            yield return new WaitForSeconds (0.5f);
 
        Advertisement.Show(InitializeAdsScript.placementId);
    }


}
