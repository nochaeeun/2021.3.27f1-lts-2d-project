using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
public class EnemyBase : MonoBehaviour
{
    public int Count;


    // Start is called before the first frame update
    public enum EnemyType
    {
        Red
    }

    public class Action
    {

        public string Name { get; set; }  // 이름
        public int Power { get; set; }    // 수치 (공격력, 방어력, 스킬 효과 등)

        public Action(string name, int power)
        {
            Name = name;
            Power = power;
        }
    }

    public class Enemy
    {
        public string Name { get; set; }
        public int _Health { get; set; } // 불안도
        public int _block { get; set; } // 안정도
        public List<Action> Actions { get; set; }
        // 공격, 방어, 스킬을 저장하는 통합 리스트
        public EnemyType Type { get; set; }

        public Enemy(EnemyType type)
        {
            Type = type;
            Actions = new List<Action>();
            switch (type)
            {
                case EnemyType.Red:
                    Name = "Red";
                    Uneasy = 40;
                    Stastability = 0;

                    Actions.Add(new Action("Uneasy1", 9));
                    Actions.Add(new Action("Uneasy2", 1));
                    Actions.Add(new Action("Uneasy3", 14));

                    // 방어 추가
                    Actions.Add(new Action("Stastability1", 15));

                    // 스킬 추가
                    Actions.Add(new Action("PlayerCostSub", -2));
                    break;
            }
        }

    }

    public static class ActionPrinter                               // Close to DecideAction Method
    {
        public static void PrintActions(Enemy enemy)
        {
            /*for (int i = 0; i < enemy.Actions.Count; i++)
            {
                Debug.Log($"{i}: {enemy.Actions[i].Name} with {enemy.Actions[i].Power} power.");
            }*/

            System.Random random = new System.Random();
            int randomIndex = random.Next(enemy.Actions.Count); //count 리스트에 포함된 요소의 개수
            Action randomAction = enemy.Actions[randomIndex];
            Debug.Log($"Random Action: {randomAction.Name} with {randomAction.Power} power.");

        }
    }

    class EnemycreateRandom
    {
        public static void CreateRandomEnemy()
        {
            System.Random random = new System.Random();
            Array enemtTypes = Enum.GetValues(typeof(EnemyType));
            //EnemtType의 모든 값을 가져옴
            EnemyType randomEnemyType = (EnemyType)enemtTypes.GetValue
                                        (random.Next(enemtTypes.Length));
            //랜덤으로 하나의 EnemyType을 가져옴
            Enemy enemy = new Enemy(randomEnemyType);
            //랜덤으로 선택된 적 생성
            ActionPrinter.PrintActions(enemy);
        }
    }

    void Start()
    {
        EnemycreateRandom.CreateRandomEnemy(); // 적을 랜덤으로 생성
    }
    // Update is called once per frame
    void Update()
    {

    }

    // {Actions[i].Name / Actions[i].Power);
    //Debug로 출력 / 랜덤으로 하나 뽑아서 출력해보기

    // 적의 행동을 수행하는 메서드
    void PerformAction()
    {
        // TODO: 적의 행동을 구현하세요
        // 저장된 행동 실행
        // 행동 결과 처리
    }

    // 적의 의도를 업데이트하는 메서드
    void UpdateIntention()
    {
        // TODO: 적의 의도 업데이트 로직을 구현하세요
        // 다음 행동에 대한 의도 표시 업데이트
    }

    // 적의 특수 능력을 사용하는 메서드
    void UseSpecialAbility()
    {
        // TODO: 적의 특수 능력 사용 로직을 구현하세요
        // 특수 스킬 사용
    }
}
