using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Lab.PrismEventAggregator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public ICommand OnFire { get; set; }

        public string Text { get { return _text; } set{ this.SetProperty(ref _text, value); } }
        private string _text;
        private IEventAggregator _eventAggregator;
        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            _eventAggregator = eventAggregator;


           


            Title = "Main Page";


            this.OnFire = new DelegateCommand(() =>
              {
                  eventAggregator.GetEvent<MyEvent>().Publish("我是訂閱通知");
              });

            eventAggregator.GetEvent<MyEvent>().Subscribe(EventFire);

        }
        public void EventFire(string arg)
        {
            this.Text = arg;
            _eventAggregator.GetEvent<MyEvent>().Unsubscribe(EventFire);
        }

    
    }

    public class MyEvent: PubSubEvent<string>
    {

    }
}
