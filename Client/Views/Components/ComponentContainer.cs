﻿using Client.Views.Components.Styles;
using Client.Views.Contents;
using System.Data;

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

        public void RemoveAllChilds()
        {
            foreach (Component child in childs.ToArray())
            {
                RemoveChild(child);
            }
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
            StyleDimension dimension = dimensionHelper.CalculateDimension(this);
            foreach (Component child in childs.ToArray())
            {
                ContentCanvas childCanvas = child.View();
                StyleDimension childDimension = dimensionHelper.CalculateDimension(child);
                int left = childDimension.position.x.value - dimension.position.x.value;
                int top = childDimension.position.y.value - dimension.position.y.value;

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
