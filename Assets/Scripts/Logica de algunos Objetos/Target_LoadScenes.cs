using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetEscenes : MonoBehaviour
{
    // nombre de la escena

    public string nextSceneName;

    private void OnCollisionEnter(Collision collision)
    {
        // Carga la siguiente escena si la "ball" colisiona con la red o las barras
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

