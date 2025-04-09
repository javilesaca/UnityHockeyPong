using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject Pelota;
    public GameObject JugadorUno;
    public GameObject JugadorDos;
    [SerializeField] private GameObject JugadorUnoDelantero;
    [SerializeField] private GameObject JugadorDosDelantero;
    public GameObject TipoPista;

    public TextMeshProUGUI MarcadorUno;
    public TextMeshProUGUI MarcadorDos;
    public TextMeshProUGUI MenuWinner;


 // Configuración de puntaje
    private int jugadorUnoPuntuacion;
    private int jugadorDosPuntuacion;
    public int puntuacionLimite = 1;

    // Variables para configurar modos de juego y pista
    public static int tipoJuego;
    public static int tipoPista;
    public Sprite[] spritesTipoPista;
    public Sprite[] spritesTipoPelota;
    public bool IAGame;

    // Singleton
    public static GameManager Instance { get; private set; }

       // --- Guardado de posiciones iniciales en PlayerPrefs ---
    // Se usarán los nombres exactos de los objetos para formar las claves.
    // Ejemplo de clave: "JugadorUno_PosX" y "JugadorUno_PosY"
    public void GuardarPosicionInicial(string key, Vector2 pos)
    {
        PlayerPrefs.SetFloat(key + "_PosX", pos.x);
        PlayerPrefs.SetFloat(key + "_PosY", pos.y);
        // Opcionalmente, puedes llamar a PlayerPrefs.Save() aquí,
        // aunque conviene hacerlo una sola vez al final del Start()
    }

    public Vector2 ObtenerPosicionInicial(string key)
    {
        float posX = PlayerPrefs.GetFloat(key + "_PosX", 0f);
        float posY = PlayerPrefs.GetFloat(key + "_PosY", 0f);
        return new Vector2(posX, posY);
    }

   // --- Inicialización ---
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
            return;
        }
    }

  
 void Start()
    {
        // Asignar referencias si aún no están asignadas (puedes asignarlas desde el Inspector)
        if (JugadorUno == null) JugadorUno = GameObject.FindWithTag("Player1");
        if (JugadorDos == null) JugadorDos = GameObject.FindWithTag("Player2");
        if (JugadorUnoDelantero == null) JugadorUnoDelantero = GameObject.Find("DelanteroUno");
        if (JugadorDosDelantero == null) JugadorDosDelantero = GameObject.Find("DelanteroDos");
        if (Pelota == null) Pelota = GameObject.FindWithTag("Ball");
        if (TipoPista == null) TipoPista = GameObject.FindWithTag("Pista");

        // Guardar las posiciones iniciales usando los nombres exactos
        if (JugadorUno != null) 
            GuardarPosicionInicial("JugadorUno", JugadorUno.transform.position);
        if (JugadorUnoDelantero != null)
            GuardarPosicionInicial("DelanteroUno", JugadorUnoDelantero.transform.position);
        if (JugadorDos != null)
            GuardarPosicionInicial("JugadorDos", JugadorDos.transform.position);
        if (JugadorDosDelantero != null)
            GuardarPosicionInicial("DelanteroDos", JugadorDosDelantero.transform.position);
        if (Pelota != null)
            GuardarPosicionInicial("Pelota", Pelota.transform.position);

        // Guardar cambios de PlayerPrefs
        PlayerPrefs.Save();

        AplicarVolumenGuardado();
        AudioManager.Instance.PlayGameMusic();
        ObtenerTipoPista();
        CambiarSpriteTipoPista();
        ObtenerTipoJuego();
    }


    // Método para cargar la escena correcta según el tipo de juego
    private void CargarEscenaSegunTipoJuego()
    {
        tipoJuego = PlayerPrefs.GetInt("tipoJuego", 0); // Obtener el tipo de juego guardado (0 para IA, 1 para jugador vs jugador)
        string escenaActual = SceneManager.GetActiveScene().name; // Tipo de escena cargada
        string escenaACargar = tipoJuego == 0 ? "PvsIA" : "PvsP"; // Dependiendo del tipo de juego, selecciona la escena
        
        if (escenaActual != escenaACargar)
        {
            SceneManager.LoadScene(escenaACargar); // Cargar la escena correspondiente
        }
    }

    // --- Métodos para restaurar posiciones ---
    public void ResetPosition()
    {   //DEBUG
        if (JugadorDos != null)
        {
            Players p = JugadorDos.GetComponent<Players>();
            if (p != null)
            {
                JugadorDos.SetActive(true);
                p.Reset();
            }
            else
            {
                Debug.LogWarning("El componente 'Players' no se encontró en JugadorDos.");
            }
        }
        else
        {
            Debug.LogWarning("JugadorDos no está asignado.");
        }
        //FIN DEBUG

        // Restaurar la posición de la Pelota
        if (Pelota != null)
        {
            Pelota.transform.position = ObtenerPosicionInicial("Pelota");
            Pelota.GetComponent<Ball>().Reset();
        }
        else
        {
            Debug.LogWarning("La Pelota no está asignada o ha sido destruida.");
        }

        // Restaurar JugadorUno
        if (JugadorUno != null)
        {
            JugadorUno.transform.position = ObtenerPosicionInicial("JugadorUno");
            JugadorUno.SetActive(true);
            JugadorUno.GetComponent<Players>().Reset();
        }
        else
        {
            Debug.LogWarning("JugadorUno no está asignado.");
        }

        // Restaurar JugadorDos
        if (JugadorDos != null)
        {
            JugadorDos.transform.position = ObtenerPosicionInicial("JugadorDos");
            JugadorDos.SetActive(true);
            JugadorDos.GetComponent<Players>().Reset();
        }
        else
        {
            Debug.LogWarning("JugadorDos no está asignado.");
        }

        // Restaurar DelanteroUno
        if (JugadorUnoDelantero != null)
        {
            JugadorUnoDelantero.transform.position = ObtenerPosicionInicial("JugadorUnoDelantero");
            JugadorUnoDelantero.SetActive(true);
            JugadorUnoDelantero.GetComponent<Players>().Reset();
        }
        else
        {
            Debug.LogWarning("DelanteroUno no está asignado.");
        }

        // Restaurar DelanteroDos
        if (JugadorDosDelantero != null)
        {
            JugadorDosDelantero.transform.position = ObtenerPosicionInicial("JugadorDosDelantero");
            JugadorDosDelantero.SetActive(true);
            JugadorDosDelantero.GetComponent<Players>().Reset();
        }
        else
        {
            Debug.LogWarning("DelanteroDos no está asignado.");
        }
    }


    // --- Métodos de puntaje y finalización ---
    public void JugadorUnoMarca()
    {
        jugadorUnoPuntuacion++;
        MarcadorUno.text = jugadorUnoPuntuacion.ToString();
        AudioManager.Instance.PlayGoalSound();

        if (jugadorUnoPuntuacion >= puntuacionLimite)
        {
            FinDelJuego(JugadorUno);
        }
        else
        {
            // Resetear posiciones después de gol
            ResetPosition();
        }
    }

    public void JugadorDosMarca()
    {
         Debug.Log("jugadorDos: " + (JugadorDos != null ? JugadorDos.name : "null"));
         Debug.Log("jugadorDosMarcador: " + (MarcadorDos != null ? "asignado" : "null"));
        
        
        jugadorDosPuntuacion++;
        MarcadorDos.text = jugadorDosPuntuacion.ToString();
        AudioManager.Instance.PlayGoalSound();
        //DEBUG
        if (MarcadorDos != null)
{
    MarcadorDos.text = jugadorDosPuntuacion.ToString();
}
else
{
    Debug.LogWarning("jugadorDosMarcador es null en JugadorDosMarca.");
}
        //Fin  DEBUG
        if (jugadorDosPuntuacion >= puntuacionLimite)
        {
            FinDelJuego(JugadorDos);
        }
        else
        {
            ResetPosition();
        }
    }

    private void FinDelJuego(GameObject jugadorGanador)
    {
        Debug.Log("¡Fin del juego! El jugador " + jugadorGanador.name + " ha ganado.");

        GameOverMenu gameOverMenu = FindFirstObjectByType<GameOverMenu>();

        if (gameOverMenu != null)
        {
            gameOverMenu.ShowMenu();
            AudioManager.Instance.PlayGameOverSound();
            MenuWinner.text = "Ganador: " + jugadorGanador.name;
        }
        else
        {
            Debug.LogError("No se encontró un objeto GameOverMenu en la escena.");
        }

        // Resetear los puntajes
        jugadorUnoPuntuacion = 0;
        jugadorDosPuntuacion = 0;
        MarcadorUno.text = "0";
        MarcadorDos.text = "0";

        // Resetear posiciones
        ResetPosition();
    }

    // --- Métodos para tipo de pista, tipo de juego y volumen ---
    public void ObtenerTipoPista()
    {
        tipoPista = PlayerPrefs.GetInt("tipoPista", 0);
    }

    public void CambiarSpriteTipoPista()
    {
        if (TipoPista == null || Pelota == null)
        {
            Debug.LogWarning("No se puede cambiar el sprite porque falta alguna referencia.");
            return;
        }

        int spriteIndex = Mathf.Clamp(tipoPista, 0, spritesTipoPista.Length - 1);
        TipoPista.GetComponent<SpriteRenderer>().sprite = spritesTipoPista[spriteIndex];
        Pelota.GetComponent<SpriteRenderer>().sprite = spritesTipoPelota[spriteIndex];
    }

    private void ObtenerTipoJuego()
    {
        tipoJuego = PlayerPrefs.GetInt("tipoJuego", 0);
        IAGame = tipoJuego == 0;
    }

    private void AplicarVolumenGuardado()
    {
        float volumen = PlayerPrefs.GetFloat("volumenAudio", 1f);
        AudioListener.volume = volumen;
    }

    // --- Reasignar referencias al recargar la escena ---
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasignar las referencias para objetos que puedan haber sido recreados
        Pelota = GameObject.FindWithTag("Ball");
        TipoPista = GameObject.FindWithTag("Pista");
        JugadorUno = GameObject.FindWithTag("Player1");
        JugadorDos = GameObject.FindWithTag("Player2");
        JugadorUnoDelantero = GameObject.FindWithTag("DelanteroUno");
        JugadorDosDelantero = GameObject.FindWithTag("DelanteroDos");

        /* Aplicar el sprite correcto de la pista
        CambiarSpriteTipoPista();*/

            // Solo aplicar cambio de sprite si estamos en una escena que tenga los objetos necesarios (por ejemplo, la escena Main)
    if (scene.name != "Creditos")
    {
        if (Pelota != null && TipoPista != null)
        {
            CambiarSpriteTipoPista();
        }
        else
        {
            Debug.LogWarning("No se puede cambiar el sprite porque falta alguna referencia (Pelota o Pista no encontrada).");
        }
    }
    else
    {
        Debug.Log("Escena de créditos cargada. No se aplica cambio de sprites.");
    }

    // Desvincular el evento para evitar llamadas múltiples
    SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}