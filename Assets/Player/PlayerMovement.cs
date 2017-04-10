using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMovement : MonoBehaviour
{
    //TODO Solve fight between serializable and const
    [SerializeField]
    const int walkableLayerNumber = 8;
    [SerializeField]
    const int enemyLayerNumber = 9;

    ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
   // Vector3 currentDestination, clickPoint;
    AICharacterControl aiCahracterControl = null;
    GameObject targetObject = null;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiCahracterControl = GetComponent<AICharacterControl>();
        //currentDestination = transform.position;
        targetObject = new GameObject("targetObject");

        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }


    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (Input.GetMouseButton(0))
        {
            switch (layerHit)
            {
                case walkableLayerNumber:
                    targetObject.transform.position = raycastHit.point;
                    break;
                case enemyLayerNumber:
                    //Navigate to the enemy
                    targetObject.transform.position = raycastHit.collider.gameObject.transform.position;
                    break;
                default:
                    Debug.LogWarning("Don't know how to handle player click movement.");
                    return;
            }
            aiCahracterControl.SetTarget(targetObject.transform);
        }
    }

    //TODO Make this get called again  later
    void ProcessDirectMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        var movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }
}

