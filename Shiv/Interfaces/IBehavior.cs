using Shiv.Core;
using Shiv.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiv.Interfaces
{
    public interface IBehavior
    {
        bool Act(Monster monster, Commands commandSystem);
    }
}
