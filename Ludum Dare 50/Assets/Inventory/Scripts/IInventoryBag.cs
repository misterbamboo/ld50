using System;
using UnityEngine;

namespace Assets.Inventory.Scripts
{
    public interface IInventoryBag
    {
        /// <summary>
        /// Retourne l'item choisi sans le faire disparaître de l'inventaire.
        /// </summary>
        GameObject Peek();

        /// <summary>
        /// Retourne l'item choisi et le retire de l'inventaire.
        /// </summary>
        /// <returns></returns>
        GameObject Use();

        event ItemSelectedHandler ItemSelected;
    }

    public delegate void ItemSelectedHandler(object sender, ItemSelectedEventArgs e);

    public class ItemSelectedEventArgs : EventArgs
    {
        public ItemSelectedEventArgs(GameObject o)
        {
            Item = o;
        }

        public GameObject Item { get; private set; }
    }
}