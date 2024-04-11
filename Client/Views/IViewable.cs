using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views.Contents;

namespace Client.Views
{
    public delegate void UpdateHandler();
    public interface IViewable
    {
        public event UpdateHandler? Update;

        public ContentLine[] View();
    }
}
