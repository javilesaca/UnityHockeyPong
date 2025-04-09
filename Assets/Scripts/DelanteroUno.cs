using UnityEngine;

public class JugadorUno : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el trigger es la pelota
        if (other.CompareTag("Ball"))
        {
            // Obtener la posición de la pelota
            float ballXPosition = other.transform.position.x;

            // Si la pelota viene de una posición positiva en el eje x, rebote adquiriendo velocidad 0.5f
            if (ballXPosition > transform.position.x)
            {
                Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();
                ballRigidbody.linearVelocity = new Vector2(0.5f, ballRigidbody.linearVelocity.y);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el trigger es la pelota
        if (other.CompareTag("Ball"))
        {
            // No se necesita hacer nada cuando la pelota sale del trigger
        }
    }
}

