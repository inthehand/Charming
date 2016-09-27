﻿//-----------------------------------------------------------------------
// <copyright file="DataPackagePropertySet.cs" company="In The Hand Ltd">
//     Copyright © 2013-16 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(Windows.ApplicationModel.DataTransfer.DataPackagePropertySet))]
#else
namespace Windows.ApplicationModel.DataTransfer
{
    /// <summary>
    /// Defines a set of properties to use with a <see cref="DataPackage"/> object.
    /// </summary>
    public sealed class DataPackagePropertySet
    {
        internal DataPackagePropertySet()
        {
            Title = string.Empty;
        }

        /// <summary>
        /// Gets or sets the text that displays as a title for the contents of the <see cref="DataPackage"/> object.
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Gets or sets text that describes the contents of the <see cref="DataPackage"/>.
        /// </summary>
        /// <remarks>We recommend adding a description to a DataPackage object if you can.
        /// Target apps can use this description to help users identify what content they're sharing.</remarks>
        public string Description { get; set; }
    }
}
#endif