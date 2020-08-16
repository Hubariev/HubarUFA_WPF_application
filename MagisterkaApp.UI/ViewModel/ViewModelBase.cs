using Hangfire.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MagisterkaApp.UI.ViewModel
{
    public abstract class ViewModelBase: INotifyPropertyChanged
    {
        protected ViewModelBase()
        {
            RegisterCommands();
            RegisterCollections();
        }

        protected virtual void RegisterCommands() { }
        protected virtual void RegisterCollections() { }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
