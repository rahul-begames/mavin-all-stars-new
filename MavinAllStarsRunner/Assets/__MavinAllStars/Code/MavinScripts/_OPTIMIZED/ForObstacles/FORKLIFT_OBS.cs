/*
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FORKLIFT_OBS : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private GameObject GO;
    public DOTweenAnimation liftDOTween;
    private void OnEnable()
    {
        objectToSpawn.SetActive(false);
        
        DoThis();
    }

    private void OnDisable()
    {
        Destroy(GO);
    }

    void DoThis()
    {
        GO = (GameObject)Instantiate(objectToSpawn, this.transform);
        liftDOTween = GO.GetComponent<DOTweenAnimation>();
        GO.SetActive(true);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FORKLIFT_OBS : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private GameObject spawnedObject;

    private void OnEnable()
    {
        objectToSpawn.SetActive(false);
        SpawnObject();
    }

    /*private void OnDisable()
    {
        Destroy(spawnedObject);
    }*/

    private void SpawnObject()
    {
        spawnedObject = objectToSpawn;
        spawnedObject.transform.position = transform.position;
        //spawnedObject = Instantiate(objectToSpawn, transform);
        spawnedObject.SetActive(true);
    }
}

