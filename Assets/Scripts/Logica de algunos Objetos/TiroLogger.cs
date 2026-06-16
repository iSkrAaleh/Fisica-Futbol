using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TiroLogger : MonoBehaviour
{
    public static TiroLogger Instance;

    private List<TiroData> listaDeTiros = new List<TiroData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarTiro(float tiempo, float angulo, float velocidad, float distancia, float altura)
    {
        TiroData nuevoTiro = new TiroData(tiempo, angulo, velocidad, distancia, altura);
        listaDeTiros.Add(nuevoTiro);
    }

    public List<TiroData> ObtenerTiros()
    {
        return listaDeTiros;
    }
}

