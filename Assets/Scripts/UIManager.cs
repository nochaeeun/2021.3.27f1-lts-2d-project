using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button turnEndButton;

    private bool playerEndsTurn = false;

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

    public void CallEndTurn(){
        EndTurn();
    }

    private void EndTurn()
    {
        combatManager.turnEndButtonDown = true;
        Debug.Log("턴 종료 버튼이 눌렸습니다.");
    }

    public bool IsPlayerTurnEnded()
    {
        bool result = playerEndsTurn;
        playerEndsTurn = false; // 턴 종료 상태 초기화
        Debug.Log("플레이어 턴 종료 됨.");
        return result;
    }
}
