
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingFromSide_O : MonoBehaviour
{
    [SerializeField] private bool Japan, Generic;
    [SerializeField] private GameObject[] allGO, genericGO, japanGO;
    private List<GameObject> selectedGO = new List<GameObject>();
    private GameObject objectToSpawn;
    public DOTweenAnimation _doTweenAnimation;
    private void OnEnable()
    {
        foreach (var aGameObject in allGO)
        {
            aGameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "Japan")
        {
            foreach (var jp in japanGO)
            {
                selectedGO.Add(jp);
            }
        }

        if (Generic)
        {
            foreach (var gp in genericGO)
            {
                selectedGO.Add(gp);
            }
        }

        SpawnObject();
    }

    private void SpawnObject()
    {
        if (selectedGO.Count > 0)
        {
            int randomIndex = Random.Range(0, selectedGO.Count);
            var selectedPrefab = selectedGO[randomIndex];

            if (selectedPrefab != null)
            {
                objectToSpawn = selectedPrefab;
                objectToSpawn.transform.parent = transform.GetChild(0);
                
                objectToSpawn.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        //Destroy(objectToSpawn);
        objectToSpawn.SetActive(false);
        selectedGO.Clear();
    }
}

