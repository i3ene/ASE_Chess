using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views.Contents;

namespace Client.Views
{
    public class ViewService
    {
        public Task Display(IViewable view)
        {
            return Task.Factory.StartNew(() =>
            {
                bool needsUpdate = true;
                view.OnUpdate += () => needsUpdate = true;
                while (true)
                {
                    if (needsUpdate)
                    {
                        Task.Factory.StartNew(() => DisplayContent(view));
                        needsUpdate = false;
                    }
                    Thread.Sleep(33);
                }
            });
        }

        private void DisplayContent(IViewable view)
        {
            ContentString[] lines = view.View();
            Console.SetCursorPosition(0, 0);
            foreach (ContentString line in lines)
            {
                ContentString row = line.PadRight(Console.WindowWidth);
                Console.WriteLine(row);
            }
        }
    }
}
