using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextProperties : MonoBehaviour
{
    public float DestroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //destroy game object
        Destroy(gameObject, DestroyTime);   
    }

  
}
