using System;
using System.Collections.Generic;
using System.Text;
using Inventory_System.Item_Behaviours;
using System.Threading.Tasks;
using System.Reflection;

namespace Inventory_System {
    class BehaviourStack {

        private Dictionary<string, BaseItemBehaviour> behaviourStack = new Dictionary<string, BaseItemBehaviour>();

        public BehaviourStack() {
            LoadBehaviourClasses();
        }

        public async Task LoadBehaviourClasses() {
            foreach(Type t in Assembly.GetExecutingAssembly().GetTypes()) {
                if(t.IsSubclassOf(typeof(BaseItemBehaviour))) {
                    BaseItemBehaviour b = (BaseItemBehaviour)Activator.CreateInstance(t);
                    b.init();
                    behaviourStack.Add(b.GetBehaviourName(), b);
                }
            }
        }
    }
}
