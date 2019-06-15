using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public string hitTag;
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitTag || hitTag == "")
        {
            onEnter.Invoke();
        }
    }
}
