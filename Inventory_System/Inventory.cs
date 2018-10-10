using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_System {
    class Inventory {

        private ItemStack _itemStack;
        private List<ItemStack.Item> _itemList = new List<ItemStack.Item>(); 

        public Action<ItemStack.Item> ItemUsed;

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
