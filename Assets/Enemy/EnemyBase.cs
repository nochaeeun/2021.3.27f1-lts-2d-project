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
    public class Enemy
    {
        public string Name { get; set; }
        public int Uneasy { get; set; }
        public int Stastability { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Skill { get; set; }
        public EnemyType Type { get; set; }

        public Enemy(EnemyType type)
        {
            Type = type;
            switch (type)
            {
                case EnemyType.Red:
                    Name = "Red";
                    Uneasy = 40;
                    Stastability = 0;
                    //�ൿ ������ ���������� ���� ������ ���� ���� ���� ���� �ֱ� ������ 
                    //������� �Ҵ� �ʿ�
                    break;
            }
        }


    }

    class EnemycreateRandom
    {
        static void main(string[] args)
        {
            System.Random random = new System.Random();
            Array enemtTypes = Enum.GetValues(typeof(EnemyType));
            //EnemtType�� ��� ���� ������
            EnemyType randomEnemyType = (EnemyType)enemtTypes.GetValue
                                        (random.Next(enemtTypes.Length));
            //�������� �ϳ��� EnemyType ����
            Enemy enemy = new Enemy(randomEnemyType); 
            //�������� ���õ� �� ����
        }
    }

    public void Attack()
    {
        // �÷��̾� �ڽ�Ʈ�� ���� �ʿ�
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }


}
