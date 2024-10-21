using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ExplosiveMiner.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [Inject] private GameManagerController _gameManager;

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
            SettingsMenu,
            PauseMenu,
            Game
        }

        private void Start()
        {        
            ChangeMenuState(MenuState.MainMenu);
            _gameManager.SetGameInteract(false);
        }

        public void NewGameButton()
        {
            ChangeMenuState(MenuState.Game);
            _gameManager.StartGame();
            _gameManager.SetGameInteract(true);
        }

        public void ResumeGameButton()
        {
            ChangeMenuState(MenuState.Game);
            _gameManager.ResumeGame();
            _gameManager.SetGameInteract(true);
        }

        public void RestartGameButton()
        {
            ChangeMenuState(MenuState.Game);
            _gameManager.RestartGame();
            _gameManager.SetGameInteract(true);
        }

        public void MainMenuButton()
        {
            _gameManager.EndGame();
            Start();
        }

        public void SettingsMenuButton()
        {
            ChangeMenuState(MenuState.SettingsMenu);
        }

        public void BackToMenuButton()
        {
            ChangeMenuState(MenuState.MainMenu);
        }

        public void PauseMenuButton()
        {
            if (pauseMenuUI.activeSelf)
            {
                ChangeMenuState(MenuState.Game);
                _gameManager.SetGameInteract(true);
            }
            else
            {
                ChangeMenuState(MenuState.PauseMenu);
                _gameManager.SetGameInteract(false);
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

        private void ChangeMenuState(MenuState newState)
        {
            mainMenuUI.SetActive(false);
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            gameUI.SetActive(false);

            switch (newState)
            {
                case MenuState.MainMenu:
                    mainMenuUI.SetActive(true);
                    break;
                case MenuState.SettingsMenu:
                    settingsMenuUI.SetActive(true);
                    break;
                case MenuState.PauseMenu:
                    pauseMenuUI.SetActive(true);
                    break;
                case MenuState.Game:
                    gameUI.SetActive(true);
                    break;
            }
        }
    }
}