using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Target : MonoBehaviour
{
    public BallMover ball;
    public float moveForce = 10f;
    public GameObject instructionPanel;
    public Transform ballStartPosition;
    public string nextSceneName;
    public Animator playerAnimator;
    public float delayBeforeBallMoves = 2f;

    // Valores fijos de cada target
    public float velocity = 20f;  // Valor de ejemplo
    public float angle = 30f;     // Valor de ejemplo

    private static float selectedVelocity;
    private static float selectedAngle;

    private void OnMouseDown()
    {
        // Asignar los valores fijos de este target a las variables estáticas
        selectedVelocity = velocity;
        selectedAngle = angle;

        playerAnimator.SetTrigger("Kick");
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
        StartCoroutine(MoveBallAfterDelay());
    }

    private IEnumerator MoveBallAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeBallMoves);
        ball.MoveToTarget(transform.position, moveForce);
    }

    public void OnBallReachedTarget()
    {
        Debug.Log("Pelota llegó a la diana");
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la siguiente escena no está asignado.");
        }
    }

    // Métodos para obtener los valores de velocidad y ángulo seleccionados
    public static float GetSelectedVelocity() => selectedVelocity;
    public static float GetSelectedAngle() => selectedAngle;
}





