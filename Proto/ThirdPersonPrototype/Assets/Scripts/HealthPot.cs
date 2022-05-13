using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            other.GetComponent<Health>().UpdatePots(true);
            Destroy(gameObject);
        }
    }
}
