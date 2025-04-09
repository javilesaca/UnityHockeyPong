using UnityEngine;

public class DelanteroDos : MonoBehaviour
{
    public float deviationAngle = 45f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con la pelota
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Obtener la dirección de la colisión y la posición de la pelota
            Vector2 collisionDirection = collision.contacts[0].point - (Vector2)transform.position;
            float ballXPosition = collision.gameObject.transform.position.x;

            // Si la pelota viene de una posición positiva en el eje x, desviarla
            if (ballXPosition > transform.position.x)
            {
                // Desviar la pelota en el ángulo especificado
                Vector2 deviation = Quaternion.Euler(0, 0, deviationAngle) * collisionDirection.normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = deviation;
            }
            else
            {
                // Si la pelota viene de una posición negativa en el eje x, dejar que colisione y rebote normalmente
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.Reflect(collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity, collision.contacts[0].normal);
            }
        }
    }
}

