using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private IChildTriggerParent parentScript;

    private void Start()
    {
        // Get the parent's script
        bool hasParentScript = transform.parent.TryGetComponent(out parentScript);
        if (!hasParentScript) Debug.LogError("Parent script not found!");
    }

    private void OnTriggerEnter2D(Collider2D collision) => parentScript?.OnChildTriggerEnter(collision.gameObject);
}