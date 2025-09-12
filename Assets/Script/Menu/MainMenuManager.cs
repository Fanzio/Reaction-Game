using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Riferimenti ai pannelli
    public GameObject startPanel;
    public GameObject durationPanel;

    // Metodo chiamato dal bottone "Play" nel StartPanel
    public void OnPlayClicked()
    {
        // Nasconde il pannello iniziale e mostra quello con le scelte di durata
        startPanel.SetActive(false);
        durationPanel.SetActive(true);
    }

    // Metodo chiamato dal bottone "10" nel DurationPanel
    public void SetGameDuration10()
    {
        GameSettings.GameDuration = 10f;
        LoadGameScene();
    }

    // Metodo chiamato dal bottone "15" nel DurationPanel
    public void SetGameDuration15()
    {
        GameSettings.GameDuration = 15f;
        LoadGameScene();
    }

    // Metodo chiamato dal bottone "20" nel DurationPanel
    public void SetGameDuration20()
    {
        GameSettings.GameDuration = 20f;
        LoadGameScene();
    }

    // Metodo chiamato dal bottone "Back" nel DurationPanel per tornare al pannello iniziale
    public void OnBackClicked()
    {
        durationPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    // Carica la scena di gioco (assicurati che l'indice o il nome siano corretti)
    private void LoadGameScene()
    {
        // Se la scena di gioco ha build index 1:
        SceneManager.LoadScene(1);
        // Oppure, se preferisci usare il nome:
        // SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("L'applicazione è stata chiusa."); // Questo lo vedrai solo nell'Editor
    }
}
