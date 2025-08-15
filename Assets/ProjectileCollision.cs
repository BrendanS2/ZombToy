using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    PlayerHealth p_health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p_health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            p_health.TakeDamage(20);

        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Invoke(nameof(WaitTillDelete), 2f);
        }


    }

    public void WaitTillDelete()
    {
        Destroy(gameObject);
    }
}
