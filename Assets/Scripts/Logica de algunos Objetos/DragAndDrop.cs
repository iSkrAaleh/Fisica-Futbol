using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private Transform originalParent;    // Contenedor original del objeto
    private GameObject currentTarget;
    private GameManager gameManager;     // Referencia al GameManager
    private Draggable draggable;         // Referencia al componente Draggable

    public GameObject dragLayer;         // Contenedor específico para arrastre

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Busca el GameManager en la escena
        draggable = GetComponent<Draggable>();          // Obtiene el componente Draggable

        // Guardamos el contenedor original
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        // Mueve el objeto al contenedor de arrastre para que quede al frente
        if (dragLayer != null)
        {
            transform.SetParent(dragLayer.transform, true); // Mueve el objeto al contenedor de arrastre
        }

        if (currentTarget != null)
        {
            DraggableTarget target = currentTarget.GetComponent<DraggableTarget>();
            target.isOccupied = false;  // Liberar el espacio cuando el objeto se arrastra
            currentTarget.SetActive(true);
            currentTarget = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Regresa el objeto al contenedor original
        transform.SetParent(originalParent, true);

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        RaycastResult hit = raycastResults.Find(x => x.gameObject.CompareTag("TargetArea"));

        if (hit.gameObject != null)
        {
            DraggableTarget target = hit.gameObject.GetComponent<DraggableTarget>();

            // Verificar si el target está ocupado
            if (!target.isOccupied)
            {
                // Nueva verificación: verificar si el tipo coincide
                if (draggable.type == target.type)
                {
                    // Si coincide el tipo, colocamos el objeto
                    transform.position = hit.gameObject.transform.position;
                    hit.gameObject.SetActive(false);
                    target.isOccupied = true; // Marca el target como ocupado
                    currentTarget = hit.gameObject;
                    draggable.SetTargetID(target.targetID);
                    gameManager.CheckIfAllPlaced();
                }
                else
                {
                    // Si el tipo no coincide, volver a la posición original
                    transform.position = originalPosition;
                    Debug.Log("Tipo incorrecto. No se puede colocar aquí.");
                }
            }
            else
            {
                // Si el target está ocupado, vuelve a la posición original
                transform.position = originalPosition;
            }
        }
        else
        {
            // Vuelve a la posición original si no toca ningún target
            transform.position = originalPosition;
        }
    }
}
