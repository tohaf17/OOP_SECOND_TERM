using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4_Core.Interface
{
    public interface IRelayCommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
        event EventHandler CanExecuteChanged;
        void RaiseCanExecuteChanged();
    }
    public delegate IRelayCommand CommandFactory(Action<object> execute,
                                               Predicate<object> canExecute = null);
}
