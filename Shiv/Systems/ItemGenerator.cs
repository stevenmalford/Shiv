using Shiv.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiv.Systems
{
    public class ItemGenerator
    {
        public string randomItem;
        public string rarity;
        public string itemPiece;
        public string itemType;
        public string descriptor;
        public int set;
        public int multiplier;
        public int rng;

        public void GenerateRandomItem()
        {
            getRarity();
            getItemPiece();
            getDescriptor();
            randomItem = rarity + " " + itemPiece + " of " + descriptor;
        }

        public void UpdatePlayerStats()
        {
            Player player = Game.Player;
            if (itemType == "Head")
            {
                player.Head = randomItem;
            }
            if (itemType == "Neck")
            {
                player.Neck = randomItem;
            }
            if (itemType == "Chest")
            {
                player.Chest = randomItem;
            }
            if (itemType == "Legs")
            {
                player.Legs = randomItem;
            }
            if (itemType == "Gloves")
            {
                player.Gloves = randomItem;
            }
            if (itemType == "Boots")
            {
                player.Boots = randomItem;
            }
            if (itemType == "Weapon")
            {
                player.Weapon = randomItem;
            }
            if (itemType == "Shield")
            {
                player.Shield = randomItem;
            }
            return;
        }

        public void getRarity()
        {
            rng = Game.Random.Next(100);
            if (rng <= 5)
            {
                multiplier = 5;
                set = 5;
                rarity = "Legendary";
            }
            if (rng > 5 && rng <= 15)
            {
                multiplier = 4;
                set = 4;
                rarity = "Heroic";
            }
            if (rng > 15 && rng <= 35)
            {
                multiplier = 3;
                set = 3;
                rarity = "Ordinary";
            }
            if (rng > 35 && rng <= 65)
            {
                set = 2;
                multiplier = 2;
                rarity = "Weak";
            }
            if (rng > 65 && rng <= 100)
            {
                set = 1;
                multiplier = 1;
                rarity = "Puny";
            }
        }

        public void getItemPiece()
        {
            rng = Game.Random.Next(100);
            //Head
            if (rng <= 12)
            {
                itemType = "Head";
                if(set == 1)
                    itemPiece = "Sallet";
                if(set == 2)
                    itemPiece = "Basinet";
                if(set == 3)
                    itemPiece = "Armet";
                if(set == 4)
                    itemPiece = "Casque";
                if(set == 5)
                    itemPiece = "Crown";
            }
            //Neck
            if (rng > 12 && rng <= 25)
            {
                itemType = "Neck";
                if (set == 1)
                    itemPiece = "Choker";
                if (set == 2)
                    itemPiece = "Chain";
                if (set == 3)
                    itemPiece = "Locket";
                if (set == 4)
                    itemPiece = "Pendant";
                if (set == 5)
                    itemPiece = "Riviere";
            }
            //Chest
            if(rng > 25 && rng <= 37)
            {
                itemType = "Chest";
                if (set == 1)
                    itemPiece = "Shirt";
                if (set == 2)
                    itemPiece = "Chiton";
                if (set == 3)
                    itemPiece = "Tunic";
                if (set == 4)
                    itemPiece = "Tabard";
                if (set == 5)
                    itemPiece = "Hauberk";
            }
            //Legs
            if(rng > 37 && rng <= 50)
            {
                itemType = "Legs";
                if (set == 1)
                    itemPiece = "Pants";
                if (set == 2)
                    itemPiece = "Leggings";
                if (set == 3)
                    itemPiece = "Greaves";
                if (set == 4)
                    itemPiece = "Tassets";
                if (set == 5)
                    itemPiece = "Cuisse";
            }
            //Gloves
            if(rng > 50 && rng <= 62)
            {
                itemType = "Gloves";
                if (set == 1)
                    itemPiece = "Handwraps";
                if (set == 2)
                    itemPiece = "Cuffs";
                if (set == 3)
                    itemPiece = "Mittens";
                if (set == 4)
                    itemPiece = "Gloves";
                if (set == 5)
                    itemPiece = "Gauntlets";
            }
            //Boots
            if (rng > 62 && rng <= 75)
            {
                itemType = "Boots";
                if (set == 1)
                    itemPiece = "Footwraps";
                if (set == 2)
                    itemPiece = "Shoes";
                if (set == 3)
                    itemPiece = "Boots";
                if (set == 4)
                    itemPiece = "Buskins";
                if (set == 5)
                    itemPiece = "Hessians";
            }
            //Weapon
            if (rng > 75 && rng <= 87)
            {
                itemType = "Weapon";
                if (set == 1)
                    itemPiece = "Blade";
                if (set == 2)
                    itemPiece = "Dagger";
                if (set == 3)
                    itemPiece = "Sabre";
                if (set == 4)
                    itemPiece = "Dirk";
                if (set == 5)
                    itemPiece = "Shiv";
            }
            //Shield
            if (rng > 87 && rng <= 100)
            {
                itemType = "Shield";
                if (set == 1)
                    itemPiece = "Ward";
                if (set == 2)
                    itemPiece = "Shield";
                if (set == 3)
                    itemPiece = "Buckler";
                if (set == 4)
                    itemPiece = "Aegis";
                if (set == 5)
                    itemPiece = "Bulwark";
            }
        }

        public void getDescriptor()
        {
            rng = Game.Random.Next(100);
            if (rng <= 5)
            {
                descriptor = "the Gods";
            }
            if (rng > 5 && rng <= 15)
            {
                descriptor = "Heroism";
            }
            if (rng > 15 && rng <= 35)
            {
                descriptor = "the Light";
            }
            if (rng > 35 && rng <= 65)
            {
                descriptor = "the Dark";
            }
            if (rng > 65 && rng <= 100)
            {
                descriptor = "Mysticism";
            }
        }
    }
}
