

/*
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ALL_OBS : MonoBehaviour
{
    public enum FallingOption
    {
        Random,
        Falling,
        NotFalling,
    }

    public FallingOption fallingOption = FallingOption.Random;

    [SerializeField] private bool generic;
    [SerializeField] private GameObject[] japanGO, usGO, ukGO, germanyGO, netherlandsGO, franceGO, indiaGO, mexicoGO, saudiGO, genericGO;

    private List<GameObject> selectedObjects = new List<GameObject>();
    private GameObject objectSelected;
    private bool isFallingActive;

    public ParticleSystem groundExplosionEffect;

    private void OnEnable()
    {
        foreach (var genericGO in genericGO)
        {
            genericGO.SetActive(false);
        }

        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "Japan" || activeSceneName == "China")
        {
            generic = false;
            selectedObjects.AddRange(japanGO);
        }
        else if (activeSceneName == "Nigeria")
        {
            generic = true;
        }
        else if (activeSceneName == "USA")
        {
            generic = false;
            selectedObjects.AddRange(usGO);
        }
        else if (activeSceneName == "United Kingdom")
        {
            generic = false;
            selectedObjects.AddRange(ukGO);
        }
        else if (activeSceneName == "Germany")
        {
            generic = false;
            selectedObjects.AddRange(germanyGO);
        }
        else if (activeSceneName == "Netherlands")
        {
            generic = false;
            selectedObjects.AddRange(netherlandsGO);
        }
        else if (activeSceneName == "France")
        {
            generic = false;
            selectedObjects.AddRange(franceGO);
        }
        
        else if (activeSceneName == "Mexico")
        {
            generic = false;
            selectedObjects.AddRange(mexicoGO);
        }
       

        if (generic)
        {
            selectedObjects.AddRange(genericGO);
        }

        DoThis();

        switch (fallingOption)
        {
            case FallingOption.Falling:
                isFallingActive = true;
                transform.DOMoveY(200, 0.8f).SetEase(Ease.OutCirc).SetRelative(true);
                break;
            case FallingOption.NotFalling:
                isFallingActive = false;
                break;
            case FallingOption.Random:
                isFallingActive = UnityEngine.Random.Range(0, 2) == 0;
                if (isFallingActive)
                {
                    transform.DOMoveY(200, 0.8f).SetEase(Ease.OutCirc).SetRelative(true);
                }
                break;
        }
    }

    private void DoThis()
    {
        if (selectedObjects.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, selectedObjects.Count);
            objectSelected = selectedObjects[randomIndex];
            objectSelected.SetActive(true);
        }
    }

    private void OnDisable()
    {
       // Destroy(objectSelected);
       if(objectSelected != null)
            objectSelected.SetActive(false);
       if(selectedObjects != null)
            selectedObjects.Clear();
    }
}
*/

using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ALL_OBS : MonoBehaviour
{
    public enum FallingOption
    {
        Random,
        Falling,
        NotFalling,
    }

    public FallingOption fallingOption = FallingOption.Random;

    [SerializeField] private bool generic;
    [SerializeField] private GameObject[] japanGO, usGO, ukGO, germanyGO, netherlandsGO, franceGO, indiaGO, mexicoGO, saudiGO, genericGO;

    private List<GameObject> selectedObjects = new List<GameObject>();
    private GameObject objectSelected;
    private bool isFallingActive;

    public ParticleSystem groundExplosionEffect;

    private void OnEnable()
    {
        DeactivateObjects(genericGO);

        string activeSceneName = SceneManager.GetActiveScene().name;

        selectedObjects = GetSceneSpecificObjects(activeSceneName);

        if (generic)
        {
            selectedObjects.AddRange(genericGO);
        }

        ActivateRandomObject();
        HandleFallingOption();
    }

    private List<GameObject> GetSceneSpecificObjects(string sceneName)
    {
        generic = false;
        switch (sceneName)
        {
            case "Japan":
                return new List<GameObject>(japanGO);
            case "China":
                return new List<GameObject>(japanGO);
            case "USA":
                return new List<GameObject>(usGO);
            case "UnitedKingdom":
                return new List<GameObject>(ukGO);
            case "Germany":
                return new List<GameObject>(germanyGO);
            case "Netherlands":
                return new List<GameObject>(netherlandsGO);
            case "France":
                return new List<GameObject>(franceGO);
            case "Mexico":
                return new List<GameObject>(mexicoGO);
            case "Nigeria":
                generic = true;
                return new List<GameObject>();
            default:
                return new List<GameObject>();
        }
    }

    private void ActivateRandomObject()
    {
        if (selectedObjects.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, selectedObjects.Count);
            objectSelected = selectedObjects[randomIndex];
            objectSelected.SetActive(true);
        }
    }

    private void HandleFallingOption()
    {
        switch (fallingOption)
        {
            case FallingOption.Falling:
                isFallingActive = true;
                StartFallingAnimation();
                break;
            case FallingOption.NotFalling:
                isFallingActive = false;
                break;
            case FallingOption.Random:
                isFallingActive = UnityEngine.Random.Range(0, 2) == 0;
                if (isFallingActive)
                {
                    StartFallingAnimation();
                }
                break;
        }
    }

    private void StartFallingAnimation()
    {
        transform.DOMoveY(200, 0.8f).SetEase(Ease.OutCirc).SetRelative(true);
    }

    private void DeactivateObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (objectSelected != null)
            objectSelected.SetActive(false);
        selectedObjects.Clear();
    }
}


