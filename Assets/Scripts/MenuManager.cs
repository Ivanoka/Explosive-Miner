using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameUI;
    
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;

    private enum MenuState
    {
        MainMenu,
        PauseMenu,
        Game
    }

    private void Start()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(false);

        SetResumeButton();

        GameManager.instance.SetGameInteract(false);
    }

    public void NewGameButton()
    {
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);

        GameManager.instance.RestartGame();
        GameManager.instance.SetGameInteract(true);
    }

    public void ResumeGameButton()
    {
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);

        GameManager.instance.LoadData();
        GameManager.instance.InitGame();
        GameManager.instance.SetGameInteract(true);
    }

    public void MainMenuButton()
    {
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        SetResumeButton();

        GameManager.instance.SaveData();
        GameManager.instance.HideExplosionHoles();
        GameManager.instance.SetGameInteract(false);
    }

    public void BackToMenuButton()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void SettingNemuButton()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void PauseMenuButton()
    {
        if (pauseMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(false);
            gameUI.SetActive(true);

            GameManager.instance.SetGameInteract(true);
        }
        else
        {
            gameUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            
            GameManager.instance.SetGameInteract(false);
        }
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetResumeButton()
    {
        if (GameManager.instance.CheckData())
        {
            resumeButton.interactable = true;
        }
        else
        {
            resumeButton.interactable = false;
        }
    }
}
