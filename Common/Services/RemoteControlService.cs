using System.Collections.Generic;
using Catel.IoC;
using Catel.Services;
using Common.Models.Interfaces;
using Common.Services.Interfaces;
using Drivers.LptIO;
using Drivers.LptIO.lib;
using System.Threading.Tasks;

namespace Common.Services
{
    public class RemoteControlService : ServiceBase, IRemoteControlService
    {
        public IGun Gun1 { get; }
        public IGun Gun2 { get; }
        public IExitsAk ExitsAk { get; }
        public ISynthesizer Syn1 { get; }
        public ISynthesizer Syn2 { get; }

        private readonly IStatusBar _statusBar;
        private readonly IFrequencyModulation _modulation;
        public IRebuildFrequency RebuildFreq { get; }
        private readonly ICaptureFapch _captureFapch;

        private bool _isStartWork;
        private readonly object _lockFile = new object();
        private readonly ILtpRemoteControl _remoteControl;
        private TypeLptPort _currentLptPort;

        public RemoteControlService()
        {
            Gun1 = ServiceLocator.Default.ResolveType<ICodeManipulationRepository>().GetGunById(1);
            Gun2 = ServiceLocator.Default.ResolveType<ICodeManipulationRepository>().GetGunById(2);
            Syn1 = ServiceLocator.Default.ResolveType<ISynthesizerRepository>().GetSynthesizerById(1);
            Syn2 = ServiceLocator.Default.ResolveType<ISynthesizerRepository>().GetSynthesizerById(2);
            ExitsAk = ServiceLocator.Default.ResolveType<IRangeReproductionFRepository>().GetExitsAk();
            RebuildFreq = ServiceLocator.Default.ResolveType<IRebuildFrequencyRepository>().GetRebuildFrequency();
            _captureFapch = ServiceLocator.Default.ResolveType<ICaptureFapchRepository>().GetCaptureFapch();
            _modulation = ServiceLocator.Default.ResolveType<IFrequencyModulationRepository>().GetFrequencyModulation();
            _statusBar = ServiceLocator.Default.ResolveType<IStatusBarRepository>().GetStatusBar();

            _remoteControl = new LptRemoteControl();
            _statusBar.LptAddressList = _remoteControl.GetActualPortAddressList();
            PortAddressList = _statusBar.LptAddressList;
            _statusBar.CurrentLpt = _remoteControl.CurrentAddressPort;
            CurrentLptPort = _remoteControl.CurrentTypeLptPort;
            ReadFapch1();
            ReadFapch2();
            Run();
        }

        #region Implementation of IRemoteControlService

        public bool SetPortAddress(int portAddress)
        {
            return _remoteControl.SetPortAddress(portAddress);
        }
        //private DispatcherTimer _timerOld;
        private readonly bool IsRunExchange = true;

        public void Run()
        {
            //_timerOld = new DispatcherTimer();
            //_timerOld.Interval = new TimeSpan(0, 0, 1);
            //_timerOld.Tick += (s, e) => { SendBunchToExchange(null); };
            //_timerOld.Start();

            var task = Task.Factory.StartNew(async () =>
            {
                while (IsRunExchange)
                {
                    SendBunchToExchange(null);
                    await Task.Delay(1);
                }
            });
        }

        /// <summary>
        /// Proccess of communication for remote control
        /// </summary>
        /// <param name="state"></param>
        private void SendBunchToExchange(object state)
        {
            LinkStatus = _statusBar.IsLinkOn = _remoteControl.TestLink();   // ----- Проверка канала связи --------
            KnpStatus = _statusBar.IsKnp = _remoteControl.ReadKnp();        // -------- читаю КНП --------
            Ak1Status = ExitsAk.IsOnAk1 = _remoteControl.ReadAk1();         // -------- читаю АК1 --------
            Ak2Status = ExitsAk.IsOnAk2 = _remoteControl.ReadAk2();         // -------- читаю АК2 --------

            if (LinkStatus && _isStartWork)
            {
                if (!RebuildFreq.IsAuto)
                    _remoteControl.ShutdownSrch(); // выключение СРЧ

                // ***** 1.Установил код     управления *****
                if (Gun1.IsActiveGunState) _remoteControl.WriteGun1(Gun1.CurrentGunValue);
                if (Syn1.IsActiveState) _remoteControl.WriteSyn1(Syn1.CurrentF);
                if (Gun2.IsActiveGunState) _remoteControl.WriteGun2(Gun2.CurrentGunValue);
                if (Syn2.IsActiveState) _remoteControl.WriteSyn2(Syn2.CurrentF);

                // ********** 2. Установил параметры модуляции **********
                _remoteControl.WriteParamModulation(_modulation.AmplitudeCode, _modulation.IsNoise,
                    _modulation.AmplitudeCodeMseq, _modulation.IsMseq);

                // ********** 3. Установил коммутаторы **********
                _remoteControl.WriteCommutators(_modulation.IsOnUk1, _modulation.IsOnUk2);

                // ********** 4. Режим работы **********
                _remoteControl.WriteModeWork(new RemoteModeWork
                {
                    IsExit1 = ExitsAk.IsCheckedExit1,
                    IsExit2 = ExitsAk.IsCheckedExit2,
                    IsExit1HighLimit = ExitsAk.IsHighThresholdExit1,
                    IsExit1LowLimit = ExitsAk.IsLowThresholdExit1,
                    IsExit2HighLimit = ExitsAk.IsHighThresholdExit2,
                    IsExit2LowLimit = ExitsAk.IsLowThresholdExit2,
                    IsGun1 = Gun1.IsActiveGunState,
                    IsGun2 = Gun2.IsActiveGunState,
                    IsSyn1 = Syn1.IsActiveState,
                    IsSyn2 = Syn2.IsActiveState,
                });
                _isStartWork = false;
            }
        }

        public IEnumerable<int> PortAddressList { get; }
        public bool LinkStatus { get; private set; }
        public bool KnpStatus { get; private set; }
        public bool Ak2Status { get; private set; }
        public bool Ak1Status { get; private set; }
        public bool CaptureFapch1Status
        {
            get { return ReadFapch1(); }
        }

        private bool ReadFapch1()
        {
            return _captureFapch.IsCaptureFapch1 = _remoteControl.ReadCaptureFapch1();   /* -------- читаю Захват ФАПЧ1 --------*/
        }

        public bool CaptureFapch2Status
        {
            get { return ReadFapch2(); }
        }

        private bool ReadFapch2()
        {
            return _captureFapch.IsCaptureFapch2 = _remoteControl.ReadCaptureFapch2();   /* -------- читаю Захват ФАПЧ2 --------*/
        }

        public TypeLptPort CurrentLptPort
        {
            get { return _currentLptPort; }
            set { _currentLptPort = _remoteControl.CurrentTypeLptPort = value; }
        }

        public void SetStartWork()
        {
            lock(_lockFile)
            {
                _isStartWork = true;
            }
        }

        public void WriteDebugData(byte address, ushort data)
        {
            _remoteControl.WriteDebugData(address, data);
        }

        #endregion
    }
}