using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWalkMoveProvider : MonoBehaviour, IMoveProvider
{
    [SerializeField] private PlayerInputReader input;

    public bool TryGetMoveCommand(out MoveCommand command)
    {
        Vector2 move = input.Snapshot.Move;

        //inactive if no movement inputted
        if (move == Vector2.zero) {
            command = default;
            return false;
        }

        command = new MoveCommand(move, false);
        return true;
    }
}
