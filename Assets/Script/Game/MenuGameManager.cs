using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    // Riferimento al pannello del menu (contenente Resume, Reset, Option, Quit)
    public GameObject menuPanel;
    private bool isPaused = false;

    // Metodo da chiamare al click sul gear
    public void ToggleMenu()
    {
        // Se il gioco è finito, non permette l'apertura del menu
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Metodo per mettere in pausa il gioco e mostrare il menu
    void PauseGame()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0f; // Ferma il gioco
        isPaused = true;
    }

    // Metodo per riprendere il gioco
    public void ResumeGame()
    {
        // Se il gioco è finito, non fare nulla
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        menuPanel.SetActive(false);
        Time.timeScale = 1f; // Riprende il gioco
        isPaused = false;
    }

    // Metodo per resettare la scena corrente
    public void OnReset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Metodo per uscire dal gioco, portando alla scena con build index 0
    public void OnQuit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
