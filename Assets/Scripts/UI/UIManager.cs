using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Game game;
    public GameObject playerInfoPrefab;
    public Vector2[] playerInfoAnchors;
    public GameObject canvas;

    public int maxFrames;
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
        frameWidth = timelineWidth / maxFrames + frameTickOffset;
        pointerTransform = pointer.GetComponent<RectTransform>();
        pointerY = pointerTransform.anchoredPosition.y;
        SetupPlayerUI();
    }

    void SetupPlayerUI()
    {
        Debug.Log("setup");
        for (int i = 0; i < game.players.Count; i++)
        {
            GameObject playerInfo = Instantiate(playerInfoPrefab, canvas.transform);
            // Set anchor to corresponding corner
            RectTransform rectTransform = playerInfo.GetComponent<RectTransform>();
            rectTransform.anchorMin = playerInfoAnchors[i];
            rectTransform.anchorMax = playerInfoAnchors[i];
            Vector2 position = new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);
            // Reflect on each axis if their anchor value is 1
            position += new Vector2(-2 * position.x, -2 * position.y) * new Vector2(playerInfoAnchors[i].x, playerInfoAnchors[i].y);
            rectTransform.anchoredPosition = position;
            // Fill in information
            playerInfo.GetComponent<PlayerInfoUI>().UpdateInformation(game.players[i]);
        }
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
