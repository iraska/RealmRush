using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    
    void Start()
    {
        // Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
        // InvokeRepeating("FollowPath", 0, 1f);

        // A coroutine is a method that you declare with an IEnumerator return type and with a yield return statement included somewhere in the body. The yield return nullline is the point where execution pauses and resumes in the following frame. To set a coroutine running, you need to use the StartCoroutine function (we need to call our coroutine)
        StartCoroutine(FollowPath());
    }

    // IEnumerator: Supports a simple iteration over a non-generic collection.
    // returning smth countable that the system can use
    IEnumerator FollowPath()
    {
        foreach (WayPoint wayPoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = wayPoint.transform.position;
            // between 0 and 1
            float travelPercent = 0f;

            // Always going to be facing the waypoint that we are heading towards
            transform.LookAt(endPosition);

            // so while we are not at our end position (1f)
            while (travelPercent < 1f)
            {
                // LERP: Linear Interpolation --> Enemy is jumping to tile, we want smth that's a bit smoother and moves between the tiles over conseculative frames
                // We ll use Vector3.LERP(startPosition, endPosition, travelPercent)
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            // transform.position = wayPoint.transform.position;
            // You use a yield return statement to return each element one at a time.
            // If we didn't want to return anything at all, we could return null but we want to delay
            // give up control and than come back to me in 1 second.
            // yield return new WaitForSeconds(waitTime);
        }
    }
}
