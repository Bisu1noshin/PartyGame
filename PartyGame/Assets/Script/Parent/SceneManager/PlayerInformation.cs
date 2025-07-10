using UnityEngine;
using UnityEngine.InputSystem;
public enum CharacterType
{
    Character1,
    Character2,
    Character3,
    Character4,
}
public class PlayerInformation
{
    public InputDevice PairWithDevice { get; private set; } = default;
    public CharacterType SelectedCharacter { get; private set; } = default;

    public PlayerInformation(InputDevice pairWithDevice, CharacterType selectedCharacter)
    {
        PairWithDevice = pairWithDevice;
        SelectedCharacter = selectedCharacter;
    }
}
