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
        // - 초기 게임 상태 설정
        deckSystem.InitializeDeck();
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

    public void EndTurn() 
    {
        // TODO: 턴 종료 처리
        // - 플레이어 상태 업데이트
        // - 적 행동 처리
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
    }

    /*
    public void LoadNextFloor() 
    {
        // TODO: 다음 층 로드
        // - 새로운 맵 생성
        // - 플레이어 위치 초기화
    }
    */

    public void UpdateGameState(GameState newState) 
    {
        // TODO: 게임 상태 업데이트
        // - 현재 상태 변경
        // - 상태에 따른 처리
    }

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
