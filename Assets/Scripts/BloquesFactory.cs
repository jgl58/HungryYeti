using UnityEngine;
using System.Collections.Generic;

public class BloquesFactory : MonoBehaviour
{
    public static int inicio = -10;

    //Carreteras y coches
    private static GameObject carretera = (GameObject)Resources.Load("Prefabs/Carretera");
    private static GameObject busAzul = (GameObject)Resources.Load("Cars/Prefabs/Bus_Blue");
    private static GameObject busRojo = (GameObject)Resources.Load("Cars/Prefabs/Bus_Red");
    private static GameObject busAmarillo = (GameObject)Resources.Load("Cars/Prefabs/Bus_Yellow");
    private static GameObject coche2Verde = (GameObject)Resources.Load("Cars/Prefabs/Car_2_Green");
    private static GameObject coche2Morado = (GameObject)Resources.Load("Cars/Prefabs/Car_2_Purple");
    private static GameObject coche2Gris = (GameObject)Resources.Load("Cars/Prefabs/Car_2_Silver");
    private static GameObject coche5Rojo = (GameObject)Resources.Load("Cars/Prefabs/Car_5_Red");
    private static GameObject coche5Gris = (GameObject)Resources.Load("Cars/Prefabs/Car_5_Silver");
    private static GameObject coche5Amarillo = (GameObject)Resources.Load("Cars/Prefabs/Car_5_Yellow");
    private static GameObject camion1Azul = (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Blue");
    private static GameObject camion1Red = (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Red");
    private static GameObject camion1Morado = (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Purple");
    private static GameObject policeCar = (GameObject)Resources.Load("Cars/Prefabs/Policecar");

    public static void generateSuelo(int distancia)
    {
        //print(OBJETO.GetComponent<Renderer>().bounds.size.y);

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
                int bloque = Random.Range(1, 7);
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
                                GameObject wood = Instantiate(tronco, new Vector3(-8, 0, i), new Quaternion());
                                wood.transform.parent = obj.transform;
                            }
                            else
                            {
                                //direccion izquierda
                                GameObject wood = Instantiate(tronco, new Vector3(8, 0, i), new Quaternion());
                                wood.transform.parent = obj.transform;
                            }
                            i++;
                        }
                        i--;
                        break;
                    case 6:
                        int howMany = Random.Range(2, 5);
                        for (int j = 0; j < howMany; j++)
                            {
                                obj = Instantiate(carretera, new Vector3(0, -0.2f, i), new Quaternion());
                                obj.transform.parent = suelo.transform;
                                int direccion = Random.Range(0, 2);
                                if(direccion == 0) { //derecha
                                    createCoche(obj, i,true);
                                } else { //izquierda
                                    createCoche(obj, i,false);
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

    public static void createCoche(GameObject parent, int i,bool derecha){
            int car = Random.Range(1, 13);
            GameObject coche = Instantiate(getCoche(car), new Vector3(derecha ? 7 : -7, getYSizeOfCar(car), i), new Quaternion());
            if(derecha){ coche.transform.Rotate(new Vector3(0,180,0)); }
            coche.transform.parent = parent.transform;
    }

    private static GameObject getCoche(int i){
        switch(i){
            case 1: return busAzul; break;
            case 2: return busRojo; break;
            case 3: return busAmarillo; break;
            case 4: return coche2Verde; break;
            case 5: return coche2Morado; break;
            case 6: return coche2Gris; break;
            case 7: return coche5Rojo; break;
            case 8: return coche5Gris; break;
            case 9: return coche5Amarillo; break;
            case 10: return camion1Azul; break;
            case 11: return camion1Red; break;
            case 12: return camion1Morado; break;
            case 13: return policeCar; break;
            default: return busAmarillo; break;
        }
    }

    private static float getYSizeOfCar(int i){
        float res = 1.00f;
        /*  bus //0.9772003
            coche2 //0.5305743
            coche5 //0.645594
            camion; //1.41829
            police //0.5673869
        */
        switch(i){
            case 1:
            case 2:
            case 3: res = 1.2f; break;
            case 4: 
            case 5: 
            case 6: res = 0.53f; break;
            case 7:
            case 8: 
            case 9: res = 0.64f; break;
            case 10:
            case 11:
            case 12: res = 1.41f; break;
            case 13: res = 0.56f; break;
            default: res = 1.00f; break;
        }
        return 0.35f+(res/2); //UN POCO A OJO, PERO QUEDA BIEN
    }
}
