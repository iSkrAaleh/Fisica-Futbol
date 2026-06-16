using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvasTiros; // Canvas de la tabla de tiros
    public GameObject canvasEcuaciones; // Canvas de ecuaciones

    // Método para mostrar el Canvas de ecuaciones y ocultar el de tiros
    public void MostrarEcuaciones()
    {
        canvasEcuaciones.SetActive(true);
        canvasTiros.SetActive(false);
    }

    // Método para ocultar el Canvas de ecuaciones y mostrar el de tiros
    public void CerrarEcuaciones()
    {
        canvasEcuaciones.SetActive(false);
        canvasTiros.SetActive(true);
    }
}

