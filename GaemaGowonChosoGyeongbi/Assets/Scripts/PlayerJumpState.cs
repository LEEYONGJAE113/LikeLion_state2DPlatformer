using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.linearVelocityY);
        }

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        
        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
