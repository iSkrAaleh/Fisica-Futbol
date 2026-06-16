using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // El jugador
    public Transform net; // La red (o portería)

    public float distanceBehindPlayer = 5.0f; // Distancia detrás del jugador en el eje Z
    public float heightOffset = 2.0f; // Desplazamiento vertical para que la cámara esté más alta que el jugador

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Posicionar la cámara detrás del jugador en el eje Z, alineada con el jugador en el eje X
        Vector3 newPosition = new Vector3(player.position.x, player.position.y + heightOffset, player.position.z - distanceBehindPlayer);
        transform.position = newPosition;

        // Hacer que la cámara mire hacia donde el jugador está mirando, copiando la rotación del jugador
        transform.rotation = player.rotation;

        // Si quieres que la cámara mire hacia la red y no solo en la dirección del jugador
        if (net != null)
        {
            Vector3 targetPosition = new Vector3(net.position.x, transform.position.y, net.position.z);
            transform.LookAt(targetPosition);
        }
    }
}
