using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloquesFactory : MonoBehaviour
{
    public static int inicio = -10;

    public static void generateSuelo(int distancia)
    {

        GameObject suelo = GameObject.FindGameObjectWithTag("Suelo");
        GameObject bloque1 = (GameObject)Resources.Load("Prefabs/Bloque1");
        GameObject bloqueNieve = (GameObject)Resources.Load("Prefabs/BloqueNieve");
        GameObject bloqueAgua = (GameObject)Resources.Load("Prefabs/BloqueAgua");
        GameObject tronco = (GameObject)Resources.Load("Prefabs/Tronco");

        int aux = inicio;
        distancia = distancia * 2;

        for (int i = aux; i < aux + distancia; i += 2)
        {
            GameObject obj;
            int pongoPremio = Random.Range(0, 3);
            if (pongoPremio == 1)
            {
                //obj = (GameObject)Resources.Load("Prefabs/Bloque1", typeof(GameObject));

                obj = Instantiate(bloque1, new Vector3(0, 0, i), new Quaternion());
                obj.transform.parent = suelo.transform;

                //Instantiate(bloque, new Vector3(0, 0, i), new Quaternion()).transform.parent = suelo.transform;
            }else if (pongoPremio == 2)
            {

                obj = Instantiate(bloqueAgua, new Vector3(0, -0.5f, i), new Quaternion());
                obj.transform.parent = suelo.transform;

                GameObject wood = Instantiate(tronco, new Vector3(0, 0, i), new Quaternion());
                wood.transform.parent = obj.transform;

                //Instantiate(bloque, new Vector3(0, 0, i), new Quaternion()).transform.parent = suelo.transform;
            }
            else
            {

                obj = Instantiate(bloqueNieve, new Vector3(0, 0, i), new Quaternion());
                obj.transform.parent = suelo.transform;
                //Instantiate(nieve, new Vector3(0, 0, i), new Quaternion()).transform.parent = suelo.transform;
            }
        }
        inicio = inicio + distancia - 5;
    }
}
