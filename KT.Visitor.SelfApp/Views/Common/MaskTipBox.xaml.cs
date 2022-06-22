using KT.Common.WebApi.HttpApi;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Settings;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.SelfApp.Views.Common
{
    /// <summary>
    /// MaskTipBox.xaml 的交互逻辑
    /// </summary>
    public partial class MaskTipBox : WindowX
    {
        private AppSettings _appSettings;

        public MaskTipBox()
        {
            InitializeComponent();

            _appSettings = ContainerHelper.Resolve<AppSettings>();

            this.Loaded += MaskTipBox_Loaded;
        }

        private void MaskTipBox_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置全屏
            WindowHelper.ReleaseFullWindow(this, _appSettings.IsFullScreen);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        public static bool IsRuning { get; set; }

        public static void Run(Func<Task<List<RegisterResultModel>>> runSubmitAsync, Action<List<RegisterResultModel>> successSubmit, Action<Exception> errorSubmit)
        {
            IsRuning = true;

            var tipBox = new MaskTipBox();
            tipBox.Show();
            var startTime = DateTime.Now;
            Task.Run(async () =>
            {
                try
                {
                    var results = await runSubmitAsync?.Invoke();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CloseTipBox(startTime, tipBox, () =>
                        {
                            successSubmit?.Invoke(results);
                        });
                    });

                    IsRuning = false;
                }
                catch (Exception ex)
                {
                    IsRuning = false;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CloseTipBox(startTime, tipBox, () =>
                        {
                            errorSubmit?.Invoke(ex);
                        });
                    });
                }
            });

        }

        private static void CloseTipBox(DateTime startTime, MaskTipBox tipBox, Action endAction)
        {
            var times = (DateTime.Now - startTime).TotalSeconds;
            if (times < 3)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(Convert.ToInt32((3 - times) * 1000));
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (tipBox != null && tipBox.IsActive)
                        {
                            tipBox.Close();
                            endAction?.Invoke();
                        }
                    });
                });
            }
            else
            {
                if (tipBox != null && tipBox.IsActive)
                {
                    tipBox.Close();
                    endAction?.Invoke();
                }
            }
        }

        public static void Run<T1, T2>(T1 appoint, Func<T1, Task<T2>> runSubmitAsync, Action<T2, T1> successSubmit, Action<Exception> errorSubmit)
        {
            IsRuning = true;

            MaskTipBox tipBox = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                tipBox = new MaskTipBox();
                tipBox.Show();
            });

            var startTime = DateTime.Now;
            Task.Run(async () =>
            {
                try
                {
                    var results = await runSubmitAsync?.Invoke(appoint);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CloseTipBox(startTime, tipBox, () =>
                        {
                            successSubmit?.Invoke(results, appoint);
                        });
                    });

                    IsRuning = false;
                }
                catch (Exception ex)
                {
                    IsRuning = false;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CloseTipBox(startTime, tipBox, () =>
                        {
                            errorSubmit?.Invoke(ex);
                        });
                    });
                }
            });
        }
    }
}
