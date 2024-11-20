using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIndex : MonoBehaviour
{

    // Reference to the other GameObject
    public GameObject otherObject;
    public float verticalThreshold;
    void Update()
    {
        if (otherObject != null)
        {
            DetectPosition();
        }
    }

    void DetectPosition()
    {
        // Get the positions of the current GameObject and the other GameObject
        Vector3 thisPosition = transform.position;
        Vector3 otherPosition = otherObject.transform.position;

        // Threshold to determine if the other object is "on top" (can be adjusted based on object sizes)
        verticalThreshold = 0.1f;

        // Check if the other object is vertically aligned and within the threshold for being "on top"
        if (Mathf.Abs(otherPosition.x - thisPosition.x) < verticalThreshold &&
            otherPosition.y > thisPosition.y)
        {
            Debug.Log($"{otherObject.name} is ON TOP of {gameObject.name}");
            this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }
        // Check if the other object is to the right
        else if (otherPosition.x > thisPosition.x)
        {
            Debug.Log($"{otherObject.name} is to the RIGHT of {gameObject.name}");
            this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
        }
        // Check if the other object is to the left
        else if (otherPosition.x < thisPosition.x)
        {
            Debug.Log($"{otherObject.name} is to the LEFT of {gameObject.name}");
            this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
