using UnityEngine;
using TMPro;  // Importa el namespace de TextMeshPro

public class ShowResults : MonoBehaviour
{
    public TMP_Text textoResultado;  // Cambia a TMP_Text para usar TextMeshPro

    void Start()
    {
        // Recuperar los valores de velocidad y ángulo de PlayerPrefs
        float velocidad = PlayerPrefs.GetFloat("Velocidad", 0f);
        float angulo = PlayerPrefs.GetFloat("Angulo", 0f);

        // Limitar la velocidad a un máximo de 100
        velocidad = Mathf.Min(velocidad, 100);

        // Limitar el ángulo a 2 decimales
        angulo = Mathf.Round(angulo * 100f) / 100f;

        // Actualizar el texto con los valores de velocidad y ángulo
        textoResultado.text = $"Perfecto, para conseguir dicho tiro se requirió de una velocidad de {velocidad} m/s, ángulo de {angulo}º";
    }
}
