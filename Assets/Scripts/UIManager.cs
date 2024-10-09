using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button turnEndButton;

    private Sprite[] enemyIntention;

    private charController player;
    private CombatManager combatManager;

    private void Start()
    {
        enemyIntention = Resources.LoadAll<Sprite>("EnemyIntention");
        player = FindObjectOfType<charController>();
        combatManager = FindObjectOfType<CombatManager>();

        if (turnEndButton != null)
        {
            turnEndButton.onClick.AddListener(EndTurn);
        }
        else
        {
            turnEndButton = GameObject.Find("Button_TurnEnd").GetComponent<Button>();
            turnEndButton.onClick.AddListener(EndTurn);
        }
    }

    private void Update()
    {
        
    }

    public void UpdateCombatUI(charController player, List<EnemyAi> enemies){
        // 플레이어 스텟 업데이트
        // 적 스텟 업데이트
    }

    public bool CallEndTurn(bool result){
        return result;
    }

    // private void EndTurn()
    // {
    //     combatManager.getTurnEndedButtonDown = true;
    //     Debug.Log($"턴 종료 버튼이 눌렸습니다. {combatManager.getTurnEndedButtonDown}");
    //     IsPlayerTurnEnded();
    //     return true;
    // }

    // public bool IsPlayerTurnEnded()
    // {
    //     bool result = combatManager.getTurnEndedButtonDown;
    //     Debug.Log($"IsPlayerTurnEnded 호출: {result}");
    //     Debug.Log($"getTurnEndedButtonDown: {combatManager.getTurnEndedButtonDown}");
    //     combatManager.getTurnEndedButtonDown = false;
    //     Debug.Log("플레이어 턴 종료 됨.");
    //     return result;
    // }

    // 턴 종료 상태를 나타내는 변수
    private bool isTurnEnded = false;

    // 턴 종료를 기다리는 메서드 (CombatManager에서 사용)
    public bool IsTurnEnded()
    {
        return isTurnEnded;
    }

    // 턴 종료 버튼 클릭 시 호출되는 메서드
    private void EndTurn()
    {
        // 턴 종료 상태를 true로 설정
        isTurnEnded = true;
        Debug.Log($"턴 종료 버튼이 눌렸습니다. 턴 종료 상태: {isTurnEnded}");
    }

    // 턴 종료 상태 초기화 메서드 (CombatManager에서 턴 시작 시 호출)
    public void ResetTurnEndStatus()
    {
        isTurnEnded = false;
        Debug.Log("턴 종료 상태가 초기화되었습니다.");
    }

    // CombatManager의 플레이어 턴 관리에서 사용될 WaitUntil에 넣을 문구:
    // yield return new WaitUntil(() => uiManager.IsTurnEnded());
}
