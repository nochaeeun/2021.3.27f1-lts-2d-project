using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ProjectSCCard;

public class DataManager : MonoBehaviour
{
    // 외부 컨트롤러
    public charController playerManager;
    public EnemyAi enemyAi;
    public GameManager gameManager;
    public deckSystem deckManager;
    public UIManager uiManager;
    public CombatManager combatManager;

    // 카드 데이터
    public List<Card> cardDatabase;
    public Card[] cardDataArray;


    // 싱글톤 인스턴스
    private static DataManager instance;

    // 싱글톤 인스턴스에 접근하기 위한 프로퍼티
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 씬에서 DataManager 오브젝트를 찾음
                instance = FindObjectOfType<DataManager>();

                // 씬에 DataManager가 없다면 새로 생성
                if (instance == null)
                {
                    GameObject obj = new GameObject("DataManager");
                    instance = obj.AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        // TODO: 데이터 로드
        // 불러온 외부 컨트롤러 초기화
        playerManager = FindObjectOfType<charController>();
        enemyAi = FindObjectOfType<EnemyAi>();
        gameManager = FindObjectOfType<GameManager>();
        deckManager = FindObjectOfType<deckSystem>();
        uiManager = FindObjectOfType<UIManager>();
        combatManager = FindObjectOfType<CombatManager>();
        // 없다면 생성
        if(playerManager == null)
            playerManager = new GameObject("playerManager").AddComponent<charController>();
        if(enemyAi == null)
            enemyAi = new GameObject("enemyAi").AddComponent<EnemyAi>();
        if(gameManager == null)
            gameManager = new GameObject("gameManager").AddComponent<GameManager>();
        if(deckManager == null)
            deckManager = new GameObject("deckManager").AddComponent<deckSystem>();
        if(uiManager == null)
            uiManager = new GameObject("uiManager").AddComponent<UIManager>();
        if(combatManager == null)
            combatManager = new GameObject("combatManager").AddComponent<CombatManager>();
    }

    public void LoadCardData()
    {
        // TODO: 카드 데이터 로드
        // - JSON 또는 ScriptableObject에서 카드 정보 로드
        // 리소스 폴더에서 모든 Card ScriptableObject 로드
        cardDataArray = Resources.LoadAll<Card>("cardsData");
        // Debug.Log($"cardsData 폴더에서 {cardDataArray.Length}개의 카드 데이터를 로드했습니다.");
        
        // 로드된 카드 데이터를 저장할 Dictionary 생성
        cardDatabase = new List<Card>();
        
        // 각 카드 데이터를 Dictionary에 추가
        foreach (Card card in cardDataArray)
        {
            if (!cardDatabase.Contains(card))
            {
                cardDatabase.Add(card);
            }
            else
            {
                Debug.LogWarning($"중복된 카드 ID 발견: {card._cardID}");
            }
        }
        
        // Debug.Log($"총 {cardDatabase.Count}개의 카드 데이터가 로드되었습니다.");
    }

    public void PlayerDeckData()
    {
        // TODO: 플레이어 덱 데이터 로드
        // 플레이어 덱 데이터를 로드하는 로직을 구현
        // 예를 들어, 플레이어의 덱 데이터를 저장하는 변수를 선언하고, 해당 변수에 데이터를 로드하는 코드를 작성
        // 로드된 데이터는 플레이어의 덱 데이터를 나타내는 변수에 저장될 수 있음
        // 임시로 플레이어 덱 데이터를 저장하고 불러오는 코드를 작성
        // 플레이어 덱 데이터는 세이브파일에서 관리할 예정
    }

    public void LoadEnemyData()
    {
        // TODO: 적 데이터 로드
        // - JSON 또는 ScriptableObject에서 적 정보 로드
    }

    public void LoadRelicData()
    {
        // TODO: 유물 데이터 로드
        // - JSON 또는 ScriptableObject에서 유물 정보 로드
    }

    public void LoadEventData()
    {
        // TODO: 이벤트 데이터 로드
        // - JSON 또는 ScriptableObject에서 이벤트 정보 로드
    }

    public void LoadAchievementData()
    {
        // TODO: 업적 데이터 로드
        // - JSON 또는 ScriptableObject에서 업적 정보 로드
    }

    public Card GetCardById(int cardId)
    {
        // TODO: ID로 카드 데이터 검색
        foreach (Card Icard in cardDatabase)
        {
            // Debug.Log($"카드 ID: {card._cardID}, 카드 이름: {card._cardName}");
            if (Icard._cardID == cardId)
            {
                // Debug.Log($"카드 ID: {card._cardID}, 카드 이름: {card._cardName}");
                return Icard;
            }
        }
        // Debug.LogWarning($"ID {cardId}에 해당하는 카드를 찾을 수 없습니다.");
        return null;
    }

    public EnemyAi GetEnemyById(int enemyId)
    {
        // TODO: ID로 적 데이터 검색
        // - 지정된 ID에 해당하는 적 데이터 반환
        return null;
    }

    // public Relic GetRelicById(string relicId)
    // {
    //     // TODO: ID로 유물 데이터 검색
    //     // - 지정된 ID에 해당하는 유물 데이터 반환
    //     return null;
    // }

    public void UpdateLocalizations()
    {
        // TODO: 현지화 데이터 업데이트
        // - 선택된 언어에 따라 텍스트 데이터 업데이트
    }

    public void CacheFrequentlyUsedData()
    {
        // TODO: 자주 사용되는 데이터 캐싱
        // - 성능 향상을 위해 자주 접근하는 데이터 메모리에 캐싱
    }

    public void ReloadAllData()
    {
        // TODO: 모든 데이터 리로드
        // - 모든 게임 데이터 다시 로드
    }
}
