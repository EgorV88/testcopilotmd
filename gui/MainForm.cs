using CoPilotMD.GuiClient;

namespace gui
{
    public partial class MainForm : Form
    {
        private ClientService clientService;
        public MainForm()
        {
            InitializeComponent();

            clientService = new ClientService();
            clientService.onLogRecieved += ClientService_onLogRecieved;
            clientService.StartService();
        }

        private void ClientService_onLogRecieved(object? sender, CoPilodMD.Core.pipes.ServiceMessage msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(ClientService_onLogRecieved, sender, msg);
            }
            else
            {
                txtLogs.Text += $"[{DateTime.UtcNow}]{msg.Sender}: {msg.Message}{Environment.NewLine}";
            }
        }
    }
}
