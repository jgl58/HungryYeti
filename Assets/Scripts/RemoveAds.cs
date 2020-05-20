using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour
{
    // Start is called before the first frame update
   public Button yourButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        print("remove button pressed");
    }
}
