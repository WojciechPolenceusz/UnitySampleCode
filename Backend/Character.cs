using System;
using UnityEngine;

namespace Backend
{
    public class Character : ICharacter
    {
        public event Action<int> OnChangeArmor;
        public event Action<int, int> OnChangeHp;
        public event Action OnDeath;

        private int _instanceId;

        public CharacterStatistics Statistics { get; private set; }

        private bool CanBeHealed => Statistics.IsAlive && Statistics.IsDamaged;

        public Character(int instanceId)
        {
            _instanceId = instanceId;
        }

        public void Initialize(CharacterStatistics characterStatistics)
        {
            Statistics = characterStatistics;
        }

        public void ApplyDamage(int damageValue)
        {
            damageValue = ReduceDamageByArmor(damageValue);

            var healthDelta = damageValue * -1;
            Statistics.ModifyStatistics(EStatistic.CurrentHp, healthDelta);

            OnChangeHp?.Invoke(Statistics.CurrentHp, Statistics.MaxHp);

            if (Statistics.IsDead)
            {
                OnDeath?.Invoke();
            }
        }

        private int ReduceDamageByArmor(int damage)
        {
            var damageAbsorbedByArmor = Mathf.Min(damage, Statistics.CurrentArmor);
            Statistics.ModifyStatistics(EStatistic.CurrentArmor, damageAbsorbedByArmor * -1);
            OnChangeArmor?.Invoke(Statistics.CurrentArmor);
            
            var remainingDamage = damage - damageAbsorbedByArmor;

            return remainingDamage;
        }

        public bool TryApplyHeal(int healValue)
        {
            var canBeHealed = CanBeHealed;

            if (canBeHealed)
            {
                var lostHp = Statistics.MaxHp - Statistics.CurrentHp;
                healValue = Mathf.Min(healValue, lostHp);
                Statistics.ModifyStatistics(EStatistic.CurrentHp, healValue);

                OnChangeHp?.Invoke(Statistics.CurrentHp, Statistics.MaxHp);
            }

            return canBeHealed;
        }
    }
}
