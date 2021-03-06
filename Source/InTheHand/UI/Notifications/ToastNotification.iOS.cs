﻿//-----------------------------------------------------------------------
// <copyright file="ToastNotification.iOS.cs" company="In The Hand Ltd">
//     Copyright © 2016-17 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation;
using UserNotifications;

namespace InTheHand.UI.Notifications
{
    partial class ToastNotification
    {
        internal UNMutableNotificationContent _content;
        internal UNNotificationRequest _request;

        internal ToastNotification(UNMutableNotificationContent content)
        {
            _content = content;
        }
        
        private string GetGroup()
        {
            return _content.CategoryIdentifier;
        }

        private void SetGroup(string value)
        {
            _content.CategoryIdentifier = value;
        }

        private bool GetSuppressPopup()
        {
            if (_content.UserInfo.ContainsKey(new NSString("SuppressPopup")))
            {
                return ((NSNumber)_content.UserInfo["SuppressPopup"]).BoolValue;
            }

            return false;
        }

        private void SetSuppressPopup(bool value)
        {
            _content.UserInfo.SetValueForKey(NSNumber.FromBoolean(value), new NSString("SuppressPopup"));
        }

        private string GetTag()
        {
            return _content.ThreadIdentifier;
        }

        private void SetTag(string value)
        {
            _content.ThreadIdentifier = value;
        }
    }
}