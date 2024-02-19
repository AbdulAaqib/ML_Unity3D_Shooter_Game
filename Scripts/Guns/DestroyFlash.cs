using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlash : MonoBehaviour
{
    //this is a code to time the flash of bullet
    public float maxLifetime;
    public GameObject flash; 
    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if(maxLifetime < 0 )
        {
            Destroy(flash);
        }
    }
}
