using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Actors
{
    interface IEnemy
    {
        float DPS { get; }
        float Health { get; set; }
    }
}
