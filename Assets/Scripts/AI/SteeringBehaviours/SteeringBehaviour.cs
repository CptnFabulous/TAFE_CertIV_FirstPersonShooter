using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    protected AI owner; // Reference to owner (for getting Velocity)
    
    
    void Awake()
    {
        owner = GetComponent<AI>();
    }

    public virtual Vector3 GetForce() // Calculates all forces from all behaviours
    {
        Vector3 force = Vector3.zero;

        // Do nothing in the base class (always returns zero)

        return force;
    }
}
