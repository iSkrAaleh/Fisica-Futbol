using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TablaTirosControl : MonoBehaviour
{
    public TMP_InputField[] tiempoFields;
    public TMP_InputField[] anguloFields;
    public TMP_InputField[] velocidadFields;
    public TMP_InputField[] distanciaFields;
    public TMP_InputField[] alturaFields;

    public GameObject[] vistoDistanciaIcons; // Íconos de "Visto" para distancia
    public GameObject[] errorDistanciaIcons; // Íconos de "Error" para distancia
    public GameObject[] vistoAlturaIcons;    // Íconos de "Visto" para altura
    public GameObject[] errorAlturaIcons;    // Íconos de "Error" para altura


    public TextMeshProUGUI aciertosText;
    public TextMeshProUGUI erroresText;

    private UIManager uiManager;
    private float[] tiempos = new float[3];
    private float[] angulos = new float[3];
    private float[] velocidades = new float[3];
    private float[] distanciasCalculadas = new float[3];
    private float[] alturasCalculadas = new float[3];
    private const float gravedad = 9.81f;
    private const float margenError = 0.1f; // Margen de error del 10% (10% = 0.1)

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        RandomizarValores();

        // Suscribirse al evento de cambio de texto en los campos de distancia y altura
        for (int i = 0; i < 3; i++)
        {
            distanciaFields[i].onValueChanged.AddListener(delegate { RevisarCamposCompletos(); });
            alturaFields[i].onValueChanged.AddListener(delegate { RevisarCamposCompletos(); });
        }

        OcultarIconos(); // Ocultar los íconos al iniciar
    }

    public void RandomizarValores()
    {
        for (int i = 0; i < 3; i++)
        {
            tiempos[i] = Random.Range(1.0f, 3.0f);
            angulos[i] = Random.Range(30f, 50f);
            velocidades[i] = Random.Range(20f, 35f);

            float anguloRad = Mathf.Deg2Rad * angulos[i];
            distanciasCalculadas[i] = (velocidades[i] * tiempos[i]) * Mathf.Cos(anguloRad);
            alturasCalculadas[i] = (velocidades[i] * tiempos[i] * Mathf.Sin(anguloRad)) - (0.5f * gravedad * Mathf.Pow(tiempos[i], 2));

            if (alturasCalculadas[i] < 0) alturasCalculadas[i] = 0; // Asegurar altura positiva

            tiempoFields[i].text = tiempos[i].ToString("F2");
            anguloFields[i].text = angulos[i].ToString("F2");
            velocidadFields[i].text = velocidades[i].ToString("F2");
            distanciaFields[i].text = "";
            alturaFields[i].text = "";
        }

        OcultarIconos(); // Ocultar íconos tras randomizar valores
        uiManager.MostrarBotonVerificar(false); // Ocultar botón al randomizar valores
    }

    private void RevisarCamposCompletos()
    {
        bool todosCompletos = true;
        for (int i = 0; i < 3; i++)
        {
            if (string.IsNullOrEmpty(distanciaFields[i].text) || string.IsNullOrEmpty(alturaFields[i].text))
            {
                todosCompletos = false;
                break;
            }
        }

        uiManager.MostrarBotonVerificar(todosCompletos); // Mostrar o ocultar botón
    }
    private void OcultarIconos()
    {
        // Oculta todos los íconos de "visto" y "error" para distancia y altura
        foreach (var icon in vistoDistanciaIcons) icon.SetActive(false);
        foreach (var icon in errorDistanciaIcons) icon.SetActive(false);
        foreach (var icon in vistoAlturaIcons) icon.SetActive(false);
        foreach (var icon in errorAlturaIcons) icon.SetActive(false);
    }

    public void ValidarRespuestas()
    {
        int aciertosTotales = 0;
        int erroresTotales = 0;

        for (int i = 0; i < 3; i++)
        {
            // Recuperar valores ingresados por el usuario
            float distanciaJugador, alturaJugador;
            bool distanciaValida = float.TryParse(distanciaFields[i].text, out distanciaJugador);
            bool alturaValida = float.TryParse(alturaFields[i].text, out alturaJugador);

            // Calcular el margen de error en base al 10% del valor calculado
            float margenDistancia = distanciasCalculadas[i] * margenError;
            float margenAltura = alturasCalculadas[i] * margenError;

            bool distanciaCorrecta = false;
            bool alturaCorrecta = false;

            // Validar distancia
            if (distanciaValida)
            {
                float diferenciaDistancia = Mathf.Abs(distanciaJugador - distanciasCalculadas[i]);
                distanciaCorrecta = diferenciaDistancia <= margenDistancia;
            }

            // Validar altura
            if (alturaValida)
            {
                float diferenciaAltura = Mathf.Abs(alturaJugador - alturasCalculadas[i]);
                alturaCorrecta = diferenciaAltura <= margenAltura;
            }

            // Actualizar íconos de resultado para distancia
            if (distanciaCorrecta)
            {
                aciertosTotales++;
                vistoDistanciaIcons[i].SetActive(true);
                errorDistanciaIcons[i].SetActive(false);
            }
            else
            {
                erroresTotales++;
                vistoDistanciaIcons[i].SetActive(false);
                errorDistanciaIcons[i].SetActive(true);
            }

            // Actualizar íconos de resultado para altura
            if (alturaCorrecta)
            {
                aciertosTotales++;
                vistoAlturaIcons[i].SetActive(true);
                errorAlturaIcons[i].SetActive(false);
            }
            else
            {
                erroresTotales++;
                vistoAlturaIcons[i].SetActive(false);
                errorAlturaIcons[i].SetActive(true);
            }
        }

        // Actualizar la interfaz con el conteo de aciertos y errores
        aciertosText.text = "  " + aciertosTotales;
        erroresText.text = "  "  +  erroresTotales;
    }

}
