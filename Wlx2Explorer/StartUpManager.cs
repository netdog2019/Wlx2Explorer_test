﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;

namespace Wlx2Explorer
{
    static class StartUpManager
    {
        private const String RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void AddToStartup(String keyName, String assemblyLocation)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION, true))
            {
                key.SetValue(keyName, assemblyLocation);
            }
        }

        public static void RemoveFromStartup(String keyName)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION, true))
            {
                key.DeleteValue(keyName);
            }
        }

        public static Boolean IsInStartup(String keyName, String assemblyLocation)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION, true))
            {
                if (key == null) return false;
                var value = (String)key.GetValue(keyName);
                if (String.IsNullOrEmpty(value)) return false;
                var result = (value == assemblyLocation);
                return result;
            }
        }
    }
}
