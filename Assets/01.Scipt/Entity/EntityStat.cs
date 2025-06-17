﻿using System.Collections.Generic;
using System.Linq;
using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Core.StatSystem
{
    public class EntityStat : MonoBehaviour, IEntityComponet
    {
        [SerializeField] private StatOverride[] statOverrides;
        //private StatSO[] _stats; //진짜 스탯들
        private Dictionary<string, StatSO> _stats;
        public Entity Owner { get; private set; } //밖에서 참조 가능하게
        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stats = statOverrides.ToDictionary(s => s.Stat.statName, s=>s.CreateStat());
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, $"Stat: GetStat - {stat.statName} can not be null");
            return _stats.GetValueOrDefault(stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, $"Stats: TryGetStat - stat cannot be null");
            outStat = _stats.GetValueOrDefault(stat.statName);
            return outStat != null;
        }

        public void SetBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue = value;
        public float GetBaseValue(StatSO stat) => GetStat(stat).BaseValue;
        public void IncreaseBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue += value;

        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddModifier(key, value);

        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllStatModifier()
        {
            foreach (StatSO stat in _stats.Values)
            {
                stat.ClearModifier();
            }
        }
        
    }
}