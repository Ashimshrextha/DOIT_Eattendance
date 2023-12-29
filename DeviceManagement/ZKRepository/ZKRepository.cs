using DeviceManagement.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;
using static SystemStores.ENUMData.EnumGlobal;

namespace DeviceManagement.ZKRepository
{
    public class ZKRepository
    {
        private readonly CZKEM _cZKEM;
        private bool IsConnected;
        private readonly Logger _logger;

        public ZKRepository()
        {
            _cZKEM = new CZKEM();
            IsConnected = false;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public virtual bool IsDeviceConnected(string ip, int portNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    _logger.Error($"IP {ip} is null.....Please check your ip Address!!!!");
                }
                if (portNumber is 0)
                {
                    _logger.Error($"Port Number  {portNumber} is null.....Please check your Port Number!!!!");
                }
                IPAddress iPAddress = IPAddress.Parse(ip);
                IsConnected = _cZKEM.Connect_Net(ip, portNumber);
                if (IsConnected)
                {
                    _logger.Info($"Congratulations!!!...Your Device is Connected Having IP {ip} and Port Number{portNumber}");
                    return IsConnected;
                }
                else
                    _logger.Error($"Sorry Your Device Couldn't be connected having IP {ip} and Port Number {portNumber}..Please Try Again.");
                return IsConnected;
            }
            catch (Exception)
            {
                return IsConnected;
            }
        }

        public virtual bool IsDeviceConnected(int commPort, int machineNumber, int baudRate)
        {
            try
            {
                IsConnected = _cZKEM.Connect_Com(commPort, machineNumber, baudRate);
                switch (IsConnected)
                {
                    case true:
                        _logger.Info($"Congratulations.....Your Device is Connected having CommPort{commPort} :Machine Number {machineNumber} and BaudRate {baudRate}");
                        return IsConnected;
                    case false:
                        _logger.Error($"Sorry Your Device Could not be Connected having Comport{commPort}: Machine Number{machineNumber} and BaudRate {baudRate}");
                        return IsConnected;
                }
                return false;
            }
            catch (Exception)
            {
              //  _logger.ErrorException("Sorry Your Device Could not Connect due to this reason", exp);
                return IsConnected;
            }
        }

        public virtual bool IsDeviceConnected(int machineNumber)
        {
            try
            {
                IsConnected = _cZKEM.Connect_USB(machineNumber);
                switch (IsConnected)
                {
                    case true:
                        _logger.Info($"Congratulations.....Your Device is Connected having Machine Number {machineNumber}");
                        return IsConnected;
                    case false:
                        _logger.Error($"Sorry Your Device Could not be Connected having Machine Number{machineNumber}");
                        return IsConnected;
                }
                return false;
            }
            catch (Exception)
            {
               // _logger.LogException(LogLevel.Error, "Sorry Your Device is Connected due to this reason", exp.InnerException);
                return IsConnected;
            }
        }

        public ICollection<DeviceLogsModel> GetUserLogs(string ipAddress, int portNumber, int machineNumber, int comPort, int baudRate, ZKConnectVia zKConnectVia)
        {
            try
            {
                ICollection<DeviceLogsModel> _deviceLogs = new List<DeviceLogsModel>();
                switch (zKConnectVia)
                {
                    case ZKConnectVia.TCPIP:
                        IsConnected = IsDeviceConnected(ipAddress, portNumber);
                        break;
                    case ZKConnectVia.USB:
                        IsConnected = IsDeviceConnected(machineNumber);
                        break;
                    case ZKConnectVia.SERIALPORT:
                        IsConnected = IsDeviceConnected(comPort, machineNumber, baudRate);
                        break;
                }
                if (IsConnected)
                {
                    string IdEnroll = string.Empty;
                    int VerifyMode = 0;
                    int InOutMode = 0;
                    int Year = 0;
                    int Month = 0;
                    int Day = 0;
                    int Hour = 0;
                    int Minute = 0;
                    int Second = 0;
                    int WorkCode = 0;
                    _cZKEM.RegEvent(machineNumber, 65535);
                    _cZKEM.EnableDevice(machineNumber, false);
                    _logger.Info("Device Hasbeen Disabled for some time...after fetching logs from device it will enable automatically");
                    if (_cZKEM.ReadGeneralLogData(machineNumber))
                    {
                        _logger.Info("Started to Read the logs from Device......");
                        while (_cZKEM.SSR_GetGeneralLogData(machineNumber, out IdEnroll, out VerifyMode, out InOutMode, out Year, out Month, out Day, out Hour, out Minute, out Second, ref WorkCode))
                        {
                            _deviceLogs.Add(new DeviceLogsModel
                            {
                                IdEnroll = int.Parse(IdEnroll),
                                DeviceNumber = machineNumber,
                                PunchDateTime = $"{Year}-{Month}-{Day}{Hour}:{Minute}:{Second}",
                                VerifyMode = VerifyMode,
                                InOutMode = InOutMode
                            });
                        }
                        _logger.Info($"Total Record Found in Device {_deviceLogs.Count}");
                    }
                    _cZKEM.EnableDevice(machineNumber, true);
                    _logger.Info("Device Hasbeen enabled");
                }
                return _deviceLogs;
            }
            catch (Exception)
            {
                //_logger.ErrorException("You got error", exp.InnerException);
                return null;
            }
        }
    }
}
