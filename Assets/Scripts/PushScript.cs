using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{

    public float pushStrength = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic)
        {
            return;
        }
        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 direction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.velocity = direction * pushStrength;
    }
}
