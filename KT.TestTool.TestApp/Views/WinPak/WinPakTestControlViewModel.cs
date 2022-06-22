using KT.Common.Core.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.TestTool.TestApp.Apis;
using KT.TestTool.TestApp.Common.JsonData;
using KT.TestTool.TestApp.ViewModels;
using KT.WinPak.Data.Models;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Models;
using KT.WinPak.Service.IServices;
using Newtonsoft.Json;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.TestTool.TestApp.Views.WinPak
{
    public class WinPakTestControlViewModel : PropertyChangedBase
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        public ScrollMessageViewModel ScrollMessage { get; set; }

        private WinPakApi _winPakApi;

        //属性字段
        private string _baseUrl;
        private string _token;
        private int _startCardNo;
        private int _endCardNo;

        private IAccessLevelSdkService _accessLevelSdkService;
        private ICardSdkService _cardSdkService;
        private ICardHolderSdkService _cardHolderSdkService;
        private IHWDeviceSdkService _hWDeviceSdkService;
        private ITimeZoneSdkService _zoneTimeSdkService;
        private IUserService _userService;

        public WinPakTestControlViewModel(ScrollMessageViewModel scrollMessageViewModel,
            WinPakApi winPakApi,
            IAccessLevelSdkService accessLevelSdkService,
            ICardSdkService cardSdkService,
            ICardHolderSdkService cardHolderSdkService,
            IHWDeviceSdkService hWDeviceSdkService,
            ITimeZoneSdkService zoneTimeSdkService,
            IUserService userService)
        {
            _baseUrl = "http://192.168.0.180:1241";
            _token = "";
            _startCardNo = 100000;
            _endCardNo = 999999;

            ScrollMessage = scrollMessageViewModel;
            _winPakApi = winPakApi;

            _accessLevelSdkService = accessLevelSdkService;
            _cardSdkService = cardSdkService;
            _cardHolderSdkService = cardHolderSdkService;
            _hWDeviceSdkService = hWDeviceSdkService;
            _zoneTimeSdkService = zoneTimeSdkService;
            _userService = userService;
        }


        private CardModel SelectCard(Action<Action> dispatcherAction, string cardNumber)
        {
            var startTime1 = DateTime.Now;
            try
            {
                var modelTask = _winPakApi.CardDetailAsync(BaseUrl, Token, cardNumber);
                var oldModel = modelTask.Result;
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    if (oldModel == null)
                    {
                        ScrollMessage.InsertTop("查询卡不存在：timespan:{0}s cardNumber:{1}", timeSpan1, cardNumber.ToString());
                    }
                    else
                    {
                        ScrollMessage.InsertTop("查询卡已存在：timespan:{0}s cardNumber:{1}", timeSpan1, cardNumber.ToString());
                    }
                });
                return oldModel;
            }
            catch (Exception ex)
            {
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("查询卡失败：timespan:{0}s cardNumber:{1} message:{2} ", timeSpan1, cardNumber, ex.Message);
                });
                return null;
            }
        }
        private void AddCard(Action<Action> dispatcherAction, string cardNumber, string activationDate, string expirationDate)
        {
            var model = CardAndCardHolderModel.Create();
            model.Card.CardNumber = cardNumber.ToString();
            model.Card.ActivationDate = activationDate;
            model.Card.ExpirationDate = expirationDate;
            model.Card.CardStatus = 1;
            model.Card.AccessLevels.Add("管理平台默认门禁级别[不可删除]");

            model.CardHolder.FirstName = "人员";
            model.CardHolder.LastName = "假";

            var startTime = DateTime.Now;
            try
            {
                var taskReslt = _winPakApi.AddCardAndCardHolderAsync(BaseUrl, Token, model);
                var result = taskReslt.Result;
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("新增人员与卡成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("新增人员与卡失败： timespan:{0}s cardNumber:{1} message:{2} ", timeSpan, cardNumber, ex.Message);
                });
            }
        }
        private void DeleteCardAndHolder(Action<Action> dispatcherAction, string cardNumber, int cardHolderId)
        {
            var cardAndCardHolder = CardAndCardHolderModel.Create();
            cardAndCardHolder.Card.CardNumber = cardNumber;

            cardAndCardHolder.CardHolder.CardHolderID = cardHolderId;
            cardAndCardHolder.CardHolder.DeleteCardType = 0;
            cardAndCardHolder.CardHolder.DeleteImageType = 0;

            var startTime2 = DateTime.Now;
            try
            {
                _winPakApi.DeleteWithCardHolderAsync(BaseUrl, Token, cardAndCardHolder).Wait();
                var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;

                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡与持卡人成功：timespan:{0}s cardNumber:{1} ", timeSpan2, cardNumber);
                });
            }
            catch (Exception ex)
            {
                var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡与持卡人失败：timespan:{0}s cardNumber:{1} message:{2} ", timeSpan2, cardNumber, ex.Message);
                });
            }
        }

        internal void StartAddCard(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始人员与卡添加！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(() =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = SelectCard(dispatcherAction, i.ToString());
                    if (oldModel != null)
                    {
                        continue;
                    }

                    AddCard(dispatcherAction, i.ToString(), activationDate, expirationDate);
                }
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"人员与卡添加完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }


        internal void AddAndDeleteCard(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始人员与卡添加与删除！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(() =>
            {
                for (int repeatTimes = 1; repeatTimes < 10000; repeatTimes++)
                {
                    //新增卡
                    if (repeatTimes % 2 == 1)
                    {
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"开始人员与卡添加 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                        for (int i = StartCardNo; i <= EndCardNo; i++)
                        {
                            var oldModel = SelectCard(dispatcherAction, i.ToString());
                            if (oldModel != null)
                            {
                                continue;
                            }

                            AddCard(dispatcherAction, i.ToString(), activationDate, expirationDate);
                        }
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"人员与卡添加完成 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                    }

                    //删除卡
                    else
                    {
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"开始人员与卡删除 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                        for (int i = StartCardNo; i <= EndCardNo; i++)
                        {
                            CardModel oldModel = SelectCard(dispatcherAction, i.ToString());
                            if (oldModel == null)
                            {
                                continue;
                            }
                            DeleteCardAndHolder(dispatcherAction, oldModel.CardNumber, oldModel.CardHolderID);
                        }
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"人员与卡删除完成 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                    }
                }
            });
        }

        internal void DeleteCard(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape($"开始人员与卡删除  ！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(() =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    CardModel oldModel = SelectCard(dispatcherAction, i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    DeleteCardAndHolder(dispatcherAction, oldModel.CardNumber, oldModel.CardHolderID);
                }
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"人员与卡删除完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void SearchCardAccessLevel(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始查询并修改卡门禁级别！！！！！！！！！！！！！！！！！！！");
            Task.Run(() =>
            {
                var datas = JsonDataHelper.GetWinPakCards();
                var multileAccessLevels = new List<string>()
                {
                    "混合分组",
                    "管理平台默认门禁级别[不可删除]"
                };

                var oneAccessLevels = new List<string>()
                {
                    "管理平台默认门禁级别[不可删除]"
                };
                CardModel model = null;

                for (int repeatTimes = 1; repeatTimes < 10000; repeatTimes++)
                {
                    dispatcherAction.Invoke(() =>
                    {
                        ScrollMessage.InsertTopShape($"开始查询并修改卡门禁级别 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                    });
                    for (int i = StartCardNo; i <= EndCardNo; i++)
                    {
                        var startTime1 = DateTime.Now;
                        try
                        {
                            var modelTask = _winPakApi.CardDetailAsync(BaseUrl, Token, i.ToString());
                            model = modelTask.Result;
                            var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                            if (model == null)
                            {
                                dispatcherAction.Invoke(() =>
                                {
                                    ScrollMessage.InsertTop("查询卡不存在：timespan:{0}s cardNumber:{1}", timeSpan1, i.ToString());
                                });
                                continue;
                            }
                            dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("查询卡成功：timespan:{0}s cardNumber:{1}", timeSpan1, i.ToString());
                            });
                        }
                        catch (Exception ex)
                        {
                            var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                            dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("查询卡失败：timespan:{0}s cardNumber:{1} message:{2} ", timeSpan1, i.ToString(), ex.Message);
                            });
                            continue;
                        }

                        if (model.AccessLevels == null)
                        {
                            model.AccessLevels = oneAccessLevels;
                        }
                        else if (model.AccessLevels.Count == 1)
                        {
                            model.AccessLevels = multileAccessLevels;
                        }
                        else
                        {
                            model.AccessLevels = oneAccessLevels;
                        }

                        model.CardOldNumber = model.CardNumber;

                        var startTime2 = DateTime.Now;
                        try
                        {
                            _winPakApi.EditCardAsync(BaseUrl, Token, model).Wait();
                            var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;

                            dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("修改卡门禁级别成功：timespan:{0}s cardNumber:{1} ", timeSpan2, model.CardNumber);
                            });
                        }
                        catch (Exception ex)
                        {
                            var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;
                            dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("修改卡门禁级别失败：timespan:{0}s cardNumber:{1} message:{2} ", timeSpan2, model.CardNumber, ex.Message);
                            });
                        }
                    }
                }

                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("查询并修改卡门禁级别完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void EditCardAccessLevel(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始修改卡门禁级别！！！！！！！！！！！！！！！！！！！");
            Task.Run(() =>
            {
                var datas = JsonDataHelper.GetWinPakCards();
                var multileAccessLevels = new List<string>()
                {
                    "混合分组",
                    "管理平台默认门禁级别[不可删除]"
                };

                var oneAccessLevels = new List<string>()
                {
                    "管理平台默认门禁级别[不可删除]"
                };

                foreach (var item in datas)
                {
                    var startTime = DateTime.Now;

                    if (item.AccessLevels == null)
                    {
                        item.AccessLevels = oneAccessLevels;
                    }
                    else if (item.AccessLevels.Count == 1)
                    {
                        item.AccessLevels = multileAccessLevels;
                    }
                    else
                    {
                        item.AccessLevels = oneAccessLevels;
                    }

                    item.CardOldNumber = item.CardNumber;

                    try
                    {
                        _winPakApi.EditCardAsync(BaseUrl, Token, item).Wait();

                        var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTop("修改卡门禁级别成功：timespan:{0}s ", timeSpan);
                        });
                    }
                    catch (Exception ex)
                    {
                        var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                        dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTop("修改卡门禁级别失败：timespan:{0}s message:{1} ", timeSpan, ex.Message);
                        });
                    }
                }

                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("修改卡门禁级别完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void RefrenceSearch(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始引用查询！！！！！！！！！！！！！！！！！！！");
            Task.Run(() =>
            {
                //先登录
                _userService.LoginApp(new LoginUserModel() { PCUser = "admin", PCPassword = "Admin123" }, false);
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var func = new Func<int>(() =>
                    {
                        var result = _accessLevelSdkService.GetAll();
                        return result.Count;
                    });
                    GetTest("查询门禁级别", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var result = _cardSdkService.GetAll();
                        var json = JsonConvert.SerializeObject(result.Where(x => Convert.ToInt64(x.CardNumber) < 1000), JsonUtil.JsonSettings);
                        return result.Count;
                    });
                    GetTest("查询卡", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var result = _cardHolderSdkService.GetAll();
                        return result.Count;
                    });
                    GetTest("查询持卡人", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var result = _hWDeviceSdkService.GetAll();
                        return result.Count;
                    });
                    GetTest("查询读卡器", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var result = _zoneTimeSdkService.GetAll();
                        return result.Count;
                    });
                    GetTest("查询时区", dispatcherAction, func);
                }
                //退出登录
                _userService.Logout();
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("引用查询完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void Search(Action<Action> dispatcherAction)
        {
            ScrollMessage.InsertTopShape("开始查询！！！！！！！！！！！！！！！！！！！");
            Task.Run(() =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var func = new Func<int>(() =>
                    {
                        var taskReslt = _winPakApi.GetAccessLevelsAsync(BaseUrl, Token);
                        var result = taskReslt.Result;
                        return result.Count;
                    });
                    GetTest("查询门禁级别", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var taskReslt = _winPakApi.GetCardsAsync(BaseUrl, Token);
                        var result = taskReslt.Result;
                        return result.Count;
                    });
                    GetTest("查询卡", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var taskReslt = _winPakApi.GetCardHoldersAsync(BaseUrl, Token);
                        var result = taskReslt.Result;
                        return result.Count;
                    });
                    GetTest("查询持卡人", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var taskReslt = _winPakApi.GetReadersAsync(BaseUrl, Token);
                        var result = taskReslt.Result;
                        return result.Count;
                    });
                    GetTest("查询读卡器", dispatcherAction, func);

                    func = new Func<int>(() =>
                    {
                        var taskReslt = _winPakApi.GetTimeZonesAsync(BaseUrl, Token);
                        var result = taskReslt.Result;
                        return result.Count;
                    });
                    GetTest("查询时区", dispatcherAction, func);
                }
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("查询完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        private void GetTest(string title, Action<Action> dispatcherAction, Func<int> execFunc)
        {
            var startTime = DateTime.Now;
            try
            {
                int total = execFunc.Invoke();

                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("{0}成功：timespan:{1}s total:{2} ", title, timeSpan, total);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("{0}失败： timespan:{1}s message:{2} ", title, timeSpan, ex.Message);
                });
            }
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                NotifyPropertyChanged();
            }
        }

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                NotifyPropertyChanged();
            }
        }

        public int StartCardNo
        {
            get { return _startCardNo; }
            set
            {
                _startCardNo = value;
                NotifyPropertyChanged();
            }
        }

        public int EndCardNo
        {
            get { return _endCardNo; }
            set
            {
                _endCardNo = value;
                NotifyPropertyChanged();
            }
        }


    }
}
