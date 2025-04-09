using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    public float deviationForce = 5f;
    public float maxDeviationAngle = 45f;
    public LayerMask platformLayer1;
    public LayerMask platformLayer2;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;

        if (((1 << layer) & platformLayer1) != 0)
        {
            // Calcula un ángulo de desviación aleatorio
            float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);

            // Mantén la velocidad vertical intacta y aplica el ángulo de desviación a la velocidad horizontal
            Vector2 currentVelocity = rb.linearVelocity;
            Vector2 verticalVelocity = Vector2.up * currentVelocity.y;
            Vector2 deviationDirection = Quaternion.AngleAxis(deviationAngle, Vector3.forward) * Vector2.right;
            Vector2 horizontalVelocity = deviationDirection * currentVelocity.magnitude;
            rb.linearVelocity = verticalVelocity + horizontalVelocity;
        }
        else if (((1 << layer) & platformLayer2) != 0)
        {
            // Calcula un ángulo de desviación aleatorio
            float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);

            // Mantén la velocidad vertical intacta y aplica el ángulo de desviación a la velocidad horizontal
            Vector2 currentVelocity = rb.linearVelocity;
            Vector2 verticalVelocity = Vector2.up * currentVelocity.y;
            Vector2 deviationDirection = Quaternion.AngleAxis(deviationAngle, Vector3.forward) * Vector2.right;

            // Invierte la dirección horizontal
            deviationDirection *= -1f;

            Vector2 horizontalVelocity = deviationDirection * currentVelocity.magnitude;
            rb.linearVelocity = verticalVelocity + horizontalVelocity;
        }
    }
}





