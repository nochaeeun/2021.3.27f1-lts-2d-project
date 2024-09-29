using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectSCCard;
using UnityEngine.UI;
using TMPro;

public class cardDisplay : MonoBehaviour
{
    public Card[] allcardData;
    public int cardIndex;
    public Card cardData;
    public TextMeshProUGUI cardCost;
    public TextMeshProUGUI cardName;
    public Image[] cardIllustration;
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardDescription;

    // Start is called before the first frame update
    void Start()
    {
        allcardData = Resources.LoadAll<Card>("cardsData");
        SetCardData(cardIndex);
    }

    public void updateCardDisplay() {
        Debug.Log(cardData._cost);
        cardCost.text = cardData._cost.ToString();
        cardName.text = cardData._cardName;
        AdjustFontSize(cardName, cardData._cardName, 0.24f, 0.36f);

        for (int i = cardIndex; i < cardIllustration.Length; i++){
            if(cardIllustration[i].name == cardName.text){
                cardIllustration[i].gameObject.SetActive(true);
            }else{
                cardIllustration[i].gameObject.SetActive(false);
            }
        }
        cardType.text = $"{cardData._cardType}";
        cardDescription.text = cardData._description;
        AdjustFontSize(cardDescription, cardData._description, 0.18f, 0.33f);

    }    

    // Update is called once per frame
    void Update() {  }


    void AdjustFontSize(TextMeshProUGUI textComponent, string content, float min, float max) {
        textComponent.text = content;
        textComponent.enableAutoSizing = true;
        textComponent.fontSizeMin = min;
        textComponent.fontSizeMax = max;
        textComponent.ForceMeshUpdate();
    }


    public void SetCardData(int index) {
        cardData = allcardData[index];
        updateCardDisplay();
    }
}
