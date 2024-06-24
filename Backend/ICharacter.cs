using System;

namespace Backend
{
    public interface ICharacter
    {
        public event Action<int, int> OnChangeHp;
        public event Action OnDeath;
    }
}
