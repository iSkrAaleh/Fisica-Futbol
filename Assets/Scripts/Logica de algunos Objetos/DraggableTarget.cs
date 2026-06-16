using UnityEngine;

public class DraggableTarget : MonoBehaviour
{
    public string type;  // Tipo del target: "Tiempo", "Ángulo", "Velocidad", "Distancia", "Altura"
    public int targetID;  // ID del target
    public bool isOccupied = false;


}

