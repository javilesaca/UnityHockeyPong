using UnityEngine;

public class Players : MonoBehaviour
{

    [SerializeField]
    private bool playerUno; // Variable para determinar si este jugador es el jugador uno o no
    [SerializeField]
    private float speed = 3; // Velocidad de movimiento
    [SerializeField]
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D del jugador

    [SerializeField]
    private GameObject portero; // Referencia al GameObject del portero del jugador 1
    [SerializeField]
    private GameObject portero2; // Referencia al GameObject del portero del jugador 2
    [SerializeField]
    private GameObject delantero; // Referencia al GameObject del portero del jugador 2
    [SerializeField]
    private GameObject delantero2; // Referencia al GameObject del portero del jugador 2

    // Variables para almacenar las posiciones iniciales de cada objeto
    private Vector2 startPos; // Posición inicial del jugador
    private Vector2 porteroStartPos; // Posición inicial del portero del jugador 1
    private Vector2 portero2StartPos; // Posición inicial del portero del jugador 2
    private Vector2 delanteroStartPos; // Posición inicial del delantero del jugador 1
    private Vector2 delantero2StartPos; // Posición inicial del delantero del jugador 2

    // Límites de movimiento en el eje X
    [SerializeField]
    private float minX = -8f; // Límite mínimo en X
    [SerializeField]
    private float maxX = 8f; // Límite máximo en X

    void Start()
    {
        // Al inicio, almacenar las posiciones iniciales de los objetos
        startPos = transform.position;
        GameManager.Instance.GuardarPosicionInicial(gameObject.name, startPos);
        porteroStartPos = portero.transform.position;
        GameManager.Instance.GuardarPosicionInicial(portero.name, porteroStartPos);
        portero2StartPos = portero2.transform.position;
        GameManager.Instance.GuardarPosicionInicial(portero2.name, portero2StartPos);
        delanteroStartPos = delantero.transform.position;
        GameManager.Instance.GuardarPosicionInicial(delantero.name, delanteroStartPos);
        delantero2StartPos = delantero2.transform.position;
        GameManager.Instance.GuardarPosicionInicial(delantero2.name, delantero2StartPos);
    }

    void Update()
    {
        float moveVertical = 0f; // Movimiento vertical del delantero
        float moveHorizontal = 0f; // Movimiento horizontal del delantero

        // Movimientos del jugador dependiendo de si es el jugador uno o no
        if (playerUno)
        {
            moveVertical = Input.GetAxisRaw("Vertical"); // Movimiento vertical del portero
            moveHorizontal = Input.GetAxisRaw("Horizontal"); // Movimiento horizontal del delantero
                                                             // moveVertical = Input.GetAxisRaw("Vertical"); // Movimiento vertical del delantero
                                                             // rb.velocity = new Vector2(moveHorizontal * speed, movePortero * speed); // Aplicar velocidad al delantero y al portero

            // Limitar la posición en el eje X

            // Actualizar la posición del Rigidbody2D

        }
        else
        {
            moveVertical = Input.GetAxisRaw("Vertical2"); // Movimiento vertical del portero
            moveHorizontal = Input.GetAxisRaw("Horizontal2"); // Movimiento horizontal del delantero
                                                              // moveVertical = Input.GetAxisRaw("Vertical2"); // Movimiento vertical del delantero
                                                              //rb.velocity = new Vector2(moveHorizontal * speed, movePortero * speed); // Aplicar velocidad al delantero y al portero

            // Limitar la posición en el eje X

            // Actualizar la posición del Rigidbody2D

        }

        // Calcular nueva posicióntor2(moveHoriz
        // Calcular nueva posición
        Vector2 newPosition = rb.position + new Vector2(moveHorizontal * speed * Time.deltaTime, moveVertical * speed * Time.deltaTime);

        // Limitar la posición en el eje X
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Actualizar la posición del Rigidbody2D
        rb.MovePosition(newPosition);
    }

    public void Reset()
    {
        // Al llamar al método de reinicio, restablecer la velocidad del jugador y su posición inicial
        rb.linearVelocity = Vector2.zero;
        transform.position = GameManager.Instance.ObtenerPosicionInicial(gameObject.name);

        /*/ Restablecer las posiciones iniciales de los porteros y los delanteros para ambos jugadores
        portero.transform.position = porteroStartPos;
        portero2.transform.position = portero2StartPos;
        delantero.transform.position = delanteroStartPos;
        delantero2.transform.position = delantero2StartPos;*/
        portero.transform.position = GameManager.Instance.ObtenerPosicionInicial(portero.name);
        portero2.transform.position = GameManager.Instance.ObtenerPosicionInicial(portero2.name);
        delantero.transform.position = GameManager.Instance.ObtenerPosicionInicial(delantero.name);
        delantero2.transform.position = GameManager.Instance.ObtenerPosicionInicial(delantero2.name);
    }
}

