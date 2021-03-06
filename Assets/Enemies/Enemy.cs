﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    float maxHealthPoints = 100f;
    [SerializeField]
    float attackRadius = 4f;
    [SerializeField]
    float chaseRadius = 6f;
    [SerializeField]
    GameObject projectileToUse;
    [SerializeField]
    GameObject projectileSocket;
    [SerializeField]
    float damagePerShot = 9f;
    [SerializeField]
    float secondsBetweenShots = 2f;
    [SerializeField]
    Vector3 aimOffest = new Vector3(0, 1f, 0);

    bool isAttacking = false;
    private float currentHealthPoints;
    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    public float HealthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if (currentHealthPoints <= 0) { Destroy(gameObject); }
    }

    public void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        var targetTransform = this.transform;
        if (distanceToPlayer <= chaseRadius)
        {
            targetTransform = player.transform;
        }

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots);  //TODO : Switch to coroutines
        }


        if (distanceToPlayer >= attackRadius)
        {
            isAttacking = false;
            CancelInvoke();
        }

        aiCharacterControl.SetTarget(targetTransform);
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        var projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetDamage(damagePerShot);
        // projectileComponent.ProjectileSpeed = 4;

        //Get distance and fire
        Vector3 unitVectorToPlayer = (player.transform.position + aimOffest - projectileSocket.transform.position).normalized;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.ProjectileSpeed;
        //print(projectileComponent.ProjectileSpeed);
    }

    void OnDrawGizmos()
    {
        //Movement gizmo
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position, clickPoint);
        //Gizmos.DrawSphere(currend, 0.15f);
        //Gizmos.DrawSphere(ckil, 0.1f);

        //Chase sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        //Attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
