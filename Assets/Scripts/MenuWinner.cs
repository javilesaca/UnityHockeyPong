using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuWinner : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject menuWinner;
    [SerializeField] private TextMeshProUGUI ganadorText;

    [Header("Referencias")]
    private GameOverMenu gameOverMenu;
    public GameManager gameManager;

    private GameObject pelota;
    private GameObject jugadorUno;
    private GameObject jugadorDos;
    private GameObject delanteroUno;
    private GameObject delanteroDos;

    private void Start()
    {
        gameOverMenu = FindFirstObjectByType<GameOverMenu>();
        gameManager = GameManager.Instance;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        string nombreGanador = (sender as GameObject)?.name ?? "Desconocido";
        ganadorText.text = "Ganador: " + nombreGanador;
        menuWinner.SetActive(true);
    }

    public void Reiniciar()
    {
        if (gameOverMenu != null)
        {
            // Guardar el tipo de pista para usar después de recargar
            PlayerPrefs.SetInt("tipoPista", GameManager.tipoPista);

            // Detener música y reanudar juego si estaba pausado
            AudioManager.Instance.StopMusic();
            gameOverMenu.ResumeGame();

            // Importante: remover GameManager antiguo si es persistente
            if (GameManager.Instance != null)
                Destroy(GameManager.Instance.gameObject);

            // Escuchar evento de carga de escena
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Recargar escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasignar referencias
        gameManager = GameManager.Instance;
        pelota = GameObject.FindWithTag("Ball");
        jugadorUno = GameObject.FindWithTag("Player1");
        jugadorDos = GameObject.FindWithTag("Player2");
        delanteroUno = GameObject.FindWithTag("DelanteroUno");
        delanteroDos = GameObject.FindWithTag("DelanteroDos");

        // Verificar que los objetos existen antes de usar
        if (gameManager != null)
        {
            gameManager.ResetPosition();
            gameManager.ObtenerTipoPista();
            gameManager.CambiarSpriteTipoPista();
        }
        else
        {
            Debug.LogWarning("GameManager no se encontró al recargar la escena.");
        }

        // Desvincular el evento para evitar duplicación
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Salir()
    {
        if (gameOverMenu != null)
            gameOverMenu.ResumeGame();

        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene("Creditos");
    }
}
