using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotData
{
    public int shotNumber;   // Número del disparo
    public float time;       // Tiempo del disparo
    public float angle;      // Ángulo del disparo
    public float speed;      // Velocidad inicial
    public float distance;   // Distancia recorrida
    public float height;     // Altura alcanzada

    public ShotData(int shotNumber, float time, float angle, float speed, float distance, float height)
    {
        this.shotNumber = shotNumber;
        this.time = time;
        this.angle = angle;
        this.speed = speed;
        this.distance = distance;
        this.height = height;
    }
}


