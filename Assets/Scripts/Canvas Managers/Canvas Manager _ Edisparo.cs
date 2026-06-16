using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager_Edisparo : MonoBehaviour
{
    public GameObject canvasInicialIndicaciones;
    public GameObject canvasSegundoIndicaciones;
    public GameObject canvasTerceroIndicaciones;
    public GameObject canvasCuartoIndicaciones;

    public float tiempoEntreIndicaciones = 1.5f;

    private bool hasShownCanvases = false; // Bandera para evitar que se muestren dos veces

    // Método para ser llamado cuando se ingresen la velocidad y el ángulo
    public void OnDataInputCompleted()
    {
        if (!hasShownCanvases)
        {
            // Desactivar el Canvas inicial
            canvasInicialIndicaciones.SetActive(false);

            // Activar el Canvas de segunda indicación
            canvasSegundoIndicaciones.SetActive(true);

            // Iniciar la secuencia para mostrar y ocultar los otros canvases
            StartCoroutine(ShowNextCanvases());
        }
    }

    private IEnumerator ShowNextCanvases()
    {
        yield return new WaitForSeconds(tiempoEntreIndicaciones);

        canvasSegundoIndicaciones.SetActive(false);
        canvasTerceroIndicaciones.SetActive(true);

        yield return new WaitForSeconds(tiempoEntreIndicaciones);

        canvasTerceroIndicaciones.SetActive(false);
        canvasCuartoIndicaciones.SetActive(true);

        // Marcar como ya mostrado para que no se vuelva a ejecutar
        hasShownCanvases = true;
    }
}
