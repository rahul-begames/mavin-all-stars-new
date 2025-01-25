using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MultipleLaneRoad))]
public class MultiRoadLane : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MultipleLaneRoad road = (MultipleLaneRoad) target;
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Order1"))
        {
            road.Combo1();
        }
        
        if (GUILayout.Button("Order2"))
        {
            road.Combo2();
        }
        
        if (GUILayout.Button("Order3"))
        {
            road.Combo3();
        }
        
        /*if (GUILayout.Button("Order4"))
        {
            road.Combo4();
        }*/
        
        if (GUILayout.Button("Order5"))
        {
            road.Combo5();
        }
        
        if (GUILayout.Button("Order6"))
        {
            road.Combo6();
        }
        
        GUILayout.EndHorizontal();
        
         if (GUILayout.Button("Random"))
         {
             switch (UnityEngine.Random.Range(0, 7))
             {
                 case 0:
                     road.Combo1();
                     break;
                 case 1:
                     road.Combo2();
                     break;
                 case 2:
                     road.Combo2();
                     break;
                 case 3:
                     road.Combo3();
                     break;
                 case 4:
                     road.Combo4();
                     break;
                 case 5:
                     road.Combo5();
                     break;
                 case 6:
                     road.Combo6();
                     break;
             }
             
         }
    }
}
