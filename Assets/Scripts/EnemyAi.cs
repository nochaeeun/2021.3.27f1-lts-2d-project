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

public class Action{
    private string ActionName;
    private int _ePower;
    private ActionType _type;
    
    public Action(string actionName, int ePower, ActionType type){
        ActionName = actionName;
        _ePower = ePower;
        _type = type;
    }
}

public class EnemyAi : MonoBehaviour
{
    public string Name;
    public int Health;
    public int Block;
    public ActionType Intention;

    private List<Action> Actions;
    private List<Action> SpecialActions;

    private EnemyID eID {get; set;}
    private int E_Power;

    private void Enemy(EnemyID ID){
        eID = ID;
        Actions = new List<Action>();
        switch(ID){
            case EnemyID.Red:
            Name = "Red";
            Health = 40;
            Block = 0;

            Actions.Add(new Action("UnKnown1", 9, ActionType.Attack));
            Actions.Add(new Action("UnKnown2", 11, ActionType.Attack));
            Actions.Add(new Action("UnKnown3", 14, ActionType.Attack));
            Actions.Add(new Action("UnKnown4", 15, ActionType.Attack));

            SpecialActions.Add(new Action("UnKnown5", 2, ActionType.PlayerCost));
            break;
        }
    }

    private Action nextAction;

    public void PerformAction(){
        // 적의 전체 행동을 수행하는 메서드
        if(nextAction != null){
            ExecuteAction();
            nextAction = null;
        }
        PrepareNextAction();
    }

    public void ExecuteAction(){
        // 적의 행동을 수행하는 메서드 
        if(nextAction != null){
            switch(nextAction._type){
                case ActionType.Attack:
                break;
                case ActionType.PlayerCost:
                break;
                // next Time
            }
        }
    }
    public void UpdateIntention(int ActionIndex, ActionType type){
        // 적의 의도UI를 업데이트하는 메서드
        if(nextAction != null){
            Intention = nextAction._type;
        }
    }
    public void ChooseRandomAction(){
        // 적의 다음 턴 랜덤 행동을 선택하는 메서드
        int maxActionIndex = Actions.Count + SpecialActions.Count;
        int randomIndex = Random.Range(0, maxActionIndex);

        if(randomIndex < Actions.Count){
            nextAction = Actions[randomIndex];
        }
        else{
            nextAction = SpecialActions[randomIndex - Actions.Count];
        }
    }

    public void PrepareNextAction(){
        ChooseRandomAction();
        UpdateIntention();
    }


    // public void UseSpecialAbility(){
        // 적의 특수 능력을 사용하는 메서드
    // }
}


