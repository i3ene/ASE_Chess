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
            ComponentDimension dimension = new ComponentDimension();
            foreach (Component child in childs)
            {
                ContentCanvas childCanvas = child.View();
                ComponentDimension childDimension = dimensionService.CalculateDimension(child);

                foreach ((ContentString row, int x) in childCanvas.GetRows().Select((row, index) => (row, index)))
                {
                    foreach ((ContentCharacter column, int y) in row.GetCharacters().Select((column, index) => (column, index)))
                    {
                        int left = dimension.position.x - childDimension.position.x;
                        int top = dimension.position.y - childDimension.position.y;
                        canvas.SetColumn(x + left, y + top, column);
                    }
                }
            }

            return canvas;
        }
    }
}
