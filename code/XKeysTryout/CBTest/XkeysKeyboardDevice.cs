using PIEHid64Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;

namespace CBTest
{
    public class XkeysKeyboardDevice : IDisposable, PIEDataHandler, PIEErrorHandler
    {
        #region Properties

        const int CONSUMER_USAGE_PAGE_INPUT_AND_OUTPUT_HID_USAGE=1;
        const int PID_XK80 = 1089;
        const int PID_XK128 = 1227;
        const string PIE_XKEYS_HID = @"HID\VID_05F3&PID_0441";   // TODO: Verify XK128 has the same HID

        public delegate void DeviceClosedDelegate();

        private PIEDevice _device;

        CompositeDisposable _disposables= new CompositeDisposable();

        public XkeysKeyboardDeviceStatus Status { get; set; }
        public SupportedXkeysKeyboardDevice DeviceType { get; set; }

        public event DeviceClosedDelegate DeviceClosed;

        #endregion


        #region Open/Close Device

        public XkeysKeyboardDeviceStatus TryOpen(SupportedXkeysKeyboardDevice deviceEnum)
        {
            DeviceType = deviceEnum;
            if(_device!=null)
            {
                Dispose();
            }

            OpenDevice();

            CheckStatus();

            if (Status== XkeysKeyboardDeviceStatus.Opened)
            {
                var disp = Observable.Interval(TimeSpan.FromSeconds(3)).Subscribe(s => EnsureSofwareRunning());
                _disposables.Add(disp);

               
            }
            return Status;    
        }



        public void TryClose()
        {
            if(_device==null)
            {
                Status = XkeysKeyboardDeviceStatus.HardwareNotFound;
                return;
            }
            _device.CloseInterface();
            _disposables.Dispose(); // stop Ensure if Close or it will reset to open
            Status = XkeysKeyboardDeviceStatus.Closed;
            if(DeviceClosed!=null)
            {
                DeviceClosed();
            }
        }



        #endregion

        #region Ensure Software Running upon Hardware restore

