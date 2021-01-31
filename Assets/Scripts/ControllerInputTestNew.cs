using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ControllerInputTestNew : MonoBehaviour
{
    public TextMeshProUGUI buttonPressText;
    public TextMeshProUGUI moveAxisText;
    public TextMeshProUGUI lookAxisText;
    public TextMeshProUGUI controllerText;

    public InputMaster controls;

    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Move.Enable();
        controls.Player.Look.Enable();
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
        controls.Player.WestButton.performed += _ => WestButtonPressed();
        controls.Player.EastButton.performed += _ => EastButtonPressed();
        controls.Player.NorthButton.performed += _ => NorthButtonPressed();
        controls.Player.SouthButton.performed += _ => SouthButtonPressed();
        controls.Player.LeftBumper.performed += _ => LeftBumperPressed();
        controls.Player.RightBumper.performed += _ => RightBumperPressed();
        controls.Player.LeftTrigger.performed += _ => LeftTriggerPressed();
        controls.Player.RightTrigger.performed += _ => RightTriggerPressed();
        controls.Player.Confirm.performed += _ => SouthButtonPressed();
        controls.Player.Start.performed += _ => StartPressed();
        controls.Player.Back.performed += _ => EastButtonPressed();
        controls.Player.LeftStick.performed += _ => LeftStickPressed();
        controls.Player.RightStick.performed += _ => RightStickPressed();
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    public void WestButtonPressed()
    {
        Debug.Log("West button pressed");
        buttonPressText.SetText("X(Xbox)/SQUARE(Playstation)");
    }

    public void EastButtonPressed()
    {
        Debug.Log("Back/East button pressed");
        buttonPressText.SetText("Back/B(Xbox)/CIRCLE(Playstation)");        
    }

    void NorthButtonPressed()
    {
        Debug.Log("North button pressed");
        buttonPressText.SetText("Y(Xbox)/TRIANGLE(Playstation)");
    }

    void SouthButtonPressed()
    {
        Debug.Log("Confirm/South button pressed");
        buttonPressText.SetText("Confirm/A(Xbox)/X(Playstation)");
    }

    void LeftBumperPressed()
    {
        Debug.Log("Left Bumper pressed");
        buttonPressText.SetText("LB(Xbox)/L1(Playstation)");
    }

    void RightBumperPressed()
    {
        Debug.Log("Right Bumper pressed");
        buttonPressText.SetText("RB(Xbox)/R1(Playstation)");
    }

    void LeftTriggerPressed()
    {
        Debug.Log("Left Trigger pressed");
        buttonPressText.SetText("LT(Xbox)/L2(Playstation)");
    }

    void RightTriggerPressed()
    {
        Debug.Log("Right Trigger pressed");
        buttonPressText.SetText("RT(Xbox)/R2(Playstation)");
    }

    void StartPressed()
    {
        Debug.Log("Start pressed");
        buttonPressText.SetText("Start");
    }

    void LeftStickPressed()
    {
        Debug.Log("Left Stick pressed");
        buttonPressText.SetText("LS(Xbox)/L3(Playstation)");
    }

    void RightStickPressed()
    {
        Debug.Log("Right Stick pressed");
        buttonPressText.SetText("RS(Xbox)/R3(Playstation)");
    }

    void Move(Vector2 movement)
    {
        Debug.Log(movement);

        if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x)){
            moveAxisText.SetText("Moving forward/back");
        }
        
        if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y)){
            moveAxisText.SetText("Moving left/right");
        }
    }

    void Look(Vector2 looking)
    {
        Debug.Log(looking);

        if(Mathf.Abs(looking.y) > Mathf.Abs(looking.x)){
            lookAxisText.SetText("Looking forward/back");
        }
        
        if(Mathf.Abs(looking.x) > Mathf.Abs(looking.y)){
            lookAxisText.SetText("Looking left/right");
        }
    }
}
