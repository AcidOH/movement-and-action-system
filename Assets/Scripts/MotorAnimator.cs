using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Motor))]
public class MotorAnimator : MonoBehaviour
{
    [Header("Animator Params")]
    [SerializeField] private string moveXParam = "MoveX";
    [SerializeField] private string moveYParam = "MoveY";
    [SerializeField] private string moveMagnitudeParam = "MoveMagnitude";
    [SerializeField] private string lastMoveXParam = "LastMoveX";
    [SerializeField] private string lastMoveYParam = "LastMoveY";
    [SerializeField] private string isSketchingParam = "IsSketching";

    private Animator anim;
    private Motor motor;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<Motor>();
    }

    private void Update()
    {
        Vector2 move = motor.DesiredMove;
        Vector2 last = motor.LastMoveDirection;

        anim.SetBool(isSketchingParam, motor.IsSketching);

        // If sketching, we want a still pose, not idle/walk blending.
        float magnitude = motor.IsSketching ? 0f : move.magnitude;

        anim.SetFloat(moveXParam, move.x);
        anim.SetFloat(moveYParam, move.y);
        anim.SetFloat(moveMagnitudeParam, magnitude);
        anim.SetFloat(lastMoveXParam, last.x);
        anim.SetFloat(lastMoveYParam, last.y);
    }
}