        private void CheckStatus()
        {
            bool found = PIEDevice.EnumeratePIE().Any(p => p.HidUsage == CONSUMER_USAGE_PAGE_INPUT_AND_OUTPUT_HID_USAGE && p.Pid == GetPid());
            if (!found)
            {
                Status = XkeysKeyboardDeviceStatus.HardwareNotFound;
                return;
            }
            Status = XkeysKeyboardDeviceStatus.HardwareFound;

            FieldInfo fiConn = typeof(PIEDevice).GetField("connected", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiDataThread = typeof(PIEDevice).GetField("dataThreadActive", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiErrThread = typeof(PIEDevice).GetField("errorThreadActive", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiReadThread = typeof(PIEDevice).GetField("readThreadActive", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiWriteThread = typeof(PIEDevice).GetField("writeThreadActive", BindingFlags.Instance | BindingFlags.NonPublic);

            bool connected = fiConn.GetValue(_device).ToString().ToLower() == "true";
            bool dataThreadActive = fiDataThread.GetValue(_device).ToString().ToLower() == "true";
            bool errorThreadActive = fiErrThread.GetValue(_device).ToString().ToLower() == "true";
            bool readThreadActive = fiReadThread.GetValue(_device).ToString().ToLower() == "true";
            bool writeThreadActive = fiWriteThread.GetValue(_device).ToString().ToLower() == "true";
            if (connected && dataThreadActive && errorThreadActive && readThreadActive && writeThreadActive)
            {
                Status = XkeysKeyboardDeviceStatus.Opened;
            }
            else
            {
                Status = XkeysKeyboardDeviceStatus.SoftwareError;
            }
        }

        // Note that Unplug and replug in will create sofware Error and can be restore by this function.
        // But BA requested, unplug should be detected through WMI and close down software, then reopen by user. So this function is not useful for unplug.
        // Still this function is here to make sure no other situation occures where new up/reset are needed.
        // Tests showed PC sleep will not stop sofware.
        private void EnsureSofwareRunning()
        {
            CheckStatus();
            if (Status == XkeysKeyboardDeviceStatus.SoftwareError)
            {
                ResetInterface();  // try reset first, if failed re-open where _device new up against hardware.
                CheckStatus();
                if (Status == XkeysKeyboardDeviceStatus.SoftwareError)
                {
                    OpenDevice();
                }
            }
        }

        private void OpenDevice()
        {
            _device = PIEDevice.EnumeratePIE().FirstOrDefault(p => p.HidUsage == CONSUMER_USAGE_PAGE_INPUT_AND_OUTPUT_HID_USAGE && p.Pid == GetPid());
            if (_device == null) return;

            ResetInterface();
        }

        private void ResetInterface()
        {
            if (_device == null) return;
            _device.CloseInterface();
            _device.SetupInterface();
            _device.SetDataCallback(this);
            _device.SetErrorCallback(this);
        }

        #endregion

        #region callback hooks

        public void HandlePIEHidData(byte[] data, PIEDevice sourceDevice, int error)
        {
            if (Status != XkeysKeyboardDeviceStatus.Opened)
            {
                return;  // just to make sure Data only go through when both hardware/software are in good condition.
            }
            Debug.WriteLine("JQD Handle Data" + GetKeyCode(data));

        }

        public void HandlePIEHidError(PIEDevice sourceDevices, int error)
        {
            if(error==309 || error==307) // e.g unplug
            {
                TryClose();
                Status = XkeysKeyboardDeviceStatus.Closed;
            }

            Debug.WriteLine("JQD Handle Err "+error);
        }

        #endregion

        #region Splat Message Processing

        // data example XK-80:  
        //  030 1  000000000 18180100000000000000000   rowByte=1, skip 3, take 13
        //  030 64 000000000 1211781680000000000000000 rowByte=64 skip 3, take 13
        // data example XK-128:  Take 19

        public string GetKeyCode(byte[] inputData)
        {
            var keyData = inputData.Take(GetBitToTake()).Skip(3); 
            if (keyData.All(b => b == 0)) return "[KeyUp]";

            int i = 0;
            int keyInt = 0;

            foreach (var b in keyData)
            {
                if (b != 0)
                {
                     keyInt = i + RowOffset(b);
                }
                i++;
            }
            return (keyInt+1).ToString();

        }

        #region Row Offset and Bit to take

        int GetBitToTake()
        {
            switch (DeviceType)
            {
                case SupportedXkeysKeyboardDevice.XK80:
                    return 13;
                case SupportedXkeysKeyboardDevice.XK128:
                    return 19;
                default:
                    return 0;
            }
        }

        public int RowOffset(byte rowByte)
        {
            if (DeviceType == SupportedXkeysKeyboardDevice.XK80)
            {
                switch (rowByte)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 10;
                    case 4:
                        return 20;
                    case 8:
                        return 30;
                    case 16:
                        return 40;
                    case 32:
                        return 50;
                    case 64:
                        return 60;
                    case 128:
                        return 70;
                }
            }

            if (DeviceType == SupportedXkeysKeyboardDevice.XK128)
            {
                switch (rowByte)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 16;
                    case 4:
                        return 32;
                    case 8:
                        return 48;
                    case 16:
                        return 64;
                    case 32:
                        return 80;
                    case 64:
                        return 96;
                    case 128:
                        return 112;
                }
            }

            return 0;
        }

        #endregion

        #endregion

        #region IDispoable

        public void Dispose()
        {
            _disposables.Dispose();
            TryClose();
            _device = null;
            Status = XkeysKeyboardDeviceStatus.Closed;
        }

        #endregion

        #region Misc

        int GetPid()
        {
            switch (DeviceType)
            {
                case SupportedXkeysKeyboardDevice.XK80:
                    return PID_XK80;
                case SupportedXkeysKeyboardDevice.XK128:
                    return PID_XK128;
                default:
                    return -1;
            }
        }

        #endregion

        void WriteReadCustomData()
        {
            
        }
    }

    #region Enum Def

    public enum XkeysKeyboardDeviceStatus
    {
        HardwareNotFound,
        HardwareFound,
        Opened,
        Closed,
        SoftwareError,
        HardwareUnplugged // TODO use WMI dectect USB Unplug.
    }

    public enum SupportedXkeysKeyboardDevice
    {
        XK80,
        XK128
    }

   #endregion
}
