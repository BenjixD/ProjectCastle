using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Game gameManager;
    public GameObject deploymentPhaseControl;

    private List<GameObject> actionButtons;
    public GameObject actionsMenu;
    public GameObject buttonTemplate;
    private float buttonTemplateHeight;

    public List<GameObject> deployPhaseButtons;
    public GameObject deployMenu;

    public Vector3 listTopPosition;

    void Awake()
    {
        actionButtons = new List<GameObject>();
        buttonTemplateHeight = buttonTemplate.GetComponent<RectTransform>().rect.height;
        listTopPosition = actionsMenu.transform.position + new Vector3(0, actionsMenu.GetComponent<RectTransform>().rect.height / 2 - buttonTemplateHeight / 2, 0);
    }

    void SelectFirst(List<GameObject> buttons)
    {
        if (buttons.Count > 0)
        {
            buttons[0].GetComponent<Button>().Select();
            buttons[0].GetComponent<Button>().OnSelect(null);
        }
    }

    public void OpenActionsMenu(Unit unit)
    {
        CleanButtons(actionButtons);
        foreach (Action skill in unit.skills)
        {
            GameObject newButton = Instantiate(buttonTemplate, listTopPosition - new Vector3(0, actionButtons.Count * buttonTemplateHeight, 0), Quaternion.identity, actionsMenu.transform);
            newButton.GetComponentInChildren<Text>().text = skill.actionName;
            newButton.GetComponent<Button>().onClick.AddListener(DisableDeployControl);
            newButton.GetComponent<Button>().onClick.AddListener(CloseAllMenus);
            newButton.GetComponent<Button>().onClick.AddListener(delegate { skill.Select(unit); });
            actionButtons.Add(newButton);
        }
        SetupNavigation(actionButtons);
        SelectFirst(actionButtons);
        actionsMenu.SetActive(true);
    }

    public void CloseActionsMenu()
    {
        actionsMenu.SetActive(false);
    }

    public bool ActionMenuActive()
    {
        return actionsMenu.activeSelf;
    }

    public void OpenPhaseMenu()
    {
        SelectFirst(deployPhaseButtons);
        deployMenu.SetActive(true);
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

    public void EndDeployPhase()
    {
        //gameManager.nextTurn = true;
        gameManager.endPhase = true;
        ClosePhaseMenu();
        DisableDeployControl();
    }

    public void DisableDeployControl()
    {
        deploymentPhaseControl.SetActive(false);
    }
}
