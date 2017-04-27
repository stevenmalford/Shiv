/* Name: Steven Alford
 * File: Actor.cs
 * Date: 3/9/17
 * Desc: Creates an actor class that draws from the IActor and IDrawable
 *       interfaces to encapsulate a class that can be used by game objects
 *       such as the player/enemies/traps/etc
 */

using RLNET;
using RogueSharp;
using Shiv.Interfaces;

namespace Shiv.Core
{
    public class Actor : IActor, IDrawable, IScheduleable
    {
        //IActor inheritance
        private string _name;
        private int _fov;
        private int _damage;
        private int _accuracy;
        private int _defense;
        private int _blockChance;
        private int _maxHealth;
        private int _currentHealth;
        private int _speed;
        private int _gold;

        public string Name
        {
            get
            { return _name; }
            set
            { _name = value; }
        }

        public int Fov
        {
            get
            { return _fov; }
            set
            { _fov = value; }
        }

        public int Damage
        {
            get
            { return _damage; }
            set
            { _damage = value; }
        }

        public int Accuracy
        {
            get
            { return _accuracy; }
            set
            { _accuracy = value; }
        }

        public int Defense
        {
            get
            { return _defense; }
            set
            { _defense = value; }
        }

        public int BlockChance
        {
            get
            { return _blockChance; }
            set
            { _blockChance = value; }
        }

        public int MaxHealth
        {
            get
            { return _maxHealth; }
            set
            { _maxHealth = value; }
        }

        public int CurrentHealth
        {
            get
            { return _currentHealth; }
            set
            { _currentHealth = value; }
        }

        public int Speed
        {
            get
            { return _speed; }
            set
            { _speed = value; }
        }

        public int Gold
        {
            get
            { return _gold; }
            set
            { _gold = value; }
        }

        public int Time
        {
            get
            {
                return Speed;
            }
        }


        //IDrawable inheritance
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //Defines the draw function from IDrawable
        public void Draw(RLConsole console, IMap map)
        {
            //If the cell is not explored, don't draw it to the screen
            if(!map.GetCell(X, Y).IsExplored)
            { return; }

            //If the cell is in the field of view, draw it to the screen
            //      using field of view colors
            if(map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            //Else, draw it with darker colors
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, (char)177);
            }
        }
    }
}
