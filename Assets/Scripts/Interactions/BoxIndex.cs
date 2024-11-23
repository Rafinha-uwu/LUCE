using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIndex : MonoBehaviour
{

    // Reference to the other GameObject
    private GameObject otherObject;
    public float verticalThreshold;

    private void Start()
    {

        otherObject = GameObject.FindGameObjectWithTag("Player");

    }


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



        // Check if the other object is vertically aligned and within the threshold for being "on top"
        if (otherPosition.y > thisPosition.y + verticalThreshold)
        {

            otherObject.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
        // Check if the other object is to the right
        else if (otherPosition.x > thisPosition.x)
        {
            otherObject.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
        // Check if the other object is to the left
        else if (otherPosition.x < thisPosition.x)
        {
            otherObject.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
    }
}
