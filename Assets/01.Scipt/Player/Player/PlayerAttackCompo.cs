using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using _01.Scipt.Player.Player;
using Blade.Combat;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponet, IAfterInit
{
    [SerializeField] private LayerMask _whatIsEnemy;

    public bool useMouseDirection;
    
    private Entity _entity;
    private EntityAnimator _entityAnimator;
    private Player _player;

    private EntityAnimatorTrigger _triggerCompo;
    

    [Space(10f)]
    [Header("AttackStat")]
    [SerializeField] private StatSO _atkDamageSO;
    
    private EntityStat _statCompo;
    public float atkDamage { get; set; }
    
    public float baseAtkDamage { get; private set; }


    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = entity as Player;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        
        _statCompo = entity.GetCompo<EntityStat>();
        _triggerCompo = entity.GetCompo<EntityAnimatorTrigger>();
    }


    private void OnDisable()
    {
        StatSO targetStat = _statCompo.GetStat(_atkDamageSO);
        Debug.Assert(targetStat != null, $"{_atkDamageSO.statName} stat could not be found");
        targetStat.OnValudeChanged -= HandleAttackStatChange;
    }
    
   
    private void HandleAttackStatChange(StatSO stat, float currentValue, float previousValue)
    {
        atkDamage += currentValue - previousValue;
        baseAtkDamage += currentValue - previousValue;
    }
    
   
    public void AfterInit()
    {
        StatSO targetStat = _statCompo.GetStat(_atkDamageSO);
        Debug.Assert(targetStat != null, $"{_atkDamageSO.statName} stat could not be found");
        targetStat.OnValudeChanged += HandleAttackStatChange;
        atkDamage = targetStat.BaseValue;
        baseAtkDamage = targetStat.BaseValue;
    }
    


    
}
