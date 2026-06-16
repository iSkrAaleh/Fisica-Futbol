using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void ExitGame()
    {
        // Cierra la aplicación si es una build.
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
