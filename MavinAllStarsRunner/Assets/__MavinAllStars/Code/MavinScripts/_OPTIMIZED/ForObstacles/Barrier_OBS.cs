/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_OBS : MonoBehaviour
{
    [SerializeField] private bool isLeft, isRight,isRandom;
    [Space(8)]
    
    [SerializeField] private GameObject generalBarrierL,generalBarrierR;

    private GameObject selectedBarrier;
    
    [Space(8)]
    
    [SerializeField] private bool generalBarrierOn;

    private GameObject objectToSpawn;

    
    private void OnEnable()
    {
        generalBarrierL.SetActive(false);
        generalBarrierR.SetActive(false);
        
        if (isLeft)
        {
            selectedBarrier = generalBarrierL;
        }
        if (isRight)
        {
            selectedBarrier = generalBarrierR;
        }

        if (isRandom)
        {
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    selectedBarrier = generalBarrierL;
                    break;
                case 1:
                    selectedBarrier = generalBarrierR;
                    break;
            }
            
        }

        DoThis();
           
    }

    void DoThis()
    {
        if (generalBarrierOn && selectedBarrier != null)
        {
            objectToSpawn = (GameObject) Instantiate(selectedBarrier, this.transform);
            objectToSpawn.SetActive(true);
        }
    }

    private void OnDisable()
    {
       Destroy(objectToSpawn);
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_OBS : MonoBehaviour
{
    [SerializeField] private bool isLeft, isRight, isRandom;
    [SerializeField] private GameObject generalBarrierL, generalBarrierR;

    private GameObject selectedBarrier;
    private GameObject objectToSpawn;

    private void OnEnable()
    {
        generalBarrierL.SetActive(false);
        generalBarrierR.SetActive(false);

        if (isLeft)
        {
            selectedBarrier = generalBarrierL;
        }
        else if (isRight)
        {
            selectedBarrier = generalBarrierR;
        }
        else if (isRandom)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    selectedBarrier = generalBarrierL;
                    break;
                case 1:
                    selectedBarrier = generalBarrierR;
                    break;
            }
        }

        DoThis();
    }

    private void DoThis()
    {
        if (selectedBarrier != null)
        {
            selectedBarrier.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if(selectedBarrier != null)
            selectedBarrier.SetActive(false);
    }
}

