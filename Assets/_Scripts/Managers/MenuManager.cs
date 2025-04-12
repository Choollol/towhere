using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Whether this script's associated menu should close when the UI stack is empty
    [SerializeField] private bool doCloseMenuOnEmpty = true;

    [Header("The main/starting UI page should be named 'Main UI'")]
    [SerializeField] private List<GameObject> uiList;

    private const string MAIN_UI_NAME = "Main UI";

    private string currentUI;

    private Dictionary<string, GameObject> uiDict = new();

    private Stack<string> uiStack = new();
    private void Awake()
    {
        foreach (GameObject ui in uiList)
        {
            uiDict.Add(ui.name, ui);
        }
    }
    public virtual void OnEnable()
    {
        EventMessenger.StartListening(EventKey.CloseMenu, CloseMenu);

        EventMessenger.StartListening(EventKey.SwitchMenuToMain, SwitchToMainUI);
        EventMessenger.StartListening(EventKey.MenuGoBack, GoBack);

        EventMessenger.StartListening(EventKey.SwitchUI, SwitchUIHelper);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening(EventKey.CloseMenu, CloseMenu);

        EventMessenger.StopListening(EventKey.SwitchMenuToMain, SwitchToMainUI);
        EventMessenger.StopListening(EventKey.MenuGoBack, GoBack);

        EventMessenger.StopListening(EventKey.SwitchUI, SwitchUIHelper);
    }
    public virtual void Start()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, true);

        EventMessenger.TriggerEvent(EventKey.MenuOpened, true);

        if (uiDict.TryGetValue(MAIN_UI_NAME, out _))
        {
            SwitchUI(MAIN_UI_NAME);
            uiStack.Pop();
        }
    }
    public void ClearUI()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
    }
    public void SwitchUI(string newUI)
    {
        ClearUI();
        uiStack.Push(currentUI);
        currentUI = newUI;
        uiDict[currentUI].SetActive(true);
    }
    /// <summary>
    /// Messenger helper for SwitchUI().
    /// </summary>
    private void SwitchUIHelper()
    {
        SwitchUI(DataMessenger.GetString(StringKey.NewUIName));
    }
    public void GoBack()
    {
        if (uiStack.TryPop(out string newUI))
        {
            SwitchUI(newUI);
            // Don't push the previous UI page onto the stack
            uiStack.Pop();
        }
        else
        {
            if (doCloseMenuOnEmpty)
            {
                EventMessenger.TriggerEvent(EventKey.CloseMenu);
            }
        }
    }
    private void SwitchToMainUI()
    {
        SwitchUI(MAIN_UI_NAME);
    }
    protected virtual void CloseMenu()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, false);

        EventMessenger.TriggerEvent(EventKey.MenuClosed, true);

        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}