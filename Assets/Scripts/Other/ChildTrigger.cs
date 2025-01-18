using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private Hand parentScript;

    void Start()
    {
        // Get the parent's script
        parentScript = transform.parent.GetComponent<Hand>();
        if (parentScript == null)
        {
            Debug.LogError("Parent script not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentScript != null)
        {
            parentScript.OnChildTriggerEnter(collision.gameObject);
        }
    }
}