using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour
{
    public Transform posToSpawn;
    private float alphaValue;
    public GameObject toSpawn;

    private void Start()
    {
        Spawn(toSpawn);
    }

    public void Spawn(GameObject prefabToSpawn)
    {
        StartCoroutine(ToSpawn(prefabToSpawn));
    }

    IEnumerator ToSpawn(GameObject prefabToSpawn)
    {
        GameObject collectible = (GameObject) Instantiate(prefabToSpawn, posToSpawn);
        
        collectible.GetComponent<TextMeshProUGUI>().color = new Color( collectible.GetComponent<TextMeshProUGUI>().color.r,
            collectible.GetComponent<TextMeshProUGUI>().color.g, collectible.GetComponent<TextMeshProUGUI>().color.b,0);


        DOTween.To(() => alphaValue,
            x => alphaValue = x, 1f, 1f);
        
        collectible.GetComponent<TextMeshProUGUI>().color = new Color( collectible.GetComponent<TextMeshProUGUI>().color.r,
            collectible.GetComponent<TextMeshProUGUI>().color.g, collectible.GetComponent<TextMeshProUGUI>().color.b,alphaValue);
        
        collectible.name = prefabToSpawn.name;
        
        yield return new WaitForSeconds(0.8f);
        
        DOTween.To(() => alphaValue,
            x => alphaValue = x, 0f, 1f).OnComplete((() =>
        {
            //Destroy(collectible);
        }));
        
        collectible.GetComponent<TextMeshProUGUI>().color = new Color( collectible.GetComponent<TextMeshProUGUI>().color.r,
            collectible.GetComponent<TextMeshProUGUI>().color.g, collectible.GetComponent<TextMeshProUGUI>().color.b,alphaValue);
        
    }
}
