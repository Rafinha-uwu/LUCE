using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "FMODEventDatabase", menuName = "Sound/FMODEventDatabase")]
public class FMODEventDatabase : ScriptableObject
{
    [field: SerializeField] public EventReference EventForTesting { get; private set; }

    // Game + Start Menu
    [field: Header("Game + Start Menu")]
    [field: SerializeField] public EventReference GameBGM { get; private set; }
    [field: SerializeField] public EventReference GameAmbience { get; private set; }
    [field: SerializeField] public EventReference StartMenuBGM { get; private set; }

    // Player
    [field: Header("Player")]
    [field: SerializeField] public EventReference PlayerMove { get; private set; }
    [field: SerializeField] public EventReference PlayerJump { get; private set; }
    [field: SerializeField] public EventReference PlayerGroundHit { get; private set; }
    [field: SerializeField] public EventReference PlayerDeath { get; private set; }

    // Objects
    [field: Header("Objects")]
    [field: SerializeField] public EventReference ObjectMove { get; private set; }
    [field: SerializeField] public EventReference ObjectGroundHit { get; private set; }

    // Interactions
    [field: Header("Interactions")]
    [field: SerializeField] public EventReference Lever { get; private set; }
    [field: SerializeField] public EventReference Button { get; private set; }
    [field: SerializeField] public EventReference PressurePlate { get; private set; }
    [field: SerializeField] public EventReference Elevator { get; private set; }
    [field: SerializeField] public EventReference Door { get; private set; }

    // Lights
    [field: Header("Lights")]
    [field: SerializeField] public EventReference Light { get; private set; }
    [field: SerializeField] public EventReference MovingLight { get; private set; }
}
