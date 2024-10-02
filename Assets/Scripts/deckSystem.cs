using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;

public class deckSystem : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();           // 모든 카드를 저장하는 리스트

    private int currentIndex = 0;                            // 현재 뽑을 카드의 인덱스

    void Start(){
        Card[] allcardData = Resources.LoadAll<Card>("cardsData");
        allCards.AddRange(allcardData);

        cardManager CManager = FindObjectOfType<cardManager>();
        for (int i = 0; i < 6; i++){
            DrawCard(CManager);
        }
    }

    public void DrawCard(cardManager cardManager){
        if(allCards.Count == 0) return;                      // 카드가 없으면 함수 종료

        Card nextCard = allCards[currentIndex];              // 현재 인덱스의 카드를 가져옴
        cardManager.AddCardToHand(nextCard);                 // 카드 매니저를 통해 손에 카드 추가
        currentIndex = (currentIndex + 1) % allCards.Count;  // 다음 카드 인덱스 계산 (순환)
    }
}
