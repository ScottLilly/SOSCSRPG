using Engine.Models;
using Engine.Shared;

namespace Engine.Services
{
    public static class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

        public static Combatant FirstAttacker(Player player, Monster opponent)
        {
            // Formula is: ((Dex(player)^2 - Dex(monster)^2)/10) + Random(-10/10)
            // For dexterity values from 3 to 18, this should produce an offset of +/- 41.5
            int playerDexterity = player.GetAttribute("DEX").ModifiedValue * 
                                  player.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = opponent.GetAttribute("DEX").ModifiedValue * 
                                    opponent.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset 
                       ? Combatant.Player 
                       : Combatant.Opponent;
        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            // Currently using the same formula as FirstAttacker initiative.
            // This will change as we include attack/defense skills,
            // armor, weapon bonuses, enchantments/curses, etc.
            int playerDexterity = attacker.GetAttribute("DEX").ModifiedValue * 
                                  attacker.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = target.GetAttribute("DEX").ModifiedValue * 
                                    target.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset;
        }
    }
}