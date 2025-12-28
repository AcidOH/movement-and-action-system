using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class KeyboardMoveInput : MonoBehaviour
{
    private CharacterMotor motor;

    private void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(moveX, moveY);
        motor.SetMoveIntent(move);
    }
}