using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;

public class charController : MonoBehaviour
{
    // 캐릭터 기본 속성
    private int maxHealth;
    private int currentHealth;
    private int maxEnergy;
    private int currentEnergy;
    private int block;
    private int startHealth;

    // 전투 관련 속성
    private int sadness;

    // 덱 관련 속성
    private List<Card> deck;
    private List<Card> hand;
    private List<Card> discardPile;

    // 버프/디버프 관련 속성
    // private Dictionary<BuffType, int> buffs;
    // private Dictionary<DebuffType, int> debuffs;

    // 턴 관련 속성
    private bool isPlayerTurn;
    private int turnCount;

    // 기타 속성
    private int gold;
    private int experience;
    private int level;

    public void Initialize()
    {
        // TODO: 캐릭터 초기화
        maxHealth = 50;
        startHealth = 0;
        currentHealth = startHealth;
        maxEnergy = 5;
        currentEnergy = 0;
        block = 0;
        sadness = 0;
        
        // - 기본 스탯 설정
        // 플레이어 스탯의 대한 내용은 아직 
        // - 시작 덱 생성
        deck = new List<Card>();
        
    }

    // public void ResetForCombat(){}

    public void TakeDamage(int amount)
    {
        // TODO: 데미지 처리
        // - 방어력 고려하여 실제 데미지 계산
        // - 체력 감소
        // - 사망 체크
    }

    public void GainBlock(int amount)
    {
        // TODO: 방어력 증가
        // - 현재 방어력에 amount 추가
    }

    public void UseEnergy(int amount)
    {
        // TODO: 에너지 사용
        // - 현재 에너지에서 amount 차감
    }

    public void GainEnergy(int amount)
    {
        // TODO: 에너지 획득
        // - 현재 에너지에 amount 추가
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
        // - 현재 체력에 amount만큼 회복
        // - 최대 체력 초과하지 않도록 처리
    }

    public void Die()
    {
        // TODO: 캐릭터 사망 처리
        // - 사망 애니메이션 재생
        // - 게임 오버 처리
    }

    public bool CanPlayCard(Card card)
    {
        // TODO: 카드 플레이 가능 여부 확인
        // - 현재 에너지와 카드 비용 비교
        // - 특수 조건 확인 (예: 특정 버프 필요)
        return false;
    }

    public void StartTurn()
    {
        // TODO: 턴 시작 처리
        // - 에너지 회복
        // - 카드 드로우
        // - 턴 시작 효과 적용
    }

    public void EndTurn()
    {
        // TODO: 턴 종료 처리
        // - 남은 카드 버리기
        // - 턴 종료 효과 적용
    }

    public void UpdateStats()
    {
        // TODO: 캐릭터 스탯 업데이트
        // - 버프/디버프 효과 적용
        // - UI 업데이트
    }

    public bool isPlayerLose(){
        if(currentHealth >= maxHealth){
            return true;
        }
        return false;
    }
}
