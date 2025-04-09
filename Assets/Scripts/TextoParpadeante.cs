using System.Collections;
using UnityEngine;
using TMPro; // Importa este espacio de nombres si estás utilizando TextMeshPro

public class TextBlink : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // Referencia al componente TextMeshProUGUI
    public float blinkInterval = 0.80f; // Intervalo de tiempo entre parpadeos

    private void Start()
    {
        // Inicia la corutina que hace parpadear el texto
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            // Cambia la visibilidad del texto
            textMesh.enabled = !textMesh.enabled;

            // Espera el intervalo de tiempo especificado
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}

