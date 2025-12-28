using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchMoveProvider : MonoBehaviour, IMoveProvider
{
    [SerializeField] private PlayerInputReader input;

    public bool TryGetMoveCommand(out MoveCommand command)
    {
        var snap = input.Snapshot;

        //active only if shift if pressed and player no movement is inputted
        if (snap.ShiftHeld && snap.Move == Vector2.zero)
        {
            command = new MoveCommand(Vector2.zero, true);
            return true;
        }

        command = default;
        return false;
    }
}
