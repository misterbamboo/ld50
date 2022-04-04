using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SharedScripts
{
    public static class KnownedLayers
    {
        public static int Default = LayerMask.NameToLayer("Default");
        public static int Ground = LayerMask.NameToLayer("Ground");
        public static int Water = LayerMask.NameToLayer("Water");
        public static int Tower = LayerMask.NameToLayer("Tower");
        public static int ItemCamera = LayerMask.NameToLayer("ItemCamera");
        public static int Item = LayerMask.NameToLayer("Item");

        public static int GetTrowableSurfacesLayerMask()
        {
            int groundMask = 1 << Ground;
            int waterMask = 1 << Water;
            int towerMask = 1 << Tower;
            int itemMask = 1 << Item;

            return groundMask | waterMask | towerMask | itemMask;
        }

        internal static bool IsGroundable(int layer)
        {
            return
                layer == Ground ||
                layer == Item ||
                layer == Tower;
        }
    }
}
