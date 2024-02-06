using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float radius = 5f;
    public float power = 500;

    

    public void TriggerC4()
    {
        Vector3 position = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach(Collider thing in hitColliders)
        {
            if (thing.GetComponent<Rigidbody>())
            {
                Rigidbody rb = thing.GetComponent<Rigidbody>();
                rb.AddExplosionForce(power, position, radius, 0.0f, ForceMode.Impulse);
            }
        }
    }
}
