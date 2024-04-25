using Client.Views.Contents;
using Client.Views.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class SliderComponent : Component, IInteraction
    {
        private const char emptyCharacter = '░';
        private const char filledCharacter = '█';

        private int maxValue;
        private int minValue;
        private int currentValue;

        private bool displayValue;

        public SliderComponent() : base()
        {
            maxValue = 100;
            minValue = 0;
            currentValue = 50;
            displayValue = true;
        }

        public bool IsValueDisplayed()
        {
            return displayValue;
        }

        public void SetValueDisplay(bool display)
        {
            displayValue = display;
            Update();
        }

        public bool ToogleValueDisplay()
        {
            bool display = !IsValueDisplayed();
            SetValueDisplay(display);
            return display;
        }

        public void SetMaxValue(int value)
        {
            maxValue = value;
            if (maxValue < minValue) minValue = maxValue;
            if (maxValue < currentValue) currentValue = maxValue;
            Update();
        }

        public void SetMinValue(int value)
        {
            minValue = value;
            if (minValue > maxValue) maxValue = minValue;
            if (minValue > currentValue) currentValue = minValue;
            Update();
        }

        public int SetCurrentValue(int value)
        {
            value = Math.Min(Math.Max(value, minValue), maxValue);
            currentValue = value;
            Update();
            return currentValue;
        }

        public int IncreaseCurrentValue()
        {
            return SetCurrentValue(currentValue + 1);
        }

        public int DecreaseCurrentValue()
        {
            return SetCurrentValue(currentValue - 1);
        }

        public override ContentCanvas GetCanvas(ContentCanvas canvas)
        {
            ContentString[] rows = canvas.GetRows();
            if (rows.Length == 0) return canvas;

            int width = rows[0].Length;
            int progress = (int)(width * ((currentValue - minValue) / (double)(maxValue - minValue)));

            ContentString line = new ContentString(new string(emptyCharacter, width));
            line.Inplace(0, new string(filledCharacter, progress));

            for (int i = 0; i < rows.Length; i++)
            {
                canvas.SetRow(i, line.Clone());
            }

            if (displayValue)
            {
                ContentString? centerRow = canvas.GetRow(rows.Length / 2);
                if (centerRow != null)
                {
                    string displayedValue = currentValue.ToString();
                    int startIndex = (centerRow.Length / 2) - (displayedValue.Length / 2) - 1;
                    for (int i = 0; i < displayedValue.Length; i++)
                    {
                        ContentCharacter character = centerRow[i + startIndex];
                        if (character.character == filledCharacter)
                        {
                           character.Foreground(ContentColor.BLACK);
                           character.Background(ContentColor.GRAY);
                        }
                        character.character = displayedValue[i];
                    }
                }
            }

            return canvas;
        }

        public void HandleInteraction(InteractionArgument args)
        {
            switch (args.key.Key)
            {
                case ConsoleKey.LeftArrow:
                    DecreaseCurrentValue();
                    args.handled = true;
                    return;
                case ConsoleKey.RightArrow:
                    IncreaseCurrentValue();
                    args.handled = true;
                    return;
            }
        }
    }
}
