using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class RotateOnY : MonoBehaviour
{
   
    private float y;

    private float x;
    // Update is called once per frame

    private float zValue;

    private void Start()
    {
        zValue = this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>()
            .m_TrackedObjectOffset.z;
        
       Rotate();
    }

    void Rotate()
    {
        DOTween.To(() => zValue, 
            x => zValue = x, -2f, 3f).OnComplete(() =>
        {
            DOTween.To(() => zValue,
                x => zValue = x, 2f, 3f).OnComplete(Rotate);

        });

        
    }

    void Update()
    {
        this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>()
            .m_TrackedObjectOffset.z = zValue;
    }


}
