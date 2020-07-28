//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System.Collections;

namespace nanoFramework.Devices.Can
{
    internal sealed class CanControllerManager
    {
        private static readonly object _syncLock = new object();

        // backing field for ControllersCollection
        // to store the controllers that are open
        private static ArrayList s_controllersCollection;

        /// <summary>
        /// <see cref="CanController"/> collection.
        /// </summary>
        /// <remarks>
        /// This collection is for internal use only.
        /// </remarks>
        internal static ArrayList ControllersCollection
        {
            get
            {
                if (s_controllersCollection == null)
                {
                    lock (_syncLock)
                    {
                        if (s_controllersCollection == null)
                        {
                            s_controllersCollection = new ArrayList();
                        }
                    }
                }

                return s_controllersCollection;
            }

            set
            {
                s_controllersCollection = value;
            }
        }
    }
}
