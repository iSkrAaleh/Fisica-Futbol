using UnityEngine;

public class Draggable : MonoBehaviour
{
    public string type;  // Tipo del arrastrable: "Tiempo", "Ángulo", "Velocidad", "Distancia", "Altura"
    public int groupID;  // ID del grupo al que pertenece el arrastrable
    private int targetID; // ID del target donde fue colocado
    private Vector3 initialPosition; // Almacena la posición inicial del arrastrable

    // Se llama al inicio para almacenar la posición inicial del objeto
    private void Start()
    {
        initialPosition = transform.position;
    }

    // Método que se llama cuando se coloca en un target
    public void SetTargetID(int id)
    {
        targetID = id;
    }

    // Devuelve el targetID (fila) donde el Draggable fue colocado
    public int GetPlacedRow()
    {
        return targetID;
    }

    // Resetea el objeto a su posición inicial
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
