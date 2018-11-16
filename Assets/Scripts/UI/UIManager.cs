using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int maxFrames;
    public Image timelineDisplay;
    public Image pointer;

    private float timelineWidth;
    private float frameWidth;
    public float frameTickOffset;
    private float pointerY;
    private RectTransform pointerTransform;

    public GameObject testIcon; //TODO:remove
    public List<GameObject> displayedIcons;
    private float iconsY;

    void Start()
    {
        displayedIcons = new List<GameObject>();
        timelineWidth = timelineDisplay.GetComponent<RectTransform>().rect.width;
        frameWidth = timelineWidth / maxFrames + frameTickOffset;
        pointerTransform = pointer.GetComponent<RectTransform>();
        pointerY = pointerTransform.anchoredPosition.y;
        iconsY = pointerTransform.anchoredPosition.y - testIcon.GetComponent<RectTransform>().rect.height*2f;
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
                rectTransform.anchoredPosition = new Vector3(GetPosXOfTick(i), iconsY, 0);
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
