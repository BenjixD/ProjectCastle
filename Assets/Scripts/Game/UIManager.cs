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

    void Start()
    {
        timelineWidth = timelineDisplay.GetComponent<RectTransform>().rect.width;
        frameWidth = timelineWidth / maxFrames + frameTickOffset;
        pointerTransform = pointer.GetComponent<RectTransform>();
        pointerY = pointerTransform.anchoredPosition.y;
    }

    public void UpdateTimelineDisplay(int frame)
    {
        Debug.Log("update timeline for frame " + (frame+1));
        pointerTransform.anchoredPosition = new Vector2(-timelineWidth / 2 + (frame * frameWidth), pointerY);
    }
}
