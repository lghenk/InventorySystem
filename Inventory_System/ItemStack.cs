using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Inventory_System {
    class ItemStack {
        public struct Item {
            public int _id; // This serves as the unique item ID
            public string InternalName; // If we do not wish to refer to this item using its ID we can use their Internal ID
            public string PublicName; // The item name that can be shown to the public
            public int StackLimit; // -1 = infinite, 0 & 1 both mean that it cannot stack. This is for UI related purposes
            public string[] Actions; // Actions are formatted as following "ACTION_BEHAVIOUR_NAME|var1,var2,var3"

            public Item(int id, string iName, string pName, int stackLimit, string[] actions) {
                _id = id;
                InternalName = iName;
                PublicName = pName;
                StackLimit = stackLimit;
                Actions = actions;

                StringProperty = new Dictionary<string, string>();
                IntProperty = new Dictionary<string, int>();
                BoolProperty = new Dictionary<string, bool>();
            }

            // ########### SAVE GAME ITEM PROPERTY SETTINGS FOR PERSISTENCE ###########

            // TODO: Think about if this should be usable if stacklimit is -1 or greater than 1
            private readonly Dictionary<string, string> StringProperty;
            private readonly Dictionary<string, int> IntProperty;
            private readonly Dictionary<string, bool> BoolProperty;

            public string GetString(string name, string def) {
                if (StringProperty == null) return def;
                return (StringProperty.ContainsKey(name)) ? StringProperty[name] : def;
            }

            public int GetInt(string name, int def) {
                if (IntProperty == null) return def;
                return (IntProperty.ContainsKey(name)) ? IntProperty[name] : def;
            }

            public bool GetBool(string name, bool def) {
                if (BoolProperty == null) return def;
                return (BoolProperty.ContainsKey(name)) ? BoolProperty[name] : def;
            }
        }

        private string FILE_LOC = Directory.GetCurrentDirectory() + "/inventory_data.json";
        private bool hasLoadedItems = false;
        private List<Item> itemStack = new List<Item>();

        public ItemStack() {
            Console.WriteLine("ItemStack :: Constructing");

            LoadItems();

            itemStack.ForEach(delegate (Item i) {
                Console.WriteLine($"ItemStack :: Loaded item with id {i._id}");
            });
        }

        public async Task LoadItems() {
            if (HasInitializedItems()) return;

            Console.WriteLine($"ItemStack :: Loading items from file {FILE_LOC}");

            string rawData = await LoadItemsFile();

            try {
                itemStack = JsonConvert.DeserializeObject<List<ItemStack.Item>>(rawData);
                Console.WriteLine($"ItemStack :: Deserialized {itemStack.Count} items from {FILE_LOC}");
            } catch {
                Console.WriteLine($"ItemStack :: Could not deserialize {FILE_LOC}");
            }

            hasLoadedItems = true;
        }

        private async Task<string> LoadItemsFile() {
            string result = string.Empty;
           
            if(File.Exists(FILE_LOC)) {
                result = File.ReadAllText(FILE_LOC);
            } else {
                Console.WriteLine($"ItemStack :: Could not find {FILE_LOC}");

            }

            return result;
        }

        public bool HasInitializedItems() {
            return hasLoadedItems;
        }

        public Item GetById(int id) {
            return itemStack.Find(i => i._id == id);
        }

        public Item GetByInternalName(string name) {
            return itemStack.Find(i => i.InternalName == name);
        }
    }
}
