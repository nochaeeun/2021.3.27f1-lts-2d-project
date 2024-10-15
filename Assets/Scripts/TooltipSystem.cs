using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipSystem : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI intentionText;
    [SerializeField]
    private RectTransform toolTipPanel;

    [SerializeField]
    private string attackIntentionTip;
    [SerializeField]
    private string costIntentionTip;
    [SerializeField]
    private string defenceIntentionTip;

    private float panelSizeX = 10f;
    private float panelSizeY = 10f;
    [SerializeField]
    private float panelSizeM = 1f;


    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipPanel.gameObject.SetActive(true);
        TooltipPanelSize();
        toolTipPanel.position = Input.mousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipPanel.gameObject.SetActive(false);
    }

    public void SetupToolTip(ActionType intention)
    {
        switch (intention)
        {
            case ActionType.Attack:
                intentionText.text = attackIntentionTip;
                break;
            case ActionType.PlayerCost:
                intentionText.text = costIntentionTip;
                break;
            case ActionType.Defence:
                intentionText.text = defenceIntentionTip;
                break;
        }
        TooltipPanelSize();
    }


    private void TooltipPanelSize()
    {
        Vector2 textSize = intentionText.GetPreferredValues();
        toolTipPanel.sizeDelta = new Vector2(textSize.x + panelSizeX * panelSizeM, textSize.y + panelSizeY * panelSizeM);
    }
}
