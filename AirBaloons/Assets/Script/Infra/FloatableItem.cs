using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Infra
{
    public class FloatItem
    {
        public FloatItem(string prefabPath, Vector3 location)
        {
            Prefab = prefabPath;
            Location = location;
        }

        public string Prefab { get; set; }
        public Vector3 Location { get; set; }
    }
}
