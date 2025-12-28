using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputSnapshot
{
    public Vector2 Move;
    public bool ShiftHeld;

    public PlayerInputSnapshot(Vector2 move, bool shiftHeld) {
        Move = move;
        ShiftHeld = shiftHeld;
    }
}

