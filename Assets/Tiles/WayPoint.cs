using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble; //flag
    [SerializeField] GameObject towerPrefab;
 
    // OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    void OnMouseDown() 
    {
        if (isPlaceble)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceble = false; // we don't wanna more than one tower on sama tile
        }
        
    }
}
