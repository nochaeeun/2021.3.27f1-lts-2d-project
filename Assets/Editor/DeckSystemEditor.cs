using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(deckSystem))]                                              // deckSystem용 커스텀 에디터
public class DeckSystemEditor : Editor
{
    public override void OnInspectorGUI(){                                      // 인스펙터 GUI 재정의
        DrawDefaultInspector();                                                 // 기본 인스펙터 GUI

        deckSystem deckSystem = (deckSystem)target;                             // 현재 선택된 deckSystem 가져와서
        if(GUILayout.Button("Draw Next Card")){                                 // 다음 카드 뽑는 버튼 만들고 클릭 처리
            cardManager cardManager = FindObjectOfType<cardManager>();          // 씬에서 cardManager 찾아와
            if(cardManager != null) deckSystem.DrawCard(cardManager);           // cardManager 있으면 카드 뽑기 실행
        }
    }
}
#endif