using System;
using Engine.EventArgs;
using Engine.Services;

namespace Engine.Models
{
    public class Battle : IDisposable
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private readonly Player _player;
        private readonly Monster _opponent;

        private enum Combatant
        {
            Player,
            Opponent
        }

        private static Combatant FirstAttacker =>
            RandomNumberGenerator.NumberBetween(1, 2) == 1 ?
                Combatant.Player :
                Combatant.Opponent;

        public event EventHandler<CombatVictoryEventArgs> OnCombatVictory;

        public Battle(Player player, Monster opponent)
        {
            _player = player;
            _opponent = opponent;

            _player.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnKilled += OnOpponentKilled;

            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You see a {_opponent.Name} here!");

            if(FirstAttacker == Combatant.Opponent)
            {
                AttackPlayer();
            }
        }

        public void AttackOpponent()
        {
            if(_player.CurrentWeapon == null)
            {
                _messageBroker.RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            _player.UseCurrentWeaponOn(_opponent);

            if(_opponent.IsAlive)
            {
                AttackPlayer();
            }
        }

        public void Dispose()
        {
            _player.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnKilled -= OnOpponentKilled;
        }

        private void OnOpponentKilled(object sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You defeated the {_opponent.Name}!");

            _messageBroker.RaiseMessage($"You receive {_opponent.RewardExperiencePoints} experience points.");
            _player.AddExperience(_opponent.RewardExperiencePoints);

            _messageBroker.RaiseMessage($"You receive {_opponent.Gold} gold.");
            _player.ReceiveGold(_opponent.Gold);

            foreach(GameItem gameItem in _opponent.Inventory.Items)
            {
                _messageBroker.RaiseMessage($"You receive one {gameItem.Name}.");
                _player.AddItemToInventory(gameItem);
            }

            OnCombatVictory?.Invoke(this, new CombatVictoryEventArgs());
        }

        private void AttackPlayer()
        {
            _opponent.UseCurrentWeaponOn(_player);
        }

        private void OnCombatantActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }
    }
}