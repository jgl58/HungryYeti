﻿using UnityEngine;
using System.Collections.Generic;

public class BloquesFactory : MonoBehaviour
{
    public static int inicio;
    public static int BLOQUES_ITERACION = 6;
    public static int BLOQUES_BLANCOS_INICIO = 10;
    public static int FRECUENCIA_FRUTAS = 5;
    public static int FRECUENCIA_POWERUP = 20;
    public static int BLOQUES_ESQUIADORES = 30;

    //Suelos y prefabs
    private static GameObject suelo = GameObject.FindGameObjectWithTag("Suelo");
    private static GameObject bloque1 = (GameObject)Resources.Load("Prefabs/Bloque1");
    private static GameObject bloque2 = (GameObject)Resources.Load("Prefabs/Bloque2");
    private static GameObject bloque3 = (GameObject)Resources.Load("Prefabs/Bloque3");
    private static GameObject bloqueRoca = (GameObject)Resources.Load("Prefabs/BloqueRoca");
    private static GameObject bloqueNieve = (GameObject)Resources.Load("Prefabs/BloqueNieve");
    private static GameObject bloqueAgua = (GameObject)Resources.Load("Prefabs/BloqueAgua");
    private static GameObject lateral = (GameObject)Resources.Load("Prefabs/Lateral");
    private static GameObject tronco = (GameObject)Resources.Load("Prefabs/Tronco");
    private static GameObject trineo = (GameObject)Resources.Load("Prefabs/Trineo");
    private static GameObject escudo = (GameObject)Resources.Load("Prefabs/Escudo");
    private static GameObject doublePoints= (GameObject)Resources.Load("Prefabs/double");

    private static List<int> points = new List<int>() { -3, -1, 1, 3 };

    public static void generateInit()
    {
        GameObject obj;
        Celda celda;
        List<BloquesType> lista;
        for(int i = 0; i < BLOQUES_BLANCOS_INICIO ; i++){
            obj = Instantiate(bloqueNieve, new Vector3(0, 0, inicio), new Quaternion());
            obj.transform.parent = suelo.transform;
            if (inicio >= -3)
            {
                lista = new List<BloquesType>()
                {
                    BloquesType.Nieve,
                    BloquesType.Nieve,
                    BloquesType.Nieve,
                    BloquesType.Nieve
                };
                celda = new Celda(lista, points);
                Juego.camino.AddLast(celda);
            }
            ponerLateral(lateral, suelo, inicio);
            inicio++;
        }
    }

