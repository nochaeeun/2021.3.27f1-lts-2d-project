using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectSCCard;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 카드의 RectTransform 컴포넌트
    private RectTransform rectTransform;
    // 카드가 속한 Canvas
    private Canvas canvas;
    // 드래그 시작 시 마우스 위치
    private Vector2 originalLocalPointerPos;
    // 드래그 시작 시 카드의 위치
    private Vector3 originalPanelLocalPos;
    // 카드의 원래 크기
    private Vector3 originalScale;
    // 현재 카드 상태 (0: 기본, 1: 호버, 2: 드래그, 3: 플레이)
    public int currentState = 0;
    // 카드의 원래 회전
    private Quaternion originalRotation;
    // 카드의 원래 위치
    private Vector3 originalPos;

    // 카드 선택 시 크기 배율
    [SerializeField] private float selectionScale = 1.1f;
    // 카드 플레이 임계값
    //[SerializeField] private Vector2 cardPlay;
    // 카드 플레이 시 위치
    [SerializeField] private Vector3 playPos;
    // 카드 선택 시 나타나는 발광 효과
    [SerializeField] private GameObject glowEffect;
    // 카드 플레이 가능 시 나타나는 화살표
    [SerializeField] private GameObject playArrow;
    // 카드 플레이 임계값 (화면 높이의 비율)
    [SerializeField] private float cardPlayThreshold = 0.7f;

    public CombatManager combatManager;
    public deckSystem deckSystem;
    public cardManager cardManager;
    public charController player;


    public Camera cam;
    private int layerMask;



    // 컴포넌트 초기화 및 원래 변환 정보 저장
    void Awake(){
        //Debug.Log("1-01");
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        combatManager = FindObjectOfType<CombatManager>();
        deckSystem = FindObjectOfType<deckSystem>();
        cardManager = FindObjectOfType<cardManager>();
        player = FindObjectOfType<charController>();
        originalScale = rectTransform.localScale;
        originalPos = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
        //Debug.Log("1-02");
        layerMask = LayerMask.GetMask("PlayCard");
    }

    // 매 프레임마다 현재 상태에 따른 처리
    void Update(){
        //Debug.Log("1-03");
        switch(currentState){
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if(!Input.GetMouseButton(0)) {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                break;
        }
        //Debug.Log("1-04");
    }

    // 기본 상태(0)로 전환
    private void TransitionToState0(){
        //Debug.Log("1-05");
        currentState = 0;
        rectTransform.localScale = originalScale; // 크기 초기화
        rectTransform.localRotation = originalRotation; // 회전 초기화
        rectTransform.localPosition = originalPos; // 위치 초기화
        glowEffect.SetActive(false); // 발광 효과 비활성화
        playArrow.SetActive(false); // 플레이 화살표 비활성화
        //Debug.Log("1-06");
    }

    // 마우스가 카드 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData){
        //Debug.Log("1-07");
        if(currentState == 0) {
            originalPos = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;
            
            currentState = 1;
        }
        //Debug.Log("1-08");
    }

    // 마우스가 카드에서 벗어났을 때
    public void OnPointerExit(PointerEventData eventData){
        //Debug.Log("1-09");
        if(currentState == 1)
            TransitionToState0();
        //Debug.Log("1-10");
    }

    // 카드를 클릭했을 때
    public void OnPointerDown(PointerEventData eventData){
        //Debug.Log("1-11");
        if(currentState == 1 ){
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPos);
            originalPanelLocalPos = rectTransform.localPosition;
        }
        //Debug.Log("1-12");
    }

    // 카드를 드래그할 때
    public void OnDrag(PointerEventData eventData){
        //Debug.Log("1-13");
        if(currentState == 2){
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition)){
                                // 코드 수정 후
                localPointerPosition /= canvas.scaleFactor;
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPos;
                rectTransform.localPosition = originalPanelLocalPos + offsetToOriginal;
                
                // 화면 높이의 설정된 비율 이상으로 드래그되면 플레이 상태로 전환
                if(eventData.position.y > Screen.height * cardPlayThreshold){
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = playPos;
                }
            }
        }
        //Debug.Log("1-14");
    }

    // 호버 상태 처리
    private void HandleHoverState(){
        //Debug.Log("1-15");
        //glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectionScale;
        //Debug.Log("1-16");
    }

    // 드래그 상태 처리
    private void HandleDragState(){
        //Debug.Log("1-17");
        rectTransform.localRotation = Quaternion.identity;
        //Debug.Log("1-18");
    }

    // 플레이 상태 처리
    private void HandlePlayState(){
        //Debug.Log("1-19");
        Vector3 position = rectTransform.localPosition;
        position.z = -10f;
        rectTransform.localPosition = position;

        rectTransform.localPosition = playPos;
        rectTransform.localRotation = Quaternion.identity;

        if(player.GetCostAmount() < GetComponent<cardDisplay>().cardData._cost){
            TransitionToState0();
        }

        if(!Input.GetMouseButton(0)){                                         // 전투관련 코드 : 마우스 왼쪽 버튼이 눌리지 않았을 때
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      // 전투관련 코드 : 마우스 위치에서 레이 생성
            // 레이를 시각화하기 위한 디버그 라인 그리기
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            // Debug.Log($"레이 생성: {ray.origin}, {ray.direction}");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);  // 전투관련 코드 : 레이를 캐스트하여 충돌 감지
            // RaycastHit2D hit = Physics2D.Raycast(wPoint, Vector2.zero, Mathf.Infinity, layerMask);
            Card selectedCard = GetComponent<cardDisplay>().GetCard();
            //GetComponent<cardDisplay>().cardData;
            ICharacter character = hit.collider.GetComponent<ICharacter>();
            if(hit.collider != null){
                if(hit.collider.GetComponent<EnemyAi>() != null){
                    // 적에게 플레이
                if (currentState == 3 && selectedCard._target == Card.Target.Enemy)
                {
                    // 적에게 카드 효과 적용
                    EnemyAi enemy = hit.collider.GetComponent<EnemyAi>();
                    if (enemy != null)
                    {
                        // 카드 효과 실행
                        combatManager.CardEffect(selectedCard);

                        // 카드 사용 후 처리
                        deckSystem.UseCard(selectedCard);
                        selectedCard = null;
                        cardManager.inHandCards.Remove(this.gameObject);
                        Destroy(this.gameObject);
                    }
                }
                } else if (hit.collider.GetComponent<charController>() != null){
                    // 플레이어에게 플레이
                    if (currentState == 3 && selectedCard._target == Card.Target.Self)
                    {
                        // 플레이어에게 카드 효과 적용
                        charController player = hit.collider.GetComponent<charController>();
                        if (player != null)
                        {
                            // 카드 효과 실행
                            combatManager.CardEffect(selectedCard);
                            
                            // 카드 사용 후 처리
                            deckSystem.UseCard(selectedCard);
                            selectedCard = null;
                            cardManager.inHandCards.Remove(this.gameObject);
                            Destroy(this.gameObject);
                        }
                    }
                }
            } else {
                TransitionToState0();
                selectedCard = null;
            }
        }
        // 코드 수정 후
        if(Input.mousePosition.y <= Screen.height * cardPlayThreshold){
            currentState = 2;
            playArrow.SetActive(false);
        }
        //Debug.Log("1-20");
    }
}
