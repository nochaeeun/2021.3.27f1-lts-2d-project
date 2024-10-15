using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSCCard;
using System;
using System.Linq;
using Random = UnityEngine.Random;   //Unity and the system have the same content, which can cause a crash.

public enum EnemyID{
    Red
}

public enum ActionType{
    Attack,
    PlayerCost,
    DebuffType,
    BuffType,
    Defence
}

public enum EnemyType{
    elite,
    boss,
    normal
}

public class Action{
    public string ActionName;
    public int _ePower;
    public ActionType _type;
    
    public Action(string actionName, int ePower, ActionType type){
        ActionName = actionName;
        _ePower = ePower;
        _type = type;
    }
}

public class EnemyAi : MonoBehaviour, ICharacter
{
    public bool IsPlayer => false;
    // 외부 컨트롤러
    public GameManager gameManager;
    public CombatManager combatManager;
    public UIManager uiManager;
    public charController playerManager;
    public TooltipSystem tooltip;

    public string Name;
    public int Health;
    public int Block;
    public ActionType Intention;

    private EnemyID eID {get; set;}
    private int E_Power;

    // 공격 관련 변수 , 리스트
    private List<Action> Actions;
    private List<Action> SpecialActions;
    public int AttackPower;

    public void Start()
    {
        InitializeControllers();
    }

    private void InitializeControllers()
    {
        gameManager = FindObjectOfType<GameManager>();
        combatManager = FindObjectOfType<CombatManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerManager = FindObjectOfType<charController>();
        tooltip = FindObjectOfType<TooltipSystem>();

        if(gameManager == null) gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        if(combatManager == null) combatManager = new GameObject("CombatManager").AddComponent<CombatManager>();
        if(uiManager == null) uiManager = new GameObject("UIManager").AddComponent<UIManager>();
        if(playerManager == null) playerManager = new GameObject("PlayerManager").AddComponent<charController>();
    }

    public void CallEnemy(){
        Enemy(EnemyID.Red);
    }
    
    private void Enemy(EnemyID ID){
        eID = ID;
        Actions = new List<Action>();
        SpecialActions = new List<Action>();
        switch(ID){
            case EnemyID.Red:
            Name = "Red";
            Health = 40;
            Block = 0;
            Debug.Log("EnemyAi: 적 행동 초기화 시작");
            Actions.Add(new Action("UnKnown1", 9, ActionType.Attack));
            Debug.Log("EnemyAi: UnKnown1 행동 추가됨: 공격력 9");
            Debug.Log(Actions.Count);
            Debug.Log(Actions[0].ActionName);
            Actions.Add(new Action("UnKnown2", 11, ActionType.Attack));
            Debug.Log("EnemyAi: UnKnown2 행동 추가됨: 공격력 11");
            Debug.Log(Actions.Count);
            Debug.Log(Actions[1].ActionName);
            Actions.Add(new Action("UnKnown3", 14, ActionType.Attack));
            Debug.Log("EnemyAi: UnKnown3 행동 추가됨: 공격력 14");
            Debug.Log(Actions.Count);
            Debug.Log(Actions[2].ActionName);
            Actions.Add(new Action("UnKnown4", 15, ActionType.Attack));
            Debug.Log("EnemyAi: UnKnown4 행동 추가됨: 공격력 15");
            Debug.Log(Actions.Count);
            Debug.Log(Actions[3].ActionName);

            Debug.Log("EnemyAi: 특수 행동 리스트 초기화됨");
            SpecialActions.Add(new Action("UnKnown5", 2, ActionType.PlayerCost));
            Debug.Log(SpecialActions.Count);
            Debug.Log(SpecialActions[0].ActionName);
            Debug.Log("EnemyAi: 특수 행동 UnKnown5 추가됨: 플레이어 코스트 2 감소");
            Debug.Log("EnemyAi: 적 행동 초기화 완료");
            break;
        }
        Debug.Log("EnemyAi: Enemy 메서드 종료");
    }

    private Action nextAction;
    public Action NextAction => nextAction;

    public void PerformAction(){
        Debug.Log("EnemyAi: PerformAction 메서드 시작");
        Debug.Log($"EnemyAi: 다음 행동: {nextAction?.ActionName}");
        if(nextAction != null){
            ExecuteAction();
            nextAction = null;
        }
        Debug.Log("EnemyAi: PerformAction 메서드 종료");
    }

