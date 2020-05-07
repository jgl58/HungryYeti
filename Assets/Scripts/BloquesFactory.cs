using UnityEngine;
using System.Collections.Generic;

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
        bool ponerNieve = true;

        for (int i = aux; i < aux + distancia; i++)
        {
            GameObject obj;

            Celda celda;
            List<BloquesType> lista;

            if (ponerNieve)
            {
                obj = Instantiate(bloqueNieve, new Vector3(0, 0, i), new Quaternion());
                obj.transform.parent = suelo.transform;
                ponerNieve = false;
                if (i >= -3)
                {
                    lista = new List<BloquesType>()
                    {
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve
                    };
                    celda = new Celda(lista);
                    GeneraSuelo.camino.AddLast(celda);
                }
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
                        if (i >= -3)
                        {
                            lista = new List<BloquesType>()
                            {
                                BloquesType.Obstaculo,
                                BloquesType.Nieve,
                                BloquesType.Obstaculo,
                                BloquesType.Obstaculo
                            };
                            celda = new Celda(lista);
                            GeneraSuelo.camino.AddLast(celda);
                        }
                        break;
                    case 2:
                        obj = Instantiate(bloque2, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        if (i >= -3)
                        {
                            lista = new List<BloquesType>()
                            {
                                BloquesType.Nieve,
                                BloquesType.Nieve,
                                BloquesType.Nieve,
                                BloquesType.Obstaculo
                            };
                            celda = new Celda(lista);
                            GeneraSuelo.camino.AddLast(celda);
                        }
                        break;
                    case 3:
                        obj = Instantiate(bloque3, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        if (i >= -3)
                        {
                            lista = new List<BloquesType>()
                            {
                                BloquesType.Obstaculo,
                                BloquesType.Nieve,
                                BloquesType.Nieve,
                                BloquesType.Nieve
                            };
                            celda = new Celda(lista);
                            GeneraSuelo.camino.AddLast(celda);
                        }
                        break;
                    case 4:
                        obj = Instantiate(bloqueRoca, new Vector3(0, 0, i), new Quaternion());
                        obj.transform.parent = suelo.transform;
                        ponerNieve = true;
                        ponerLateral(lateral, suelo, i);
                        if (i >= -3)
                        {
                            lista = new List<BloquesType>()
                            {
                                BloquesType.Obstaculo,
                                BloquesType.Nieve,
                                BloquesType.Nieve,
                                BloquesType.Obstaculo
                            };
                            celda = new Celda(lista);
                            GeneraSuelo.camino.AddLast(celda);

                        }
                        
                        break;
                    case 5:
                        for (int j = 0; j < 2; j++)
                        {
                            obj = Instantiate(bloqueAgua, new Vector3(0, -0.5f, i), new Quaternion());
                            obj.transform.parent = suelo.transform;
                            if (i >= -3)
                            {
                                lista = new List<BloquesType>()
                                {
                                    BloquesType.Nieve,
                                    BloquesType.Nieve,
                                    BloquesType.Nieve,
                                    BloquesType.Nieve
                                };
                                celda = new Celda(lista);
                                GeneraSuelo.camino.AddLast(celda);
                            }
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
                            i++;
                        }
                        i--;
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
        GameObject lat = Instantiate(lateral, new Vector3(-4.5f, 0f, i), new Quaternion());
        lat.transform.parent = suelo.transform;
        GameObject lat2 = Instantiate(lateral, new Vector3(4.5f, 0f, i), new Quaternion());
        lat2.transform.parent = suelo.transform;
        

    }
}
