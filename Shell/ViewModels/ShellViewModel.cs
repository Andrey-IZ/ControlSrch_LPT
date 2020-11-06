
namespace Shell.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Reflection;
    using Catel.Services;
    using Catel.Messaging;
    using Catel.MVVM;

    public class ShellViewModel : ViewModelBase
    {
        public IMessageMediator MessageMediator { get; set; }

        public ShellViewModel(IMessageMediator messageMediator, IMessageService messageService)
        {
            MessageMediator = messageMediator;

            CloseAppCommand = new Command(async () => await CloseViewModelAsync(true));
            HideAppCommand = new Command(() => Application.Current.MainWindow.WindowState = WindowState.Minimized);

            var versionStr = ((AssemblyInformationalVersionAttribute) Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof (AssemblyInformationalVersionAttribute), false)[0])
                .InformationalVersion;

            ShowAboutCommand =
                new TaskCommand(
                    async () =>
                        await messageService.ShowInformationAsync($"Версия ПО: \n\n {versionStr}", "О программе"));
        }

        public override string Title => "Тест - программа проверки";

        public Command CloseAppCommand { get; private set; }

        public Command HideAppCommand { get; private set; }

        public TaskCommand ShowAboutCommand { get; private set; }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
