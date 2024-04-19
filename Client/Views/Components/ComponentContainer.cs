using Client.Views.Components.Styles;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class ComponentContainer : Component
    {
        private List<Component> childs;

        public ComponentContainer() : base()
        {
            childs = new List<Component>();
        }

        public void AddChild(Component child)
        {
            child.parent = this;
            child.OnUpdate += Update;
            childs.Add(child);
            Update();
        }

        public void RemoveChild(Component child)
        {
            bool success = childs.Remove(child);
            if (!success) return;
            child.parent = null;
            child.OnUpdate -= Update;
            Update();
        }

        public IEnumerable<Component> GetAllChilds()
        {
            return childs.ToArray();
        }

        public override ContentCanvas GetCanvas(ContentCanvas canvas)
        {
            foreach (Component child in childs)
            {
                ContentCanvas childCanvas = child.View();
                ComponentDimension childDimension = dimensionService.CalculateDimension(child);

                foreach ((ContentString row, int x) in childCanvas.GetRows().Select((row, index) => (row, index)))
                {
                    foreach ((ContentCharacter column, int y) in row.GetCharacters().Select((column, index) => (column, index)))
                    {
                        canvas.SetColumn(x + childDimension.position.x, y + childDimension.position.y, column);
                    }
                }
            }

            return canvas;
        }
    }
}
