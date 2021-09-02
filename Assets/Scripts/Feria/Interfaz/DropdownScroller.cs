using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownScroller : MonoBehaviour, ISelectHandler
{
    public RectTransform content;
    public RectTransform rectTransform;

    public void OnSelect(BaseEventData eventData)
    {
        float top = rectTransform.anchoredPosition.y;
        float bottom = top - rectTransform.rect.height;

        float contentTop = content.anchoredPosition.y;
        float contentBottom = contentTop + 140;

        if ((374-top) < contentTop)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y = Mathf.Clamp(374-top, 0, 238);
            content.anchoredPosition = pos;
        }
        else if ((374-bottom) > contentBottom)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y = Mathf.Clamp(374-top-120, 0, 238);
            content.anchoredPosition = pos;
        }
    }
}
