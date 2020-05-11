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


    private static GameObject suelo = GameObject.FindGameObjectWithTag("Suelo");
    private static GameObject bloque1 = (GameObject)Resources.Load("Prefabs/Bloque1");
    private static GameObject bloque2 = (GameObject)Resources.Load("Prefabs/Bloque2");
    private static GameObject bloque3 = (GameObject)Resources.Load("Prefabs/Bloque3");
    private static GameObject bloqueRoca = (GameObject)Resources.Load("Prefabs/BloqueRoca");
    private static GameObject bloqueNieve = (GameObject)Resources.Load("Prefabs/BloqueNieve");
    private static GameObject bloqueAgua = (GameObject)Resources.Load("Prefabs/BloqueAgua");
    private static GameObject lateral = (GameObject)Resources.Load("Prefabs/Lateral");
    private static GameObject tronco = (GameObject)Resources.Load("Prefabs/Tronco");

    public static void generateSuelo(int distancia)
    {
        //NO QUITAR
        //print(OBJETO.GetComponent<Renderer>().bounds.size.y);

        int aux = inicio;
        bool ponerNieve = true;

        List<int> points = new List<int>() { -3, -1, 1, 3 };

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
                    celda = new Celda(lista,points);
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
                            celda = new Celda(lista, points);
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
                            celda = new Celda(lista, points);
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
                            celda = new Celda(lista, points);
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
                            celda = new Celda(lista, points);
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
                                celda = new Celda(lista, points);
                                GeneraSuelo.camino.AddLast(celda);
                            }
                            int direccion = Random.Range(0, 3);
                        
                            if (direccion == 0)
                            {
                                //direccion derecha
                                createTronco(obj, i, true);
                                /*GameObject wood = Instantiate(tronco, new Vector3(-8, 0, i), new Quaternion());
                                wood.transform.parent = obj.transform;*/
                            }
                            else
                            {
                                //direccion izquierda
                                createTronco(obj, i, false);
                                /*GameObject wood = Instantiate(tronco, new Vector3(8, 0, i), new Quaternion());
                                wood.transform.parent = obj.transform;*/
                            }
                            i++;
                        }
                        i--;
                        break;
                    case 6:
                        
                        for (int j = 0; j < 2; j++)
                            {
                                obj = Instantiate(carretera, new Vector3(0, -0.2f, i), new Quaternion());
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
                                    celda = new Celda(lista, points);
                                    GeneraSuelo.camino.AddLast(celda);
                                }
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

    public static void createTronco(GameObject parent, int i, bool derecha)
    {

        GameObject t = Instantiate(tronco, new Vector3(derecha ? -7 : 7, 0, i), new Quaternion());
        t.transform.parent = parent.transform;
    }

    private static GameObject getCoche(int i){
        switch(i){
            case 1: return busAzul; 
            case 2: return busRojo; 
            case 3: return busAmarillo; 
            case 4: return coche2Verde; 
            case 5: return coche2Morado; 
            case 6: return coche2Gris; 
            case 7: return coche5Rojo; 
            case 8: return coche5Gris; 
            case 9: return coche5Amarillo; 
            case 10: return camion1Azul; 
            case 11: return camion1Red; 
            case 12: return camion1Morado; 
            case 13: return policeCar; 
            default: return busAmarillo; 
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
