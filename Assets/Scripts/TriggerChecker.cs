using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerChecker : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            FallDown();
        }

    }
    void FallDown()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 0.3f);
    }
}