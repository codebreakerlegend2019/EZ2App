using EasyTwoJuetengApp.Enumerations;
using EasyTwoJuetengApp.Helpers;
using EasyTwoJuetengApp.Helpers.JdsHelpers;
using EasyTwoJuetengApp.Interfaces;
using EasyTwoJuetengApp.Models.CustomerRelated;
using Newtonsoft.Json;
using Plugin.Toast;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.ViewModels.CustomerRelated
{
    public class CustomerPageViewModel : CustomBaseViewModel, INavigationAware
    {
        public bool IsPageEnabled { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value, nameof(IsLoading));
            }
        }

        private bool _isCustomerEmpty;

        public bool IsCustomerEmpty
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isCustomerEmpty, value, nameof(IsCustomerEmpty));
            }
        }

        public List<CustomerReadDto> Customers { get; set; }
        private List<CustomerReadDto> _customers { get; set; }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isRefreshing, value, nameof(IsRefreshing));
            }
        }

        private readonly IGetAsync<JdsMultiReponse<CustomerReadDto>> _customerGetAsync;

        private string _keyWord;

        public string KeyWord
        {
            get => _keyWord ?? "";
            set
            {
                SetProperty(ref _keyWord, value, nameof(KeyWord));
                filterCustomers(value.ToLower());
            }
        }

        public CustomerPageViewModel(IGetAsync<JdsMultiReponse<CustomerReadDto>> customerGetAsync)
        {
            Title = "Customers";
            _customerGetAsync = customerGetAsync;
            LoadCustomersCommand.Execute();
        }

        public DelegateCommand<CustomerReadDto> CallCommand => new DelegateCommand<CustomerReadDto>((CustomerReadDto customer) =>
        {
            try
            {
                PhoneDialer.Open(customer.CellphoneNumber);
            }
            catch (ArgumentNullException anEx)
            {
                CrossToastPopUp.Current.ShowToastError("Invalid Cellphone Number");
            }
            catch (FeatureNotSupportedException ex)
            {
                CrossToastPopUp.Current.ShowToastError("Phone Dialer is not supported on this device.");
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError("Cannot Open Dialer Please Try Again");
            }
        });

        public DelegateCommand<CustomerReadDto> GoToEditCommand => new DelegateCommand<CustomerReadDto>(async (CustomerReadDto customer) =>
        {
            try
            {
                var customerInJson = JsonConvert.SerializeObject(customer);
                await Shell.Current.GoToAsync($"/AddOrEditCustomerPage?CustomerToEdit={customerInJson}&FormAction={ActionType.Edit}");
            }
            catch (Exception ex)
            {
                throw;
            }
        });

        private DelegateCommand LoadCustomersCommand => new DelegateCommand(() =>
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(500);
                await LoadCustomers();
            });
        });

        public DelegateCommand<CustomerReadDto> EmailCommand => new DelegateCommand<CustomerReadDto>(async (CustomerReadDto customer) =>
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = string.Empty,
                    Body = string.Empty,
                    To = new List<string>() { customer.EmailAddress }
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                CrossToastPopUp.Current.ShowToastError("Email is not supported on this device.");
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError("Cannot open any Emailing App Please Try Again");
            }
        });

        public DelegateCommand<CustomerReadDto> SmsCommand => new DelegateCommand<CustomerReadDto>(async (CustomerReadDto customer) =>
        {
            try
            {
                var message = new SmsMessage(string.Empty, new[] { customer.CellphoneNumber });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                CrossToastPopUp.Current.ShowToastError("SMS is not supported on this device.");
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError("Cannot Open Any SMS App Please Try Again");
            }
        });

        private void filterCustomers(string searchData)
        {
            if (_customers != null)
            {
                var customers = new List<CustomerReadDto>();
                foreach (var customer in _customers)
                {
                    var searchMatch = customer.FullName.ToLower().Contains(searchData)
                       || customer.CellphoneNumber.ToLower().Contains(searchData)
                       || customer.EmailAddress.ToLower().Contains(searchData);
                    if (searchMatch)
                        customers.Add(customer);
                }
                Customers = customers;
                RaisePropertyChanged(nameof(Customers));
            }
        }

        private async Task LoadCustomers()
        {
            _isLoading = true;
            Customers = App.DummyCustomers;
            RaisePropertyChanged(nameof(Customers));
            var customerRequest = await _customerGetAsync.Get();
            if (customerRequest.StatusCode == HttpStatusCode.OK)
            {
                _customers = customerRequest.Data;
                Customers = _customers;
                RaisePropertyChanged(nameof(Customers));
            }
            else
            {
                Customers = null;
                RaisePropertyChanged(nameof(Customers));
                _isCustomerEmpty = true;
                UIHelper.ErrorRequestHandler(customerRequest, "Having Trouble Loading Customers");
            }
            _isLoading = false;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public DelegateCommand RefreshCommand => new DelegateCommand(async () =>
      {
          try
          {
              _isRefreshing = true;
              await LoadCustomers();
              _isRefreshing = false;
          }
          catch (Exception ex)
          {
          }
      });

        public DelegateCommand GoToAddCustomer => new DelegateCommand(async () =>
        {
            await Shell.Current.GoToAsync($"/AddOrEditCustomerPage?FormAction={ActionType.Add}");
        });
    }
}