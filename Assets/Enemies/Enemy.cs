using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    float maxHealthPoints = 100f;
    [SerializeField]
    float attackRadius = 10f;

    float currentHealthPoints = 100f;
    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    public float HealthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    public void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        var targetTransform = (distanceToPlayer <= attackRadius) ? player.transform : this.transform;
        aiCharacterControl.SetTarget(targetTransform);
    }
}
