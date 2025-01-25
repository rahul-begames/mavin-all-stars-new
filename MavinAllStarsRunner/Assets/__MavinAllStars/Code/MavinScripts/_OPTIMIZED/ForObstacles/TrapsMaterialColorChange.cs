using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapsMaterialColorChange : MonoBehaviour
{
   [SerializeField] private Material trapsBaseMat,rampBaseMat;
   [SerializeField] private Color[] colorBasedOnLevel;

   private void OnEnable()
   {
      
      switch (SceneManager.GetActiveScene().name)
      {
         case "Nigeria":
            trapsBaseMat.color = colorBasedOnLevel[0];
            rampBaseMat.color = colorBasedOnLevel[0];
            break;
         case "UnitedKingdom":
            trapsBaseMat.color = colorBasedOnLevel[1];
            rampBaseMat.color = colorBasedOnLevel[1];
            break;
         case "Japan":
            trapsBaseMat.color = colorBasedOnLevel[2];
            rampBaseMat.color = colorBasedOnLevel[2];
            break;
         case "USA":
            trapsBaseMat.color = colorBasedOnLevel[3];
            rampBaseMat.color = colorBasedOnLevel[3];
            break;
         case "France":
            trapsBaseMat.color = colorBasedOnLevel[4];
            rampBaseMat.color = colorBasedOnLevel[4];
            break;
         case "China":
            trapsBaseMat.color = colorBasedOnLevel[5];
            rampBaseMat.color = colorBasedOnLevel[5];
            break;
         case "Germany":
            trapsBaseMat.color = colorBasedOnLevel[6];
            rampBaseMat.color = colorBasedOnLevel[6];
            break;
         case "Mexico":
            trapsBaseMat.color = colorBasedOnLevel[7];
            rampBaseMat.color = colorBasedOnLevel[7];
            break;
         case "Netherlands":
            trapsBaseMat.color = colorBasedOnLevel[8];
            rampBaseMat.color = colorBasedOnLevel[8];
            break;
         case "India":
            trapsBaseMat.color = colorBasedOnLevel[9];
            rampBaseMat.color = colorBasedOnLevel[9];
            break;
         case "SaudiArabia":
            trapsBaseMat.color = colorBasedOnLevel[10];
            rampBaseMat.color = colorBasedOnLevel[10];
            break;
      }
   }
}
