using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;
using UnityEngine.UI;
using System.Linq;

public class cardManager : MonoBehaviour
{
    public deckSystem deckSystem;
    public DataManager dataManager;
    public List<Card> cardInHand = new List<Card>();

    private int cardInHandCount = 0;

    public GameObject cardPrefab;
    public Transform handTransform;

    public float cardSpread = 7.5f;
    public float cardSpacing = -166f;
    public float verticalSpacing = 49f;

    public List<GameObject> inHandCards = new List<GameObject>();
    public List<Card> inHandCardData = new List<Card>();
    public Dictionary<Card, int> cardInstanceIDs = new Dictionary<Card, int>(); // 카드 데이터와 인스턴스 ID를 매핑

    private Transform playerHandPoint;

    void Start()
    {
        deckSystem = FindObjectOfType<deckSystem>();
        dataManager = FindObjectOfType<DataManager>();
        cardInHandCount = cardInHand.Count;
        playerHandPoint = GameObject.Find("playerHandPoint").transform;
        launchAddCardToHand();
        CleanupUnusedCards();
    }

    void Update()
    {
        cardInHand = deckSystem.listUpCardInHand();
        if (cardInHandCount != cardInHand.Count)
        {
            ClearHandCards();
            launchAddCardToHand();
            cardInHandCount = cardInHand.Count;
            CleanupUnusedCards();
        }
    }

    public void launchAddCardToHand()
    {
        for (int i = 0; i < cardInHand.Count; i++)
        {
            AddCardToHand(cardInHand[i]);
        }
    }

    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        inHandCards.Add(newCard);
        inHandCardData.Add(cardData);
        cardInstanceIDs[cardData] = newCard.GetInstanceID(); // 인스턴스 ID 저장
        newCard.GetComponent<cardDisplay>().cardData = cardData;
        UpdateHandVisual();
    }

    public void RemoveCardFromHand(Card cardData)
    {
        int index = inHandCardData.IndexOf(cardData);
        if (index != -1)
        {
            GameObject cardToRemove = inHandCards[index];
            cardToRemove.SetActive(false); // 오브젝트를 비활성화합니다.
            Destroy(cardToRemove);
            inHandCards.RemoveAt(index);
            inHandCardData.RemoveAt(index);
            cardInstanceIDs.Remove(cardData); // 인스턴스 ID 제거
            UpdateHandVisual();
        }
    }

    private void ClearHandCards()
    {
        foreach (GameObject card in inHandCards)
        {
            card.SetActive(false); // 오브젝트를 비활성화합니다.
            Destroy(card);
        }
        inHandCards.Clear();
        inHandCardData.Clear();
    }

    public void UpdateHandVisual()
    {
        int cardCount = inHandCards.Count;

        if (cardCount == 0) return;
        else if (cardCount == 1)
        {
            inHandCards[0].transform.localPosition = Vector3.zero;
            inHandCards[0].transform.localRotation = Quaternion.identity;
        }
        else
        {
            cardSpread = 7.5f * 5f / cardCount;
            cardSpacing = -166f * 5f / cardCount;

            for (int i = 0; i < cardCount; i++)
            {
                float rotationAngle = (cardSpread * (i - (cardCount - 1) / 2f));
                inHandCards[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

                float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
                float normalPosition = (2f * i / (cardCount - 1) - 1f);
                float verticalOffset = verticalSpacing * (1f - normalPosition * normalPosition);
                inHandCards[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
            }
        }
    }

    private void CleanupUnusedCards()
    {
        List<GameObject> playerHandCards = new List<GameObject>();
        foreach (Transform child in playerHandPoint)
        {
            playerHandCards.Add(child.gameObject);
        }

        List<GameObject> cardsToDestroy = playerHandCards.Except(inHandCards).ToList();
        foreach (GameObject card in cardsToDestroy)
        {
            Destroy(card);
        }
    }
}
