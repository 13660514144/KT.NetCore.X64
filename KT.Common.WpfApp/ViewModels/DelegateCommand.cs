using KT.Common.Core.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Common.WpfApp.ViewModels
{
    public class DelegateCommand : Prism.Commands.DelegateCommand
    {
        public DelegateCommand(Action executeMethod) : base(executeMethod)
        {
        }
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }
        public DelegateCommand(Func<Task> executeMethod) : base(async () =>
        {
            await executeMethod?.Invoke();
        })
        {

        }
    }
}

//private readonly Action<object> action;
//private bool isEnabled;

//public DelegateCommand(Action action)
//{
//    //参数转换,可定义有参跟无参的Command方法
//    this.action = (val) =>
//    {
//        action?.Invoke();
//    };
//    isEnabled = true;
//}
//public DelegateCommand(Action<object> action)
//{
//    this.action = action;
//    isEnabled = true;
//}
//public DelegateCommand(Action<string> action)
//{
//    this.action = (val) =>
//    {
//        action?.Invoke(ConvertUtil.ToString(val));
//    };
//    isEnabled = true;
//}

//public void Execute(object parameter)
//{
//    action(parameter);
//}

//public bool CanExecute(object parameter)
//{
//    return isEnabled;
//}

//public bool IsEnabled
//{
//    get
//    {
//        return isEnabled;
//    }
//    set
//    {
//        if (isEnabled != value)
//        {
//            isEnabled = value;
//            OnCanExecuteChanged();
//        }
//    }
//}

//public event EventHandler CanExecuteChanged;

//protected virtual void OnCanExecuteChanged()
//{
//    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//}
