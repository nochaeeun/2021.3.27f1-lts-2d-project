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
    private int currentState = 0;
    // 카드의 원래 회전
    private Quaternion originalRotation;
    // 카드의 원래 위치
    private Vector3 originalPos;

    // 카드 선택 시 크기 배율
    [SerializeField] private float selectionScale = 1.1f;
    // 카드 플레이 임계값
    [SerializeField] private Vector2 cardPlay;
    // 카드 플레이 시 위치
    [SerializeField] private Vector3 playPos;
    // 카드 선택 시 나타나는 발광 효과
    [SerializeField] private GameObject glowEffect;
    // 카드 플레이 가능 시 나타나는 화살표
    [SerializeField] private GameObject playArrow;

    // 컴포넌트 초기화 및 원래 변환 정보 저장
    void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        originalScale = rectTransform.localScale;
        originalPos = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
    }

    // 매 프레임마다 현재 상태에 따른 처리
    void Update(){
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
                if(!Input.GetMouseButton(0)) {
                    TransitionToState0();
                }
                break;
        }
    }

    // 기본 상태(0)로 전환
    private void TransitionToState0(){
        currentState = 0;
        rectTransform.localScale = originalScale; // 크기 초기화
        rectTransform.localRotation = originalRotation; // 회전 초기화
        rectTransform.localPosition = originalPos; // 위치 초기화
        glowEffect.SetActive(false); // 발광 효과 비활성화
        playArrow.SetActive(false); // 플레이 화살표 비활성화
    }

    // 마우스가 카드 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData){
        if(currentState == 0) {
            originalPos = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;
            
            currentState = 1;
        }
    }

    // 마우스가 카드에서 벗어났을 때
    public void OnPointerExit(PointerEventData eventData){
        if(currentState == 1)
            TransitionToState0();
    }

    // 카드를 클릭했을 때
    public void OnPointerDown(PointerEventData eventData){
        if(currentState == 1 ){
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPos);
            originalPanelLocalPos = rectTransform.localPosition;
        }
    }

    // 카드를 드래그할 때
    public void OnDrag(PointerEventData eventData){
        if(currentState == 2){
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition)){
                // 코드 수정 전
                localPointerPosition /= canvas.scaleFactor;
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPos;
                rectTransform.localPosition = originalPanelLocalPos + offsetToOriginal;
                
                if(rectTransform.localPosition.y > cardPlay.y){
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = playPos;
                }
            }
        }
    }

    // 호버 상태 처리
    private void HandleHoverState(){
        //glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectionScale;
    }

    // 드래그 상태 처리
    private void HandleDragState(){
        rectTransform.localRotation = Quaternion.identity;
    }

    // 플레이 상태 처리
    private void HandlePlayState(){
        
        Vector3 position = rectTransform.localPosition;
        position.z = -10f;
        rectTransform.localPosition = position;

        rectTransform.localPosition = playPos;
        rectTransform.localRotation = Quaternion.identity;
        
        if(Input.mousePosition.y > cardPlay.y){
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
