using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHouseObject : HouseObjectScript
{
    // Start is called before the first frame update
    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
