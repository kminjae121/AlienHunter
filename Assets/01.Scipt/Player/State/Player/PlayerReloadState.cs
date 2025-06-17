using Member.Kmj._01.Scipt.Entity.AttackCompo;

public class PlayerReloadState : PlayerMoveState
{
    public PlayerReloadState(Entity entity, int animationHash) : base(entity, animationHash)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}