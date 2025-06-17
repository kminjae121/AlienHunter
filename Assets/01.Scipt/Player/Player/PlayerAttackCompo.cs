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
    public AttackDataSO[] attackDataList;

    [SerializeField] private float comboWindow;

    [SerializeField] private LayerMask _whatIsEnemy;

    

    [SerializeField] private Vector3 _boxsize;

    [field: SerializeField] public float MaxHoldTime { get; set; }
    [field: SerializeField] public bool IsAttack { get; set; }

    public bool useMouseDirection;

    private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");
    private readonly int _comboCounterHash = Animator.StringToHash("COMBO_COUNTER");

    private float _attackSpeed = 0.3f;

    private Coroutine _chargeRoutine;
    private Entity _entity;
    private EntityAnimator _entityAnimator;
    private float _lastAttackTime;
    private Player _player;

    private EntityAnimatorTrigger _triggerCompo;
    
    private EntityVFX _vfxCompo;
    private float attackHoldTime;

    public int ComboCounter { get; set; }
    

    [Space(10f)]
    [Header("AttackStat")]
    [SerializeField] private StatSO _atkDamageSO;
    
    private EntityStat _statCompo;
    public float atkDamage { get; set; }
    
    public float baseAtkDamage { get; private set; }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
        }
    }


    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = entity as Player;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        _statCompo = entity.GetCompo<EntityStat>();
        
        _vfxCompo = entity.GetCompo<EntityVFX>();
        AttackSpeed = 1.6f;
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
    
    
    public void Attack()
    {
        var comboCounterOver = ComboCounter > 2;
        var comboWindowExhaust = Time.time > _lastAttackTime + comboWindow;
        if (comboCounterOver || comboWindowExhaust) ComboCounter = 0;
        _entityAnimator.SetParam(_comboCounterHash, ComboCounter);
    }


    public void EndAttack()
    {
        ComboCounter++;
        IsAttack = false;
        _player._isSkilling = false;
        _lastAttackTime = Time.time;
    }

    private void AttackCancel()
    {
        IsAttack = true;
    }
    


    public AttackDataSO GetCurrentAttackData()
    {
        Debug.Assert(attackDataList.Length > ComboCounter, "Combo counter is out of range");
        return attackDataList[ComboCounter];
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _boxsize);
        Gizmos.color = Color.white;
    }
    
}
