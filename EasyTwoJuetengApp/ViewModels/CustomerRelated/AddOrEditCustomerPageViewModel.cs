using EasyTwoJuetengApp.Enumerations;
using EasyTwoJuetengApp.Helpers;
using EasyTwoJuetengApp.Helpers.JdsHelpers;
using EasyTwoJuetengApp.Interfaces;
using EasyTwoJuetengApp.Models.AuthRelated;
using EasyTwoJuetengApp.Models.CustomerRelated;
using Newtonsoft.Json;
using Plugin.Toast;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.ViewModels.CustomerRelated
{
    [QueryProperty(nameof(CustomerToEdit), nameof(CustomerToEdit))]
    [QueryProperty(nameof(FormAction), nameof(FormAction))]
    public class AddOrEditCustomerPageViewModel : BindableBase, IFormValidator
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _customerId;

        private string _customerToEdit;

        public string CustomerToEdit
        {
            get => _customerToEdit;
            set
            {
                SetProperty(ref _customerToEdit, Uri.UnescapeDataString(value ?? string.Empty));
                if (_customerToEdit != string.Empty || _customerToEdit != string.Empty)
                {
                    var customerToBeEdited = JsonConvert.DeserializeObject<CustomerReadDto>(_customerToEdit);
                    customerToBeEdited.CellphoneNumber = customerToBeEdited.CellphoneNumber.Substring(3);
                    _customerId = customerToBeEdited.Id;
                    FirstName = customerToBeEdited.FirstName;
                    MiddleName = customerToBeEdited.MiddleName;
                    LastName = customerToBeEdited.LastName;
                    CellphoneNumber = customerToBeEdited.CellphoneNumber;
                    EmailAddress = customerToBeEdited.EmailAddress;
                    RaisePropertyChanged(nameof(FirstName));
                    RaisePropertyChanged(nameof(MiddleName));
                    RaisePropertyChanged(nameof(LastName));
                    RaisePropertyChanged(nameof(CellphoneNumber));
                    RaisePropertyChanged(nameof(EmailAddress));
                    _cacheCustomerInfo = customerToBeEdited;
                    RaisePropertyChanged(nameof(_cacheCustomerInfo));
                }
            }
        }

        private string _formAction;

        public string FormAction
        {
            get => _formAction;
            set
            {
                SetProperty(ref _formAction, Uri.UnescapeDataString(value ?? string.Empty));
                var action = (_formAction == "Add") ? "Save" : "Edit";
                _title = $"{action} Customer";
                RaisePropertyChanged();
            }
        }

        private CustomerSaveDto _customerSave = new CustomerSaveDto();

        private CustomerReadDto _cacheCustomerInfo = new CustomerReadDto();

        public string FirstName
        {
            get => _customerSave.FirstName ?? "";
            set
            {
                _customerSave.FirstName = value;
                ValidateChanges();
            }
        }

        public string LastName
        {
            get => _customerSave.LastName ?? "";
            set
            {
                _customerSave.LastName = value;
                ValidateChanges();
            }
        }

        public string MiddleName
        {
            get => _customerSave.MiddleName ?? "";
            set
            {
                _customerSave.MiddleName = value;
                ValidateChanges();
            }
        }

        public string CellphoneNumber
        {
            get => _customerSave.CellphoneNumber ?? "";
            set
            {
                _customerSave.CellphoneNumber = value;
                ValidateChanges();
            }
        }

        public string EmailAddress
        {
            get => _customerSave.EmailAddress ?? "";
            set
            {
                _customerSave.EmailAddress = value;
                ValidateChanges();
            }
        }

        private bool _isEditable;

        private readonly IPostAsync<CustomerSaveDto, JdsResponse> _customerPostService;
        private readonly IPutAsync<CustomerSaveDto, JdsResponse> _customerPutService;

        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
            }
        }

        public AddOrEditCustomerPageViewModel(IPostAsync<CustomerSaveDto, JdsResponse> customerPostService,
            IPutAsync<CustomerSaveDto, JdsResponse> customerPutService)
        {
            try
            {
                _title = "Add Customer";
            }
            catch (Exception ex)
            {
                throw;
            }
            _customerPostService = customerPostService;
            _customerPutService = customerPutService;
        }

        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
         {
             await UIHelper.Load(SaveCustomerTask(), "Adding Customer...");
         });

        public DelegateCommand UpdateCommand => new DelegateCommand(async () =>
        {
            await UIHelper.Load(UpdateCustomerTask(), "Updating Customer...");
        });

        private async Task SaveCustomerTask()
        {
            var customerPostRequest = await _customerPostService.PostAsync(_customerSave);
            if (customerPostRequest.StatusCode == HttpStatusCode.Created)
            {
                App.SetPreviousPageReloadableToTrue();
                await Shell.Current.Navigation.PopAsync();
                CrossToastPopUp.Current.ShowToastSuccess("Customer Added Successfully!");
            }
            else
                UIHelper.ErrorRequestHandler(customerPostRequest, "Having Trouble Adding Customer");
        }

        private async Task UpdateCustomerTask()
        {
            var customerPostRequest = await _customerPutService.PutAsync(_customerId, _customerSave);
            if (customerPostRequest.StatusCode == HttpStatusCode.OK)
            {
                App.SetPreviousPageReloadableToTrue();
                await Shell.Current.Navigation.PopAsync();
                CrossToastPopUp.Current.ShowToastSuccess("Customer Edited Successfully!");
            }
            else
                UIHelper.ErrorRequestHandler(customerPostRequest, "Having Trouble Adding Customer");
        }

        public void ValidateChanges()
        {
            if (_formAction == ActionType.Edit.ToString())
            {
                if (_customerSave.FirstName.ToLower() != _cacheCustomerInfo.FirstName.ToLower() ||
                    _customerSave.MiddleName.ToLower() != _cacheCustomerInfo.MiddleName.ToLower() ||
                    _customerSave.LastName.ToLower() != _cacheCustomerInfo.LastName.ToLower() ||
                    _customerSave.EmailAddress.ToLower() != _cacheCustomerInfo.EmailAddress.ToLower() ||
                    _customerSave.CellphoneNumber != _cacheCustomerInfo.CellphoneNumber)
                {
                    if (string.IsNullOrEmpty(_customerSave.FirstName) || string.IsNullOrEmpty(_customerSave.MiddleName) ||
                       string.IsNullOrEmpty(_customerSave.LastName) || string.IsNullOrEmpty(_customerSave.CellphoneNumber) ||
                        string.IsNullOrEmpty(_customerSave.EmailAddress))
                        _isEditable = false;
                    else
                        _isEditable = true;
                }
                else
                    _isEditable = false;

                RaisePropertyChanged(nameof(IsEditable));
            }
        }
    }
}