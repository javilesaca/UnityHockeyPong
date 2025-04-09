using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject menuWinner;

    public void ShowMenu()
    {
        menuWinner.SetActive(true);
        PauseGame();
    }

    public void HideMenu()
    {
        menuWinner.SetActive(false);
        
    }
    
     public void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el juego
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el juego
    }
}


