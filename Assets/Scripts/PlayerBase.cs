using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using ProjectSCCard;
using UnityEngine.UI;
using TMPro;

public class PlayerBase : MonoBehaviour
{
    public TextMeshProUGUI costText;
    private Player player; 
    private deckSystem deckSys; // deckSystem�� ����

    public class Player
    {
        public const int MaxCost = 5; //max �ڽ�Ʈ 5
        private const int MaxUneasy = 50; //  �Ҿȵ��� �ִ밪 50
        public const int MaxDeckSize = 5;

        public int CurrentCost { get;  set; } // ���� �ڽ�Ʈ
        public int Uneasy { get; set; } // �Ҿȵ�/ �Ҿȵ��� �ʱⰪ��
        public int Stastability { get; set; } // �������� ���۵� ������ �ʱ�ȭ

        // �ܺο��� ī�� �ҷ�����
        public List<Card> Deck { get; private set; } // ��(���� ī�� ���)
        //public List<Card> DiscardPile { get; private set; } // ���� ī�� ���� (���� ���) �̰� ��� �����ؾ� ����
        // ����� ī�� +1 ������ �Ҵ�?

        public Player()
        {
            //CurrentCost = 3; // ù ������ 3
            Uneasy = 0;
            //Stastability = 0;
            Deck = new List<Card>();
        }
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

    public void StartGame() // ���� ���� �� ȣ��� �޼���
    {

        player = new Player();
        /*deckSys = FindObjectOfType<deckSystem>(); // deckSys ã��

        if (deckSys != null)
        {
            // �÷��̾� ���� �� ���� �ʱ�ȭ
            player = new Player();
            // Player ���� �ʱ�ȭ �� ī�� �̱�
            StartTurn(); //ù������

            // �������� ���� ī�� ��� (����)
            foreach (var card in player.Deck)
            {
              //  Debug.Log($"���� ī��: {card.Name}");
            }
        }
        else
        {
            Debug.LogError("deckSystem�� ã�� �� �����ϴ�.");
        }*/

        StartTurn(); //ù������
    }

    public void  StartTurn()
    {
        player.CurrentCost += 3;//�ڽ�Ʈ 3 ����
        player.CurrentCost = Mathf.Min(player.CurrentCost, Player.MaxCost);
        //  player.Stastability = 0;
        //player.Deck.Clear(); // �� �ʱ�ȭ �� ī�� �̱�
        //DrawRandomCards(); // ī�� �̱�

        UpdateCostUI(); // ���� ���۵� ������ �Ҵ�
    }

    public void UpdateCostUI() //costText �� �� �ϸ��� +3 �Ҵ��
    {
        if (costText != null)
        {
            // CurrentCost�� �ִ� 5�� ������ ������ UI �ؽ�Ʈ�� ������Ʈ
            costText.text = "(" + player.CurrentCost.ToString() + " / 5)";
        }
        else
        {
            Debug.LogWarning("costText�� �Ҵ���� �ʾҽ��ϴ�.");
        }
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
}
