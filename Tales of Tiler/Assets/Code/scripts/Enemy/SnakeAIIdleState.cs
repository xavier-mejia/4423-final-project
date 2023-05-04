using UnityEngine;

public class SnakeAIIdleState : SnakeAIState
{
    private PlayerController _player;
    
    public SnakeAIIdleState(SnakeAI snakeAI) : base(snakeAI)
    {

    }

    public override void UpdateState()
    {
        if (snakeAI.GetTarget() != null)
        {
            snakeAI.ChangeState(snakeAI.chaseState);
        }
    }

    public override void BeginState()
    {

    }
}
