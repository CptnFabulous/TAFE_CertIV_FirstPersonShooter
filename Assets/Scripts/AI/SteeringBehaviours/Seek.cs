using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    // These are variables, they store data
    public Transform target;
    public float stoppingDistance;
    
    // This is a property, it 
    public override Vector3 GetForce() // Any code different from what is in the base class, you put in an override function
    {
        Vector3 force = Vector3.zero;

        // Step 1). Check if we have a valid target
        if (target == null) // IF target is null
        {
            return force; //  return force (zero)
        }

        // Step 2). Get direction we want to go
        Vector3 desiredForce = target.position - transform.position; // SET desiredForce to target - current

        // Step 3). Apply weighting to desired force
        if (desiredForce.magnitude > stoppingDistance) // IF desiredForce distance is greater than stoppingDistance
        {
            desiredForce = desiredForce.normalized * weighting; //  SET desiredForce to restricted desiredForce (using weighting)
            force = desiredForce - owner.Velocity; //  SET force to desiredForce - velocity
        }

        return force;
        //return base.GetForce();
    }
}
