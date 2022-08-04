using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    
    [SerializeField] bool isPlaceable; //flag
    public bool IsPlaceable { get { return isPlaceable; } } // making more visible

    // OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    void OnMouseDown() 
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            // This function makes a copy of an object in a similar way to the Duplicate command in the editor.
            // Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = !isPlaced; // we don't wanna more than one tower on sama tile
        }
        
    }
}
