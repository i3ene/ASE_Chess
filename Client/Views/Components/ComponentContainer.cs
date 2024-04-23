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
    public class ComponentContainer : Component, IContainer
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

        object[] IContainer.GetAllChilds()
        {
            return GetAllChilds();
        }

        public Component[] GetAllChilds()
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
                int left = dimension.position.x + childDimension.position.x;
                int top = dimension.position.y + childDimension.position.y;

                foreach ((ContentString row, int y) in childCanvas.GetRows().Select((row, index) => (row, index)))
                {
                    foreach ((ContentCharacter column, int x) in row.GetCharacters().Select((column, index) => (column, index)))
                    {
                        canvas.SetColumn(y + top, x + left, column);
                    }
                }
            }

            return canvas;
        }
    }
}