    public static void generateSuelo(int distancia)
    {
        //NO QUITAR
        //print(OBJETO.GetComponent<Renderer>().bounds.size.y);
  
        bool ponerNieve = true;

        for (int i = 0; i < distancia; i++)
        {
            GameObject obj;

            Celda celda;
            List<BloquesType> lista;
            if (ponerNieve)
            {
                obj = Instantiate(bloqueNieve, new Vector3(0, 0, inicio), new Quaternion());
                obj.transform.parent = suelo.transform;
                ponerNieve = false;
                if (inicio >= -3)
                {
                    lista = new List<BloquesType>()
                    {
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve
                    };
                    celda = new Celda(lista, points);
                    Juego.camino.AddLast(celda);
                }
                ponerLateral(lateral, suelo, inicio);

                ponerCollecionable(getPosX(1, 5), inicio);
            
                inicio++;
            }
            else
            {
                int bloque = Random.Range(1, 8);

                switch (bloque)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        inicio = ponerObstaculos(inicio, bloque);
                        ponerNieve = true;
                        break;
                    case 5:
                        inicio = ponerAgua(inicio);
                        ponerNieve = true;
                        break;
                    case 6:
                        int carr = Random.Range(0, 2);
                        if(carr == 0){
                            inicio = ponerCarretera(inicio);
                        } else {
                            int auxBloque = Random.Range(1, 5);
                            inicio = ponerObstaculos(inicio, auxBloque);
                        }
                        ponerNieve = true;
                        break;
                    case 7:
                        int bloqueEsqui = Random.Range(1, 6);
                        if(bloqueEsqui == 5)
                        {
                            inicio = ponerEsquiadores(inicio);
                        }
                        else
                        {
                            inicio = ponerObstaculos(inicio,bloqueEsqui);
                        }
                        ponerNieve = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //Funciones bloques

    public static int ponerObstaculos(int posicion, int tipo)
    {
        GameObject obj;
        List<BloquesType> lista;
        switch (tipo)
        {
            case 1:
                obj = Instantiate(bloque1, new Vector3(0, 0, posicion), new Quaternion());
                obj.transform.parent = suelo.transform;
                
                ponerLateral(lateral, suelo, posicion);
                if (posicion >= -3)
                {
                    lista = new List<BloquesType>()
                            {
                                BloquesType.Obstaculo,
                                BloquesType.Nieve,
                                BloquesType.Obstaculo,
                                BloquesType.Obstaculo
                            };
                    Juego.camino.AddLast(new Celda(lista, points));

                    ponerCollecionable(-1, inicio);

                }
                break;
            case 2:
                obj = Instantiate(bloque2, new Vector3(0, 0, posicion), new Quaternion());
                obj.transform.parent = suelo.transform;
                
                ponerLateral(lateral, suelo, posicion);
                if (posicion >= -3)
                {
                    lista = new List<BloquesType>()
                    {
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Obstaculo
                    };
                    Juego.camino.AddLast(new Celda(lista, points));

                    ponerCollecionable(getPosX(1, 4), inicio);

                }
                break;
            case 3:
                obj = Instantiate(bloque3, new Vector3(0, 0, posicion), new Quaternion());
                obj.transform.parent = suelo.transform;
                
                ponerLateral(lateral, suelo, posicion);
                if (posicion >= -3)
                {
                    lista = new List<BloquesType>()
                    {
                        BloquesType.Obstaculo,
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Nieve
                    };
                    Juego.camino.AddLast(new Celda(lista, points));

                    ponerCollecionable(getPosX(2, 5), inicio);

                }
                break;
            case 4:
                obj = Instantiate(bloqueRoca, new Vector3(0, 0, posicion), new Quaternion());
                obj.transform.parent = suelo.transform;
                ponerLateral(lateral, suelo, posicion);
                if (posicion >= -3)
                {
                    lista = new List<BloquesType>()
                    {
                        BloquesType.Obstaculo,
                        BloquesType.Nieve,
                        BloquesType.Nieve,
                        BloquesType.Obstaculo
                    };
                    Juego.camino.AddLast(new Celda(lista, points));

                    ponerCollecionable(getPosX(2, 4), inicio);

                }

                break;
        }

        return posicion + 1;

    }


    public static void ponerCollecionable(float x, float z)
    {
        int ponerFruta = Random.Range(0, FRECUENCIA_FRUTAS);
        if (ponerFruta == 0)
        {
            GameObject fruit = Instantiate(getFruta(), new Vector3(x, 0.7f, z), new Quaternion());
            fruit.transform.parent = suelo.transform;
        }
        else
        {
            int ponerPowerup = Random.Range(0, FRECUENCIA_POWERUP);
            if (ponerPowerup == 0)
            {
                GameObject power = Instantiate(escudo, new Vector3(x, 1.0f, z), new Quaternion());
                power.transform.parent = suelo.transform;
            }
            else if(ponerPowerup == 1)
            {
                GameObject power = Instantiate(doublePoints, new Vector3(x, 1.0f, z), new Quaternion());
                power.transform.parent = suelo.transform;

            }
        }
    }

    public static int ponerAgua(int posicion)
    {
        GameObject obj;
        List<BloquesType> lista;
        int max = Random.Range(2, 5);
        for (int j = posicion; j < posicion + max; j++)
        {

            obj = Instantiate(bloqueAgua, new Vector3(0, -0.5f, j), new Quaternion());
            obj.transform.parent = suelo.transform;
            if (j >= -3)
            {
                lista = new List<BloquesType>()
                {
                    BloquesType.Agua,
                    BloquesType.Agua,
                    BloquesType.Agua,
                    BloquesType.Agua
                };
                Juego.camino.AddLast(new Celda(lista, points));
            }
            int direccion = Random.Range(0, 2);

            if (direccion == 0)
            {
                //direccion derecha
                createTronco(obj, j, true);
            }
            else
            {
                //direccion izquierda
                createTronco(obj, j, false);
            }
        }

        return posicion + max;

    }


    public static int ponerCarretera(int posicion)
    {
        GameObject obj;
        List<BloquesType> lista;
        int max = Random.Range(2, 7);
        for (int j = posicion; j < posicion + max; j++)
        {

            obj = Instantiate((GameObject)Resources.Load("Prefabs/Carretera"), new Vector3(0, -0.2f, j), new Quaternion());
            obj.transform.parent = suelo.transform;
            if (j >= -3)
            {
                lista = new List<BloquesType>()
                {
                    BloquesType.Carretera,
                    BloquesType.Carretera,
                    BloquesType.Carretera,
                    BloquesType.Carretera
                };
                Juego.camino.AddLast(new Celda(lista, points));
            }
            int direccion = Random.Range(0, 2);
            if (direccion == 0)
            { //derecha
                createCoche(obj, j, true);
            }
            else
            { //izquierda
                createCoche(obj, j, false);
            }
        }

        return posicion + max;

    }

    public static int ponerEsquiadores(int posicion)
    {
        GameObject obj;
        List<BloquesType> lista;
        for (int j = posicion; j < posicion + BLOQUES_ESQUIADORES; j++)
        {
           
            obj = Instantiate(bloqueNieve, new Vector3(0, 0, j), new Quaternion());
            obj.transform.parent = suelo.transform;

            if (j >= -3)
            {
                lista = new List<BloquesType>()
                {
                    BloquesType.Nieve,
                    BloquesType.Nieve,
                    BloquesType.Nieve,
                    BloquesType.Nieve
                };
                Juego.camino.AddLast(new Celda(lista, points));

                ponerCollecionable(getPosX(1, 5), j);

                if(j == posicion + 2){
                    MeshFilter mf = obj.GetComponent<MeshFilter>();
                    if (mf && mf.sharedMesh)
                    {
                        Bounds bounds = mf.sharedMesh.bounds;
                        BoxCollider collider = mf.gameObject.AddComponent<BoxCollider>();
                        collider.center = bounds.center;
                        collider.size = new Vector3(bounds.size.x, bounds.size.y * 2, bounds.size.z);
                        collider.isTrigger = true;
                    }
                }
            }
            ponerLateral(lateral, suelo, j);
        }

        return posicion + BLOQUES_ESQUIADORES;

    }



    public static void ponerLateral(GameObject lateral,GameObject suelo, int i)
    {

        for(float left = -4.5f; left > -10.5f; left += -1f)
        {
            GameObject lat = Instantiate(lateral, new Vector3(left, 0f, i), new Quaternion());
            lat.transform.parent = suelo.transform;
        }
        
        for (float right = 4.5f; right < 10.5f; right += 1f)
        {
            GameObject lat = Instantiate(lateral, new Vector3(right, 0f, i), new Quaternion());
            lat.transform.parent = suelo.transform;
        }

    }

    public static void createCoche(GameObject parent, int i,bool derecha){
            int car = Random.Range(1, 14);
            GameObject coche = Instantiate(getCoche(car), new Vector3(derecha ? Random.Range(10, 13) : Random.Range(-13, -9), getYSizeOfCar(car), i), new Quaternion());
            if(derecha){ coche.transform.Rotate(new Vector3(0,180,0)); }
            coche.transform.parent = parent.transform;
    }

    public static void createTronco(GameObject parent, int i, bool derecha)
    {

        GameObject t = Instantiate(tronco, new Vector3(derecha ? -10 : 10, 0, i), new Quaternion());
        t.transform.parent = parent.transform;
    }

    public static void createTrineo(int i, int pos)
    {
        int xInicial = Random.Range(1, 5);
        GameObject t = Instantiate(trineo, new Vector3(pos, 0.5f, i), new Quaternion());
        t.transform.parent = suelo.transform;
        t.transform.Rotate(new Vector3(270, 0, 180));
        //t.transform.localScale = new Vector3(10, 45, 45);
    }

    private static GameObject getCoche(int i){
        switch(i){
            case 1: return (GameObject)Resources.Load("Cars/Prefabs/Bus_Blue"); 
            case 2: return (GameObject)Resources.Load("Cars/Prefabs/Bus_Red"); 
            case 3: return (GameObject)Resources.Load("Cars/Prefabs/Bus_Yellow"); 
            case 4: return (GameObject)Resources.Load("Cars/Prefabs/Car_2_Green"); 
            case 5: return (GameObject)Resources.Load("Cars/Prefabs/Car_2_Purple"); 
            case 6: return (GameObject)Resources.Load("Cars/Prefabs/Car_2_Silver"); 
            case 7: return (GameObject)Resources.Load("Cars/Prefabs/Car_5_Red"); 
            case 8: return (GameObject)Resources.Load("Cars/Prefabs/Car_5_Silver"); 
            case 9: return (GameObject)Resources.Load("Cars/Prefabs/Car_5_Yellow"); 
            case 10: return (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Blue"); 
            case 11: return (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Red"); 
            case 12: return (GameObject)Resources.Load("Cars/Prefabs/Truck_1_Purple"); 
            case 13: return (GameObject)Resources.Load("Cars/Prefabs/Policecar"); 
            default: return (GameObject)Resources.Load("Cars/Prefabs/Bus_Yellow"); 
        }
    }

    public static int getPosX(int min, int max){
        int x = Random.Range(min, max);
        switch(x){
            case 1: return -3; 
            case 2: return -1; 
            case 3: return 1; 
            case 4: return 3; 
            default: return 1;
        }
    }

    private static GameObject getFruta(){
        int fruta = Random.Range(1, 9);
        switch(fruta){
            case 1: return (GameObject)Resources.Load("Prefabs/apple"); 
            case 2: return (GameObject)Resources.Load("Prefabs/banana"); 
            case 3: return (GameObject)Resources.Load("Prefabs/cherries"); 
            case 4: return (GameObject)Resources.Load("Prefabs/lemon"); 
            case 5: return (GameObject)Resources.Load("Prefabs/peach"); 
            case 6: return (GameObject)Resources.Load("Prefabs/pear"); 
            case 7: return (GameObject)Resources.Load("Prefabs/strawberry"); 
            case 8: return (GameObject)Resources.Load("Prefabs/watermelon"); 
            default: return (GameObject)Resources.Load("Prefabs/apple");
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
    private static float getXtrineo(int i){
        switch(i){
            case 1: return -3; 
            case 2: return -1; 
            case 3: return 1; 
            case 4: return 3; 
            default: return 1;
        }
    }

}
