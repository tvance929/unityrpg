using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour
{

    [SerializeField]
    float xRotationsPerMinute = 1f;
    [SerializeField]
    float yRotationsPerMinute = 1f;
    [SerializeField]
    float zRotationsPerMinute = 1f;

    bool windIsBlowing = true;
    int windStoppedTimer = 0;
    int windStoppedFrames = 0;

    void Update()
    {
        if (windIsBlowing)
        {
            //xDegreesPerFrame = Time.DeltaTime, 60, xRotationsPerMinute, 360
            //// Debug.Log("Time : " + (60 - Time.deltaTime));
            //Debug.Log("Degrees Per Minute : " + (360 * zRotationsPerMinute));
            //Debug.Log("Degrees Per Second: " + (360 * zRotationsPerMinute) / 60);
            ////Debug.Log... got it!          
            float xDegreesPerFrame = ((360 * xRotationsPerMinute) / 60) * Time.deltaTime;
            transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);

            float yDegreesPerFrame = ((360 * yRotationsPerMinute) / 60) * Time.deltaTime;
            transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);

           
            float zDegreesPerFrame = ((360 * zRotationsPerMinute) / 60) * Time.deltaTime;
            transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);

            //Give a 1 in 20 chance of not spinning ( wind stopped blowing )
            //  windIsBlowing = Random.Range(0, 20) < 18;
            //  if (!windIsBlowing) windStoppedFrames = Random.Range(5, 10); //Set the amount of frames to keep the wind stopped

            Debug.Log(windIsBlowing);
            Debug.Log(windStoppedFrames);
        }
        else
        {
            //Add to timer, check if need to clear
            windStoppedTimer++;

            //float xDegreesPerFrame = (((360 * xRotationsPerMinute) / 60) * Time.deltaTime) * (.5f * windStoppedTimer);
            //transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);

            //float yDegreesPerFrame = (((360 * yRotationsPerMinute) / 60) * Time.deltaTime) * (.5f * windStoppedTimer);
            //transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);


            //float zDegreesPerFrame = (((360 * zRotationsPerMinute) / 60) * Time.deltaTime) * (.5f * windStoppedTimer);
            //transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);

            if (windStoppedTimer >= windStoppedFrames)
            {
                windIsBlowing = true;
                windStoppedFrames = 0;
                windStoppedTimer = 0;
            }

            //Debug.Log(zDegreesPerFrame);
        }
    }
}
