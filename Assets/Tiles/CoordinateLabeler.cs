using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Makes instances of a script always execute, both as part of Play Mode and when editing.
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.black;

    TextMeshPro label;
    // We're in a 3D word, (x, z) should be an integer 
    Vector2Int coordinates = new Vector2Int();
    WayPoint wayPoint;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        wayPoint = GetComponentInParent<WayPoint>();
        DisplayCoordinates();
    }

    void Update()
    {
        // This script is only happening on the edit mode
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        // Turn the label on/off
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void ColorCoordinates()
    {
        if (wayPoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        // This script is on a child our Tile object
        // float to int
        // Grid's widh-height are 10 (x, z)
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        // This is actually going to be the z coordinate of our object, cause we working in the 2d (x, z) plane.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }
    
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
