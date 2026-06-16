using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public Transform net; // Asigna el objeto de la red desde el editor

    void Update()
    {
        // Asegúrate de que el jugador siempre mire hacia la red
        if (net != null)
        {
            Vector3 targetPosition = new Vector3(net.position.x, transform.position.y, net.position.z);
            transform.LookAt(targetPosition);
        }
    }
}