    public void ExecuteAction(){
        Debug.Log("EnemyAi: ExecuteAction 메서드 시작");
        if(nextAction != null){
            switch(nextAction._type){
                case ActionType.Attack:
                    Debug.Log($"EnemyAi: 플레이어에게 {nextAction._ePower} 데미지 공격");
                    if(playerManager != null){
                        playerManager.TakeDamage(nextAction._ePower);
                    } else {
                        Debug.LogError("EnemyAi: playerManager가 null입니다.");
                    }
                    break;
                case ActionType.PlayerCost:
                    Debug.Log($"EnemyAi: 플레이어의 코스트 {nextAction._ePower} 감소");
                    if(playerManager != null){
                        playerManager.UseCost(nextAction._ePower);
                    } else {
                        Debug.LogError("EnemyAi: playerManager가 null입니다.");
                    }
                    break;
            }
        }
        Debug.Log("EnemyAi: ExecuteAction 메서드 종료");
    }
    
    public void UpdateIntention(){
        Debug.Log("EnemyAi: UpdateIntention 메서드 시작");
        if(nextAction != null){
            Intention = nextAction._type;
            Debug.Log($"EnemyAi: 다음 행동 의도 업데이트 - {Intention}");
        }
        Debug.Log("EnemyAi: UpdateIntention 메서드 종료");
    }

    public void UpdateIntentionUI(){
        uiManager.UpdateEnemyIntention(Intention);

        tooltip.SetupToolTip(Intention);
    }

    public void ChooseRandomAction(){
        Debug.Log("EnemyAi: ChooseRandomAction 메서드 시작");
        int maxActionIndex = Actions.Count + SpecialActions.Count;
        int randomIndex = Random.Range(0, maxActionIndex);

        nextAction = randomIndex < Actions.Count ? Actions[randomIndex] : SpecialActions[randomIndex - Actions.Count];
        Debug.Log($"EnemyAi: {(randomIndex < Actions.Count ? "일반" : "특수")} 행동 선택 - {nextAction.ActionName}");
        Debug.Log("EnemyAi: ChooseRandomAction 메서드 종료");
    }

    public void PrepareNextAction(){
        Debug.Log("EnemyAi: PrepareNextAction 메서드 시작");
        ChooseRandomAction();
        UpdateIntention();
        Debug.Log("EnemyAi: PrepareNextAction 메서드 종료");
    }

    public bool IsAlive(){
        Debug.Log($"EnemyAi: IsAlive 체크 - 현재 체력: {Health}");
        return Health > 0;
    }

    public void eTakeDamage(int amount){
        Debug.Log($"EnemyAi: eTakeDamage 메서드 시작 - 받은 데미지: {amount}");
        if(Block >= amount){
            Block -= amount;
            Debug.Log($"EnemyAi: 방어력으로 데미지 흡수 - 남은 방어력: {Block}");
        } else {
            int newDamage = amount - Block;
            Health -= newDamage;
            Block = 0;
            Debug.Log($"EnemyAi: 데미지 받음 - 현재 체력: {Health}");
            if(Health <= 0){
                Health = 0;
                Debug.Log("EnemyAi: 체력이 0 이하로 떨어짐");
                Die();
            }
        }
        Debug.Log("EnemyAi: eTakeDamage 메서드 종료");
    }

    public void eGainBlock(int amount){
        Debug.Log($"EnemyAi: eGainBlock 메서드 - 얻은 방어력: {amount}");
        Block += amount;
        Debug.Log($"EnemyAi: 현재 방어력: {Block}");
    }

    public void Die(){
        Debug.Log("EnemyAi: Die 메서드 시작");
        // TODO: 적 사망 처리
        if(Health <= 0){
            Debug.Log("EnemyAi: 적 사망 - 게임 상태를 Win으로 변경");
            if(gameManager != null){
                gameManager.currentState = GameManager.GameState.Win;
            } else {
                Debug.LogError("EnemyAi: gameManager가 null입니다.");
            }
        }
        Debug.Log("EnemyAi: Die 메서드 종료");
    }


    // public void UseSpecialAbility(){
        // 적의 특수 능력을 사용하는 메서드
    // }
}
