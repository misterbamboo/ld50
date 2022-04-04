using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils
{
    public static class ColliderUtils
    {
        public static void ChangeCollidersTrigger(GameObject item, bool isTrigger)
        {
            var colliders = item.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                collider.isTrigger = isTrigger;
            }
        }
    }
}
