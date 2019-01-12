using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Timeline timeline;
    public Image timelineDisplay;
    public Image pointer;
    public Text phaseDisplay;
	public UnitInfoContainer unitInfoContainer;

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

    public void ShowDeploymentPhaseUI(string playerName)
    {
        phaseDisplay.text = "Deployment - " + playerName;
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

    public void DisplayTimelineIcons(Plan plan)
    {
        ClearTimelineIcons();
        if(plan != null) {
            int frames = plan.GetTotalFramesCount();
            for(int i = 0; i < frames; i++) {
                Command command = plan.GetCommand(i);
                if (command.frame.icon)
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
                }
            }
        }
    }

	public void DisplayUnitInfo(Player viewer, Unit unit)
	{
		unitInfoContainer.UpdateUnitInfoDisplay(viewer, unit);
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
