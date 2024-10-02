using System.Collections.Generic;                                               
using UnityEngine;  


public class ArrowRenderer : MonoBehaviour                                  
{
    public GameObject arrowPrefab;                                              // 화살표의 모양을 정의하는 프리팹 오브젝트
    public GameObject dotPrefab;                                                // 화살표 경로를 표시할 점의 프리팹 오브젝트
    public int poolSize = 50;                                                   // 오브젝트 풀링을 위한 초기 점 개수

    [SerializeField] private List<GameObject> dotPool = new List<GameObject>(); // 재사용 가능한 점 오브젝트들을 저장하는 풀
    [SerializeField] private GameObject arrowInstance;                          // 실제로 화면에 표시되는 화살표 인스턴스

    ResourceManager resourceManager = new ResourceManager();

    public float spacing = 50f;                                                 // 화살표 경로 상의 점들 사이의 간격
    public float arrowAngleAdjustment = 0f;                                     // 화살표 회전 각도를 미세 조정하기 위한 값
    public int dotsToSkip = 1;                                                  // 화살표 끝부분에서 표시하지 않을 점의 개수
    private Vector3 arrowDirection;                                             // 화살표가 가리키는 방향을 저장하는 벡터


    void Start()                                                                // 스크립트가 시작될 때 호출되는 메서드
    {
        arrowPrefab = resourceManager.Load<GameObject>("Prefab/Arrow");
        dotPrefab = resourceManager.Load<GameObject>("Prefab/Dot");
        


        if(arrowPrefab == null){                                                // 화살표 프리팹이 할당되지 않았는지 확인
            Debug.LogError("화살표 프리팹이 할당되지 않았습니다.");                  // 에러 메시지 출력
            return;                                                             // 메서드 종료
        }
        
        try {
            arrowInstance = Instantiate(arrowPrefab, transform);                // 화살표 프리팹을 인스턴스화
        } catch (System.Exception e) {                                          // 예외 발생 시 처리
            Debug.LogError("화살표 인스턴스 생성 중 오류 발생: " + e.Message);       // 에러 메시지 출력
            return;                                                             // 메서드 종료
        }

        if(arrowInstance == null){                                              // 화살표 인스턴스가 생성되지 않았는지 확인
            Debug.LogError("화살표 인스턴스가 생성되지 않았습니다.");                // 에러 메시지 출력
            return;                                                             // 메서드 종료
        }
        
        arrowInstance.transform.localPosition = Vector3.zero;                   // 화살표 위치 초기화
        InitializeDotPool(poolSize);                                            // 점 오브젝트 풀 초기화
        
        Debug.Log("화살표 인스턴스가 성공적으로 생성되었습니다.");                    // 성공 메시지 출력
    }

    
    void Update()                                                               // 매 프레임마다 호출되는 메서드
    {
        if(arrowInstance == null){                                              // 화살표 인스턴스가 없는지 확인
            Debug.LogWarning("Arrow instance is null. Trying to recreate...");  // 경고 메시지 출력
            Start();                                                            // Start 메서드 재호출
            if(arrowInstance == null)   return;                                 // 여전히 null이면 메서드 종료
        }
        Vector3 mousePos = Input.mousePosition;                                 // 현재 마우스 위치 가져오기
        mousePos.z = 0f;                                                        // z 좌표를 0으로 설정

        Vector3 startPos = transform.position;                                  // 화살표 시작 위치
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);               // 중간 제어점 계산

        UpdateArrow(startPos, midPoint, mousePos);                              // 화살표 경로 업데이트
        PositionAndRotateArrow(mousePos);                                       // 화살표 위치 및 회전 조정
    }

    void UpdateArrow(Vector3 start, Vector3 mid, Vector3 end){                  // 화살표 경로 업데이트 메서드
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end)/spacing);    // 필요한 점의 개수 계산

        for(int i = 0 ; i < numDots && i < dotPool.Count; i++){                 // 각 점에 대해 반복
            float t = i / (float)numDots;
            t = Mathf.Clamp(t,0f,1f);                                           // t 값을 0과 1 사이로 제한

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);        // 베지어 곡선 상의 점 위치 계산

            if(i != numDots - dotsToSkip){                                      // 마지막 몇 개의 점을 제외하고
                dotPool[i].transform.position = position;                       // 점의 위치 설정
                dotPool[i].SetActive(true);                                     // 점 활성화
            }
            if(i==numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0){       // 화살표 방향을 결정할 점 선택
                arrowDirection = dotPool[i].transform.position;                 // 화살표 방향 설정
            }
        }

        for (int i = numDots - dotsToSkip; i < dotPool.Count; i++){             // 남은 점들에 대해
            if(i>0)
                dotPool[i].SetActive(false);                                    // 불필요한 점 비활성화
        }
    }

    void PositionAndRotateArrow(Vector3 position){                              // 화살표 위치 및 회전 설정 메서드
        arrowInstance.transform.position = position;                            // 화살표 위치 설정
        Vector3 direction = arrowDirection - position;                          // 화살표 방향 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    // 각도 계산
        angle += arrowAngleAdjustment;                                          // 각도 조정
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // 화살표 회전
    }

    Vector3 CalculateMidPoint(Vector3 start, Vector3 end){                      // 중간점 계산 메서드
        Vector3 midPoint = (start + end) / 2f;                                  // 시작점과 끝점의 중간점 계산
        float arrowHeight = Vector3.Distance(start, end) / 3f;                  // 화살표 높이 계산
        midPoint.y += arrowHeight;                                              // 중간점의 높이 조정
        return midPoint;                                                        // 계산된 중간점 반환
    }

    Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t){ // 2차 베지어 곡선 점 계산 메서드
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;                                             // 시작점의 영향
        point += 2 * u * t * control;                                           // 제어점의 영향
        point += tt * end;                                                      // 끝점의 영향
        return point;                                                           // 계산된 점 반환
    }

    void InitializeDotPool(int count){                                          // 점 오브젝트 풀 초기화 메서드
        for (int i = 0; i < count ; i++){                                       // 지정된 개수만큼 반복
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform); // 점 오브젝트 생성
            dot.SetActive(false);                                               // 초기에는 비활성화
            dotPool.Add(dot);                                                   // 풀에 추가
        }
    }
}
