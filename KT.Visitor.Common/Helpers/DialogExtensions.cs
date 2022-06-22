using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace KT.Visitor.Common.Helpers
{
    public static class DialogExtensions
    {
        public static void ShowContentConfirm<T>(this IDialogService dialogService, T control, Action<IDialogResult> callBack) where T : UserControl
        {
            dialogService.ShowDialog("ContentConfirmWindow", new DialogParameters($"control={control}"), callBack);
        }

        public static void ShowConfirmation(this IDialogService dialogService, string message, Action<IDialogResult> callBack)
        {
            dialogService.ShowDialog("ConfirmationDialog", new DialogParameters($"message={message}"), callBack);
        }

        public static void ShowNotificationInAnotherWindow(this IDialogService dialogService, string message, Action<IDialogResult> callBack)
        {
            dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}"), callBack, "AnotherDialogWindow");
        }
    }
}
