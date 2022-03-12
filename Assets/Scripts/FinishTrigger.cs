using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public static FinishTrigger instance;
    public bool isFinish; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 
    void Start()
    {
        isFinish = false;
    }  
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFinish = true;
        } 
    }
}
