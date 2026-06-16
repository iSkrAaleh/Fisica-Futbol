using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    void Start()
    {
        // Verificar si la posición del jugador ha sido guardada
        if (PlayerPrefs.HasKey("playerX"))
        {
            // Recuperar las coordenadas guardadas
            float x = PlayerPrefs.GetFloat("playerX");
            float y = PlayerPrefs.GetFloat("playerY");
            float z = PlayerPrefs.GetFloat("playerZ");

            // Mover al jugador a la posición guardada
            transform.position = new Vector3(x, y, z);
        }
    }
}

