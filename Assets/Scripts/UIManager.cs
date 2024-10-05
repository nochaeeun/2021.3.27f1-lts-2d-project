using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button turnEndButton;

    private bool playerEndsTurn = false;

    private Sprite[] enemyIntention;

    private void Start()
    {
        enemyIntention = Resources.LoadAll<Sprite>("EnemyIntention");


        if (turnEndButton != null)
        {
            turnEndButton.onClick.AddListener(EndTurn);
        }
        else
        {
            Debug.LogError("턴 엔드 버튼이 할당되지 않았습니다.");
        }
    }

    private void Update()
    {
        
    }

    public void UpdateCombatUI(charController player, List<EnemyAi> enemies){
        // 플레이어 스텟 업데이트
        // 적 스텟 업데이트
    }

    private void EndTurn()
    {
        playerEndsTurn = true;
        Debug.Log("턴 종료 버튼이 눌렸습니다.");
    }

    public bool IsPlayerTurnEnded()
    {
        bool result = playerEndsTurn;
        playerEndsTurn = false; // 턴 종료 상태 초기화
        return result;
        Debug.Log("플레이어 턴 종료 됨.");
    }
}
