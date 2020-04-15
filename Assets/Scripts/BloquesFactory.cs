using UnityEngine;

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
        GameObject tronco = (GameObject)Resources.Load("Prefabs/Tronco");

        int aux = inicio;
        distancia = distancia * 2;

        for (int i = aux; i < aux + distancia; i += 2)
        {
            GameObject obj;
            int bloque = Random.Range(1, 6);

            switch (bloque)
            {
                case 1:
                    obj = Instantiate(bloque1, new Vector3(0, 0, i), new Quaternion());
                    obj.transform.parent = suelo.transform;
                    break;
                case 2:
                    obj = Instantiate(bloque2, new Vector3(0, 0, i), new Quaternion());
                    obj.transform.parent = suelo.transform;
                    break;
                case 3:
                    obj = Instantiate(bloque3, new Vector3(0, 0, i), new Quaternion());
                    obj.transform.parent = suelo.transform;
                    break;
                case 4:
                    obj = Instantiate(bloqueRoca, new Vector3(0, 0, i), new Quaternion());
                    obj.transform.parent = suelo.transform;
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
                    obj = Instantiate(bloqueNieve, new Vector3(0, 0, i), new Quaternion());
                    obj.transform.parent = suelo.transform;
                    break;

            }

        }
        inicio = inicio + distancia - 5;
    }
}
