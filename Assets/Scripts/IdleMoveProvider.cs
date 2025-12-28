using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMoveProvider : MonoBehaviour, IMoveProvider
{
    //idle is always active
    public bool TryGetMoveCommand(out MoveCommand command) {
        command = MoveCommand.Idle;
        return true;
    }
}
