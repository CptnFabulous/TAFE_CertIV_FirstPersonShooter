using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 10f;
    public GameObject effectsPrefab;
    public Transform line;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            line.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
    }

    private void OnCollisionEnter(Collision c)
    {
        ContactPoint contact = c.contacts[0];
        //Instantiate(effectsPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        // insert code here for dealing damage to target
        Enemy e = c.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            e.Damage(10);
        }


        Destroy(gameObject);
    }

    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        // Add an instant force to the bullet
        rb.AddForce(direction * velocity, ForceMode.Impulse);
        // Set the line's origin (different from the bullet's starting position)
        line.transform.position = lineOrigin;
    }
}
