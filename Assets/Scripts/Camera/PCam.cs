using UnityEngine;
using UnityEngine.UIElements;

public class ParentTagDetector : MonoBehaviour
{
    private Hand hand;
    public GameObject mao;

    private bool once = true;


    void Start()
    {

        hand = mao.GetComponent<Hand>();


    }

    private void Update()
    {

        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {

            if (parentTransform.name == "HoldPosition")
            {
                if (once)
                {
                    hand.start = true;
                    once = false;
                }
                
            }

        }

    }
}