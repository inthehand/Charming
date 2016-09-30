﻿//-----------------------------------------------------------------------
// <copyright file="AnalyticsVersionInfo.cs" company="In The Hand Ltd">
//     Copyright © 2016 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#if WINDOWS_UWP
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(Windows.System.Profile.AnalyticsVersionInfo))]
#else

using System;
using System.Reflection;

namespace Windows.System.Profile
{
    /// <summary>
    /// Provides version information about the device family.
    /// </summary>
    public sealed class AnalyticsVersionInfo
    {
        private object _native;

        internal AnalyticsVersionInfo(object native)
        {
            _native = native;
        }

        /// <summary>
        /// Gets a string that represents the type of device the application is running on.
        /// </summary>
        public string DeviceFamily
        {
            get
            {
                if(_native != null)
                {
                    return _native.GetType().GetRuntimeProperty("DeviceFamily").GetValue(_native).ToString();
                }
#if __ANDROID__
                return "Android";
#elif __IOS__
                return "Apple.Mobile";
#elif __MAC__
                return "Apple.Desktop";
#elif WINDOWS_APP || WIN32
                return "Windows.Desktop";
#elif WINDOWS_PHONE_APP || WINDOWS_PHONE
                return "Windows.Mobile";
#else
                return string.Empty;
#endif
            }
        }

        /// <summary>
        /// Gets the version within the device family.
        /// </summary>
        public string DeviceFamilyVersion
        {
            get
            {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE || WIN32
                return global::System.Environment.OSVersion.Version.ToString();
#else
                if (_native != null)
                {
                    return _native.GetType().GetRuntimeProperty("DeviceFamilyVersion").GetValue(_native).ToString();
                }

                return string.Empty;
#endif
            }
        }
    }
}
#endif