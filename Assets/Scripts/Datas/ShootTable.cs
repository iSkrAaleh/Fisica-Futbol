using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shotTable : MonoBehaviour
{
    public GameObject rowPrefab;          // Prefab para cada fila
    public Transform contentParent;       // El padre del contenido del scroll

    public void PopulateTable(List<ShotData> shots)
    {
        // Limpia las filas anteriores
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Crea una nueva fila por cada tiro
        foreach (ShotData shot in shots)
        {
            GameObject newRow = Instantiate(rowPrefab, contentParent);

            Text[] texts = newRow.GetComponentsInChildren<Text>();
            texts[0].text = shot.time.ToString("F2");
            texts[1].text = shot.angle.ToString("F2");
            texts[2].text = shot.speed.ToString("F2");
            texts[3].text = shot.distance.ToString("F2");
            texts[4].text = shot.height.ToString("F2");
        }
    }
}

