using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    /*/ Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
        Invoke("LoadMenuInicial", 40);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void LoadMenuInicial()
    {
        SceneManager.LoadScene("Main");
    }*/

    void Start()
    {
        // 🔥 Destruir el GameManager si existe
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            Destroy(gm.gameObject);
            Debug.Log("GameManager destruido al entrar a Créditos.");
        }

        // Reproducir música del menú
        AudioManager.Instance.PlayMenuMusic();

        // Volver al menú principal después de 40 segundos
        Invoke("LoadMenuInicial", 40);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadMenuInicial();
        }
    }

    public void LoadMenuInicial()
    {
        SceneManager.LoadScene("Main");
    }
}
