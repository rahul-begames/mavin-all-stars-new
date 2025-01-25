using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float speed = 3f;
    public float distanceToPlayerForMove = 40f;

    protected Transform player;
    protected Transform thisTransform;
    protected bool move;

    protected virtual void Start ()
    {
        thisTransform = transform;
        player = GameObject.FindObjectOfType<Player>().thisTransform;
    }

    protected virtual void Update ()
    {
		if(thisTransform.position.z - player.position.z < distanceToPlayerForMove)
        {
            move = true;
        }

        if(move)
        {
            thisTransform.Translate(0, 0, -speed * Time.deltaTime);
        }
	}
}
