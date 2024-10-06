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
    public Image[] cardIllust;
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardDescription;

    public deckSystem deckSystem;
    public cardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        allcardData = Resources.LoadAll<Card>("cardsData");
        // SetCardData(deckSystem.listUpCardInHand()[0]); // 카드 생성시 데이터 설정
    }

    public void updateCardDisplay() {
        //Debug.Log(cardData._cost);
        cardCost.text = cardData._cost.ToString();
        cardName.text = cardData._cardName;
        AdjustFontSize(cardName, cardData._cardName, 0.24f, 0.36f);

        for (int i = cardIndex; i < cardIllust.Length; i++){
            if(cardIllust[i].name == cardName.text){
                cardIllust[i].gameObject.SetActive(true);
            }else{
                cardIllust[i].gameObject.SetActive(false);
            }
        }
        cardType.text = $"{cardData._cardType}";
        cardDescription.text = cardData._description;
        AdjustFontSize(cardDescription, cardData._description, 0.18f, 0.33f);

    }    

    // Update is called once per frame
    void Update() {  
        updateCardDisplay();
    }


    void AdjustFontSize(TextMeshProUGUI textComponent, string content, float min, float max) {
        textComponent.text = content;
        textComponent.enableAutoSizing = true;
        textComponent.fontSizeMin = min;
        textComponent.fontSizeMax = max;
        textComponent.ForceMeshUpdate();
    }


    public void SetCardData(int cID) {
        // 받은 cID를 통해서 allcardData에서 카드 데이터를 찾아서 cardData에 저장
        foreach(Card card in allcardData){
            if(card._cardID == cID){
                cardData = card;
                break;
            }
        }
        updateCardDisplay();
    }
}
