using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionEsquiadores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            int pos1 = BloquesFactory.getPosX(1,5);
            int pos2 = BloquesFactory.getPosX(1,5);
            while(pos2 == pos1){
                pos2 = BloquesFactory.getPosX(1,5);
            }
            BloquesFactory.createTrineo((int)other.gameObject.transform.position.z + BloquesFactory.BLOQUES_ESQUIADORES-2, pos1);
            BloquesFactory.createTrineo((int)other.gameObject.transform.position.z + BloquesFactory.BLOQUES_ESQUIADORES-2, pos2);
        }
    }
}
