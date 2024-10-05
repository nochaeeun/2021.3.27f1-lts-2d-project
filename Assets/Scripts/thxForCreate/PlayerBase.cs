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
        private const int MaxUneasy = 50; //  불안도의 최댓값 50
        public const int MaxDeckSize = 5;

        public int CurrentCost { get;  set; } // 현재 코스트
        public int Uneasy { get; set; } // 불안도 / 불안도의 초기값
        public int Stastability { get; set; } // 안정도는 시작될 때 마다 초기화

        // 외부에서 카드 불러오기
        //public List<Card> Deck { get; private set; } // ��(���� ī�� ���)
        //public List<Card> DiscardPile { get; private set; } // ���� ī�� ���� (���� ���) �̰� ��� �����ؾ� ����
        // ����� ī�� +1 ������ �Ҵ�?

        public Player()
        {
            CurrentCost = 3; // ù ������ 3
            Uneasy = 0;
            //Stastability = 0;
            Deck = new List<Card>();
        }
    }

    public void StartGame() // ���� ���� �� ȣ��� �޼���
    {
        deckSys = FindObjectOfType<deckSystem>(); // deckSys ã��

        if (deckSys != null)
        {
            // �÷��̾� ���� �� ���� �ʱ�ȭ
            player = new Player();
            // Player ���� �ʱ�ȭ �� ī�� �̱�
            StartTurn();

            // �������� ���� ī�� ��� (����)
            foreach (var card in player.Deck)
            {
              //  Debug.Log($"���� ī��: {card.Name}");
            }
        }
        else
        {
            Debug.LogError("deckSystem�� ã�� �� �����ϴ�.");
        }
    }

    public void  StartTurn()
    {
        player.CurrentCost += 3;
        player.Stastability = 0;
        player.Deck.Clear(); // �� �ʱ�ȭ �� ī�� �̱�
        DrawRandomCards(); // ī�� �̱�
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
        //�ʿ信 ���� NextTurn ȣ��
    }

    public void NextTurn()
    {

    }

    /*public void DrawRandomCards() // �������� ī�� 5�� �̱� // ���� �Ŵ����� �̵�??
    {
        List<Card> allCards = deckSys.allCards;

        // ī�� ���� ������� Ȯ��
        if (allCards.Count >= Player.MaxDeckSize)
        {
            for (int i = 0; i < Player.MaxDeckSize; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, allCards.Count);
                Card randomCard = allCards[randomIndex];
                player.Deck.Add(randomCard);
                allCards.RemoveAt(randomIndex); // �ߺ� ����
                //Deck �� ���� ī�尡 �� ����(5����)
            }
        }
        else
        {
            Debug.LogWarning("error");
        }
    }*/
}
