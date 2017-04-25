/* Name: Steven Alford
 * File: Player.cs
 * Date: 3/10/17
 * Desc: A player object for the user to move around on the screen
 *       and complete objectives. Given malleable attributes that
 *       can change during the game loop
 */

namespace Shiv.Core
{
    //Calls the Actor class which uses the IActor and IDrawable interfaces
    public class Player : Actor
    {
        //Stats
        public int currentHealth
        { get; set; }
        public int maxHealth
        { get; set; }
        public int currentDefense
        { get; set; }
        private int maxDefense
        { get; set; }
        public int currentAccuracy
        { get; set; }
        private int maxAccuracy
        { get; set; }
        public int damage
        { get; set; }

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

            //Starting Stats
            maxHealth = 100;
            maxDefense = 100;
            maxAccuracy = 100;
            currentHealth = 100;
            currentDefense = 0;
            currentAccuracy = 30;
            damage = 10;

            Head = "-----";
            Neck = "-----";
            Chest = "-----";
            Legs = "-----";
            Gloves = "-----";
            Boots = "-----";
            Weapon = "-----";
            Shield = "-----";
        }
    }
}
