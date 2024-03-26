namespace IOTBack.Configuracao
{
    using System.IO.Ports;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class SmsService
    {
        private readonly SerialPort _serialPort;
      

        IConfiguration configuration = new ConfigurationBuilder()
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();

        public SmsService(IConfiguration configuration)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = configuration["portaGSM"];
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Open();
            this.configuration = configuration;
            _serialPort.WriteLine("AT\r");     
            _serialPort.WriteLine("AT+CSCS=\"IRA\"\r");
            _serialPort.WriteLine("AT+CMGF=1\r");
        }

        public async Task<string> SendSms(string phoneNumber, string message)
        {
            try
            {
                _serialPort.WriteLine($"AT+CMGS=\"{phoneNumber}\"\r");
                await Task.Delay(500);
                _serialPort.WriteLine($"{message}\x1A");
                await Task.Delay(2000);
                return "SMS enviado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
        }
       
 

    }
}