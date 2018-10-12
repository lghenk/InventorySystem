using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_System.Item_Behaviours {
    class TestBehaviour : BaseItemBehaviour {

        public override void init() {
            BehaviourName = "TestAction";
            base.init();
        }

        public override void use(string[] args) {
            throw new NotImplementedException();
        }
    }
}
