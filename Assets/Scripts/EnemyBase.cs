using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyType
    {
        Red
    }

    public class Action
    {

        public string Name { get; set; }  // �̸�
        public int Power { get; set; }    // ��ġ (���ݷ�, ����, ��ų ȿ�� ��)

        public Action(string name, int power)
        {
            Name = name;
            Power = power;
        }
    }

    public class Enemy
    {
        public string Name { get; set; }
        public int Uneasy { get; set; } // �Ҿȵ�
        public int Stastability { get; set; } // ������
        public List<Action> Actions { get; set; }
        // ����, ���, ��ų�� �����ϴ� ���� ����Ʈ
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

                    // ��� �߰�
                    Actions.Add(new Action("Stastability1", 15));

                    // ��ų �߰�
                    Actions.Add(new Action("PlayerCostSub", -2));
                    break;
            }
        }

    }

    public static class ActionPrinter
    {
        public static void PrintActions(Enemy enemy)
        {
            /*for (int i = 0; i < enemy.Actions.Count; i++)
            {
                Debug.Log($"{i}: {enemy.Actions[i].Name} with {enemy.Actions[i].Power} power.");
            }*/

            System.Random random = new System.Random();
            int randomIndex = random.Next(enemy.Actions.Count); //Count ����Ʈ�� ���Ե� ����� ����
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
            //EnemtType�� ��� ���� ������
            EnemyType randomEnemyType = (EnemyType)enemtTypes.GetValue
                                        (random.Next(enemtTypes.Length));
            //�������� �ϳ��� EnemyType ����
            Enemy enemy = new Enemy(randomEnemyType);
            //�������� ���õ� �� ����
            ActionPrinter.PrintActions(enemy);
        }
    }
    void Start()
    {
        EnemycreateRandom.CreateRandomEnemy(); // ���� �������� ����
    }
    // Update is called once per frame
    void Update()
    {

    }

    // {Actions[i].Name / Actions[i].Power);
    //Debug�� ��� / �������� �ϳ� �̾Ƽ� ����غ���
}
