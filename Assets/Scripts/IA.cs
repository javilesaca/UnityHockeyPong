using UnityEngine;

public class IA : MonoBehaviour
{
    public float speedY = 0.1f;
    public float speedX = 0.1f; // Velocidad en el eje X
    //public float maxXPosition = -5f; // Límite en el eje X
    public GameObject ball;
    private Vector2 ballPosition;
    public string jugadorTag = "Player2";

    // Límites en el eje X para el delantero
    public float minX = -4f;
    public float maxX = 6f;

    void Update()
    {
        Move();
    }

   /* void Move()
    {
        ballPosition = ball.transform.position;

        // Si el objeto tiene el tag "DelanteroDos", se mueve en ambos ejes (X y Y)
        if (gameObject.CompareTag("DelanteroDos"))
        {
            // Movimiento en el eje Y: ajusta la posición de la IA en el eje Y según la pelota
            if (transform.position.y < ballPosition.y)
            {
                transform.position += new Vector3(0, speedY * Time.deltaTime, 0);
            }
            else if (transform.position.y > ballPosition.y)
            {
                transform.position += new Vector3(0, -speedY * Time.deltaTime, 0);
            }

            // Movimiento en el eje X: ajusta la posición de la IA en el eje X según la pelota
            if (transform.position.x < ballPosition.x)
            {
                transform.position += new Vector3(speedX * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x > ballPosition.x)
            {
                transform.position += new Vector3(-speedX * Time.deltaTime, 0, 0);
            }

            // Limitar la posición del delantero en el eje X para que no sobrepase los límites establecidos
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
        // Si el objeto tiene el tag "Player2" (portero), se mueve solo en el eje Y
        else if (gameObject.CompareTag("Player2"))
        {
            // Movimiento en el eje Y: ajusta la posición del portero solo en el eje Y según la pelota
            if (transform.position.y < ballPosition.y)
            {
                transform.position += new Vector3(0, speedY * Time.deltaTime, 0); // Mover hacia abajo si la pelota está abajo
            }
            else if (transform.position.y > ballPosition.y)
            {
                transform.position += new Vector3(0, -speedY * Time.deltaTime, 0); // Mover hacia arriba si la pelota está arriba
            }
        }
    }*/
        void Move()
    {
        ballPosition = ball.transform.position;

        if (CompareTag("DelanteroDos"))
        {
            // Obtener posición destino limitada en X
            float targetX = Mathf.Clamp(ballPosition.x, minX, maxX);
            float targetY = ballPosition.y;

            // Crear la posición objetivo (solo X e Y, mantener Z)
            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            // Velocidades independientes en X e Y
            float stepX = speedX * Time.deltaTime;
            float stepY = speedY * Time.deltaTime;

            // Mover en X e Y usando MoveTowards
            Vector3 newPosition = new Vector3(
                Mathf.MoveTowards(transform.position.x, targetX, stepX),
                Mathf.MoveTowards(transform.position.y, targetY, stepY),
                transform.position.z
            );

            transform.position = newPosition;
        }
        else if (CompareTag("Player2"))
        {
            // Movimiento del portero solo en Y con MoveTowards
            float targetY = ballPosition.y;
            float stepY = speedY * Time.deltaTime;

            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, targetY, stepY),
                transform.position.z
            );
        }
    }
}


