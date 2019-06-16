 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;

    public float roundsPerMinute = 600;
    float fireTimer;

    public float spread = 2f;
    public float recoil = 1f;
    public float range = 10f;

    public int ammunitionReserve = 120;
    public int magazineCapacity = 30;
    int reserveCurrent = 120;
    int magazineCurrent;
    
    public Transform projectileOrigin;
    public GameObject bulletPrefab;
    public bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 60 / roundsPerMinute)
        {
            canShoot = true;
        }

    }

    public void Shoot()
    {
        // Reduce clip size
        magazineCurrent--; // magazineCurrent = magazineCurrent - 1 / magazineCurrent -= 1
        // Reset shoot timer
        fireTimer = 0f;
        // Reset canShoot
        canShoot = false;
        // Get origin + direction of fire
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 lineOrigin = projectileOrigin.position;
        Vector3 direction = camTransform.forward;
        // Shoot bullet
        GameObject clone = Instantiate(bulletPrefab, camTransform.position, camTransform.rotation);
        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Fire(lineOrigin, direction);
    }

    public void Reload()
    {
        // If there are bullets left in reserve
        if (reserveCurrent > 0)
        {
            // If there is enough bullets in reserve to fill a clip
            if (reserveCurrent >= magazineCapacity)
            {
                // Reduce the clip size by the offset from the current clip to max Clip
                int offset = magazineCapacity - magazineCurrent;
                reserveCurrent -= offset;
            }
            // If clip is below max clip
            if (magazineCurrent < magazineCapacity)
            {
                int tempMag = reserveCurrent;
                magazineCurrent = tempMag;
                reserveCurrent -= tempMag;
            }
        }
    }
}
