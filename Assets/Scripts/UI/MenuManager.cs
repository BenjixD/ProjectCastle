using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState {
    INACTIVE = 0,
    ACTIVE = 1
}

public class MenuManager : MonoBehaviour {

    public Game gameManager;

    private List<GameObject> actionButtons;
    public GameObject actionsMenu;
    public GameObject buttonTemplate;
    private float buttonTemplateHeight;

    public List<GameObject> deployPhaseButtons;
    public GameObject deployMenu;

    public Vector3 actionListTopPosition;
    public Vector3 deployListTopPosition;
    
    private MenuState state;
    private Cursor cursor;

    void Awake()
    {
        state = MenuState.INACTIVE;
        actionButtons = new List<GameObject>();
        buttonTemplateHeight = buttonTemplate.GetComponent<RectTransform>().rect.height;
        actionListTopPosition = actionsMenu.transform.position + new Vector3(0, actionsMenu.GetComponent<RectTransform>().rect.height / 2 - buttonTemplateHeight / 2, 0);
        deployListTopPosition = deployMenu.transform.position + new Vector3(0, deployMenu.GetComponent<RectTransform>().rect.height / 2 - buttonTemplateHeight / 2, 0);
    }

    void Update() {
        if(state == MenuState.ACTIVE) {
            if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
            {
                CloseAllMenus();
            }
        }
    }

    void SelectFirst(List<GameObject> buttons)
    {
        if (buttons.Count > 0)
        {
            buttons[0].GetComponent<Button>().Select();
            buttons[0].GetComponent<Button>().OnSelect(null);
        }
    }

    public void OpenActionsMenu(Unit unit, Cursor cursor)
    {
        CleanButtons(actionButtons);
        foreach (Action skill in unit.skills)
        {
            GameObject newButton = Instantiate(buttonTemplate, actionListTopPosition - new Vector3(0, actionButtons.Count * buttonTemplateHeight, 0), Quaternion.identity, actionsMenu.transform);
            newButton.GetComponentInChildren<Text>().text = skill.actionName;
            newButton.GetComponent<Button>().onClick.AddListener(delegate {
                CloseAllMenus();
                StartCoroutine(DisableCursorUI(cursor, skill.Select(unit, gameManager.GetTimelinePhase(), EnableCursorUI(cursor))));
            });
            actionButtons.Add(newButton);
        }
        SetupNavigation(actionButtons);
        SelectFirst(actionButtons);
        actionsMenu.SetActive(true);
        SetMenuActive(cursor);
    }

    public void CloseActionsMenu()
    {
        actionsMenu.SetActive(false);
    }

    public bool ActionMenuActive()
    {
        return actionsMenu.activeSelf;
    }

    public void OpenPhaseMenu(Cursor cursor)
    {
        CleanButtons(deployPhaseButtons);
        GameObject newButton = Instantiate(buttonTemplate, deployListTopPosition - new Vector3(0, deployPhaseButtons.Count * buttonTemplateHeight, 0), Quaternion.identity, deployMenu.transform);
        newButton.GetComponentInChildren<Text>().text = "End Deployment";
        newButton.GetComponent<Button>().onClick.AddListener(delegate {
            CloseAllMenus();
            EndDeployPhase(cursor);
        });
        deployPhaseButtons.Add(newButton);

        SetupNavigation(deployPhaseButtons);
        SelectFirst(deployPhaseButtons);
        deployMenu.SetActive(true);
        SetMenuActive(cursor);
    }

    public void ClosePhaseMenu()
    {
        deployMenu.SetActive(false);
    }

    public bool PhaseMenuActive()
    {
        return deployMenu.activeSelf;
    }

    public bool MenuActive()
    {
        return ActionMenuActive() || PhaseMenuActive();
    }

    public void CloseAllMenus()
    {
        CloseActionsMenu();
        ClosePhaseMenu();
        SetMenuInactive();
    }

    private void SetupNavigation(List<GameObject> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Navigation nav = buttons[i].GetComponent<Button>().navigation;
            if (i == 0)
            {
                // Set first button's up to the last element
                nav.selectOnUp = buttons[buttons.Count - 1].GetComponent<Button>();
            }
            else
            {
                nav.selectOnUp = buttons[i - 1].GetComponent<Button>();
            }
            if (i == buttons.Count - 1)
            {
                // Set last button's down to the first element
                nav.selectOnDown = buttons[0].GetComponent<Button>();
            }
            else
            {
                nav.selectOnDown = buttons[i + 1].GetComponent<Button>();
            }
            buttons[i].GetComponent<Button>().navigation = nav;
        }
    }

    private void CleanButtons(List<GameObject> buttons)
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons.Clear();
    }

    public void EndDeployPhase(Cursor cursor)
    {
        cursor.EndUserInput();
    }

    private void SetMenuActive(Cursor cursor) {
        state = MenuState.ACTIVE;
        //TODO: Remove this if statement check
        if(this.cursor != null) {
            Debug.Log("Previous Cursor was not freed!");
        }
        cursor.DisableUserInput();
        this.cursor = cursor;
    }

    private void SetMenuInactive() {
        state = MenuState.INACTIVE;
        //TODO: Remove this if statement check
        if(this.cursor == null) {
            Debug.Log("Stored cursor was lost!");
        }
        this.cursor.EnableUserInput();
        this.cursor = null;
    }

    IEnumerator DisableCursorUI(Cursor cursor, IEnumerator action) {
        cursor.DisableUserInput();
        cursor.gameObject.SetActive(false);
        StartCoroutine(action);
        yield return null;
    }

    IEnumerator EnableCursorUI(Cursor cursor) {
        cursor.EnableUserInput();
        cursor.gameObject.SetActive(true);
        yield return null;
    }
}
