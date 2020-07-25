using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using System;

namespace Engine.Services
{
    public class DiceService : IDiceService
    {
        private static readonly IDiceService _instance = new DiceService();

        /// <summary>
        /// Make constructor private to implement singletone pattern.
        /// </summary>
        private DiceService()
        {
        }

        /// <summary>
        /// Static singleton property
        /// </summary>
        public static IDiceService Instance => _instance;

        //--- IDiceService implementation

        public IDice Dice { get; } = new Dice();

        public IDieRoller DieRoller { get; private set; } = new RandomDieRoller();

        public IDiceConfiguration Configuration => Dice.Configuration;

        public IDieRollTracker RollTracker { get; private set; } = null;

        public void Configure(RollerType rollerType, bool enableTracker = false, int constantValue = 1)
        {
            RollTracker = enableTracker ? new DieRollTracker() : null;

            switch (rollerType)
            {
                case RollerType.Random:
                    DieRoller = new RandomDieRoller(RollTracker);
                    break;
                case RollerType.Crypto:
                    DieRoller = new CryptoDieRoller(RollTracker);
                    break;
                case RollerType.MathNet:
                    DieRoller = new MathNetDieRoller(RollTracker);
                    break;
                case RollerType.Constant:
                    DieRoller = new ConstantDieRoller(constantValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rollerType));
            }
        }

        public DiceResult Roll(string diceNotation) => Dice.Roll(diceNotation, DieRoller);

        public DiceResult Roll(int sides, int numDice = 1, int modifier = 0)
        {
            DiceResult result = Dice.Dice(sides, numDice).Constant(modifier).Roll(DieRoller);
            Dice.Clear();

            return result;
        }
    }
}
