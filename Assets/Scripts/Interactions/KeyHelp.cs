using UnityEngine;

public class KeyHelp : MonoBehaviour
{
    public GameObject HelpDoor;
    public GameObject HelpKey;

    [SerializeField] private Key _key;

    public bool keyreal;


    private void Update()
    {
        if (_key.gameObject.activeSelf) UpdateHelp(_key.IsBeingHeld);
        else if (HelpDoor != null)
        {
            keyreal = false;
            Kill();
        }
    }


    private void UpdateHelp(bool keyInside)
    {
        if (!keyreal) return;
        HelpKey.SetActive(!keyInside);
        HelpDoor.SetActive(keyInside);
    }

    public void Kill()
    {
        Destroy(HelpKey);
        Destroy(HelpDoor);
    }
}
