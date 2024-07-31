using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private SpriteRenderer ren;

    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount> 0){
            ren.enabled = false;
            Destroy(this.gameObject);
        }
        
    }
}
