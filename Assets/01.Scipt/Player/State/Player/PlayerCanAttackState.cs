using System.Collections.Generic;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public abstract class PlayerCanAttackState : PlayerState
{
    public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.OnAttackPressd += HandleAttackKeyPressed;
        _player.PlayerInput.OnChargeAttackCanceled += HandleAttackPressed;
    }

    public override void Exit()
    {
        _player.PlayerInput.OnChargeAttackCanceled -= HandleAttackPressed;
        _player.PlayerInput.OnAttackPressd -= HandleAttackKeyPressed;
        base.Exit();
    }

    private void HandleAttackPressed()
    {
        if (!_player._isSkilling && !_player.isUsePowerAttack)
        {
            _player._movement.CanMove = false;
            _player._movement.StopImmediately();
            _player.ChangeState("IDLE");
        }
    }

    private void HandleAttackKeyPressed()
    {
        if (_player._attackCompo.IsAttack == false && _player._isSkilling == false)
        {
            _player.ChangeState("ATTACK");
        }
    }
}
