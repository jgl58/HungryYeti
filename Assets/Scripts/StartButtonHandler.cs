using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    public GameObject tituloLabel;
    public Button yourButton;

    private GameObject hud;
    private GameObject player;

	void Start () {
        hud = (GameObject)Resources.Load("Prefabs/GameCanvas");
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        tituloLabel.SetActive(false);
        yourButton.gameObject.SetActive(false);
        Instantiate(hud, new Vector3(0, 0, 0), Quaternion.identity);
        Globals.estado = Globals.gameState.jugando;
	}

}
