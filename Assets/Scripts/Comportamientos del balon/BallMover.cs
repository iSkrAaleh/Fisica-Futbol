using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    private Rigidbody rb;
    public Target targetScript;
    private TrailRenderer trailRenderer; // Añadido para el Trail Renderer

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Buscar o añadir un Trail Renderer
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
            trailRenderer.startWidth = 0.1f; // Ajustar el ancho inicial
            trailRenderer.endWidth = 0.1f;   // Ajustar el ancho final
            Material trailMaterial = new Material(Shader.Find("Unlit/Color"));
            trailMaterial.color = Color.red; // Cambiar el color del Trail
            trailRenderer.material = trailMaterial; // Asignar el material al Trail Renderer
            trailRenderer.time = 1.0f; // Tiempo que el Trail estará visible
        }

        // Desactivar el Trail Renderer al inicio
        trailRenderer.enabled = false;
    }

    public void MoveToTarget(Vector3 targetPosition, float force)
    {
        rb.velocity = Vector3.zero;
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);

        // Guardar velocidad y ángulo
        float velocidad = force;
        float angulo = Vector3.Angle(direction, Vector3.forward);
        PlayerPrefs.SetFloat("Velocidad", velocidad);
        PlayerPrefs.SetFloat("Angulo", angulo);
        PlayerPrefs.Save();

        // Activar el Trail Renderer al iniciar el movimiento
        trailRenderer.enabled = true;

        StartCoroutine(CheckIfBallReachedTarget(targetPosition));
    }

    private IEnumerator CheckIfBallReachedTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            yield return null;
        }

        // Desactivar el Trail Renderer cuando la pelota se detiene
        trailRenderer.enabled = false;

        targetScript.OnBallReachedTarget();
    }
}
