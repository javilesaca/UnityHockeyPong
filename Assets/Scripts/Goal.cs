using UnityEngine;

public class Goal : MonoBehaviour
{
    public delegate void GoalScoredEventHandler(bool jugadorUnoGoal); // Delegado para el evento
    public static event GoalScoredEventHandler OnGoalScored; // Evento que se dispara cuando se marca un gol

    public bool jugadorUnoGoal;
    public GameObject gameManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            // Usar directamente la instancia del GameManager
            if (GameManager.Instance != null)
            {
                if (jugadorUnoGoal)
                {
                    GameManager.Instance.JugadorUnoMarca();
                }
                else
                {
                    GameManager.Instance.JugadorDosMarca();
                }
                OnGoalScored?.Invoke(jugadorUnoGoal);
            }
            else
            {
                Debug.LogError("No se encontró un objeto GameManager en la escena.");
            }
        }
    }

}
