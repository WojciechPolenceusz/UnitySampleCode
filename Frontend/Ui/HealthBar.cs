using Backend;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Ui
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBarImage;

        private ICharacter _character;
        private int _maxHp;

        public void Initialize(ICharacter character, IHealthStatistics healthStatistics)
        {
            _character = character;
            Refresh(healthStatistics.CurrentHp, healthStatistics.MaxHp);

            _character.OnChangeHp += Refresh;
            _character.OnDeath += Deinitialize;
        }

        private void Refresh(int currentHp, int maxHp)
        {
            var percentageHealth = (float)currentHp / maxHp;
            _healthBarImage.fillAmount = percentageHealth;
        }
        
        private void Deinitialize()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_character != null)
            {
                _character.OnChangeHp -= Refresh;
                _character.OnDeath -= Deinitialize;
            }
        }
    }
}
