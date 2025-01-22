using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{

    private PuzzleEye puzzle;
    public GameObject pp;


    // Start is called before the first frame update
    void Start()
    {
        puzzle = pp.GetComponent<PuzzleEye>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            puzzle.Phase = 1f;
            Invoke("Now",0.5f);
            
            
        }
    }

    public void Now()
    {
        Destroy(this.gameObject);
    }
}
