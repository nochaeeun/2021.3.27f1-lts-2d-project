using UnityEngine;

namespace ProjectSCCard{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject {
        [Header("기본 정보")]
        [Tooltip("카드 이름")]
        public string cardName;      // 카드 이름
        [Tooltip("카드 타입")]
        public CardType cardType;        // 카드 타입
        [Tooltip("비용")]
        public int cost;             // 비용
        [Tooltip("대상")]
        public Target target;        // 대상

        [Space(10f)]
        [Header("기본 효과")]
        [Tooltip("데미지 ( 불안도 )")]
        public int damage;          // 데미지 ( 불안도 )
        [Tooltip("방어력 ( 안정도 )")]
        public int shield;          // 방어력 ( 안정도 )
        [Tooltip("설명")]
        [TextArea(3,5)]
        public string description;   // 설명

        [Space(10f)]
        [Header("추가 효과")]
        [Tooltip("추가 카드 뽑기")]
        public int drawCount;           // 추가 카드 뽑기
        [Tooltip("추가 코스트 획득")]
        public int recoveryCost;        // 추가 코스트 획득
        [Tooltip("디버프 ( 슬픔 )")]
        public int debuffSadness;       // 디버프 ( 슬픔 )
        [Tooltip("카드 ( 역지사지 ) 특수 타입 카드 확인용")]
        public bool isBash;             // 카드 ( 역지사지 ) 특수 타입 카드 확인용

        public enum CardType{
            attack,
            shield,
            skill
        }

        public enum Target{
            Enemy,
            Self
        }
    }
}
