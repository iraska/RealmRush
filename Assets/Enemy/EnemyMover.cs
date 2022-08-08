using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        // Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
        // InvokeRepeating("FollowPath", 0, 1f);

        ReturnTostart();
        RecalculatePath(true);
    }

    void Awake() 
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }

        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);   //current transform.position
        }

        StopAllCoroutines();
        // This method removes all elements from the list
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        // A coroutine is a method that you declare with an IEnumerator return type and with a yield return statement included somewhere in the body. The yield return nullline is the point where execution pauses and resumes in the following frame. To set a coroutine running, you need to use the StartCoroutine function (we need to call our coroutine)
        StartCoroutine(FollowPath());
    }
    
    void ReturnTostart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    // IEnumerator: Supports a simple iteration over a non-generic collection.
    // returning smth countable that the system can use
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)    // if int i = 0: when we get a new path, we're heading back to the very first node in our current path (slow)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
                // Waits until the end of the frame after Unity has rendererd every Camera and GUI, just before displaying the frame on screen.
                yield return new WaitForEndOfFrame();
            }
            // You use a yield return statement to return each element one at a time.
            // If we didn't want to return anything at all, we could return null but we want to delay
            // give up control and than come back to me in 1 second.
            // yield return new WaitForSeconds(waitTime);
        }

        FinishPath();
    }
}
