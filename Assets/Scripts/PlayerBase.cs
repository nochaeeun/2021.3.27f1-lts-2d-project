using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using ProjectSCCard;

public class PlayerBase : MonoBehaviour
{
    private Player player;
    private deckSystem deckSys; // deckSystem을 참조

    public class Player
    {
        private const int MaxCost = 5; //max 코스트 5
        private const int MaxUneasy = 50; //  불안도의 최대값 50
        public const int MaxDeckSize = 5;

        public int CurrentCost { get;  set; } // 현재 코스트
        public int Uneasy { get; set; } // 불안도/ 불안도의 초기값은
        public int Stastability { get; set; } // 안정도는 시작될 때마다 초기화

        // 외부에서 카드 불러오기
        public List<Card> Deck { get; private set; } // 덱(진찰 카드 목록)
        //public List<Card> DiscardPile { get; private set; } // 버린 카드 더미 (진료 기록) 이건 어디서 관리해야 하지
        // 사용한 카드 +1 때마다 할당?

        public Player()
        {
            CurrentCost = 3; // 첫 시작은 3
            Uneasy = 0;
            //Stastability = 0;
            Deck = new List<Card>();
        }
    }

    public void DrawRandomCards() // 랜덤으로 카드 5장 뽑기 // 추후 매니저로 이동??
    {
        List<Card> allCards = deckSys.allCards;

        // 카드 수가 충분한지 확인
        if (allCards.Count >= Player.MaxDeckSize)
        {
            for (int i = 0; i < Player.MaxDeckSize; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, allCards.Count);
                Card randomCard = allCards[randomIndex];
                player.Deck.Add(randomCard);
                allCards.RemoveAt(randomIndex); // 중복 방지
                //Deck 에 랜덤 카드가 들어가 있음(5장의)
            }
        }
        else
        {
            Debug.LogWarning("error");
        }
    }

    public void StartGame() // 게임 시작 시 호출될 메서드
    {
        deckSys = FindObjectOfType<deckSystem>(); // deckSys 찾기

        if (deckSys != null)
        {
            // 플레이어 생성 및 상태 초기화
            player = new Player();
            // Player 상태 초기화 및 카드 뽑기
            StartTurn();

            // 랜덤으로 뽑힌 카드 출력 (예시)
            foreach (var card in player.Deck)
            {
              //  Debug.Log($"뽑힌 카드: {card.Name}");
            }
        }
        else
        {
            Debug.LogError("deckSystem을 찾을 수 없습니다.");
        }
    }

    public void  StartTurn()
    {
        player.CurrentCost += 3;
        player.Stastability = 0;
        player.Deck.Clear(); // 덱 초기화 후 카드 뽑기
        DrawRandomCards(); // 카드 뽑기
    }

    //public void 
    // Start is called before the first frame update
    void Start()
    {
       StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        //필요에 따라 NextTurn 호출
    }

    public void NextTurn()
    {

    }
}
