using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;

public class CombatManager : MonoBehaviour
{
    // 전투 관련 변수들
    private List<EnemyAi> enemies;    // EnemyAi 클래스가 있다고 가정
    private charController player;          // PlayerBase 클래스가 있다고 가정
    private int currentTurn;
    private bool isPlayerTurn;

    public GameObject enemyObj;
    public GameObject playerObj;

    public bool getTurnEndedButtonDown = false;

    [SerializeField] private UIManager uiManager; // UI 매니저 참조
    [SerializeField] private EnemyAi enemy;



    // 전투 시작 메서드
    public IEnumerator StartCombat()
    {
        // 전투 초기화
        enemies = new List<EnemyAi>();  // EnemyAi 클래스가 있다고 가정
        player = FindObjectOfType<charController>();  // PlayerBase 클래스가 있다고 가정
        uiManager = FindObjectOfType<UIManager>();
        enemy = FindObjectOfType<EnemyAi>();

        Debug.Log("전투 초기화 완료");
        
        enemyObj = GameObject.FindWithTag("Enemy");
        playerObj = GameObject.FindWithTag("Player");
        enemy = enemyObj.GetComponent<EnemyAi>();


        currentTurn = 0;
        isPlayerTurn = true;

        Debug.Log("턴 초기화 완료");

        // 적 생성 (임시로 한 마리만 생성)
        // EnemyAi newEnemy = new EnemyAi(); // EnemyAi 클래스가 있다고 가정
        // enemies = new List<EnemyAi>();
        // enemies.Add(newEnemy);
        enemies = new List<EnemyAi>();
        enemy.CallEnemy();
        enemies.Add(enemy);

        Debug.Log("적 생성 완료");

        // 플레이어 상태 초기화
        player.levelInitialize();

        Debug.Log("플레이어 상태 초기화 완료");

        // UI 업데이트
        uiManager.UpdateCombatUI(player, enemies);

        Debug.Log("UI 업데이트 완료");

        // 전투 시작 로그
        LogCombatAction("전투가 시작되었습니다.");

        Debug.Log("전투 시작 로그 기록 완료");

        // 턴 관리 코루틴 시작
        Debug.Log("턴 관리 코루틴 시작");
        // StartCoroutine(ManageTurns());

        // yield return new WaitUntil(() => IsCombatOver());
        yield return StartCoroutine(ManageTurns());
    }

    // 턴 관리 메서드
    private IEnumerator ManageTurns()
    {
        Debug.Log($"턴 관리 메서드 시작 : {IsCombatOver()}");
        while (!IsCombatOver())
        {
            currentTurn++;
            LogCombatAction($"턴 {currentTurn} 시작");

            if (isPlayerTurn)
            {
                Debug.Log($"플레이어 턴 시작 : {isPlayerTurn}");
                yield return StartCoroutine(PlayerTurn());
            }
            else
            {
                Debug.Log($"적 턴 시작 : {isPlayerTurn}");
                yield return StartCoroutine(EnemyTurn());
            }

            isPlayerTurn = !isPlayerTurn; // 턴 종료 시 isPlayerTurn을 토글합니다.
            yield return new WaitForSecondsRealtime(0.5f); // 턴 사이 짧은 대기 시간
        }

        EndCombat();
    }

    // 플레이어 턴 처리 메서드
    private IEnumerator PlayerTurn()
    {
        LogCombatAction("플레이어의 턴입니다.");

        // 적 준비
        Debug.Log("적 준비");
        enemy.PrepareNextAction();
        
        Debug.Log($"턴 시작 코스트 3 회복");
        if(currentTurn >= 2)
            player.GainCost(3);

        // 카드 드로우
        player.deckSystem.DrawCard(5); // 예시로 5장 드로우
        // player.deckSystem.DrawCard(0);

        // 플레이어 행동 처리
        bool turnEnded = false;
        getTurnEndedButtonDown = false;
        Debug.Log($"getTurnEndedButtonDown : {getTurnEndedButtonDown}");
        while (!turnEnded)
        {
            Debug.Log("플레이어 턴 중");
            // 플레이어의 입력을 기다림
            // yield return new WaitUntil(() => uiManager.IsPlayerTurnEnded());
            // yield return new WaitUntil(() => uiManager.CallEndTurn());
            yield return new WaitUntil(() => uiManager.IsTurnEnded());
            Debug.Log($"플레이어 턴 코루틴 재개 종료 버튼 눌림 : {getTurnEndedButtonDown}");
            turnEnded = uiManager.IsTurnEnded();

            // 플레이어가 카드를 사용하는 부분

            
            if(turnEnded){
                player.deckSystem.EndTurn();
                turnEnded = false;
                getTurnEndedButtonDown = false;
                Debug.Log("플레이어 턴 종료");
                uiManager.ResetTurnEndStatus();
                break;
            }
        }

        LogCombatAction("플레이어의 턴이 종료되었습니다.");
        isPlayerTurn = false;
        // yield return null;
    }

