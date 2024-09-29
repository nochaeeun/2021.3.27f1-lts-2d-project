using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSCCard{

    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]

    public class Cards : ScriptableObject {

        public string cardName;      // 카드 이름
        public Type type;           // 카드 타입
        public int cost;            // 비용
        public Target target;        // 대상
        public int damage;          // 데미지 ( 불안도 )
        public int shield;          // 방어력 ( 안정도 )
        public string description;   // 설명


        public int getCard;           // 추가 카드 뽑기
        public int getCost;           // 추가 코스트 획득
        public int debuffSad;         // 디버프 ( 슬픔 )
        public bool isBash;          // 카드 ( 역지사지 ) 특수 타입 카드 확인용

        public enum Type{
            공격,
            보호막,
            스킬
        }

        public enum Target{
            Enemy,
            Self
        }
    }
}
