using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Variables de música
    public AudioClip GameMusic;
    public AudioClip MenuMusic;

    // Variables de efectos de sonido
    public AudioClip PlayerSound;
    public AudioClip WallSound;
    public AudioClip GoalSound;
    public AudioClip GameOverSound;

    private AudioSource audioSource;  // Para música
    private AudioSource fxSource;    // Para efectos de sonido

    private float volumenGuardado;

    private void Awake()
    {
        // Si ya existe una instancia de AudioManager, destruye el objeto actual
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el AudioManager cuando cambiamos de escena
        }

        // Configurar los AudioSource
        audioSource = GetComponent<AudioSource>();  // Audio para la música
        fxSource = gameObject.AddComponent<AudioSource>();  // Audio para los efectos (si no hay otro AudioSource)

        // Guardar y aplicar volumen
        volumenGuardado = PlayerPrefs.GetFloat("volumenAudio", 0.5f); // Establecer volumen predeterminado
        SetVolume(volumenGuardado); // Aplicar el volumen guardado
    }

    // Establece el volumen para ambos audioSources
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;  // Música
        fxSource.volume = volume;     // Efectos
    }

    // Reproduce la música del juego
    public void PlayGameMusic()
    {
        audioSource.Stop();
        audioSource.clip = GameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Reproduce la música del menú
    public void PlayMenuMusic()
    {
        audioSource.Stop();
        audioSource.clip = MenuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Detiene la música actual
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Reproduce efectos de sonido
    public void PlayPlayerSound()
    {
        fxSource.PlayOneShot(PlayerSound);
    }

    public void PlayWallSound()
    {
        fxSource.PlayOneShot(WallSound);
    }

    public void PlayGoalSound()
    {
        fxSource.PlayOneShot(GoalSound);
    }

    public void PlayGameOverSound()
    {
        fxSource.PlayOneShot(GameOverSound);
    }

    // Guarda el volumen
    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("volumenAudio", volume);
        PlayerPrefs.Save();
        SetVolume(volume);
    }
}
