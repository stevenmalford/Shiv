/* Name: Steven Alford
 * File: Player.cs
 * Date: 3/10/17
 * Desc: A player object for the user to move around on the screen
 *       and complete objectives. Given malleable attributes that
 *       can change during the game loop
 */

using RLNET;

namespace Shiv.Core
{
    //Calls the Actor class which uses the IActor and IDrawable interfaces
    public class Player : Actor
    {
        public string Head
        { get; set; }
        public string Neck
        { get; set; }
        public string Chest
        { get; set; }
        public string Legs
        { get; set; }
        public string Gloves
        { get; set; }
        public string Boots
        { get; set; }
        public string Weapon
        { get; set; }
        public string Shield
        { get; set; }

        public Player()
        {
            //Stats
            Damage        = 3;
            Accuracy      = 50;
            Defense       = 0;
            MaxHealth     = 100;
            CurrentHealth = 100;
            Speed         = 5;
            Gold          = 0;

            //The name of the player
            Name = "Rogue";
            //The symbol that represents the player on the screen
            Symbol = (char)127;
            //The color of the player's symbol
            Color = Colors.Player;
            //The players field of view/awareness
            Fov = 10;
            //The x coordinate of the player
            X = 10;
            //The y coordinate of the player
            Y = 10;

            Head   = "-----";
            Neck   = "-----";
            Chest  = "-----";
            Legs   = "-----";
            Gloves = "-----";
            Boots  = "-----";
            Weapon = "-----";
            Shield = "-----";
        }

        public void DrawStats(RLConsole statConsole)
        {
            Game.statsConsole.Print(1, 5,  $"Gold:       {Gold}", Colors.Gold);
            Game.statsConsole.Print(1, 8,  $"Health:     {CurrentHealth}/{MaxHealth}", Colors.Health);
            Game.statsConsole.Print(1, 11, $"Defense:    {Defense}", Colors.TextHeading);
            Game.statsConsole.Print(1, 13, $"Accuracy:   {Accuracy}", Colors.TextHeading);
            Game.statsConsole.Print(1, 15, $"Damage:     {Damage}", Colors.TextHeading);
        }

        public void DrawArmour(RLConsole armourConsole)
        {
            armourConsole.Print(1, 5,  $"Head:    {Head}", Colors.TextHeading);
            armourConsole.Print(1, 7,  $"Neck:    {Neck}", Colors.TextHeading);
            armourConsole.Print(1, 9,  $"Chest:   {Chest}", Colors.TextHeading);
            armourConsole.Print(1, 11, $"Legs:    {Legs}", Colors.TextHeading);
            armourConsole.Print(1, 13, $"Gloves:  {Gloves}", Colors.TextHeading);
            armourConsole.Print(1, 15, $"Boots:   {Boots}", Colors.TextHeading);
            armourConsole.Print(1, 21, $"Weapon:  {Weapon}", Colors.TextHeading);
            armourConsole.Print(1, 23, $"Shield:  {Shield}", Colors.TextHeading);
        }
    }
}
