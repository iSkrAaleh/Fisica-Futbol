using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TirosUIManager : MonoBehaviour
{
    public GameObject tiroPrefab; 
    public Transform contentParent; 
    void Start()
    {
        UpdateUI(); 
    }

    public void UpdateUI()
    {
        // Eliminar todos los hijos previos para que no se dupliquen
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        List<TiroData> tiros = TiroLogger.Instance.ObtenerTiros();
        int K = 1;
        foreach (TiroData tiro in tiros)
        {
            
            GameObject newTiro = Instantiate(tiroPrefab, contentParent);
            Text tiroText = newTiro.GetComponentInChildren<Text>();
            if (tiroText == null)
            {
                Debug.LogError("El Prefab no tiene un componente Text.");
                continue;
            }
            // Formatea los números con dos decimales
            string formateado =      $"{K}.   \t{tiro.tiempo.ToString("F2")} s             \t{tiro.angulo.ToString("F2")}°         \t{tiro.velocidad.ToString("F2")} m/s       \t{tiro.distancia.ToString("F2")} m          \t{tiro.altura.ToString("F2")} m"; 
            tiroText.text = formateado;
            K++;
        }
    }

 
}


