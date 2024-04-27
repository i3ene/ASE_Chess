using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public class DialogComponent : ComponentContainer, IDynamicDimension
    {
        public delegate void ActionHandler(DialogComponent sender, string action);
        public event ActionHandler? OnAction;

        private readonly TextComponent text;
        private readonly ListComponent actionContainer;

        public DialogComponent()
        {
            size.height = new ComponentValue(ComponentUnit.Auto);

            text = new TextComponent();
            text.size.width = new ComponentValue(ComponentUnit.Auto);
            text.size.height = new ComponentValue(ComponentUnit.Auto);
            text.position.y = new ComponentValue(ComponentUnit.Auto);
            AddChild(text);

            actionContainer = new ListComponent(true);
            actionContainer.size.width = new ComponentValue(ComponentUnit.Auto);
            actionContainer.size.width = new ComponentValue(ComponentUnit.Fixed, 3);
            actionContainer.position.y = new ComponentValue(ComponentUnit.Auto);
            actionContainer.alignment.horizontal = ComponentHorizontalAlignment.Center;
            AddChild(actionContainer);
        }

        public void SetText(ContentString text)
        {
            this.text.SetText(text);
        }

        public void SetText(string text)
        {
            this.text.SetText(text);
        }

        public void SetActions(string[] actions)
        {
            actionContainer.RemoveAllChilds();

            foreach (string action in actions)
            {
                ButtonComponent button = new ButtonComponent(action);
                button.size.width = new ComponentValue(ComponentUnit.Fixed, action.Length + 2);
                button.size.height = new ComponentValue(ComponentUnit.Auto);
                button.position.x = new ComponentValue(ComponentUnit.Auto);

                button.OnSelection += (button) => OnAction?.Invoke(this, action);

                actionContainer.AddChild(button);
            }
        }

        public int GetDynamicWidth()
        {
            int width = 0;

            text.parent = parent;
            int textWidth = dimensionHelper.CalculateInnerWidth(text);
            text.parent = this;

            actionContainer.parent = parent;
            int actionWidth = dimensionHelper.CalculateOuterWidth(actionContainer);
            actionContainer.parent = this;

            width = Math.Max(textWidth, actionWidth);

            return width;
        }

        public int GetDynamicHeight()
        {
            int height = 0;

            text.parent = parent;
            int textHeight = dimensionHelper.CalculateInnerHeight(text);
            text.parent = this;

            height = textHeight;
            height += 3;

            return height;
        }
    }
}
