using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testArrow : MonoBehaviour{
    public GameObject arrowPrefab;
    public GameObject dotPrefab;
    public int poolSize = 50;
    private List<GameObject> dotPool = new List<GameObject>();
    private GameObject arrowInstance;



    public float spacing = 50;
    public float arrowAngleAdjustment = 0;
    public int dotsToSkip = 1;
    private Vector3 arrowDirection;

    void Start(){
        arrowInstance = Instantiate(arrowPrefab, transform);
        arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);
    }

    void InitializeDotPool(int count){
        GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform.parent);

    }
}
