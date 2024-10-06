using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }
    
    public CombatManager combatManager;
    public UIManager uiManager;
    public cardManager cardManager;
    public deckSystem deckSystem;
    public charController playerManager;
    public EnemyAi enemyManager;
    public DataManager dataManager;
    
    public enum GameState { Menu, Combat, Pause, GameOver, Win }
    public GameState currentState = GameState.Menu;



    private void Awake()
    {
        // TODO: 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // TODO: 게임 초기화
        // - 필요한 매니저들 초기화
        dataManager = FindObjectOfType<DataManager>();
        dataManager.LoadCardData();
        deckSystem = FindObjectOfType<deckSystem>();
        cardManager = FindObjectOfType<cardManager>();
        playerManager = FindObjectOfType<charController>();
        enemyManager = FindObjectOfType<EnemyAi>();
        uiManager = FindObjectOfType<UIManager>();
        combatManager = FindObjectOfType<CombatManager>();
        // 오브젝트 없다면 생성
        if(dataManager == null) 
            dataManager = new GameObject("DataManager").AddComponent<DataManager>();
        if(deckSystem == null)
            deckSystem = new GameObject("deckSystem").AddComponent<deckSystem>();
        if(cardManager == null)
            cardManager = new GameObject("cardManager").AddComponent<cardManager>();
        if(playerManager == null)
            playerManager = new GameObject("playerManager").AddComponent<charController>();
        if(enemyManager == null)
            enemyManager = new GameObject("enemyManager").AddComponent<EnemyAi>();
        if(uiManager == null)
            uiManager = new GameObject("uiManager").AddComponent<UIManager>();
        if(combatManager == null)
            combatManager = new GameObject("combatManager").AddComponent<CombatManager>();
        // - 초기 게임 상태 설정
        deckSystem.InitializeDeck();
        deckSystem.DrawCard(5);
        StartCombat();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: 프레임별 게임 로직 업데이트
    }

    public void StartNewGame() 
    {
        // TODO: 새 게임 시작
        // - 플레이어 초기화
        // - 첫 번째 맵 생성
    }

    public void StartCombat() 
    {
        // TODO: 전투 시작
        // - 전투 초기화
        combatManager.StartCombat();
        currentState = GameState.Combat;
    }

    public void GameOver() 
    {
        if (playerManager.isPlayerDie()) // playerManager에서 플레이어 체력을 가져와야함
        {
            currentState = GameState.GameOver;
        }
        else if(enemyManager.Health <= 0) // enemyManager에서 적 리스트를 가져와야함
        {
            currentState = GameState.Win;
        }

        // TODO: 게임 오버 처리
        // - 점수 계산
        // - 게임 오버 UI 표시
    }

    public void PauseGame() 
    {
        // TODO: 게임 일시 정지
        // - 시간 멈춤
        // - 일시 정지 메뉴 표시
    }

    public void ResumeGame() 
    {
        // TODO: 게임 재개
        // - 시간 재개
        // - 일시 정지 메뉴 숨김
    }

    public void QuitGame() 
    {
        // TODO: 게임 종료
        // - 저장 처리
        // - 애플리케이션 종료
        Application.Quit();
    }

    
    // public void LoadNextFloor() 
    // {
    //     // TODO: 다음 층 로드
    //     // - 새로운 맵 생성
    //     // - 플레이어 위치 초기화
    // }

    // public void UpdateGameState(GameState newState) 
    // {
    //     // TODO: 게임 상태 업데이트
    //     // - 현재 상태 변경
    //     // - 상태에 따른 처리
    //     currentState = newState;
    // }

    public void HandlePlayerDeath() 
    {
        // TODO: 플레이어 사망 처리
        // - 게임 오버 호출
    }

    public void AwardExperience(int amount) 
    {
        // TODO: 경험치 획득
        // - 플레이어 경험치 증가
        // - 레벨업 체크
    }

    public void UnlockAchievement(string achievementId) 
    {
        // TODO: 업적 해금
        // - 업적 시스템에 해금 상태 저장
        // - UI 알림 표시
    }
}
