using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectSCCard;
using UnityEngine.UI;
using TMPro;

public class cardDisplay : MonoBehaviour
{
    // 모든 카드 데이터를 저장하는 배열
    public Card[] allcardData;
    // 현재 카드의 인덱스
    public int cardIndex;
    // 현재 표시되는 카드의 데이터
    public Card cardData;
    // 카드 비용을 표시하는 UI 텍스트
    public TextMeshProUGUI cardCost;
    // 카드 이름을 표시하는 UI 텍스트
    public TextMeshProUGUI cardName;
    // 카드 일러스트를 표시하는 이미지 배열
    public Image[] cardIllust;
    // 카드 타입을 표시하는 UI 텍스트
    public TextMeshProUGUI cardType;
    // 카드 설명을 표시하는 UI 텍스트
    public TextMeshProUGUI cardDescription;

    // 덱 시스템 참조
    public deckSystem deckSystem;
    // 카드 매니저 참조
    public cardManager cardManager;

    // 시작 시 호출되는 메서드
    void Start()
    {
        // Resources 폴더에서 모든 Card 타입의 에셋을 로드하여 allcardData에 저장
        
        cardManager = FindObjectOfType<cardManager>();
        deckSystem = FindObjectOfType<deckSystem>();

        allcardData = Resources.LoadAll<Card>("cardsData");
        // 주석 처리된 코드: 첫 번째 카드 데이터로 카드 표시를 설정
        // SetCardData(deckSystem.listUpCardInHand()[0]);
    }

    // 매 프레임마다 호출되는 메서드
    void Update() {  
        // 카드 디스플레이 업데이트
        updateCardDisplay();
    }

    // 카드 디스플레이를 업데이트하는 메서드
    private void updateCardDisplay() {
        // 카드 비용 표시
        cardCost.text = cardData._cost.ToString();
        // 카드 이름 표시
        cardName.text = cardData._cardName;
        // 카드 이름의 폰트 크기를 조정
        AdjustFontSize(cardName, cardData._cardName, 0.24f, 0.36f);

        // 카드 일러스트 설정
        for (int i = cardIndex; i < cardIllust.Length; i++){
            if(cardIllust[i].name == cardName.text){
                cardIllust[i].gameObject.SetActive(true);
            }else{
                cardIllust[i].gameObject.SetActive(false);
            }
        }
        // 카드 타입 표시
        cardType.text = $"{cardData._cardType}";
        // 카드 설명 표시
        cardDescription.text = cardData._description;
        // 카드 설명의 폰트 크기를 조정
        AdjustFontSize(cardDescription, cardData._description, 0.18f, 0.33f);
    }    

    // 텍스트 컴포넌트의 폰트 크기를 자동으로 조정하는 메서드
    void AdjustFontSize(TextMeshProUGUI textComponent, string content, float min, float max) {
        textComponent.text = content;
        textComponent.enableAutoSizing = true;
        textComponent.fontSizeMin = min;
        textComponent.fontSizeMax = max;
        textComponent.ForceMeshUpdate();
    }

    // 카드 ID를 받아 해당 카드의 데이터를 설정하는 메서드
    public void SetCardData(int cID) {
        // 받은 cID와 일치하는 카드 데이터를 allcardData에서 찾아 cardData에 설정
        foreach(Card card in allcardData){
            if(card._cardID == cID){
                cardData = card;
                break;
            }
        }
        // 카드 디스플레이 업데이트
        updateCardDisplay();
    }

    // 현재 표시되는 카드를 반환하는 메서드
    public Card GetCard(){
        // 현재 표시되는 카드 데이터 반환
        return cardData;
    }
}
