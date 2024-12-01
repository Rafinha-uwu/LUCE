using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static SwitchWithRequirements;

public class Code : MonoBehaviour
{
    Elevator elevator;
    public GameObject el;

    public GameObject UP1;
    public GameObject UP2;
    public GameObject UP3;
    public GameObject UP4;

    public GameObject DOWN1;
    public GameObject DOWN2;
    public GameObject DOWN3;
    public GameObject DOWN4;

    public bool Lev1;
    public bool Lev2;
    public bool Lev3;
    public bool Lev4;

    public bool On = false;

    [SerializeField] private SwitchWithRequirements switchWithRequirements;

    // Start is called before the first frame update
    void Start()
    {
        elevator = el.GetComponent<Elevator>();
        Randomizer();
    }

    // Update is called once per frame
    void Update()
    {
        if (On)
        {
            elevator.On = true;
        }

        UpdateLevelSprites(Lev1, UP1, DOWN1, 0);
        UpdateLevelSprites(Lev2, UP2, DOWN2, 1);
        UpdateLevelSprites(Lev3, UP3, DOWN3, 2);
        UpdateLevelSprites(Lev4, UP4, DOWN4, 3);
    }

    void UpdateLevelSprites(bool level, GameObject upSprite, GameObject downSprite, int index)
    {
        if (level)
        {
            downSprite.GetComponent<SpriteRenderer>().enabled = false;
            upSprite.GetComponent<SpriteRenderer>().enabled = true;
            switchWithRequirements.SetRequiredState(index, true);
        }
        else
        {
            upSprite.GetComponent<SpriteRenderer>().enabled = false;
            downSprite.GetComponent<SpriteRenderer>().enabled = true;
            switchWithRequirements.SetRequiredState(index, false);
        }
    }


    public void Randomizer()
    {
        Lev1 = UnityEngine.Random.Range(0, 2) == 0;
        Lev2 = UnityEngine.Random.Range(0, 2) == 0;
        Lev3 = UnityEngine.Random.Range(0, 2) == 0;
        Lev4 = UnityEngine.Random.Range(0, 2) == 0;

        if (!Lev1 && !Lev2 && !Lev3 && !Lev4)
        {
            Lev3 = true;
        }

    }
}
