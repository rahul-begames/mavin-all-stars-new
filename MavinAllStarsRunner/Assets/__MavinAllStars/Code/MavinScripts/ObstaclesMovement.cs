/*
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
    public bool toMove;
    public GameObject vehicle;
    [SerializeField]private float speed;
    public GameObject[] randomStuff;
    public bool iCanMove;
    public Transform tomoveto;
    [SerializeField] private GameObject[] japanGO, usGO, ukGO, germanyGO, netherlandsGO, franceGO, indiaGO, mexicoGO, saudiGO, genericGO;


    private void OnEnable()
    {
        iCanMove = false;
        vehicle.transform.localPosition = new Vector3(0f,0f,0f);
        
        if(transform.GetChild(0).GetChild(0) != null)
            if(transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>() != null)
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        
        foreach (Transform vh in vehicle.transform)
        {
            if(vh!=null)
                vh.gameObject.SetActive(false);
        }
        
        if (randomStuff.Length > 0)
        {
            randomStuff[UnityEngine.Random.Range(0, randomStuff.Length)].SetActive(true);
        }
    }

    IEnumerator RandomCar()
    {
        yield return new WaitForSeconds(1f);
        
    }

    public void RandomCarSpawn()
    {

        foreach (Transform vh in vehicle.transform)
        {
            if(vh!=null)
                vh.gameObject.SetActive(false);
        }

        if (randomStuff.Length > 0)
        {
            randomStuff[UnityEngine.Random.Range(0, randomStuff.Length)].SetActive(true);
        }
    }
    

    public void Movement()
    {

        if (toMove && LevelManager.Instance.isGameStarted)
        {
            //vehicle.GetComponent<DOTweenAnimation>().DOPlay();
            
            StartCoroutine(StartMovement());
        }
        
       
        
    }

    public Vector3 check;
    Vector3 leftDirection = Vector3.left;
    public float range;
    /*
    private void Update()
    {

        if (LevelManager.Instance.isGameStarted)
        {
            Ray movingVehicleRay = new Ray(vehicle.transform.position + check,transform.TransformDirection(Vector3.forward * range));
        
            if (Physics.Raycast(movingVehicleRay, out RaycastHit hit, range))
            {
                
                /*if (hit.collider.gameObject.CompareTag("Collision"))
                {
                    Debug.Log("Stop");
                    vehicle.GetComponent<DOTweenAnimation>().DOPause();
                }#2#

                if (hit.collider.gameObject.name != "PLAYER"
                || hit.collider.gameObject.name != "DEAD"
                || hit.collider.gameObject.name != "End"
                || hit.collider.gameObject.name != "SPAWN"
                || !hit.collider.gameObject.name.Contains("Coin")
                || hit.collider.gameObject.name!="MIDOBS"
                || hit.collider.gameObject.name!="ENDOBS"
                || hit.collider.gameObject.name!="DEADOBS"
                || !hit.collider.gameObject.name.Contains("HS")
                || !hit.collider.gameObject.name.Contains("Ed"))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    vehicle.GetComponent<DOTweenAnimation>().DOPause();
                    this.enabled = false;
                }
                
            }
        }
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(vehicle.transform.position + check,transform.TransformDirection(Vector3.forward * range));
    } 
    #1#

    IEnumerator StartMovement()
    {
        if (iCanMove)
        {
            // Move the vehicle towards the destination
            vehicle.transform.localPosition = Vector3.MoveTowards(vehicle.transform.localPosition, tomoveto.localPosition, speed * Time.deltaTime);
        
            // Check if the vehicle has reached the destination
            if (Vector3.Distance(vehicle.transform.localPosition, tomoveto.localPosition) <= 0.01f)
            {
                yield return new WaitForSeconds(9f);
                
                // Reset vehicle position to the original position
                vehicle.transform.localPosition = Vector3.zero;

                // Optionally, stop the coroutine
                yield break;
            }
            else
            {
                // Continue movement
                yield return null;
                StartCoroutine(StartMovement());
            }
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ObstaclesMovement : MonoBehaviour
{
    [Header("Scene-Specific Objects")]
    [SerializeField] private GameObject[] japanGO, usGO, ukGO, germanyGO, netherlandsGO, franceGO, mexicoGO, nigeriGO;

    [Header("Movement Settings")]
    public bool toMove;
    public float speed;
    public Transform tomoveto;

    [Header("References")]
    public GameObject vehicle;
    public GameObject[] randomStuff;  // Possibly objects that can appear in any scene

    private List<GameObject> selectedObjects = new List<GameObject>();
    public bool iCanMove;

    private void OnEnable()
    {
        // 1. Reset movement
        iCanMove = false;
        vehicle.transform.localPosition = Vector3.zero;

        // 2. Hide child MeshRenderer if it exists
        if (transform.childCount > 0 && transform.GetChild(0).childCount > 0)
        {
            MeshRenderer mesh = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
            if (mesh != null) mesh.enabled = false;
        }

        speed = UnityEngine.Random.Range(12, 15);

        // 3. Deactivate all child objects under 'vehicle'
        DeactivateObjectsUnder(vehicle.transform);

        // 4. Deactivate everything in genericGO by default (similar to ALL_OBS approach)
        //DeactivateObjects(randomStuff);

        // 5. Select objects based on the active scene
        string activeSceneName = SceneManager.GetActiveScene().name;
        selectedObjects = GetSceneSpecificObjects(activeSceneName);

        // 6. Optionally, incorporate any "randomStuff" logic
        //    If you want to unify it with scene-specific objects, uncomment the line below:
        // selectedObjects.AddRange(randomStuff);

        // 7. Activate a random object from the selected objects
        if (selectedObjects.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, selectedObjects.Count);
            selectedObjects[randomIndex].SetActive(true);
        }
        else
        {
            // If no scene-specific objects exist, randomly activate one from 'randomStuff'
            if (randomStuff.Length > 0)
                randomStuff[UnityEngine.Random.Range(0, randomStuff.Length)].SetActive(true);
        }
    }

    /// <summary>
    /// Returns objects relevant to the current scene. 
    /// If a scene should use 'genericGO', just add them after the switch, 
    /// or do it directly in the switch logic for that scene.
    /// </summary>
    private List<GameObject> GetSceneSpecificObjects(string sceneName)
    {
        switch (sceneName)
        {
            case "Japan":
                return new List<GameObject>(japanGO);
            case "China":
                return new List<GameObject>(japanGO);

            case "USA":
                return new List<GameObject>(usGO);

            case "United Kingdom":
            case "UnitedKingdom": // handle naming variations
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
                // Nigeria might want to use generic objects, or special Nigeria objects, etc.
                return new List<GameObject>(nigeriGO);

            // Add other scenes as needed
            default:
                // No objects specifically assigned for this scene
                return new List<GameObject>();
        }
    }

    /// <summary>
    /// Deactivates every GameObject in the passed-in array.
    /// </summary>
    private void DeactivateObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
            obj.SetActive(false);
    }

    /// <summary>
    /// Deactivates all children under a given Transform.
    /// </summary>
    private void DeactivateObjectsUnder(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Called externally (perhaps by a manager) to spawn a random object in 'randomStuff'.
    /// </summary>
    public void RandomCarSpawn()
    {
        DeactivateObjectsUnder(vehicle.transform);

        if (randomStuff.Length > 0)
        {
            randomStuff[UnityEngine.Random.Range(0, randomStuff.Length)].SetActive(true);
        }
    }

    public void Movement()
    {
        if (toMove)
        {
            StartCoroutine(StartMovement());
        }
    }

    /// <summary>
    /// Coroutine that moves the vehicle toward tomoveto at 'speed' if iCanMove is true.
    /// </summary>
    IEnumerator StartMovement()
    {
        if (!iCanMove)
            yield break;

        // Move the vehicle towards the destination
        vehicle.transform.localPosition = Vector3.MoveTowards(
            vehicle.transform.localPosition,
            tomoveto.localPosition,
            speed * Time.deltaTime
        );

        // Check if the vehicle has reached the destination
        if (Vector3.Distance(vehicle.transform.localPosition, tomoveto.localPosition) <= 0.01f)
        {
            yield return new WaitForSeconds(9f);

            // Reset vehicle position
            vehicle.transform.localPosition = Vector3.zero;
            yield break;
        }
        else
        {
            // Keep moving
            yield return null;
            StartCoroutine(StartMovement());
        }
    }
}

