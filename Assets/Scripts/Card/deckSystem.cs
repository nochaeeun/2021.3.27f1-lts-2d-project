using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;

public class deckSystem : MonoBehaviour
{
    private List<Card> playerDeck = new List<Card>();         // 플레이어가 보유한 카드 덱
    private List<Card> usedDeck = new List<Card>();           // 사용한 카드 더미
    private List<Card> destroyedDeck = new List<Card>();      // 전투에서 소멸한 카드 더미

    private cardManager cardManager;                         // cardManager 참조

    private List<Card> _allCards = new List<Card>();          // 카드 데이터 모음

    void Start()
    {
        _allCards = DataManager.Instance.cardDatabase;
        cardManager = FindObjectOfType<cardManager>();
        InitializeDeck();
    }

    public void InitializeDeck(){ //시작 덱 구성 : 시작할 때 한번만 할 것을 추천
        // 플레이어 덱 초기화
        playerDeck.Clear();
        // 플레이어 덱에 카드 추가
        for (int i = 0; i < 5; i++)
        {
            AddCardToDeck(1001);
            AddCardToDeck(2001);
        }
        AddCardToDeck(1003);
    }

    public List<Card> listUpPlayerDeck(){
        return playerDeck;
    }

    public List<Card> listUpUsedDeck(){
        return usedDeck;
    }

    public List<Card> listUpDestroyedDeck(){
        return destroyedDeck;
    }

    // 덱을 초기화하고 시작 카드를 뽑는 메서드
    // private void InitializeDeck()
    // {
    //     // Card[] allCardData = Resources.LoadAll<Card>("cardsData");
    //     playerDeck.AddRange(allCardData);
    //     ShuffleDeck();

    //     for (int i = 0; i < 5; i++)
    //     {
    //         DrawCard();
    //     }
    // }

    // 카드를 뽑는 메서드
    public void DrawCard()
    {
        if (playerDeck.Count == 0)
        {
            if (usedDeck.Count > 0)
            {
                ResetDeck();
            }
            else
            {
                Debug.Log("덱에 카드가 없습니다.");
                return;
            }
        }

        Card nextCard = playerDeck[0]; // x로 둔 이유는 카드 덱의 개념이 확실한 상태아니기 때문
        playerDeck.RemoveAt(0);
        cardManager.AddCardToHand(nextCard);
    }

    // 덱을 섞는 메서드
    private void ShuffleDeck()
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            Card temp = playerDeck[i];
            int randomIndex = Random.Range(i, playerDeck.Count);
            playerDeck[i] = playerDeck[randomIndex];
            playerDeck[randomIndex] = temp;
        }
    }

    // 사용한 카드 더미를 다시 덱으로 되돌리는 메서드
    private void ResetDeck()
    {
        playerDeck.AddRange(usedDeck);
        usedDeck.Clear();
        ShuffleDeck();
    }

    // 카드를 사용한 더미로 이동시키는 메서드
    public void UseCard(Card card)
    {
        // 카드를 사용한 더미로 이동
        cardManager.inHandCards.Remove(card);
        usedDeck.Add(card);
    }

    // 카드를 소멸 더미로 이동시키는 메서드
    public void DestroyCard(Card card)
    {
        // 카드를 소멸 더미로 이동
        cardManager.inHandCards.Remove(card);
        destroyedDeck.Add(card);
    }

    // 턴이 끝날 때 호출되는 메서드
    public void EndTurn()
    {
        // cardManager의 inHandCards에 있는 모든 카드를 사용한 카드 더미로 이동
        foreach (GameObject cardObject in cardManager.inHandCards)
        {
            Card card = cardObject.GetComponent<cardDisplay>().cardData;
            usedDeck.Add(card);
        }
        // 손에 있는 카드 목록을 비움
        cardManager.inHandCards.Clear();
    }

    public int GetDeckSize()
    {
        return playerDeck.Count;
    }

    public void AddCardToDeck(int cardId)
    {
        // 카드 데이터 목록에서 지정된 이름의 카드 찾기
        CardData specificCardData = DataManager.Instance.GetCardById(cardId);

        if (specificCardData != null)
        {
            // 찾은 카드 데이터로 새 카드 인스턴스 생성
            Card newCard = new Card(specificCardData);

            // - 새 카드를 덱에 추가
            if (playerDeck == null)
            {
                playerDeck = new List<Card>();
            }
            playerDeck.Add(newCard);

            Debug.Log($"'{newCard.cardName}' 카드가 플레이어 덱에 추가되었습니다.");
        }
        else
        {
            Debug.LogWarning($"'{cardId}' 이름의 카드를 찾을 수 없습니다.");
        }
    }

    public Card GetRandomCard()
    {
        // TODO: 덱에서 무작위 카드 선택
        if (playerDeck.Count > 0)
        {
            int randomIndex = Random.Range(0, playerDeck.Count);        // - 덱에서 무작위로 카드 선택
            return playerDeck[randomIndex];                             // - 선택된 카드 반환
        }
        else
        {
            Debug.LogWarning("덱에 카드가 없습니다.");
            return null;
        }
    }

    public List<Card> GetPlayableCards() // 구현 x
    {
        // TODO: 카드를 소멸 파일로 이동
        // - 카드를 핸드나 덱에서 제거
        // - 소멸 파일에 카드 추가
        return null;
    }

    public void ReturnExhaustedCards() // 구현 x
    {
        // TODO: 소멸된 카드들을 덱으로 반환
        // - 소멸 파일의 모든 카드를 덱에 추가
        // - 덱 셔플
        // return null;
    }

    public void SortDeckByEnergyCost() // 구현 x
    {
        // TODO: 덱을 에너지 비용 순으로 정렬
        // - 덱의 카드를 에너지 비용 기준으로 정렬
        // return null;
    }

    public void TransformCard(Card oldCard, Card newCard) // 구현 x
    {
        // TODO: 카드 변환
        // - 지정된 카드를 덱에서 제거
        // - 새로운 카드를 덱에 추가
        // return null;
    }



}
