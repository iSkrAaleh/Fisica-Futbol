using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Button verificarButton;
    public Button reintentarButton;
    public Button salirButton;
    public GameObject panelResultados; // Panel que contiene los resultados y botones
    public TextMeshProUGUI aciertosText;
    public TextMeshProUGUI erroresText;

    private TablaTirosControl tablaTirosControl;

    void Start()
    {
        // Ocultar elementos al inicio
        panelResultados.SetActive(false);
        verificarButton.gameObject.SetActive(false);

        // Obtener referencia al script de control
        tablaTirosControl = FindObjectOfType<TablaTirosControl>();

        // Asignar eventos a los botones
        verificarButton.onClick.AddListener(MostrarResultados);
        reintentarButton.onClick.AddListener(Reintentar);
        salirButton.onClick.AddListener(Salir);
    }

    public void MostrarBotonVerificar(bool mostrar)
    {
        verificarButton.gameObject.SetActive(mostrar);
    }

    private void MostrarResultados()
    {
        if (tablaTirosControl != null)
        {
            tablaTirosControl.ValidarRespuestas();

            // Actualizar textos de aciertos y errores
            aciertosText.text = tablaTirosControl.aciertosText.text;
            erroresText.text = tablaTirosControl.erroresText.text;
        }

        // Mostrar el panel de resultados
        panelResultados.SetActive(true);
        verificarButton.gameObject.SetActive(false);
    }

    private void Reintentar()
    {
        panelResultados.SetActive(false);
        tablaTirosControl.RandomizarValores();
    }

    private void Salir()
    {
        SceneManager.LoadScene("SceneInicio");
    }
}
