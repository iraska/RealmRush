using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] float waitTime = 1f;
    
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
            transform.position = wayPoint.transform.position;
            // You use a yield return statement to return each element one at a time.
            // If we didn't want to return anything at all, we could return null but we want to delay
            // give up control and than come back to me in 1 second.
            yield return new WaitForSeconds(waitTime);
        }
    }
}
