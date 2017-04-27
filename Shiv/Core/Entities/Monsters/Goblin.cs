using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;
using RogueSharp;

namespace Shiv.Core
{
    public class Goblin : Monster
    {
        public static Goblin Create(int level)
        {
            int health = Dice.Roll("3D6");
            return new Goblin
            {
                Damage        = Dice.Roll("1D3") + level / 3,
                Accuracy      = Dice.Roll("25D3"),
                Fov           = 10,
                Color         = Colors.Goblin,
                Defense       = Dice.Roll("1D3") + level / 3,
                BlockChance   = Dice.Roll("10D4"),
                Gold          = Dice.Roll("2D5"),
                CurrentHealth = health,
                MaxHealth     = health,
                Speed         = 10,
                Symbol        = 'G',
                Name          = "Goblin"
            };
        }
    }
}
