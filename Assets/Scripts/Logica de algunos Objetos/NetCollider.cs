using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetCollider : MonoBehaviour
{
    // Método que se ejecuta cuando algo entra en el trigger de la red
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que ha colisionado es la pelota
        if (other.gameObject.CompareTag("Ball"))  // Asegúrate de que la pelota tenga el tag "Ball"
        {
            // Cambiar de escena cuando la pelota toque la red
            SceneManager.LoadScene("Scena PreguntaTiro");  
        }
    }
}