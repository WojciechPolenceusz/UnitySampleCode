using System.Collections.Generic;

namespace Backend
{
    public class CharacterStatistics : IHealthStatistics
    {
        private readonly Dictionary<EStatistic, int> _statistics = new();

        public int CurrentHp => _statistics[EStatistic.CurrentHp];
        public int MaxHp => _statistics[EStatistic.MaxHp];
        public int CurrentArmor => _statistics[EStatistic.CurrentArmor];
        
        public bool IsDamaged => CurrentHp < MaxHp;
        public bool IsAlive => CurrentHp > 0;
        public bool IsDead => IsAlive == false;
        
        public CharacterStatistics(int maxHp, int currentArmor = 0) : this(maxHp, maxHp, currentArmor)
        { }

        public CharacterStatistics(int currentHp, int maxHp, int currentArmor)
        {
            _statistics.Add(EStatistic.CurrentHp, currentHp);
            _statistics.Add(EStatistic.MaxHp, maxHp);
            _statistics.Add(EStatistic.CurrentArmor, currentArmor);
        }

        public void ModifyStatistics(EStatistic statistics, int delta)
        {
            _statistics[statistics] += delta;
        }

        public void OverrideStatistic(EStatistic statistic, int value)
        {
            _statistics[statistic] = value;
        }
    }
}
