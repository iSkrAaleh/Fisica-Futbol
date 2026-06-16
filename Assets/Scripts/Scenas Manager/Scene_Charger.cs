using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que se cambiará
    private bool isPlayerInZone = false; // Verifica si el jugador está en la zona
    private GameObject player; // Referencia al jugador

    void Start()
    {
        // Encontrar al jugador por su etiqueta (Asegúrate de que el objeto jugador tiene el tag "Player")
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    void Update()
    {
        // Si el jugador está en la zona y presiona la tecla F
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                // Guardar la posición del jugador antes de cambiar de escena
                SavePlayerPosition();

                // Cargar la nueva escena
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("Error: El nombre de la escena no está asignado.");
            }
        }
    }

    // Función para guardar la posición del jugador
    void SavePlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;
        PlayerPrefs.SetFloat("playerX", playerPosition.x);
        PlayerPrefs.SetFloat("playerY", playerPosition.y);
        PlayerPrefs.SetFloat("playerZ", playerPosition.z);

        // Guardar los cambios
        PlayerPrefs.Save();
    }
}
