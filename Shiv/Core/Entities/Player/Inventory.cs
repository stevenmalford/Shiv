using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiv.Core
{
    public class Inventory
    {
        public string slot1
        { get; set; }
        public string slot2
        { get; set; }
        public string slot3
        { get; set; }
        public string slot4
        { get; set; }
        public string slot5
        { get; set; }

        //Initial slots
        public Inventory()
        {
            slot1 = "-----";
            slot2 = "-----";
            slot3 = "-----";
            slot4 = "-----";
            slot5 = "-----";
        }
    }
}
