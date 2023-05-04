using System;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public Snake snake;
    private SnakeAIState currentState;

    public SnakeAIIdleState idleState { get; private set; }
    public SnakeAIChaseState chaseState { get; private set; }

    public PlayerController player;


    private void Start()
    {
        idleState = new SnakeAIIdleState(this);
        chaseState = new SnakeAIChaseState(this);
        
        currentState = idleState;
    }

    private void FixedUpdate()
    {
        currentState.UpdateState();
    }

    public void ChangeState(SnakeAIState newState)
    {
        currentState = newState;
        currentState.BeginStateBase();
    }
    
    public PlayerController GetTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            return player;
        }
        else
        {
            return null;
        }
    }
}
