﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents an exceptional situation where an event source is stateful 
    /// but there has been an attempt to retrieve it without restoring state by replaying events (fast-forwarding)
    /// </summary>
    public class InvalidFastForwardException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public InvalidFastForwardException(string message)
            : base(message)
        {
        }
    }
}