using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Timeline timeline;
    public Image timelineDisplay;
    public Image pointer;
    public Text phaseDisplay;

    private float timelineWidth;
    private float frameWidth;
    public float frameTickOffset;
    private float pointerY;
    private RectTransform pointerTransform;
    private List<GameObject> displayedIcons;

    void Start()
    {
        displayedIcons = new List<GameObject>();
        timelineWidth = timelineDisplay.GetComponent<RectTransform>().rect.width;
        frameWidth = timelineWidth / timeline.maxFrame + frameTickOffset;
        pointerTransform = pointer.GetComponent<RectTransform>();
        pointerY = pointerTransform.anchoredPosition.y;
    }

    public void ShowTurnSetupPhaseUI()
    {
        phaseDisplay.text = "Turn Setup";
    }

    public void ShowDeploymentPhaseUI()
    {
        phaseDisplay.text = "Deployment";
    }

    public void ShowTimelinePhaseUI()
    {
        phaseDisplay.text = "Execution";
    }

    public float GetPosXOfTick(int tick)
    {
        return -timelineWidth / 2 + (tick * frameWidth);
    }

    public void UpdateTimelineDisplay(int frame)
    {
        pointerTransform.anchoredPosition = new Vector2(GetPosXOfTick(frame), pointerY);
    }

    public void DisplayTimelineIcons(Queue<Command> commands)
    {
        ClearTimelineIcons();
        if (commands != null)
        {
            int i = 0;
            foreach (Command command in commands)
            {
                GameObject icon = Instantiate(command.frame.icon, timelineDisplay.transform);
                RectTransform rectTransform = icon.GetComponent<RectTransform>();
                if (command.dir == Direction.LEFT)
                {
                    rectTransform.Rotate(0, 0, 90);
                }
                else if (command.dir == Direction.DOWN)
                {
                    rectTransform.Rotate(0, 0, 180);
                }
                else if (command.dir == Direction.RIGHT)
                {
                    rectTransform.Rotate(0, 0, -90);
                }
                float iconY = pointerTransform.anchoredPosition.y - rectTransform.rect.height * 2f;
                rectTransform.anchoredPosition = new Vector3(GetPosXOfTick(i), iconY, 0);
                displayedIcons.Add(icon);
                i++;
            }
        }
    }

    void ClearTimelineIcons()
    {
        foreach (GameObject icon in displayedIcons)
        {
            Destroy(icon);
        }
        displayedIcons.Clear();
    }
}
