using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    /*public Texture2D[] textures;
    public float timeBetweenChange=.1f;
    
    int textureIndex;
    public Material ramp1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTexture(ramp1));
    }

    IEnumerator ChangeTexture(Material ramp)
    {
        // waits "timeBetweenChange" amount of time
        yield return new WaitForSeconds(timeBetweenChange);

        //Changes texture using list and index
        ramp.mainTexture = textures[textureIndex];

        //Checks if material uses last texture of the list 
        if (textureIndex == textures.Length - 1)
        {
            // resets index for looping
            textureIndex = 0;
        }
        else
        {
            //Adds 1 to index for changing to next texture next time coroutine starts
            textureIndex++;
        }

        //Starts coroutine for loop
        StartCoroutine(ChangeTexture(ramp));
    }*/

}
