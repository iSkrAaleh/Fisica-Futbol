using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform player; // Asigna el jugador desde el editor
    public Transform net; // Asigna la red desde el editor

    void Update()
    {
        // Mantén la cámara sobre el jugador y haz que mire hacia la red
        Vector3 targetPosition = new Vector3(net.position.x, transform.position.y, net.position.z);
        transform.LookAt(targetPosition);

        // Si la cámara debe seguir al jugador, también mueve su posición
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z - 5); // Ajusta la distancia de la cámara respecto al jugador
    }
}

