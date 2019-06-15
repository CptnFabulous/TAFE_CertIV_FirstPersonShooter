using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class AI : MonoBehaviour
{
    public float maxVelocity = 5;
    public float maxDistance = 5;
    protected NavMeshAgent na;
    protected Vector3 velocity;
    // public Vector3 velocity;
    public SteeringBehaviour[] behaviours;

    public Vector3 Velocity
    {
        protected set { velocity = value; }
        get { return velocity; }
    }

    
    // Start is called before the first frame update
    void Awake()
    {
        na = GetComponent<NavMeshAgent>();
        behaviours = GetComponents<SteeringBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateForce();
    }

    public virtual Vector3 CalculateForce() // Calculates all forces from all behaviours
    {
        Vector3 force = Vector3.zero; // Create new result Vector3, set to zero

        
        foreach (var behaviour in behaviours) // Step 2). Loop through all behaviours and get forces
        {
            force += behaviour.GetForce() * behaviour.weighting; // APPLY force to behaviour.GetForce x Weighting

            if (force.magnitude > maxVelocity) // IF force magnitude > maxVelocity
            {
                force = force.normalized * maxVelocity; // SET force to force normalized x maxVelocity
                break; // BREAK - Exits the Loop
            }
        }

        // Step 4). Limit the total velocity to our max velocity if it exceeds
        velocity += force * Time.deltaTime;
        if (velocity.magnitude > maxVelocity) // IF velocity magnitude > max velocity THEN
        {
            velocity = velocity.normalized * maxVelocity;
        }

        if (velocity.magnitude > 0)
        {
            Vector3 pos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, maxDistance, -1))
            {
                na.SetDestination(hit.position);
            }
        }

        return force;

    }
}
