using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObjectScript : MonoBehaviour
{
    protected Rigidbody rb;
    GameObject[] houseObjects;
    void Start()
    {
        setCompnents();

        houseObjects = GameObject.FindGameObjectsWithTag("HouseObj");
        if (houseObjects == null)
        {
            Debug.Log("Here:(");
        }
    }


    void setCompnents()
    {
        foreach(GameObject ho in houseObjects)
        {
            MeshCollider sc = ho.AddComponent(typeof(MeshCollider)) as MeshCollider;
            sc.convex = true;
            //sc.isTrigger = true;
            rb = ho.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.isKinematic = true;
        }
    }
}
