using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterMotor))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator anim;
    private CharacterMotor motor;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        Vector2 move = motor.DesiredMove;
        Vector2 last = motor.LastMoveDirection;

        anim.SetFloat("MoveX", move.x);
        anim.SetFloat("MoveY", move.y);
        anim.SetFloat("MoveMagnitude", move.magnitude);
        anim.SetFloat("LastMoveX", last.x);
        anim.SetFloat("LastMoveY", last.y);
    }
}