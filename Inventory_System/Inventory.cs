using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inventory_System {
    class Inventory {

        private ItemStack _itemStack;
        private List<ItemStack.Item> _itemList = new List<ItemStack.Item>();

        public Action<ItemStack.Item> ItemUsed;
        public Action<ItemStack.Item, int> ItemAdded;
        public Action<ItemStack.Item> ItemRemoved;

        public Inventory(ItemStack _is = null) {
            Console.WriteLine("Inventory :: Constructing");

            _is = (_is != null) ? _is : new ItemStack();    
        }

        public void AddItem(int id, int amount) {
            ItemStack.Item itemReference = _itemStack.GetById(id);
            AddItem(itemReference, amount);
        }

        public void AddItem(string internalName, int amount) {
            ItemStack.Item itemReference = _itemStack.GetByInternalName(internalName);
            AddItem(itemReference, amount);
        }

        public void AddItem(ItemStack.Item itemReference, int amount ) {
            for (int i = 0; i < amount; i++) {
                _itemList.Add(itemReference);
            }
            ItemAdded?.Invoke(itemReference, amount);
        }

        /// <summary>
        /// Remove item from inventory by ID
        /// </summary>
        /// <param name="id">The unique item ID</param>
        /// <param name="amount">Amount it should remove, if -1 it will remove all</param>
        public void RemoveItems(int id, int amount)
        {
            ItemStack.Item[] itemReferences = this.GetItems(id);
            RemoveItems(itemReferences, amount);
        }

        /// <summary>
        /// Remove item from inventory by Internal Name
        /// </summary>
        /// <param name="internalName">The Items Unique internal reference name</param>
        /// <param name="amount">Amount it should remove, if -1 it will remove all</param>
        public void RemoveItems(string internalName, int amount)
        {
            ItemStack.Item[] itemReferences = this.GetItems(internalName);
            RemoveItems(itemReferences, amount);
        }

        /// <summary>
        /// Remove item from inventory by ItemStack struct reference
        /// </summary>
        /// <param name="itemReference">ItemStack Item struct</param>
        /// <param name="amount">Amount it should remove, if -1 it will remove all</param>
        public void RemoveItems(ItemStack.Item[] itemReferences, int amount)
        {
            if (itemReferences.Length > amount)
                itemReferences = itemReferences.Take(amount).ToArray();

            foreach (ItemStack.Item i in itemReferences)
            {
                _itemList.Remove(i);
                ItemRemoved?.Invoke(i);
            }
        }

        public void UseItem(int id) {
            ItemStack.Item itemReference = this.GetItem(id);
            UseItem(itemReference);
        }

        public void UseItem(string internalName)
        {
            ItemStack.Item itemReference = this.GetItem(internalName);
            UseItem(itemReference);
        }

        public void UseItem(ItemStack.Item itemReference)
        {
            ItemUsed?.Invoke(itemReference);
        }
 
        public ItemStack.Item GetItem(int id) {
            return _itemList.Find(i => i._id == id);
        }

        public ItemStack.Item GetItem(string name) {
            return _itemList.Find(i => i.InternalName == name);
        }

        public ItemStack.Item[] GetItems(int id) {
            return _itemList.FindAll(i => i._id == id).ToArray();
        }

        public ItemStack.Item[] GetItems(string name) {
            return _itemList.FindAll(i => i.InternalName == name).ToArray();
        }
    }
}
