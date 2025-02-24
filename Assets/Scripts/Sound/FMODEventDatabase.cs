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

    // Cutscenes
    [field: Header("Cutscenes")]
    [field: SerializeField] public EventReference StartGameCutscene { get; private set; }
    [field: SerializeField] public EventReference EndGameCutscene { get; private set; }
    [field: SerializeField] public EventReference AllPolaroidsCutscene { get; private set; }
    // ...

    // Player
    [field: Header("Player")]
    [field: SerializeField] public EventReference PlayerMove { get; private set; }
    [field: SerializeField] public EventReference PlayerJump { get; private set; }
    [field: SerializeField] public EventReference PlayerGroundHit { get; private set; }
    [field: SerializeField] public EventReference PlayerDeath { get; private set; }
    [field: SerializeField] public EventReference PlayerScared { get; private set; }

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
    [field: SerializeField] public EventReference ElevatorShake { get; private set; }
    [field: SerializeField] public EventReference Door { get; private set; }
    [field: SerializeField] public EventReference FirstKeyHole { get; private set; }
    [field: SerializeField] public EventReference LastKeyHole { get; private set; }

    // Lights
    [field: Header("Lights")]
    [field: SerializeField] public EventReference Light { get; private set; }
    [field: SerializeField] public EventReference MovingLight { get; private set; }
    [field: SerializeField] public EventReference PuzzleEyeLight { get; private set; }

    // Holdables
    [field: Header("Holdables")]
    [field: SerializeField] public EventReference TeddyPickup { get; private set; }
    [field: SerializeField] public EventReference KeyPickup { get; private set; }
    [field: SerializeField] public EventReference KeyGroundHit { get; private set; }
    [field: SerializeField] public EventReference KeyUnlock { get; private set; }
    [field: SerializeField] public EventReference CameraPickup { get; private set; }
    [field: SerializeField] public EventReference CameraFlash { get; private set; }
    // ...

    // Polaroids
    [field: Header("Polaroids")]
    [field: SerializeField] public EventReference PolaroidPickup { get; private set; }
    [field: SerializeField] public EventReference Narrative1Pickup { get; private set; }
    [field: SerializeField] public EventReference Narrative2Pickup { get; private set; }
    [field: SerializeField] public EventReference Narrative3Pickup { get; private set; }
    [field: SerializeField] public EventReference Narrative4Pickup { get; private set; }
    // ...

    // Other
    [field: Header("Other")]
    [field: SerializeField] public EventReference Hand { get; private set; }
    [field: SerializeField] public EventReference ScreenShake { get; private set; }
    // ...
}
