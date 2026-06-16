using UnityEngine;

public class BackgroundMusicLoop : MonoBehaviour
{
    public AudioSource audioSource; // Asigna tu AudioSource en el Inspector
    private float clipLength;

    void Start()
    {
        // Obtén la duración del AudioClip
        clipLength = audioSource.clip.length;
        // Inicia la reproducción
        audioSource.Play();
        // Llama a la función LoopAudio después de la duración del clip
        Invoke("LoopAudio", clipLength - 0.1f); // Restamos un pequeño margen de tiempo
    }

    void LoopAudio()
    {
        audioSource.time = 0; // Reinicia el tiempo del audio
        audioSource.Play(); // Vuelve a reproducir el audio sin detenerlo
        Invoke("LoopAudio", clipLength - 0.01f); // Vuelve a invocar el loop
    }
}
