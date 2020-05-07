using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BloquesType
{
    Nieve,
    Obstaculo,
    Agua
}

public class Celda
{
    private List<BloquesType> celdas;

    public Celda(List<BloquesType> celdas)
    {
        this.celdas = celdas;
    }
    
    public BloquesType GetCelda(int position)
    {
        return celdas[position];
    }

}
