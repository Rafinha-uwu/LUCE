using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour
{
    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";

    public GameObject HelpDoor;

    // Start is called before the first frame update
    void Start()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnInteractAction()
    {
        HelpDoor.SetActive(false);
    }
}
