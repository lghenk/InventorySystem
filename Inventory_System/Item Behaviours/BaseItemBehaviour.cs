﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_System.Item_Behaviours {
    abstract class BaseItemBehaviour {
        protected string BehaviourName;

        public BaseItemBehaviour() {
        }

        /// <summary>
        /// Returns for which item this behaviour is made
        /// </summary>
        /// <returns>string | BehaviourName</returns>
        public string GetBehaviourName() {
            return BehaviourName;
        }

        public virtual void init() {
            Console.WriteLine($"BaseItemBehaviour :: Constructed Action: {BehaviourName}");
        }

        // Under game circumstances one would add the option to provide a game object on which this actions should be used. That however is engine specific and therefor left out here.
        // TODO: Ability to pass along gameobject of interest
        public abstract void use(string[] args);
    }
}
