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
        [Tooltip("카드 효과")]
        public EffectType _effectType;
        [Tooltip("카드 효과 - 2")]
        public EffectType _effectType2;

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
        [Tooltip("추가 효과 크기")]
        public int _effectMagnitude;
        [Tooltip("추가 효과 크기 2")]
        public int _effectMagnitude2;
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
        public EffectType EType
        {
            get => _effectType;
            set => _effectType = value;
        }
        public EffectType EType2
        {
            get => _effectType2;
            set => _effectType2 = value;
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

        public int EffectMagnitude
        {
            get => _effectMagnitude;
            set => _effectMagnitude = value;
        }
        public int EffectMagnitude2
        {
            get => _effectMagnitude2;
            set => _effectMagnitude2 = value;
        }
        // 열거형은 그대로 유지
        public enum CardType
        {
            attack,
            shield,
            skill
        }
        public enum EffectType
        {
            none,
            draw,
            cost,
            sadness,
            bash,
            Heal,
            loseCost,
            loseCard
        }
        public enum Target
        {
            Enemy,
            Self
        }
    }
}
