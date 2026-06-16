using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool isCursorLocked = true;

    void Start()
    {
        // Bloquear y ocultar el cursor al iniciar el juego
        LockCursor();
    }

    void Update()
    {
        // Verificar si se presionó la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alternar entre bloquear/desbloquear el cursor
            if (isCursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
        Cursor.visible = false; // Oculta el cursor
        isCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        Cursor.visible = true; // Muestra el cursor
        isCursorLocked = false;
    }
}
