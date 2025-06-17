using System;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponet
{
    
    public Action OnAnimationEndTrigger;
    public Action OnAttackTrigger;

    public Action OnAttackTriggerEnd, OnSwingAttackTrigger;
    

    public event Action OnMove;

    public event Action OnRollStart;
    public event Action OnRollingStopping;

    public event Action<bool> OnManualRotationTrigger;

    private Entity _entity;
    

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void AnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }
    
    private void Move()=> OnMove?.Invoke();
    
    private void RollingStart() => OnRollStart?.Invoke();
    private void RollingEnd() => OnRollingStopping?.Invoke();
    
    private void AttackEnd() => OnAttackTriggerEnd?.Invoke();
    
    private void SwingAttack() => OnSwingAttackTrigger?.Invoke();

    private void Attack()
    {
        OnAttackTrigger?.Invoke();
    }

    private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
    private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);
}
