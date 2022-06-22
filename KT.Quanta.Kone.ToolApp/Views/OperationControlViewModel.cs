using AutoMapper;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Model.Kone;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KT.Quanta.Kone.ToolApp.Views
{
    public class OperationControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return true; }
        }

        private bool _isSubmiting;

        private ICommand _safelyColseHostCommandCommand;
        public ICommand SafelyColseHostCommand => _safelyColseHostCommandCommand ??= new DelegateCommand(SafelyColseHostAsync);

        private readonly IKoneApi _koneApi;
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public OperationControlViewModel(IKoneApi koneApi,
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            _koneApi = koneApi;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);
        }

        private void Error(Exception ex)
        {
            IsSubmiting = false;
        }

        private async void SafelyColseHostAsync()
        {
            IsSubmiting = true;

            await _koneApi.SafelyColseHostAsync();

            IsSubmiting = false;
        }

        public bool IsSubmiting
        {
            get
            {
                return _isSubmiting;
            }

            set
            {
                SetProperty(ref _isSubmiting, value);
            }
        }
    }
}
