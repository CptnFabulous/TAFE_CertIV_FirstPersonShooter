/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public Transform moveTarget; // The location the enemy will move to
    public GameObject enemyTarget; // The player or NPC the enemy wishes to attack

    NavMeshAgent na;
    
    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SeekEnemy()
    {
        moveTarget = enemyTarget.transform;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace for implementing UI references (i.e, Sliders, Toggles, Buttons, etc)
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject healthBarUIPrefab; // Prefab to spawn in healt bar parent
    public Transform healthBarParent; // Prefab parent to store health bar ui
    public Transform healthBarPosition;

    private int health = 0;
    private Slider healthSlider;
    private Renderer healthRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn HealthBar UI into parent and get reference to Slider component on clone
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        healthSlider = clone.GetComponent<Slider>();
        healthRenderer = clone.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (healthRenderer.isVisible)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPosition.position); // + offset
            healthSlider.transform.position = screenPosition;
        }
    }

    private void OnDestroy()
    {
        Vector3 pos = transform.position;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthSlider.value = (float)health / (float)maxHealth; // Converts 0-100 to 0-1 (current/max)
                                                               // If health is zero
        if (health < 0)
        {
            // Destroy GameObject
            Destroy(gameObject);
        }
    }
}
