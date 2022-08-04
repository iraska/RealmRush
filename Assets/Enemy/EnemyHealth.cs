using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When you add a script which uses RequireComponent to a GameObject, the required component is automatically added to the GameObject. This is useful to avoid setup errors. For example a script might require that a Rigidbody is always added to the same GameObject. When you use RequireComponent, this is done automatically, so you are unlikely to get the setup wrong.
[RequireComponent(typeof(Enemy))] // When add the enemyHealth script, also pulled in the enemy sc
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;
    
    int currentHitPoints = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start() 
    {
        enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