    // 적 턴 처리 메서드
    private IEnumerator EnemyTurn()
    {
        LogCombatAction("적의 턴입니다.");

        enemy.PerformAction();

        enemy.PrepareNextAction();

        LogCombatAction("적의 턴이 종료되었습니다.");

        yield return new WaitForSeconds(0.1f);
    }

    // 전투 종료 확인 메서드
    private bool IsCombatOver()
    {
        if (player.isPlayerDie())
        {
            LogCombatAction("플레이어가 패배했습니다.");
            GameManager.Instance.currentState = GameManager.GameState.GameOver;
            return true;
        }

        if (enemies.TrueForAll(e => !e.IsAlive()))
        {
            LogCombatAction("모든 적을 물리쳤습니다!");
            GameManager.Instance.currentState = GameManager.GameState.Win;
            return true;
        }

        return false;
    }

    // 전투 결과 처리 메서드
    private void EndCombat()
    {
        if (player.healthCheck())
        {
            // 승리 보상
            int expGained = CalculateExperienceGain();
            // player.GainExperience(expGained);
            LogCombatAction($"전투에서 승리했습니다! {expGained} 경험치를 획득했습니다.");

            // 아이템 획득 등의 추가 보상 로직...
        }
        else
        {
            LogCombatAction("전투에서 패배했습니다.");
            // 게임 오버 처리
            GameManager.Instance.GameOver();
        }
    }

    // 데미지 계산 메서드
    public int CalculateDamage(int baseDamage, int attack, int defense)
    {
        int finalDamage = Mathf.Max(0, baseDamage + attack - defense);
        return finalDamage;
    }

    public void CardEffect(Card card){
        ApplyCardEffect(card);
    }

    // 효과 적용 메서드
    public void ApplyEffect(string effectType, int magnitude, Card card)
    {
        // 다양한 효과 (버프/디버프 등) 적용 로직 구현
        Debug.Log($"2번째 효과 여부 : {card._effectType2}");
        switch (effectType)
        {
            case "none":
                break;
            case "sadness":
                // 슬픔 효과 적용
                break;
            case "Heal":
                // 치유 효과 적용
                player.Heal(magnitude);
                break;
            case "bash":
                enemy.eTakeDamage(player.getBlock());
                break;
            case "draw":
                player.deckSystem.DrawCard(magnitude);
                break;
            case "cost":
                player.GainCost(magnitude); // 추후 다음턴 로직 추가
                break;
            case "loseCost":
                player.UseCost(magnitude);
                break;
            case "loseCard":
                // player.deckSystem.LoseCard(magnitude);
                break;
            // 기타 효과들...
        }
        if(card._effectType2 != Card.EffectType.none){
            ApplyEffect(card._effectType2.ToString(), card._effectMagnitude2, card);
        }
    }

    // 전투 로그 기록 메서드
    private void LogCombatAction(string action)
    {
        Debug.Log($"전투 로그: {action}");
    }

    // 카드 효과 적용 메서드
    private void ApplyCardEffect(Card card)
    {
        // 카드 효과 적용 로직
        // 예: 데미지, 방어력 증가, 버프/디버프 등
        Debug.Log($"카드 효과 적용: {card._cardType}");
        switch(card._cardType.ToString()){
                case "attack":
                    Debug.Log($"공격 데미지 : {card._damage}");
                    int damage = card._damage;
                    enemy.eTakeDamage(damage);
                    break;
                case "skill":
                    ApplyEffect(card._effectType.ToString(), card._effectMagnitude, card);
                    break;
                case "shield":
                    player.GainBlock(card._shield);
                    break;
        }
    }

    // 경험치 계산 메서드
    private int CalculateExperienceGain()
    {
        // 경험치 계산 로직
        return 100; // 임시 값
    }

    public void playerUseCard(Card card){
        if(player.playerCostCheck() >= card._cost){
            player.UseCost(card._cost);
            ApplyCardEffect(card);
            player.deckSystem.UseCard(card);
        }
        
    }


    // public void playCardToGrid(Card card, Vector2 gridPosition){
    //     if(gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height){

    //     }
    // }
}

// 전체 코드 설명:
// 이 CombatManager 클래스는 턴제 전투 시스템을 구현합니다.
// - StartCombat(): 전투를 초기화하고 시작합니다.
// - ManageTurns(): 플레이어와 적의 턴을 번갈아가며 관리합니다.
// - PlayerTurn(): 플레이어의 턴 동안 카드 사용과 행동을 처리합니다.
// - EnemyTurn(): 적의 턴 동안 행동을 결정하고 실행합니다.
// - IsCombatOver(): 전투 종료 조건을 확인합니다.
// - EndCombat(): 전투 결과를 처리하고 보상을 지급합니다.
// - 기타 유틸리티 메서드들: 데미지 계산, 효과 적용, 로그 기록 등을 담당합니다.
// 이 시스템은 코루틴을 사용하여 비동기적으로 턴을 관리하며,
// UI 업데이트와 로그 기록을 통해 전투 진행 상황을 표시합니다.
