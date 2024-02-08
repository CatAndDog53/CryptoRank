using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace CryptoRankApp.ViewModels
{
    public class ErrorDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Error";

        public event Action<IDialogResult> RequestClose;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { SetProperty(ref _errorMessage, value); }
        }
        public bool CanCloseDialog() => true;
        public ErrorDialogViewModel() { }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(CloseDialog));

        private void CloseDialog()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("message"))
            {
                ErrorMessage = parameters.GetValue<string>("message");
            }
        }
    }
}
