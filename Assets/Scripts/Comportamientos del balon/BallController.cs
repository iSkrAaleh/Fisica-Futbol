using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public CanvasManager_Edisparo canvasManager;  // Referencia al CanvasManager
    public InputField angleInputField;   // Campo para el ángulo
    public InputField speedInputField;   // Campo para la velocidad inicial
    public GameObject startButton;       // Botón de inicio
    public Text timerText;               // Texto del cronómetro
    public GameObject retryCanvas;       // Canvas que muestra "Inténtelo de nuevo"
    public Animator characterAnimator;
    public TiroLogger tiroLogger;
    public Canvas invalidValuesCanvas;   // Canvas para mostrar el mensaje de error
    private TrailRenderer trailRenderer; // Trail render 


    private float angle;                 // Valor del ángulo
    private float initialSpeed;          // Valor de la velocidad inicial
    private Rigidbody rb;                // Rigidbody del balón
    private bool isBallMoving = false;   // Verifica si el balón está en movimiento
    private float timeElapsed = 0f;      // Tiempo transcurrido
    private Vector3 initialPosition;     // Posición inicial del balón
    private float gravedad = 9.81f;      // Gravedad constante
    private float tiempo = 0f;           // Tiempo del movimiento parabólico
    private float maxHeight = 0f;        // Almacenar la altura máxima alcanzada

    void Start()
    {
        rb = GetComponent<Rigidbody>();               // Obtener el componente Rigidbody del balón
        initialPosition = transform.position;         // Guardar la posición inicial del balón
        startButton.SetActive(false);                 // Desactivar el botón de inicio al principio
        retryCanvas.SetActive(false);                 // Desactivar el canvas de "inténtelo de nuevo" al inicio
        invalidValuesCanvas.gameObject.SetActive(false);  // Desactivar el canvas de "valores no válidos"
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
    public void OnInputChanged()
    {
        // Verificar si los campos tienen texto antes de intentar analizar los valores
        bool isAngleFilled = !string.IsNullOrEmpty(angleInputField.text);
        bool isSpeedFilled = !string.IsNullOrEmpty(speedInputField.text);

        // Solo intenta analizar los valores si los campos no están vacíos
        bool isAngleValid = isAngleFilled && float.TryParse(angleInputField.text, out angle) && angle >= 0 && angle <= 45;
        bool isSpeedValid = isSpeedFilled && float.TryParse(speedInputField.text, out initialSpeed) && initialSpeed <= 60;

        if (isAngleFilled && isSpeedFilled)
        {
            if (isAngleValid && isSpeedValid)
            {
                startButton.SetActive(true);  // Activar el botón si los valores son válidos
                invalidValuesCanvas.gameObject.SetActive(false); // Ocultar el mensaje de error
                canvasManager.OnDataInputCompleted();
            }
            else
            {
                startButton.SetActive(false);  // Desactivar el botón de inicio si algún valor no es válido
                invalidValuesCanvas.gameObject.SetActive(true);  // Mostrar el mensaje de error
            }
        }
        else
        {
            startButton.SetActive(false);  // Desactivar el botón si los campos están vacíos
            invalidValuesCanvas.gameObject.SetActive(false);  // Ocultar el mensaje de error
        }
    }



    // Animación pateo inicio
    public void StartKick()
    {
        // Activar la animación de patear
        characterAnimator.SetTrigger("Kick");

        // Esperar a que termine la animación antes de iniciar el movimiento de la pelota
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        while (!characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Strike") ||
               characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;  // Esperar hasta que la animación termine
        }

        StartMovement();  // Inicia el movimiento después de que la animación termine
    }

    public Transform net; // Asigna el objeto de la red desde el editor

    private Vector3 directionToNet; // Vector que guarda la dirección hacia la red

   public void StartMovement()
{
    // Este método solo se llamará si isBallMoving es false
    if (!isBallMoving) 
    {
        isBallMoving = true;
        tiempo = 0f;
        timeElapsed = 0f;
        maxHeight = 0f;                  // Reiniciar la altura máxima al iniciar un nuevo tiro
        initialPosition = transform.position; // Guardar la posición inicial al momento del movimiento

        // Calcular la dirección hacia la red
        directionToNet = (net.position - initialPosition).normalized; // Normalizar para obtener solo la dirección

        angle = Mathf.Deg2Rad * angle;  // Convertir el ángulo a radianes

        canvasManager.OnDataInputCompleted();
        startButton.SetActive(false);
        
        trailRenderer.enabled = true;
    }
}

    void Update()
    {
        if (isBallMoving)
        {
            timeElapsed += Time.deltaTime;
            timerText.text = timeElapsed.ToString("F2");

            tiempo += Time.deltaTime;

            // Cálculo de la nueva posición de la pelota
            float x = initialSpeed * Mathf.Cos(angle) * tiempo;
            float y = (initialSpeed * Mathf.Sin(angle) * tiempo) - (0.5f * gravedad * tiempo * tiempo);

            // Actualiza la altura máxima alcanzada
            if (y > maxHeight)
            {
                maxHeight = y;  // Actualiza la altura máxima si la altura actual es mayor
            }

            // Aplicar movimiento hacia la red en lugar de solo en el eje X
            Vector3 movement = directionToNet * x;  // Multiplica la dirección hacia la red por la distancia en X
            movement.y = y;  // Mantiene el cálculo de Y con gravedad

            // Actualizar la posición de la pelota en dirección a la red
            transform.position = initialPosition + movement;

            // Verificar si la pelota ha caído al suelo (posición en Y <= inicial)
            if (transform.position.y <= initialPosition.y && isBallMoving)
            {
                // Detenemos el movimiento
                isBallMoving = false;

                // Calcula la distancia recorrida
                float distance = Vector3.Distance(initialPosition, transform.position);

                // Llama a la función para registrar el tiro con la altura máxima
                if (TiroLogger.Instance != null)
                {
                    TiroLogger.Instance.RegistrarTiro(timeElapsed, angle * Mathf.Rad2Deg, initialSpeed, distance, maxHeight);
                }

                // Solo reiniciar la pelota si no tocó la red
                StartCoroutine(ShowRetryMessage());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Net"))
        {
            // Registra el tiro al tocar la red con la altura máxima
            float distance = Vector3.Distance(initialPosition, transform.position);
            if (TiroLogger.Instance != null)
            {
                TiroLogger.Instance.RegistrarTiro(timeElapsed, angle * Mathf.Rad2Deg, initialSpeed, distance, maxHeight);
            }
            trailRenderer.enabled = false;
            // Cambiar de escena o hacer algo más
            SceneManager.LoadScene("Scena PreguntaTiro");
        }
    }
    private IEnumerator ShowRetryMessage()
    {
        retryCanvas.SetActive(true);  // Mostrar el mensaje "Inténtelo de nuevo"
        yield return new WaitForSeconds(2);  // Esperar 2 segundos
        retryCanvas.SetActive(false);  // Ocultar el mensaje

        // Desactivar el Trail Renderer antes de reiniciar la posición
        trailRenderer.enabled = false;

        // Detener el movimiento de la pelota
        isBallMoving = false;

        // Detener el movimiento físico de la pelota
        rb.velocity = Vector3.zero;

        // Reiniciar la posición del balón a su posición inicial
        transform.position = initialPosition;

        // Aquí puedes mantener isBallMoving en false
        // Solo habilitar el botón de inicio para intentar de nuevo
        startButton.SetActive(true);
    }

}
