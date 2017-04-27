using RLNET;
using RogueSharp;
using Shiv.Core.Behaviors;
using Shiv.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiv.Core
{
    public class Monster : Actor
    {
        public void DrawStats(RLConsole statsConsole, int position)
        {
            int yPos = 24 + (position * 2) + 1;

            // Begin the line by printing the symbol of the monster in the appropriate color
            statsConsole.Print(1, yPos, Symbol.ToString(), Color);

            // Figure out the width of the health bar by dividing current health by max health
            int width = Convert.ToInt32(((double)CurrentHealth / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // Set the background colors of the health bar to show how damaged the monster is
            statsConsole.SetBackColor(3, yPos, width, 1, Palette.Alternate);
            statsConsole.SetBackColor(3 + width, yPos, remainingWidth, 1, Palette.PrimaryDarkest);

            // Print the monsters name over top of the health bar
            statsConsole.Print(2, yPos, $": {Name}", Color);
        }

        public int? TurnsAlerted { get; set; }

        public virtual void PerformAction(Commands commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }
    }
}
