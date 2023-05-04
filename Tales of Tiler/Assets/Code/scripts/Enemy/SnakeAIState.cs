using UnityEngine;

public abstract class SnakeAIState
{
    protected SnakeAI snakeAI;
    private float _timer = 0;

    public SnakeAIState(SnakeAI newAI)
    {
        snakeAI = newAI;
    }

    public void UpdateStateBase()
    {
        _timer += Time.fixedDeltaTime;
        UpdateState();
    }

    public void BeginStateBase()
    {
        _timer = 0;
        BeginState();
    }

    public abstract void UpdateState();
    public abstract void BeginState();
}
