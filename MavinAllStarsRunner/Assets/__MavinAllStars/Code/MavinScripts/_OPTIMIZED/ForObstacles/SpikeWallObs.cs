/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeWallObs : MonoBehaviour
{
    [SerializeField] private bool isGeneric;
    [SerializeField] private GameObject[] japanGO, genericGO;
    
    [SerializeField]private List<GameObject> selectedObjects = new List<GameObject>();

    private void OnEnable()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);


        if (SceneManager.GetActiveScene().name == "Japan" || SceneManager.GetActiveScene().name == "China")
        {
            isGeneric = false;
            foreach (var japan in japanGO)
            {
                selectedObjects.Add(japan);
            }
        }
        
        else 
        {
            foreach (var generic in genericGO)
            {
                selectedObjects.Add(generic);
            }
        }

        foreach (var go in selectedObjects)
        {
            go.SetActive(false);
        }
        
        
    }

    public void RunPS()
    {

        StartCoroutine(SpikeWallPS());
        
            
    }
    
    IEnumerator SpikeWallPS()
    {
        int x = UnityEngine.Random.Range(0, selectedObjects.Count);
        if (selectedObjects[x] != null)
        {
            selectedObjects[x].SetActive(true);
            selectedObjects[x].GetComponent<ParticleSystem>().Play();
        }
        
        
        yield return new WaitForSeconds(1.5f);
        
        if (selectedObjects[x] != null)
        {
            selectedObjects[x].GetComponent<ParticleSystem>().Pause();
        }
        
        
    }

    private void OnDisable()
    {
        if (selectedObjects!= null)
        {
            selectedObjects.Clear();
        }
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeWallObs : MonoBehaviour
{
    [SerializeField] private bool isGeneric;
    [SerializeField] private GameObject[] japanGO, genericGO;
    
    private List<GameObject> selectedObjects = new List<GameObject>();

    private void OnEnable()
    {
        Transform child = transform.GetChild(0);
        child.gameObject.SetActive(false);

        string activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName == "Japan" || activeSceneName == "China")
        {
            isGeneric = false;
            selectedObjects.AddRange(japanGO);
        }
        else
        {
            selectedObjects.AddRange(genericGO);
        }

        foreach (var go in selectedObjects)
        {
            go.SetActive(false);
        }
    }

    public void RunPS()
    {
        StartCoroutine(SpikeWallPS());
    }

    IEnumerator SpikeWallPS()
    {
        int x = Random.Range(0, selectedObjects.Count);
        GameObject selectedObject = selectedObjects[x];

        if (selectedObject != null)
        {
            selectedObject.SetActive(true);
            selectedObject.GetComponent<ParticleSystem>().Play();
        }
        
        yield return new WaitForSeconds(1.5f);
        
        if (selectedObject != null)
        {
            selectedObject.GetComponent<ParticleSystem>().Pause();
        }
    }

    private void OnDisable()
    {
        selectedObjects.Clear();
    }
}

