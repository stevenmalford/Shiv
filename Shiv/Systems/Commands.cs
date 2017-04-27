using RLNET;
using RogueSharp;
using RogueSharp.DiceNotation;
using Shiv.Core;

namespace Shiv.Systems
{
    public class Commands
    {
        //If the player is able to move to the desired cell,
        //  return true. Otherwise, return false such as if
        //  the player tries to move to an invalid cell
        public bool MovePlayer(Core.Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch(direction)
            {
                case Core.Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Core.Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Core.Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Core.Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default:
                    return false;
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }

            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);

            if(monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            return false;
        }

        public void Attack(Actor attacker, Actor defender)
        {
            int hits = ResolveAttack(attacker, defender);
            int blocks = ResolveDefense(defender, hits);

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }

        private static int ResolveAttack(Actor attacker, Actor defender)
        {
            int hits = 0;

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Damage, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach(TermResult termResult in attackResult.Results)
            {
                if(termResult.Value >= 100 - attacker.Accuracy)
                {
                    hits++;
                }
            }

            return hits;
        }

        private static int ResolveDefense(Actor defender, int hits)
        {
            int blocks = 0;

            if(hits > 0)
            {
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                foreach(TermResult termResult in defenseRoll.Results)
                {
                    if(termResult.Value >= 100 - defender.BlockChance)
                    {
                        blocks++;
                    }
                }
            }

            return blocks;
        }

        private static void ResolveDamage(Actor defender, int damage)
        {
            if(damage > 0)
            {
                defender.CurrentHealth = defender.CurrentHealth - damage;

                if(defender.CurrentHealth <= 0)
                {
                    ResolveDeath(defender);
                }
            }
        }

        private static void ResolveDeath(Actor defender)
        {
            if(defender is Player)
            {

            }
            else if(defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster) defender);
                Game.Player.Gold += defender.Gold;
            }
        }
    }
}
