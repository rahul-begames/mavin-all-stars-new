﻿
using UnityEngine;

public class Level : MonoBehaviour {
    
    public float distanceToPlayerForLoad = 110f;
    public float distanceToPlayerForUnload = 20f;
   
    protected Manager manager;
    protected Transform player;
    protected Transform thisTransform;
    protected bool loadNextLevel = true;

    protected virtual void Start ()
    {
        thisTransform = transform;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        player = manager.player.transform;
    }

    protected virtual void Update ()
    {
        if (loadNextLevel && manager.bIsPlayPressed)
        {
            if (thisTransform.position.z - player.position.z < distanceToPlayerForLoad)
            {
                if (manager.levels != null)
                {
                    GameObject lvl = Instantiate(manager.levels[Random.Range(0, manager.levels.Count)], thisTransform.position, Quaternion.identity);
                    lvl.transform.SetParent(manager.transform.parent);
                    lvl.SetActive(true);
                    loadNextLevel = false;
                }
                
            }
        }
        else
        {
            if (player.position.z - thisTransform.position.z > distanceToPlayerForUnload)
            {
                Destroy(thisTransform.parent.gameObject);
            }
        }
    }
}
