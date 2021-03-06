// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VibrationDevice.cs" company="In The Hand Ltd">
//   Copyright (c) 2016-17 In The Hand Ltd, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
using Android.Content;
using Android.OS;
#elif __IOS__
using AudioToolbox;
using Foundation;
#endif

using System;

namespace InTheHand.Phone.Devices.Notification
{
    /// <summary>
    /// Vibrates the phone.
    /// </summary>
    /// <remarks>Phone devices include a vibration controller.
    /// Your app can vibrate the phone for up to 5 seconds to notify the user of an important event.
    /// Use the vibration feature in moderation.
    /// Do not rely on the vibration feature for critical notifications, because the user can disable vibration
    /// <para>To test an app that uses the vibration controller effectively, you have to test it on a physical device.
    /// The emulator cannot simulate vibration and does not provide any audible or visual feedback that vibration is occurring.
    /// <para>An app that is running in the background cannot vibrate the phone.</para>
    /// If your code tries to use vibration while the app is running in the background, nothing happens, but no exception is raised.
    /// If you want to vibrate the phone while your app is running in the background, you have to implement a toast notification.</para></remarks>
    /// <remarks>
    /// <para/><list type="table">
    /// <listheader><term>Platform</term><description>Version supported</description></listheader>
    /// <item><term>Android</term><description>Android 4.4 and later</description></item>
    /// <item><term>iOS</term><description>iOS 9.0 and later</description></item>
    /// <item><term>Tizen</term><description>Tizen 3.0</description></item>
    /// <item><term>Windows UWP</term><description>Windows 10 Mobile</description></item>
    /// <item><term>Windows Phone Store</term><description>Windows Phone 8.1 or later</description></item>
    /// <item><term>Windows Phone Silverlight</term><description>Windows Phone 8.0 or later</description></item></list>
    /// </remarks>
    public sealed class VibrationDevice
    {
        private static VibrationDevice  s_default = null;

        /// <summary>
        /// Gets an instance of the VibrationDevice class.
        /// </summary>
        /// <returns></returns>
        public static VibrationDevice GetDefault()
        {
            if(s_default == null)
            {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP || WINDOWS_PHONE
                s_default = new VibrationDevice();
#elif TIZEN
                if (Tizen.System.RuntimeInformation.GetStatus<bool>(Tizen.System.RuntimeInformationKey.Vibration))
                {
                    if (Tizen.System.Vibrator.NumberOfVibrators > 0)
                    {
                        s_default = new VibrationDevice();
                    }
                }

#elif WINDOWS_UWP
                if(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice"))
                {
                    s_default = new Notification.VibrationDevice();
                }
#endif
            }

            return s_default;
        }

#if __ANDROID__
        private Vibrator _vibrator;
#elif TIZEN
        private Tizen.System.Vibrator _vibrator;
#elif WINDOWS_UWP || WINDOWS_PHONE_APP || WINDOWS_PHONE
        private Windows.Phone.Devices.Notification.VibrationDevice _device;
#endif
        private VibrationDevice()
        {
#if __ANDROID__
            _vibrator = (Vibrator)Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetSystemService(Context.VibratorService);
#elif TIZEN
            _vibrator = Tizen.System.Vibrator.Vibrators[0];
#elif WINDOWS_UWP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            _device = Windows.Phone.Devices.Notification.VibrationDevice.GetDefault();
#endif
        }

        /// <summary>
        /// Vibrates the phone for the specified duration (from 0 to 5 seconds).
        /// </summary>
        /// <param name="duration">The duration (from 0 to 5 seconds) for which the phone vibrates. A value that is less than 0 or greater than 5 raises an exception. Ignored on iOS.</param>
        public void Vibrate(TimeSpan duration)
        {
            if(duration < TimeSpan.Zero || duration.TotalSeconds > 5.0)
            {
                throw new ArgumentOutOfRangeException("duration");
            }

#if __ANDROID__
            if (_vibrator.HasVibrator)
            {
                _vibrator.Vibrate(Convert.ToInt64(duration.TotalMilliseconds));
            }
#elif __IOS__
            SystemSound.Vibrate.PlaySystemSound();
#elif TIZEN
            _vibrator.Vibrate(Convert.ToInt32(duration.TotalMilliseconds), 100);
#elif WINDOWS_UWP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            _device.Vibrate(duration);
#elif WINDOWS_PHONE
            Microsoft.Devices.VibrateController.Default.Start(duration);
#else
#endif
        }

        /// <summary>
        /// Stops the vibration of the phone.
        /// </summary>
        public void Cancel()
        {
#if __ANDROID__
            _vibrator.Cancel();
#elif __IOS__
            SystemSound.Vibrate.Close();
#elif TIZEN
            _vibrator.Stop();
#elif WINDOWS_UWP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            _device.Cancel();
#elif WINDOWS_PHONE
            Microsoft.Devices.VibrateController.Default.Stop();
#endif
        }
    }
}