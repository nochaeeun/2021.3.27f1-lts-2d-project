using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;

public class charController : MonoBehaviour
{
    // 외부 컨트롤러
    public EnemyAi enemyAi;
    public cardManager cardManager;
    public deckSystem deckSystem;
    public UIManager uiManager;
    public CombatManager combatManager;
    public GameManager gameManager;

    // 캐릭터 기본 속성
    private int startHealth;            // 시작 체력
    private int maxHealth;              // 최대 체력
    private int currentHealth;          // 현재 체력
    private int maxCost;                // 최대 코스트
    private int currentCost;            // 현재 코스트
    private int block;                  // 방어력

    // 전투 관련 속성
    private int sadness;                // 슬픔(디버프)

    // 덱 관련 속성
    private List<Card> deck;           // 덱
    private List<Card> hand;           // 손패
    private List<Card> discardPile;    // 버린 카드 목록

    // 버프/디버프 관련 속성
    // private Dictionary<BuffType, int> buffs;
    // private Dictionary<DebuffType, int> debuffs;

    // 턴 관련 속성
    private bool isPlayerTurn;          // 플레이어 턴 여부
    private int turnCount;              // 턴 카운트 ??
    private bool keepBlock = false;     // 쉴드(안정도) 유지 여부

    // 기타 속성
    private int gold;                   // 골드
    private int experience;             // 경험치 ( 미구현 할 예정 )
    private int level;                  // 레벨 ( 미구현 할 예정 )

    public void Start()
    {
        // 외부 컨트롤러 초기화
        enemyAi = FindObjectOfType<EnemyAi>();
        cardManager = FindObjectOfType<cardManager>();
        deckSystem = FindObjectOfType<deckSystem>();
        uiManager = FindObjectOfType<UIManager>();
        combatManager = FindObjectOfType<CombatManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Initialize()
    {
        // 월드에 캐릭터가 진입할때 호출.
        // TODO: 캐릭터 초기화
        maxHealth = 50;
        startHealth = 0;
        currentHealth = startHealth;
        maxCost = 5;
        currentCost = 0;
        block = 0;
        sadness = 0;
        hand.Clear();
        discardPile.Clear();
        // - 기본 스탯 설정 : 플레이어 스탯의 대한 내용은 아직 

        // - 시작 덱 생성 deck = new List<Card>();
        deckSystem.InitializeDeck();
    }

    public void levelInitialize(){
        // 레벨(적과 조우)시 호출
        // TODO: 캐릭터 적당량 초기화
        currentCost = 3;
        block = 0;
        sadness = 0;
        hand.Clear();
        discardPile.Clear();
    }

    // public void ResetForCombat(){}

    public void TakeDamage(int amount)
    {
        int newDamage;
        // TODO: 데미지 처리
        // - 방어력 고려하여 실제 데미지 계산
        if(block > amount){
            block -= amount;
        } else {
            newDamage = amount - block;
            currentHealth += newDamage; // - 체력 감소
            // - 사망 체크
            if(isPlayerDie())
                Die();
        }
    }

    public void GainBlock(int amount)
    {
        // TODO: 방어력 증가
        // - 현재 방어력에 amount 추가
        block += amount;
    }

    public void UseCost(int amount)
    {
        // TODO: 코스트 사용
        // - 현재 코스트에서 amount 차감
        currentCost -= amount;
        if(currentCost < 0){
            currentCost = 0;
        }
    }

    public void GainCost(int amount)
    {
        // TODO: 코스트 획득
        // - 현재 에너지에 amount 추가
        if(currentCost + amount > maxCost)
            currentCost = maxCost;
        else
            currentCost += amount;
    }

    // public void ApplyBuff(BuffType buffType, int amount)
    // {
        // TODO: 버프 적용
        // - 지정된 버프를 캐릭터에 적용
        // - 버프 지속 시간 설정
    // }

    // public void ApplyDebuff(DebuffType debuffType, int amount)
    // {
        // TODO: 디버프 적용
        // - 지정된 디버프를 캐릭터에 적용
        // - 디버프 지속 시간 설정
    // }

    public void Heal(int amount)
    {
        // TODO: 체력 회복
        // - 현재 체력에 amount만큼 회복( 불안도라는 health의 수치가 최대에 도달하면 플레이어는 죽음 )
        currentHealth -= amount;
        // - 최대 체력 초과하지 않도록 처리 ( 불안도가 최대에 도달하면 플레이어는 죽음 , 최소 0 )
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void Die()
    {
        // TODO: 캐릭터 사망 처리
        if(currentHealth >= maxHealth){
            // - 사망 애니메이션 재생 ( 구현 하면 그때 찾아오고 디렉터님이 안한다고 하면 더미 데이터로 )
            // - 게임 오버 처리
            gameManager.currentState = GameManager.GameState.GameOver;
        }
    }

    // public bool CanPlayCard(Card card)
    // {
    //     // TODO: 카드 플레이 가능 여부 확인 ???
    //     // - 현재 에너지와 카드 비용 비교
    //     // - 특수 조건 확인 (예: 특정 버프 필요)
    //     return false;
    // }

    public void StartTurn()
    {
        // TODO: 턴 시작 처리
        // - 코스트 회복
        currentCost += 3;
        if(currentCost > maxCost){
            currentCost = maxCost;
        }
        // - 카드 드로우
        
        // - 턴 시작 효과 적용
    }

    public void EndTurn()
    {
        // TODO: 턴 종료 처리
        // - 남은 카드 버리기
        // - 턴 종료 효과 적용
        if(sadness > 0){
            sadness--;
        }
        if(!keepBlock){
            block = 0;
        }
    }

    // public void UpdateStats()
    // {
    //     // TODO: 캐릭터 스탯 업데이트
    //     // - 버프/디버프 효과 적용
    //     // - UI 업데이트
    // }

    public bool isPlayerDie(){
        if(currentHealth >= maxHealth){
            return true;
        }
        return false;
    }

    public bool healthCheck(){
        if(currentHealth > 0){
            return true;
        }
        return false;
    }
    public int playerCostCheck(){
        return currentCost;
    }
}
