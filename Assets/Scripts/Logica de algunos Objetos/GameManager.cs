using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetAreas;       // Referencia a todos los espacios blancos
    public Button verificarButton;         // Referencia al botón de verificar
    public List<Draggable> draggables;     // Lista de todos los arrastrables
    public List<DraggableTarget> targets;  // Lista de todos los targets (espacios blancos)

    public TMP_Text aciertosText;
    public TMP_Text erroresText;
    public GameObject resultadoPanel;      // Panel que se mostrará al terminar la verificación
    public GameObject reintentarButton;
    public GameObject mostrarTablaButton;

    public Image[] correctImages;          // Array de imágenes para mostrar "visto"
    public Image[] incorrectImages;        // Array de imágenes para mostrar "X"

    public GameObject dragLayer;           // Contenedor específico para arrastrar

    private int aciertos = 0;
    private int errores = 0;

    private void Start()
    {
        verificarButton.gameObject.SetActive(false); // Oculta el botón al inicio
        resultadoPanel.SetActive(false);             // Oculta el panel de resultados y botones al inicio
        reintentarButton.SetActive(false);
        mostrarTablaButton.SetActive(false);

        // Asegurar que las imágenes de correct/incorrect estén desactivadas al inicio
        foreach (var img in correctImages) img.gameObject.SetActive(false);
        foreach (var img in incorrectImages) img.gameObject.SetActive(false);

        // Asignar el dragLayer a cada Draggable
        foreach (Draggable draggable in draggables)
        {
            var dragComponent = draggable.GetComponent<DragAndDrop>();
            if (dragComponent != null)
            {
                dragComponent.dragLayer = dragLayer;
            }
        }
    }

    // Método para activar el botón cuando todos los arrastrables estén en su lugar
    public void CheckIfAllPlaced()
    {
        foreach (GameObject target in targetAreas)
        {
            if (target.activeSelf)
            {
                return; // Si algún espacio en blanco sigue activo, no mostramos el botón
            }
        }
        verificarButton.gameObject.SetActive(true); // Activa el botón cuando todos los arrastrables estén en su lugar
    }

    // Método para verificar aciertos y errores
    public void VerificarAciertosYErrores()
    {
        aciertos = 0;
        errores = 0;

        // Agrupar elementos por las filas donde fueron colocados (targetID)
        Dictionary<int, List<Draggable>> groupedElements = new Dictionary<int, List<Draggable>>();
        foreach (Draggable draggable in draggables)
        {
            int row = draggable.GetPlacedRow();
            if (!groupedElements.ContainsKey(row))
            {
                groupedElements[row] = new List<Draggable>();
            }
            groupedElements[row].Add(draggable);
        }

        // Verificar que los elementos de cada fila pertenezcan al mismo grupo y actualizar el estado de las imágenes
        foreach (var group in groupedElements)
        {
            int referenceGroupID = group.Value[0].groupID; // Tomar el ID de referencia del primer elemento
            foreach (var draggable in group.Value)
            {
                if (draggable.groupID == referenceGroupID)
                {
                    aciertos++;
                    correctImages[draggables.IndexOf(draggable)].gameObject.SetActive(true);
                    incorrectImages[draggables.IndexOf(draggable)].gameObject.SetActive(false);
                }
                else
                {
                    errores++;
                    incorrectImages[draggables.IndexOf(draggable)].gameObject.SetActive(true);
                    correctImages[draggables.IndexOf(draggable)].gameObject.SetActive(false);
                }
            }
        }

        aciertosText.text = "Aciertos: " + aciertos;
        erroresText.text = "Errores: " + errores;

        // Ocultar el botón de verificar tras la verificación
        verificarButton.gameObject.SetActive(false);

        // Mostrar el panel de resultados
        resultadoPanel.SetActive(true);

        // Activar los botones de reintentar y mostrar tabla
        reintentarButton.SetActive(true);
        mostrarTablaButton.SetActive(true);
    }

    public void Reintentar()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MostrarTablaOIrAlInicio()
    {
        // Aquí cambias a la escena de inicio o la tabla que desees
        SceneManager.LoadScene("SceneInicio");
    }
}
