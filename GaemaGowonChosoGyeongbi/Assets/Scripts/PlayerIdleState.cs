using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) 
    : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);
    }
    public override void Update()
    {
        base.Update();

        if(xInput == player.facingDir && player.IsWallDetected())
        {
            return;
        }


        if (xInput != 0)
        {
            player.stateMachine.ChangeState(player.moveState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
