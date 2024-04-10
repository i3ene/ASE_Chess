using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Communications.Actions
{
    public class MessageAction : Action
    {
        public string message {  get; set; }

        public MessageAction() : this("") { }

        public MessageAction(string message) : base(ActionType.Message)
        {
            this.message = message;
        }
    }
}
