using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    void Start()
    {
        // Inicializar el volumen si es la primera vez que se abre el juego
        if (!PlayerPrefs.HasKey("volumenInicializado"))
        {
            float volumenInicial = 0.5f; // Mitad del volumen
            PlayerPrefs.SetFloat("volumenAudio", volumenInicial);
            PlayerPrefs.SetInt("volumenInicializado", 1);
            PlayerPrefs.Save();
            //AudioListener.volume = volumenInicial;
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volumenAudio", 1f); // Usa el valor guardado
        }

        // Verificar si AudioManager.Instance es nulo
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("El AudioManager no está configurado correctamente. No se puede reproducir la música del menú.");

            AudioManager foundAudioManager = FindFirstObjectByType<AudioManager>();

            if (foundAudioManager != null)
            {
                AudioManager.Instance = foundAudioManager;
            }
            else
            {
                Debug.LogError("No se encontró un AudioManager en la escena.");
            }
        }

        // Si todo está bien, reproducir música de menú
        if (AudioManager.Instance != null)
        {
            float volumen = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
            AudioManager.Instance.SetVolume(volumen);
            AudioManager.Instance.PlayMenuMusic();
        }
    }


    public void Play()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic(); // Detiene la música del menú
        }
        // Obtener el tipo de juego almacenado en PlayerPrefs
        int tipoJuego = PlayerPrefs.GetInt("tipoJuego", 0);

        // Determinar la escena a cargar según el tipo de juego
        string escenaACargar = (tipoJuego == 0) ? "PvsIA" : "PvsP";

        // Cargar la escena correspondiente
        SceneManager.LoadScene(escenaACargar);
    }


    public void Salir()
    {
        Debug.Log("Saliendo de la aplicación.");
        Application.Quit();
    }
}
