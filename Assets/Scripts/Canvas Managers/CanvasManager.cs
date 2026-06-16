using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject initialCanvas;   // Canvas que contiene las instrucciones iniciales
    public GameObject nextCanvas;      // Canvas que se mostrará después de presionar teclas o mover el mouse
    public GameObject finalCanvas;     // Canvas que se mostrará después de cierto tiempo
    public GameObject anotherCanvas;   // Canvas que se mostrará después del finalCanvas
    public GameObject lastCanvas;      // Canvas que se mostrará después del anotherCanvas

    private bool canvasSwitched = false; // Bandera para verificar si ya se hizo el cambio
    public float timeToShowFinalCanvas = 5f; // Tiempo en segundos para mostrar el finalCanvas
    public float timeToShowAnotherCanvas = 5f; // Tiempo en segundos para mostrar el anotherCanvas
    public float timeToShowLastCanvas = 5f; // Tiempo en segundos para mostrar el lastCanvas

    void Start()
    {
        initialCanvas.SetActive(true);
        nextCanvas.SetActive(false);
        finalCanvas.SetActive(false);
        anotherCanvas.SetActive(false);
        lastCanvas.SetActive(false);
    }

    void Update()
    {
        // Solo cambia los Canvas si aún no se ha realizado el cambio
        if (!canvasSwitched)
        {
            // Detecta si se presionan las teclas W, A, S, D o las flechas
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Cambia los Canvas
                initialCanvas.SetActive(false);
                nextCanvas.SetActive(true);
                canvasSwitched = true; // Marca que el cambio ha ocurrido

                // Inicia la corrutina para mostrar los siguientes canvases en secuencia
                StartCoroutine(ShowCanvasSequence());
            }
        }

        // Cambia los Canvas si detecta movimiento del mouse
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            nextCanvas.SetActive(false);
        }
    }

    IEnumerator ShowCanvasSequence()
    {
        // Espera el tiempo especificado para mostrar el finalCanvas
        yield return new WaitForSeconds(timeToShowFinalCanvas);
        nextCanvas.SetActive(false);
        finalCanvas.SetActive(true);

        // Espera el tiempo especificado para mostrar el anotherCanvas
        yield return new WaitForSeconds(timeToShowAnotherCanvas);
        finalCanvas.SetActive(false);
        anotherCanvas.SetActive(true);

        // Espera el tiempo especificado para mostrar el lastCanvas
        yield return new WaitForSeconds(timeToShowLastCanvas);
        anotherCanvas.SetActive(false);
        lastCanvas.SetActive(true);
    }
}
