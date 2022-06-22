using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.WpfApp.ViewModels
{
    public class DelegateCommand<T> : Prism.Commands.DelegateCommand<T>
    {
        public DelegateCommand(Action<T> executeMethod) : base(executeMethod)
        {
        }

        public DelegateCommand(Func<T, Task> executeMethod) : base(async (t) =>
        {
            await executeMethod?.Invoke(t);
        })
        {

        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }
    }
}
