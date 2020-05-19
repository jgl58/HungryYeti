using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BloquesType
{
    Nieve,
    Obstaculo,
    Agua,
    Carretera
}

public class Celda
{
    private List<BloquesType> celdas;
    private List<int> centerPoints;

    public Celda(List<BloquesType> celdas,List<int> points)
    {
        this.celdas = celdas;
        this.centerPoints = points;
    }
    
    public BloquesType GetCelda(int position)
    {
        return celdas[position];
    }

    public int GetCentro(int position)
    {
        return centerPoints[position];
    }

    public int getColumnaPlayer(GameObject player)
    {
        //Columnas [Columna1,Columna2,Columna3,Columna4]
        float posX = player.transform.position.x;
        int columna = 0;

        if (posX >= -4f && posX < -2f)//Columna 1
        {
            columna = 0;
        }
        else if (posX >= -2f && posX < 0f)//Columna 2
        {
            columna = 1;
        }
        else if (posX >= 0f && posX < 2f)//Columna 3
        {
            columna = 2;
        }
        else//Columna 4
        {
            columna = 3;
        }
        return columna;
    }

    public bool comprobarCaminoLado(GameObject player, bool lado)
    {
        //lado = true -> izquierda ||lado = false -> derecha
        //Columnas [Columna1,Columna2,Columna3,Columna4]
        int columna = getColumnaPlayer(player);

        if (!lado)//miramos derecha
        {
            if (getColumnaPlayer(player) == 3) { return false; }
            return (GetCelda(columna + 1) != BloquesType.Obstaculo);
        }
        else //miramos izquierda
        {
            if (getColumnaPlayer(player) == 0) { return false; }
            return (GetCelda(columna - 1) != BloquesType.Obstaculo);
        }

    }


    public bool comprobarCaminoArriba(GameObject player)
    {
        int columna = getColumnaPlayer(player);
        Celda celdaSiguiente = Juego.camino.First.Next.Value;

        if (celdaSiguiente.GetCelda(columna) != BloquesType.Obstaculo)
        { 
            Juego.camino.RemoveFirst();
            return true;
        }
        return false;
    }

    public void recolocarPlayer(GameObject player)
    {
        int columna = getColumnaPlayer(player);
        Vector3 position = new Vector3(
            GetCentro(columna),
            player.transform.position.y,
            player.transform.position.z
            );
        player.transform.position = position;
    }


    public Vector3 moverDerecha(GameObject player)
    {

        if (comprobarCaminoLado(player, false))
        {

            return new Vector3(
                GetCentro(getColumnaPlayer(player) + 1),
                player.transform.position.y,
                player.transform.position.z
                );
        }
        
        return player.transform.position;
        
    }

    public Vector3 moverIzquierda(GameObject player)
    {

        if (comprobarCaminoLado(player, true))
        {

            return new Vector3(
                GetCentro(getColumnaPlayer(player) - 1),
                player.transform.position.y,
                player.transform.position.z
                );
        }

        return player.transform.position;

    }


}
