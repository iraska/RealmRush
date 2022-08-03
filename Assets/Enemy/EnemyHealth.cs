using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints = 0;

    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    // OnParticleCollision is called when a particle hits a Collider.
    // This can be used to apply damage to a GameObject when hit by particles.
    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

// Checked send collision message in inspector, otherwise currentHitPoint can not effect (ballista>particle system>collision)
    void ProcessHit()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
