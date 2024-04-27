using Client.Views.Contents;

namespace Client.Views
{
    public delegate void UpdateHandler();
    public interface IViewable
    {
        public event UpdateHandler? OnUpdate;

        public ContentCanvas View();
    }
}
