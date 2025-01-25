using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public bool isCoin;
    public bool isTimeMultiplier;
    public bool isCoinsMultiplier;
    public bool isJumpMultiplier;

    public float multiplier = 1f;
    public float time = 10f;
    public float appearanceChance = 1;

    public AudioClip sound;
    public GameObject effect;

    protected virtual void Start ()
    {
		if(Random.value > appearanceChance)
        {
            Destroy(gameObject);
        }
	}	
}
