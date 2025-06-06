using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string TESTING_SCENE_NAME = "Testing";
    private const string PAUSE_MENU_SCENE_NAME = "Pause_Menu";

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.PauseGame, PauseGame);
        EventMessenger.StartListening(EventKey.UnpauseGame, UnpauseGame);

        EventMessenger.StartListening(EventKey.MenuOpened, MenuOpened);
        EventMessenger.StartListening(EventKey.MenuClosed, MenuClosed);

        //EventMessenger.StartListening(EventKey.OpenPauseMenu, OpenPauseMenu);

        EventMessenger.StartListening(EventKey.HideCursor, HideCursor);
        EventMessenger.StartListening(EventKey.ShowCursor, ShowCursor);

        EventMessenger.StartListening(EventKey.ExitGame, ExitGame);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.PauseGame, PauseGame);
        EventMessenger.StopListening(EventKey.UnpauseGame, UnpauseGame);

        EventMessenger.StopListening(EventKey.MenuOpened, MenuOpened);
        EventMessenger.StopListening(EventKey.MenuClosed, MenuClosed);

        //EventMessenger.StartListening(EventKey.OpenPauseMenu, OpenPauseMenu);

        EventMessenger.StopListening(EventKey.HideCursor, HideCursor);
        EventMessenger.StopListening(EventKey.ShowCursor, ShowCursor);
        
        EventMessenger.StopListening(EventKey.ExitGame, ExitGame);
    }
    private void Start()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        DataMessenger.SetBool(BoolKey.CanOpenMenu, true);

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(SceneUtils.SceneName.Game.ToString(), LoadSceneMode.Additive);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Continue"))
        {
            EventMessenger.TriggerEvent(EventKey.Continue, true);
        }
        else if (Application.isEditor && Input.GetButtonDown("TestScene"))
        {
            if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning) || !DataMessenger.GetBool(BoolKey.CanOpenMenu))
            {
                return;
            }

            if (DataMessenger.GetBool(BoolKey.IsMenuOpen))
            {
                EventMessenger.TriggerEvent(EventKey.CloseMenu);
            }
            else if (DataMessenger.GetBool(BoolKey.IsGameActive))
            {
                SceneManager.LoadSceneAsync(TESTING_SCENE_NAME, LoadSceneMode.Additive);
            }
        }
        else if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause Menu"))
        {
            if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning) || !DataMessenger.GetBool(BoolKey.CanOpenMenu))
            {
                return;
            }

            // Check if a menu is open
            if (DataMessenger.GetBool(BoolKey.IsMenuOpen))
            {
                EventMessenger.TriggerEvent(EventKey.MenuGoBack);
            }
            else if (DataMessenger.GetBool(BoolKey.IsGameActive))
            {
                OpenPauseMenu();
            }
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus && DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            OpenPauseMenu();
        }
    }
    private void OpenPauseMenu()
    {
        SceneManager.LoadSceneAsync(PAUSE_MENU_SCENE_NAME, LoadSceneMode.Additive);
        PauseGame();
    }
    private void PauseGame()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        Time.timeScale = 0;
    }
    private void UnpauseGame()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
        Time.timeScale = 1;
    }
    private void MenuOpened()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, true);
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        ShowCursor();
    }
    private void MenuClosed()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, false);
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
        HideCursor();
    }
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
