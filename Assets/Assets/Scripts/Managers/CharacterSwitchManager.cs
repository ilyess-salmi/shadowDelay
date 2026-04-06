using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitchManager : MonoBehaviour
{
    public CharacterMovement playerController;
    public CharacterMovement shadowController;

    private bool controllingPlayer = true;

    void Start()
    {
        SetControl(true);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SetControl(!controllingPlayer);
        }
    }

    void SetControl(bool controlPlayer)
    {
        controllingPlayer = controlPlayer;

        if (playerController == null || shadowController == null)
            return;

        if (controlPlayer)
        {
            playerController.enabled = true;
            playerController.UnfreezeCharacter();

            shadowController.enabled = false;
            shadowController.FreezeCharacter();
        }
        else
        {
            shadowController.enabled = true;
            shadowController.UnfreezeCharacter();

            playerController.enabled = false;
            playerController.FreezeCharacter();
        }
    }
}