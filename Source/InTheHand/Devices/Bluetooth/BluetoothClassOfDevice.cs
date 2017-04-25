﻿//-----------------------------------------------------------------------
// <copyright file="BluetoothClassOfDevice.cs" company="In The Hand Ltd">
//   Copyright (c) 2017 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

namespace InTheHand.Devices.Bluetooth
{
    /// <summary>
    /// Provides functionality to determine the Bluetooth Class Of Device (Bluetooth COD) information for a device.
    /// </summary>
    /// <remarks>
    /// <para/><list type="table">
    /// <listheader><term>Platform</term><description>Version supported</description></listheader>
    /// <item><term>Windows UWP</term><description>Windows 10</description></item>
    /// <item><term>Windows Store</term><description>Windows 8.1 or later</description></item>
    /// <item><term>Windows Phone Store</term><description>Windows Phone 8.1 or later</description></item>
    /// <item><term>Windows Phone Silverlight</term><description>Windows Phone 8.1 or later</description></item></list>
    /// </remarks>
    public sealed partial class BluetoothClassOfDevice
    {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
        private Windows.Devices.Bluetooth.BluetoothClassOfDevice _classOfDevice;
                
        private BluetoothClassOfDevice(Windows.Devices.Bluetooth.BluetoothClassOfDevice classOfDevice)
        {
            _classOfDevice = classOfDevice;
        }

        public static implicit operator Windows.Devices.Bluetooth.BluetoothClassOfDevice(BluetoothClassOfDevice classOfDevice)
        {
            return classOfDevice._classOfDevice;
        }

        public static implicit operator BluetoothClassOfDevice(Windows.Devices.Bluetooth.BluetoothClassOfDevice classOfDevice)
        {
            return new BluetoothClassOfDevice(classOfDevice);
        }
#else
        private uint _rawValue;

        internal BluetoothClassOfDevice(uint rawValue)
        {
            _rawValue = rawValue;
        }
#endif

        /// <summary>
        /// Gets the Bluetooth Class Of Device information, represented as an integer value.
        /// </summary>
        /// <value>The Bluetooth Class Of Device information, represented as a raw integer value.</value>
        public uint RawValue
        {
            get
            {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
                return _classOfDevice.RawValue;
#else
                return _rawValue;
#endif
            }
        }

        /// <summary>
        /// Gets the service capabilities of the device.
        /// </summary>
        public BluetoothServiceCapabilities ServiceCapabilities
        {
            get
            {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
                return (BluetoothServiceCapabilities)((int)_classOfDevice.ServiceCapabilities);
#else
                return (BluetoothServiceCapabilities)((_rawValue & 0xFF000000) >>12);
#endif
            }
        }
    }
}