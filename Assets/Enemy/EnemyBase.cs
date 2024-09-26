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
                    //행동 유형은 여러가지가 있음 공격이 여러 개가 나올 수도 있기 때문에 
                    //개수대로 할당 필요
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
            //EnemtType의 모든 값을 가져옴
            EnemyType randomEnemyType = (EnemyType)enemtTypes.GetValue
                                        (random.Next(enemtTypes.Length));
            //랜덤으로 하나의 EnemyType 선택
            Enemy enemy = new Enemy(randomEnemyType); 
            //랜덤으로 선택된 적 생성
        }
    }

    public void Attack()
    {
        // 플레이어 코스트와 연결 필요
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }


}
