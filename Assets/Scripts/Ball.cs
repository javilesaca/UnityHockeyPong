using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f; // Velocidad inicial de la bola
    public float speedIncreaseAmount = 0.5f; // Incremento de velocidad después de cada rebote
    public float maxSpeed = 14f; // Velocidad máxima de la bola
    public float spinSpeed = 50f; // Velocidad de giro
    public Rigidbody2D rb;
    private Vector2 startPos;
    public bool jugadorUnoGol;
    void Start()
    {
        if (startPos == Vector2.zero)  // Si startPos no está asignado, asigna la posición actual
        {
            startPos = transform.position;
        }
        transform.position = startPos;
        Launch();
        Goal.OnGoalScored += LaunchAfterGoal; // Suscribir al evento

    }

    private void OnDestroy()
{
    // Nos des-suscribimos del evento para evitar referencias nulas
    Goal.OnGoalScored -= LaunchAfterGoal;
}
    public void Reset()
    {
        transform.position = startPos;
        rb.linearVelocity = Vector2.zero;
        LaunchAfterGoal(false);
    }
    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(speed * x, speed * y);

        // Aplica torque para el giro
        rb.AddTorque(spinSpeed * Random.Range(1f, -1f), ForceMode2D.Impulse);
    }

    /** el método LaunchAfterGoal() recibirá como parámetro la colisión que activó el método OnTriggerEnter2D(). Luego, verificará si la colisión ocurrió con el tag "GoalUno". Si es así, la bola será lanzada hacia la dirección positiva del eje X (hacia la portería del jugador uno). De lo contrario, la bola será lanzada hacia la dirección negativa del eje X (hacia la portería del jugador dos).**/
        private void LaunchAfterGoal(bool jugadorUnoGoal)
    {
        
        // El parámetro jugadorUnoGoal indica si el último gol fue del jugador uno
        float x = jugadorUnoGoal ? 1 : -1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(speed * x, speed * y);
        rb.AddTorque(spinSpeed * Random.Range(1f, -1f), ForceMode2D.Impulse);

    }

public void OnCollisionEnter2D(Collision2D collision)
{
    AudioManager.Instance.PlayWallSound();

    // Normaliza y aumenta la velocidad con límite
    rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Min(rb.linearVelocity.magnitude + speedIncreaseAmount, maxSpeed);

    // Evitar trayectorias perfectamente rectas: añadir leve desviación en la dirección
    Vector2 velocity = rb.linearVelocity;
    float desviacion = Random.Range(-0.2f, 0.2f);  // Desviación aleatoria

    // Si la pelota está casi en línea recta horizontalmente o verticalmente, la desviamos
    if (Mathf.Abs(velocity.y) < 0.1f)  // Si se mueve casi horizontalmente, la desviamos ligeramente en el eje Y
    {
        velocity.y += desviacion;
    }

    if (Mathf.Abs(velocity.x) < 0.1f)  // Si se mueve casi verticalmente, la desviamos ligeramente en el eje X
    {
        velocity.x += desviacion;
    }

    // Si la pelota colisiona con una paleta (Player1 o Player2)
    if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
    {
        Rigidbody2D paddleRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (paddleRb != null)
        {
            // Influencia de la paleta: ajustamos la dirección de la bola según la velocidad de la paleta
            float paddleInfluence = paddleRb.linearVelocityY * 0.5f;
            velocity.y += paddleInfluence;
        }
    }

    // Reaplica la nueva dirección con la desviación y velocidad normalizada
    rb.linearVelocity = velocity.normalized * rb.linearVelocity.magnitude;
    rb.AddTorque(spinSpeed * Random.Range(-1f, 1f), ForceMode2D.Impulse);
}

}
