

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class KnockOut_OBS : MonoBehaviour
{
    [SerializeField] private bool Japan, City, Forest;

    [SerializeField] private GameObject[] JapanGO;
    [SerializeField] private GameObject[] CityGO;
    [SerializeField] private GameObject[] ForestGO;

    private List<GameObject> selectedGO = new List<GameObject>();

    private GameObject objectSpawn;
    
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        string activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName == "Japan")
        {
            Japan = true;
        }
        else
        {
            City = true;
        }

        PopulateSelectedGO();
        SpawnObject();
    }

    private void PopulateSelectedGO()
    {
        if (Japan)
        {
            selectedGO.AddRange(JapanGO);
        }
        
        if (City)
        {
            selectedGO.AddRange(CityGO);
        }
        
        if (Forest)
        {
            selectedGO.AddRange(ForestGO);
        }
    }

    private void SpawnObject()
    {
        if (selectedGO.Count > 0)
        {
            int randomIndex = Random.Range(0, selectedGO.Count);
            GameObject selectedObject = selectedGO[randomIndex];
            
            if (selectedObject != null)
            {
                objectSpawn = selectedObject;
                objectSpawn.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        //Destroy(objectSpawn);
        objectSpawn.SetActive(false);
        selectedGO.Clear();
    }
}

