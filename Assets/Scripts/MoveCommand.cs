using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MoveCommand
{
    public Vector2 Move;
    public bool IsSketching;

    public MoveCommand(Vector2 move, bool isSketching)
    {
        Move = move;
        IsSketching = isSketching;
    }

    public static MoveCommand Idle => new MoveCommand(Vector2.zero, false);
}