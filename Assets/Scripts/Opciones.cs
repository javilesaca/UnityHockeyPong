using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Opciones : MonoBehaviour
{
    [Header("Volumen")]
    [SerializeField] private Slider sliderVolumen;
    [SerializeField] private float defaultSliderValue = 0.5f;
    [SerializeField] private Image imageMute;

    [Header("Configuraciones de Juego")]
    [SerializeField] private TMP_Dropdown dropdownTipoJuego;
    [SerializeField] private TMP_Dropdown dropdownTipoPista;
    [SerializeField] private int defaultTipoJuego = 0;
    [SerializeField] private int defaultTipoPista = 0;

    void Start()
    {
        LoadSettings();
        sliderVolumen.onValueChanged.AddListener(delegate { OnSliderChanged(); });
    }

    private void OnSliderChanged()
    {
        SetVolumenPref();
        MostrarIconoMute(sliderVolumen.value);
    }

    public void SetVolumenPref()
    {
        float volumen = sliderVolumen.value;
        PlayerPrefs.SetFloat("volumenAudio", volumen);
        AudioListener.volume = volumen;
        PlayerPrefs.Save();
    }

    private void AplicarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
        MostrarIconoMute(volumen);
    }

    /// <summary>
    /// Muestra u oculta el icono de mute según el volumen dado.
    /// </summary>
    public void MostrarIconoMute(float volumen)
    {
        if (imageMute != null)
        {
            imageMute.enabled = (volumen == 0);
        }
    }

    public void SetTipoJuegoPref()
    {
        PlayerPrefs.SetInt("tipoJuego", dropdownTipoJuego.value);
        PlayerPrefs.Save();
    }

    public void SetTipoPista()
    {
        PlayerPrefs.SetInt("tipoPista", dropdownTipoPista.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Cargar y aplicar el volumen guardado
        float volumen = PlayerPrefs.GetFloat("volumenAudio", defaultSliderValue);
        sliderVolumen.value = volumen;
        AplicarVolumen(volumen);

        // Cargar tipo de juego
        int tipoJuego = PlayerPrefs.GetInt("tipoJuego", defaultTipoJuego);
        dropdownTipoJuego.value = tipoJuego;

        // Cargar tipo de pista
        int tipoPista = PlayerPrefs.GetInt("tipoPista", defaultTipoPista);
        dropdownTipoPista.value = tipoPista;
    }
}

