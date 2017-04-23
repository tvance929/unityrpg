using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    float maxHealthPoints = 100f;
    [SerializeField]
    int enemyLayer = 9;
    [SerializeField]
    float damagePerHit = 5;
    [SerializeField] private float minTimeBetweenHits = .5f;
    [SerializeField] private float maxAttackRange = 2f;

    float currentHealthPoints;
    private GameObject currentTarget;
    private CameraRaycaster cameraRaycaster;
    private float lastHitTime = 0f;

    void Start()
    {
        currentHealthPoints = maxHealthPoints;

        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += (raycastHit, layerHit) =>
        {
            if (layerHit == enemyLayer)
            {
                currentTarget = raycastHit.collider.gameObject;

                //Check distance
                if ((currentTarget.transform.position - transform.position).magnitude > maxAttackRange)
                {
                    return;
                }

                var enemyComponent = currentTarget.GetComponent(typeof(IDamageable));

                //Check time since last attach
                if (Time.time - lastHitTime > minTimeBetweenHits)
                {
                    (enemyComponent as IDamageable).TakeDamage(damagePerHit);
                    lastHitTime = Time.time;
                }
            }
        };
    }

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
        // if (currentHealthPoints <= 0) DestroyObject(gameObject);
    }
}
