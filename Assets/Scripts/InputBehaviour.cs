
using UnityEngine;


public class InputBehaviour : MonoBehaviour
{
    public InputMaster controls;

    protected virtual void Awake() {
        controls = new InputMaster();
        controls.Player.Move.Enable();
        controls.Player.Look.Enable();
        controls.Player.Fire.Enable();
        controls.Player.WestButton.Enable();
        controls.Player.EastButton.Enable();
        controls.Player.NorthButton.Enable();
        controls.Player.SouthButton.Enable();
        controls.Player.LeftBumper.Enable();
        controls.Player.RightBumper.Enable();
        controls.Player.LeftTrigger.Enable();
        controls.Player.RightTrigger.Enable();
        controls.Player.Confirm.Enable();
        controls.Player.Start.Enable();
        controls.Player.Back.Enable();
        controls.Player.LeftStick.Enable();
        controls.Player.RightStick.Enable();
        controls.Player.Fire.performed += _ => FirePressed();
        controls.Player.WestButton.performed += _ => WestButtonPressed();
        controls.Player.EastButton.performed += _ => EastButtonPressed();
        controls.Player.NorthButton.performed += _ => NorthButtonPressed();
        controls.Player.SouthButton.performed += _ => SouthButtonPressed();
        controls.Player.LeftBumper.performed += _ => LeftBumperPressed();
        controls.Player.LeftBumper.canceled += _ => LeftBumperReleased();
        controls.Player.RightBumper.performed += _ => RightBumperPressed();
        controls.Player.RightBumper.canceled += _ => RightBumperReleased();
        controls.Player.LeftTrigger.performed += _ => LeftTriggerPressed();
        controls.Player.RightTrigger.performed += _ => RightTriggerPressed();
        // controls.Player.Confirm.performed += _ => SouthButtonPressed();
        controls.Player.Start.performed += _ => StartPressed();
        controls.Player.Back.performed += _ => BackPressed();
        controls.Player.LeftStick.performed += _ => LeftStickPressed();
        controls.Player.RightStick.performed += _ => RightStickPressed();
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => Move(Vector2.zero);
        controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        controls.Player.Look.canceled += ctx => Look(Vector2.zero);
    }

    protected virtual void FirePressed()
    {
        Debug.Log("Fire pressed");
    }

    protected virtual void WestButtonPressed()
    {
        Debug.Log("West button pressed");
    }

    protected virtual void EastButtonPressed()
    {
        Debug.Log("Back/East button pressed");
    }

    protected virtual void NorthButtonPressed()
    {
        Debug.Log("North button pressed");
    }

    protected virtual void SouthButtonPressed()
    {
        Debug.Log("Confirm/South button pressed");
    }

    protected virtual void LeftBumperPressed()
    {
        Debug.Log("Left Bumper pressed");
    }

    protected virtual void RightBumperPressed()
    {
        Debug.Log("Right Bumper pressed");
    }

    protected virtual void RightBumperReleased() {
        Debug.Log("Right bumper released");
    }

    protected virtual void LeftTriggerPressed()
    {
        Debug.Log("Left Trigger pressed");
    }

    protected virtual void LeftBumperReleased() {
        Debug.Log("Left bumper released");
    }

    protected virtual void RightTriggerPressed()
    {
        Debug.Log("Right Trigger pressed");
    }

    protected virtual void StartPressed()
    {
        Debug.Log("Start pressed");
    }

    private void BackPressed()
    {
        Debug.Log("Back pressed");
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    protected virtual void LeftStickPressed()
    {
        Debug.Log("Left Stick pressed");
    }

    protected virtual void RightStickPressed()
    {
        Debug.Log("Right Stick pressed");
    }

    protected virtual void Move(Vector2 movement)
    {
        Debug.Log(movement);
    }

    protected virtual void Look(Vector2 looking)
    {
        Debug.Log(looking);
    /*
        if(Mathf.Abs(looking.y) > Mathf.Abs(looking.x)){
            lookAxisText.SetText("Looking forward/back");
        }
        
        if(Mathf.Abs(looking.x) > Mathf.Abs(looking.y)){
            lookAxisText.SetText("Looking left/right");
        }
    */
    }
}
