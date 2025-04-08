using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if ((xInput != 0 && player.facingDir != xInput) || player.IsGroundDetected()) 
        {
                stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f/*slide speed*/);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // player.Flip();
            // player.SetVelocity(player.facingDir * player.moveSpeed, rb.linearVelocity.y * player.jumpForce);
            // stateMachine.ChangeState(player.jumpState);
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
