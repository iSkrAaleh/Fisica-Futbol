using UnityEngine;
using UnityEngine.UI;

public class DisplayValues : MonoBehaviour
{
    public Text velocityText;
    public Text angleText;

    void Start()
    {
        // Obtener los valores seleccionados del Target y mostrar en el texto
        float velocity = Target.GetSelectedVelocity();
        float angle = Target.GetSelectedAngle();

        // Actualizar el texto con los valores obtenidos
        velocityText.text = $"{velocity}";
        angleText.text = $"{angle}";
    }
}
