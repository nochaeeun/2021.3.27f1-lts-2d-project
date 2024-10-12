using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button turnEndButton;
    [SerializeField] private TextMeshProUGUI playerHpText;
    [SerializeField] private TextMeshProUGUI playerBlockText;
    [SerializeField] private TextMeshProUGUI enemyHpText;
    [SerializeField] private TextMeshProUGUI enemyBlockText;
    [SerializeField] private TextMeshProUGUI turnCountText;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private TextMeshProUGUI playerCostText;
    [SerializeField] private TextMeshProUGUI enemyInfoText;
    [SerializeField] private GameObject enemyIntentionAttackIcon;
    [SerializeField] private GameObject enemyIntentionCostIcon;
    [SerializeField] private GameObject enemyIntentionShieldIcon;
    [SerializeField] private TextMeshProUGUI enemyIntentionAmountText;

    private Sprite[] enemyIntention;

    private charController player;
    private CombatManager combatManager;
    private EnemyAi enemy;

    private int enemyMaxHp = 0;

    private void Start()
    {
        enemyIntention = Resources.LoadAll<Sprite>("EnemyIntention");
        player = FindObjectOfType<charController>();
        combatManager = FindObjectOfType<CombatManager>();
        enemy = FindObjectOfType<EnemyAi>();

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

    public void FixedUpdate()
    {
        UpdateCombatUI(player, combatManager.ListUPEnemies());
        enemy.UpdateIntentionUI();
    }

    public void UpdateCombatUI(charController player, List<EnemyAi> enemies){
        // 플레이어 스텟 업데이트
        playerHpText.text = $"{player.playerHpCheck()} / {player.MaxHealthCheck()}";
        playerBlockText.text = $"{player.getBlock()} / {player.getBlock()}";
        playerCostText.text = $"{player.playerCostCheck()} / 5";
        // 적 스텟 업데이트
        if(enemyMaxHp == 0) enemyMaxHp = enemies[0].Health;
        enemyInfoText.text = $"야생의 테스트용 보스 {enemies[0].Name}가 나타났다!";
        enemyNameText.text = enemies[0].Name;
        enemyHpText.text = $"{enemies[0].Health} / {enemyMaxHp}";
        enemyBlockText.text = $"{enemies[0].Block} / {enemies[0].Block}";
        // 턴 카운트 업데이트
        turnCountText.text = $"{combatManager.GetTurnCount()} 턴";
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

    public void UpdateEnemyIntention(ActionType intention){
        switch(intention){
            case ActionType.Attack:
                enemyIntentionAttackIcon.SetActive(true);
                enemyIntentionCostIcon.SetActive(false);
                enemyIntentionShieldIcon.SetActive(false);
                enemyIntentionAmountText.gameObject.SetActive(true);
                enemyIntentionAmountText.text = $"{enemy.NextAction._ePower}";
                break;
            case ActionType.PlayerCost:
                enemyIntentionCostIcon.SetActive(true);
                enemyIntentionAttackIcon.SetActive(false);
                enemyIntentionShieldIcon.SetActive(false);
                enemyIntentionAmountText.gameObject.SetActive(false);
                break;
            case ActionType.Defence:
                enemyIntentionShieldIcon.SetActive(true);
                enemyIntentionAttackIcon.SetActive(false);
                enemyIntentionCostIcon.SetActive(false);
                enemyIntentionAmountText.gameObject.SetActive(false);
                break;
        }
    }

    // CombatManager의 플레이어 턴 관리에서 사용될 WaitUntil에 넣을 문구:
    // yield return new WaitUntil(() => uiManager.IsTurnEnded());
}
