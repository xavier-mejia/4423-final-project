using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAIChaseState : SnakeAIState
{
    public SnakeAIChaseState(SnakeAI newAI) : base(newAI)
    {
    }

    public override void UpdateState()
    {
        if (snakeAI.GetTarget() != null)
        {

        }
    }

    public override void BeginState()
    {
        throw new System.NotImplementedException();
    }
}
