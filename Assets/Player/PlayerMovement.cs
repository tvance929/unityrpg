using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkMoveStopRadius = 0.2f;
    [SerializeField]
    float attackMoveStopRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
    bool isInDirectMode = false;
    Vector3 clickPoint;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
     //   cameraRaycaster.notifyMouseClickObservers += OnLayerChanged;
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G for gamepad.  TODO add to menu
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
          //  ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        var movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        clickPoint = cameraRaycaster.hit.point;
    //        switch (cameraRaycaster.currentLayerHit)
    //        {
    //            case Layer.Walkable:
    //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
    //                break;
    //            case Layer.Enemy:
    //                currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
    //                break;
    //            default:
    //                print("Unexpected layer found");
    //                return;
    //        }
    //    }

    //    WalkToDestination();
    //}

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        var reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.DrawSphere(clickPoint, 0.1f);

        Gizmos.color = new Color(255f, 0f, 0f, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

