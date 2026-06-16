using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvas; // Asigna el canvas desde el inspector

    private void Start()
    {
        // Asegúrate de que el canvas esté desactivado al inicio
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa el canvas cuando el jugador entra en el área
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactiva el canvas cuando el jugador sale del área
            canvas.SetActive(false);
        }
    }
}


