﻿using UnityEngine;

public class BloquesFactory : MonoBehaviour
{
    public static int inicio = -10;

    public static void generateSuelo(int distancia)
    {

        GameObject suelo = GameObject.FindGameObjectWithTag("Suelo");
        GameObject bloque1 = (GameObject)Resources.Load("Prefabs/Bloque1");
        GameObject bloque2 = (GameObject)Resources.Load("Prefabs/Bloque2");
        GameObject bloque3 = (GameObject)Resources.Load("Prefabs/Bloque3");
        GameObject bloqueRoca = (GameObject)Resources.Load("Prefabs/BloqueRoca");
        GameObject bloqueNieve = (GameObject)Resources.Load("Prefabs/BloqueNieve");
        GameObject bloqueAgua = (GameObject)Resources.Load("Prefabs/BloqueAgua");
        GameObject lateral = (GameObject)Resources.Load("Prefabs/Lateral");
        GameObject tronco = (GameObject)Resources.Load("Prefabs/Tronco");

        int aux = inicio;
        distancia = distancia * 2;
        bool ponerNieve = true;

        for (int i = aux; i < aux + distancia; i += 2)
        {
            GameObject obj;
            
            
            if (ponerNieve)
            {
                obj = Instantiate(bloqueNieve, new Vector3(0, 0, i), new Quaternion());
                obj.transform.parent = suelo.transform;
                ponerNieve = false;
                ponerLateral(lateral, suelo, i);
            }
            else
            {
                int bloque = Random.Range(1, 6);
                switch (bloque)
                {
                    case 1:
                        obj = Instantiate(bloque1, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral,suelo,i);
                        break;
                    case 2:
                        obj = Instantiate(bloque2, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        break;
                    case 3:
                        obj = Instantiate(bloque3, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        break;
                    case 4:
                        obj = Instantiate(bloqueRoca, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        break;
                    case 5:
                        obj = Instantiate(bloqueAgua, new Vector3(0, -0.5f, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        
                        int direccion = Random.Range(0, 3);
                        print(direccion);
                        if (direccion == 0)
                        {
                            //direccion derecha
                            GameObject wood = Instantiate(tronco, new Vector3(-7, 0, i), new Quaternion());
                            wood.transform.parent = obj.transform;
                        }
                        else
                        {
                            //direccion izquierda
                            GameObject wood = Instantiate(tronco, new Vector3(7, 0, i), new Quaternion());
                            wood.transform.parent = obj.transform;
                        }
                        break;
                    default:
                        break;

                }
            }

        }
        inicio = inicio + distancia - 5;
    }

    public static void ponerLateral(GameObject lateral,GameObject suelo, int i)
    {
        GameObject lat = Instantiate(lateral, new Vector3(-5, 0, i), new Quaternion());
        lat.transform.parent = suelo.transform;
        GameObject lat2 = Instantiate(lateral, new Vector3(5, 0, i), new Quaternion());
        lat2.transform.parent = suelo.transform;
        

    }
}
