using NUnit.Framework;

namespace Backend.Tests
{
    public class CharacterTests
    {
        private const int INSTANCE_ID = 0;

        [Test,
         TestCase(5, 2, 3),
         TestCase(5, 6, -1)]
        public void _00_DealDamage(int maxHp, int damage, int expected)
        {
            var statistics = new CharacterStatistics(maxHp);
            var character = CreateCharacter(statistics);
            character.ApplyDamage(damage);

            Assert.AreEqual(expected, character.Statistics.CurrentHp);
        }
        
        private Character CreateCharacter(CharacterStatistics statistics)
        {
            var character = new Character(INSTANCE_ID);
            character.Initialize(statistics);

            return character;
        }

        [Test,
         TestCase(5, 1, 3, 3, 0),
         TestCase(5, 4, 3, 5, 1),
         TestCase(5, 2, 4, 3, 0),
         TestCase(5, 2, 7, 0, 0)]
        public void _01_DealDamageWithAppliedArmor(int maxHp, int armor, int damage, int expectedHp, int expectedArmor)
        {
            var statistics = new CharacterStatistics(maxHp, armor);
            var character = CreateCharacter(statistics);
            character.ApplyDamage(damage);

            Assert.AreEqual(expectedHp, character.Statistics.CurrentHp);
            Assert.AreEqual(expectedArmor, character.Statistics.CurrentArmor);
        }

        [Test,
         TestCase(5, 3, 2, 5),
         TestCase(5, 5, 4, 5),
         TestCase(5, 1, 2, 3)]
        public void _02_DealDamageAndHeal(int maxHp, int currentHp, int heal, int expectedHp)
        {
            var statistics = new CharacterStatistics(currentHp, maxHp, 0);
            var character = CreateCharacter(statistics);
            character.TryApplyHeal(heal);

            Assert.AreEqual(expectedHp, character.Statistics.CurrentHp);
        }
    }
}
