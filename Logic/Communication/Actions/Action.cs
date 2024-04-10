using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Communication.Actions
{
    /// <summary>
    /// Template for communicable interactions.
    /// </summary>
    /// <remarks>
    /// Every property needs to have a getter and setter function to be accessible by the JSON serializer.
    /// </remarks>
    public abstract class Action
    {
        /// <summary>
        /// Type of action. Mainly used for JSON deserialization to assign to appropiate Action class.
        /// </summary>
        public ActionType type { get; set; }

        public Action(ActionType type)
        {
            this.type = type;
        }
    }
}
