using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveProvider
{
    bool TryGetMoveCommand(out MoveCommand command);
}
