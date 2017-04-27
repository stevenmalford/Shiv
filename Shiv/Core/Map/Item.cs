/* Name: Steven Alford
 * File: RandomItemGenerator.cs
 * Date: 3/15/17
 * Desc: Generates a random item when the player opens a chest
 */

namespace Shiv.Core
{
    public class Item
    {
        public enum Rarity
        {
            Puny = 0,
            Weak = 1,
            Ordinary = 2,
            Heroic = 3,
            Legendary = 4,
        }

        public enum Set
        {
            Head = 0,
            Neck = 1,
            Chest = 2,
            Legs = 3,
            Gloves = 4,
            Boots = 5,
            Weapon = 6,
            Shield = 7,
        }

        public enum Descriptor
        {
            Speed = 0,
            Accuracy = 1,
            Defense = 2,
            Health = 3,
            Damage = 4,
            //Speed + Accuracy
            Lightning = 5,
            //Speed + Defense
            Steadfast = 6,
            //Speed + Health
            Persistent = 7,
            //Speed + Damage
            Thief = 8,
            //Accuracy + Defense
            Consistency = 9,
            //Accuracy + Health
            Clarity = 10,
            //Accuracy + Damage
            Sharpshooter = 11,
            //Defense + Health
            Bulwark = 12,
            //Defense + Damage
            Berserker = 13,
            //Health + Damage
            Titan = 14,
            //Speed + Accuracy + Defense + Health + Damage
            Legend = 15,
        }

        public int speed
        { get; set; }
        public int accuracy
        { get; set; }
        public int defense
        { get; set; }
        public int health
        { get; set; }
        public int damage
        { get; set; }

        public Item()
        {
            speed = 0;
            accuracy = 0;
            defense = 0;
            health = 0;
            damage = 0;
        }
    }
}