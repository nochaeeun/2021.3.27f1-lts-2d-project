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

    public void updateCardDisplay(){
        cardCost.text = cardData.cost.ToString();
        cardName.text = cardData.cardName;

        for (int i = cardIndex; i < cardIllustration.Length; i++){
            if(cardIllustration[i].name == cardName.text){
                cardIllustration[i].gameObject.SetActive(true);
            }else{
                cardIllustration[i].gameObject.SetActive(false);
            }
        }
        cardType.text = $"{cardData.type}";
        cardDescription.text = cardData.description;

    }    

    // Update is called once per frame
    void Update() {  }



    public void SetCardData(int index){
        cardData = allcardData[index];
        updateCardDisplay();
    }
}
