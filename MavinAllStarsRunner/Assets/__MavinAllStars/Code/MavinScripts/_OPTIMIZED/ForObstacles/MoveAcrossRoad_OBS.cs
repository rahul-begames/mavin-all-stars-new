
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAcrossRoad_OBS : MonoBehaviour
{
    public enum MyEnum2
    {
        Full,
        Half,
        Random,
    }

    public MyEnum2 Mode = MyEnum2.Full;
    [SerializeField] private bool Generic;

    [SerializeField] private GameObject[] allGO, genericGO, nigeriaGO;
    [SerializeField] private GameObject[] bothModes;
    private List<GameObject> selectedGo = new List<GameObject>();

    public DOTweenAnimation fullAnim, halfAnim;

    private GameObject objectSpawned, parentGO;
    public DOTweenAnimation doTweenAnimation;

    private void OnEnable()
    {
        SetupMode();
        
        string activeSceneName = SceneManager.GetActiveScene().name;

        foreach (var aGameObject in allGO)
        {
            aGameObject.SetActive(false);
        }

        if (activeSceneName == "Nigeria")
        {
            selectedGo.AddRange(nigeriaGO);
        }
        
        else 
        {
            selectedGo.AddRange(genericGO);
        }

        SpawnObject();
    }

    private void SetupMode()
    {
        switch (Mode)
        {
            case MyEnum2.Full:
                doTweenAnimation = fullAnim;
                parentGO = bothModes[0];
                bothModes[0].SetActive(true);
                break;
                
            case MyEnum2.Half:
                doTweenAnimation = halfAnim;
                parentGO = bothModes[1];
                bothModes[1].SetActive(true);
                break;
                
            case MyEnum2.Random:
                int randomMode = UnityEngine.Random.Range(0, 2);
                doTweenAnimation = randomMode == 0 ? fullAnim : halfAnim;
                parentGO = bothModes[randomMode];
                parentGO.SetActive(true);
                break;
        }
    }

    private void SpawnObject()
    {
        if (selectedGo != null && selectedGo.Count > 0)
        {
            int randomGO = UnityEngine.Random.Range(0, selectedGo.Count);

            if (selectedGo[randomGO] != null)
            {
                objectSpawned = selectedGo[randomGO];
                objectSpawned.transform.parent = parentGO.transform;
            }
               

            objectSpawned.SetActive(true);
        }
    }

    private void OnDisable()
    {
        objectSpawned.SetActive(false);
    }
}

