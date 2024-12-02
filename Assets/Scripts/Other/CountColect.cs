using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class CountColect : MonoBehaviour
{

    public float nColect = 0;

    private TextMeshProUGUI tmpText;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

       tmpText.text = nColect + "/4";

    }
}
