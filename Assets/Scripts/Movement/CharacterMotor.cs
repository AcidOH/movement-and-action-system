using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 desiredMove;

    // State for animation/other systems
    public Vector2 DesiredMove => desiredMove;
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down; // default if you want
    public Vector2 Velocity => rb != null ? rb.velocity : Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMoveIntent(Vector2 move)
    {
        if (move.sqrMagnitude > 1f) move.Normalize();

        if (move == Vector2.zero && desiredMove != Vector2.zero)
            LastMoveDirection = desiredMove;

        desiredMove = move;
    }

    private void FixedUpdate()
    {
        rb.velocity = desiredMove * speed;
    }
}