﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UICommand.cs" company="In The Hand Ltd">
//   Copyright (c) 2012-16 In The Hand Ltd, All rights reserved.
// </copyright>
// <summary>
//   Represents a command in a context menu.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(Windows.UI.Popups.UICommand))]
#else

namespace Windows.UI.Popups
{
    /// <summary>
    /// Represents a command in a context menu.
    /// </summary>
    public sealed class UICommand : IUICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UICommand"/> class.
        /// </summary>
        public UICommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UICommand class, using the specified label and event handler.
        /// </summary>
        /// <param name="label">The label for the new command.</param>
        /// <param name="action">The event handler for the new command. </param>
        public UICommand(string label, UICommandInvokedHandler action)
        {
            Label = label;
            Invoked = action;
        }

        /// <summary>
        /// Initializes a new instance of the UICommand class, using the specified label, event handler, and command identifier.
        /// </summary>
        /// <param name="label">The label for the new command.</param>
        /// <param name="action">The event handler for the new command.</param>
        /// <param name="commandId">The command identifier for the new command.</param>
        public UICommand(string label, UICommandInvokedHandler action, object commandId) : this(label, action)
        {
            Id = commandId;
        }

        /// <summary>
        /// Gets or sets the identifier of the command.
        /// </summary>
        public object Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the handler for the event that is raised when the user selects the command.
        /// </summary>
        public UICommandInvokedHandler Invoked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label for the command.
        /// </summary>
        public string Label
        {
            get;
            set;
        }
    }
}
#endif