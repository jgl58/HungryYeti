using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private int refreshCounter = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.z--;
            this.transform.position = position;
            refreshCounter--;
            if (refreshCounter == 0)
            {
                refreshCounter = 5;
                print("Cargamos nuevos bloques");
            }

            
        }
    }
}
