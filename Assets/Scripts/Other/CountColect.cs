using UnityEngine;
using TMPro;

public class CountColect : MonoBehaviour
{

    public float nColect = 0;

    private TextMeshProUGUI tmpText;

    // Start is called before the first frame update
    private void Start()
    {
        tmpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
       tmpText.text = nColect + "/6";
    }
}
