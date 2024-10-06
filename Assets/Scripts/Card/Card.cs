using UnityEngine;

namespace ProjectSCCard{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]

    public class Card : ScriptableObject {
        // 변수 필드
        [Header("기본 정보")]
        [Tooltip("카드 ID")]
        public int _cardID;
        [Tooltip("카드 이름")]
        public string _cardName;
        [Tooltip("카드 타입")]
        public CardType _cardType;
        [Tooltip("비용")]
        public int _cost;
        [Tooltip("대상")]
        public Target _target;


        [Space(10f)]
        [Header("기본 효과")]
        [Tooltip("데미지 ( 불안도 )")]
        public int _damage;
        [Tooltip("방어력 ( 안정도 )")]
        public int _shield;
        [Tooltip("설명")]
        [TextArea(3,5)]
        public string _description;


        [Space(10f)]
        [Header("추가 효과")]
        [Tooltip("추가 카드 뽑기")]
        public int _drawCount;
        [Tooltip("추가 코스트 획득")]
        public int _recoveryCost;
        [Tooltip("디버프 ( 슬픔 )")]
        public int _debuffSadness;
        [Tooltip("카드 ( 역지사지 ) 특수 타입 카드 확인용")]
        public bool _isBash;

        // 프로퍼티
        public int CardID
        {
            get => _cardID;
            set => _cardID = value;
        }
        public string CardName
        {
            get => _cardName;
            set => _cardName = value;
        }
        public CardType Type
        {
            get => _cardType;
            set => _cardType = value;
        }
        public int Cost
        {
            get => _cost;
            set => _cost = Mathf.Max(value);
        }
        public Target TargetType
        {
            get => _target;
            set => _target = value;
        }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
        public int Shield
        {
            get => _shield;
            set => _shield = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public int DrawCount
        {
            get => _drawCount;
            set => _drawCount = value;
        }
        public int RecoveryCost
        {
            get => _recoveryCost;
            set => _recoveryCost = value;
        }
        public int DebuffSadness
        {
            get => _debuffSadness;
            set => _debuffSadness = value;
        }
        public bool IsBash
        {
            get => _isBash;
            set => _isBash = value;
        }
        // 열거형은 그대로 유지
        public enum CardType
        {
            attack,
            shield,
            skill
        }
        public enum Target
        {
            Enemy,
            Self
        }
    }
    
    /*
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
    */
}
