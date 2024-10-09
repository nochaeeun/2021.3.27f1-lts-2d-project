using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCamera : MonoBehaviour
{
    public Camera cam;
    void Start(){

    }
    void Update(){
        cam.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -70f);
    }
}
