using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLaneRoad : MonoBehaviour
{
    [SerializeField] private GameObject lRoad, cRoad, rRoad;
    [SerializeField] private GameObject lSide, cSide, rSide;

    private void OnEnable()
    {
       //Inactive();
    }

    public void Combo1()
    {
        Inactive();
        
        lRoad.SetActive(true);
        cSide.SetActive(true);
        rSide.SetActive(true);
    }
    
    public void Combo2()
    {
        Inactive();
        
        cRoad.SetActive(true);
        lSide.SetActive(true);
        rSide.SetActive(true);
    }
    
    public void Combo3()
    {
        Inactive();
        
        rRoad.SetActive(true);
        cSide.SetActive(true);
        lSide.SetActive(true);
    }
    
    public void Combo4()
    {
        Inactive();
        
        lRoad.SetActive(true);
        cRoad.SetActive(true);
        rSide.SetActive(true);
    }
    
    public void Combo5()
    {
        Inactive();
        
        rRoad.SetActive(true);
        cSide.SetActive(true);
        lSide.SetActive(true);
    }
    
    public void Combo6()
    {
        Inactive();
        
        rRoad.SetActive(true);
        lRoad.SetActive(true);
        cSide.SetActive(true);
    }

    void Inactive()
    {
        lRoad.SetActive(false);
        cRoad.SetActive(false);
        rRoad.SetActive(false);
        lSide.SetActive(false);
        cSide.SetActive(false);
        rSide.SetActive(false);
    }
}
