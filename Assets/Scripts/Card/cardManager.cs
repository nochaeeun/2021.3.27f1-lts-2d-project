using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;
using UnityEngine.UI;

public class cardManager : MonoBehaviour
{

    // 여기서는 패에 들어온 카드만 관리하기로 합시다.

    public deckSystem deckSystem;
    public DataManager dataManager;

    public GameObject cardPrefab; // 인스펙터에서 카드 프리팹 할당
    public Transform handTransform; // 손 위치 (임의로 일단 할당)

    public float cardSpread = 7.5f; // 카드 간 각도

    public float cardSpacing = -166f;  // 카드 간격

    public float verticalSpacing = 49f; // ???

    public List<GameObject> inHandCards = new List<GameObject>(); // 손에 있는 카드 목록

    void Start()
    {
        
    }

    void Update(){
        //UpdateHandVisual();
        
    }


    public void AddCardToHand(int id)
    {
        // instantiate card prefab
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);  // 카드 프리팹을 인스턴스화하여 새 카드 생성
        
        inHandCards.Add(newCard);               // 새로 생성된 카드를 손에 있는 카드 목록에 추가

        // List<Card> _cardData = deckSystem.listUpPlayerDeck();
        // Card card_Data = dataManager.GetCardById(id);
        // Card cardData = _cardData[0];
        Card cardData = dataManager.GetCardById(id);

        //인스턴스화된 카드의 CardData 설정
        newCard.GetComponent<cardDisplay>().cardData = cardData;
        UpdateHandVisual();                     // 손에 있는 카드들의 시각적 배치를 업데이트
    }

    // UpdateHandVisual 함수는 손에 있는 카드들의 시각적 배치를 업데이트합니다.
    // 각 카드의 회전 각도, 수평 및 수직 위치를 계산하여 설정합니다.
    // 카드 간의 간격과 회전 각도는 cardSpacing과 cardSpread 변수를 통해 조절됩니다.
    private void UpdateHandVisual(){
        int cardCount = inHandCards.Count;


        // 카드 추가 시 cardSpread와 cardSpacing 업데이트
        if(inHandCards.Count == 0) return;
        else if(inHandCards.Count == 1) {
            inHandCards[0].transform.localPosition = new Vector3(0f,0f,0f);
            inHandCards[0].transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }
        else{
            // 카드 수에 따라 cardSpread 조정 (5장일 때 7.5f)
            cardSpread = 7.5f * 5f / cardCount;
            // 카드 수에 따라 cardSpacing 조정 (5장일 때 -166f)
            cardSpacing = -166f * 5f / cardCount;


            for(int i = 0; i < cardCount; i++){
            float rotationAngle = (cardSpread * (i - (cardCount - 1) / 2f));                                 // 카드의 회전 각도 계산
            inHandCards[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);                // 계산된 각도로 카드 회전

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));                             // 카드의 수평 위치 계산

            float normalPosition = (2f * i / (cardCount - 1) - 1f);                                          // 카드의 정규화된 위치 계산 (-1에서 1 사이)

            float verticalOffset = verticalSpacing * (1f - normalPosition * normalPosition);                 // 카드의 수직 위치 계산 (포물선 형태)
            inHandCards[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);      // 카드의 최종 위치 설정
        }
        }
    }
}
