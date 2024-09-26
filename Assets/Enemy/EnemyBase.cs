using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyBase : MonoBehaviour
{

    // Start is called before the first frame update

    class Enemy
    {
        public string name;
        public int uneasy;
        public int stastability;
        public int attack;
        public int defense;
        public int skill;
        public int rand;

    }
    void Start()
    {
        System.Random random = new System.Random();


    }
    // Update is called once per frame
    void Update()
    {

    }
}
