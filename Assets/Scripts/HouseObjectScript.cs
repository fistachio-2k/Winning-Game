using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObjectScript : MonoBehaviour
{
    protected Rigidbody rb;
    void Start()
    {
        setCompnents();
    }


    void Update()
    {
        
    }

    void setCompnents()
    {
        MeshCollider sc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        sc.convex = true;
        sc.isTrigger = true;
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.isKinematic = true;
    }
}
