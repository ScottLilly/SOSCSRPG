using System;
using Engine.Actions;
using Engine.Factories;
using Engine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.Actions
{
    [TestClass]
    public class TestAttackWithWeapon
    {
        [TestMethod]
        public void Test_Constructor_GoodParameters()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(pointyStick, 1, 5);

            Assert.IsNotNull(attackWithWeapon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_ItemIsNotAWeapon()
        {
            GameItem granolaBar = ItemFactory.CreateGameItem(2001);

            // A granola bar is not a weapon.
            // So, the constructor should throw an exception.
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(granolaBar, 1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_MinimumDamageLessThanZero()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            // This minimum damage value is less than 0.
            // So, the constructor should throw an exception.
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(pointyStick, -1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_MaximumDamageLessThanMinimumDamage()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            // This maximum damage is less than the minimum damage.
            // So, the constructor should throw an exception.
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(pointyStick, 2, 1);
        }
    }
}