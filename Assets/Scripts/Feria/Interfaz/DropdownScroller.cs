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

        float posOffset = content.sizeDelta.y - 14;
        
        if (posOffset == 14) StartCoroutine(PositionFix());

        if ((posOffset-top) < contentTop)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y = posOffset-top;
            content.anchoredPosition = pos;
        }
        else if ((posOffset-bottom) > contentBottom)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y =posOffset-top-120;
            content.anchoredPosition = pos;
        }
    }
    private IEnumerator PositionFix()
    {
        yield return new WaitForEndOfFrame();
        float top = rectTransform.anchoredPosition.y;
        float bottom = top - rectTransform.rect.height;

        float contentTop = content.anchoredPosition.y;
        float contentBottom = contentTop + 140;

        float posOffset = content.sizeDelta.y - 14;

        if ((posOffset-top) < contentTop)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y = posOffset-top;
            content.anchoredPosition = pos;
        }
        else if ((posOffset-bottom) > contentBottom)
        {
            Vector2 pos = content.anchoredPosition;
            pos.y =posOffset-top-120;
            content.anchoredPosition = pos;
        }
    }
}
