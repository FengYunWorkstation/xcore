﻿/*==============================================================================
    文件名称：SysInfo.cs
    适用环境：CoreCLR 5.0,.NET Framework 2.0/4.0/5.0
    功能描述：当前系统信息（如：OSVersion等）
================================================================================
 
    Copyright 2015 XieChaoyi

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

               http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

===============================================================================*/
using System;
using Microsoft.AspNetCore.Hosting;
namespace Wlniao.Runtime
{
    /// <summary>
    /// 当前系统信息（如：OSVersion等）
    /// </summary>
    public partial class SysInfo
    {
        private static System.Runtime.InteropServices.OSPlatform os = System.Runtime.InteropServices.OSPlatform.Windows;        

        /// <summary>
        /// 当前是否为Linux系统
        /// </summary>
        public static Boolean IsLinux
        {
            get
            {
                return os == System.Runtime.InteropServices.OSPlatform.Linux;
            }
            set
            {
                os = System.Runtime.InteropServices.OSPlatform.Linux;
            }
        }
        /// <summary>
        /// 当前是否为Windows系统
        /// </summary>
        public static Boolean IsWindows
        {
            get
            {
                return os == System.Runtime.InteropServices.OSPlatform.Windows;
            }
            set
            {
                os = System.Runtime.InteropServices.OSPlatform.Windows;
            }
        }
        /// <summary>
        /// 当前是否为OSX系统
        /// </summary>
        public static Boolean OSX
        {
            get
            {
                return os == System.Runtime.InteropServices.OSPlatform.OSX;
            }
            set
            {
                os = System.Runtime.InteropServices.OSPlatform.OSX;
            }
        }




        #region 程序状态
        private static String appMode = null;   //web,console,service,winform
        /// <summary>
        /// 是否为Web应用
        /// </summary>
        public static Boolean IsWeb
        {
            get
            {
                return appMode == "web";
            }
            set
            {
                if (value)
                {
                    appMode = "web";
                }
            }
        }
        /// <summary>
        /// 是否为控制台应用
        /// </summary>
        public static Boolean IsConsole
        {
            get
            {
                return appMode == "console";
            }
            set
            {
                if (value)
                {
                    appMode = "console";
                }
            }
        }
        /// <summary>
        /// 是否为系统服务
        /// </summary>
        public static Boolean IsService
        {
            get
            {
                return appMode == "service";
            }
            set
            {
                if (value)
                {
                    appMode = "service";
                }
            }
        }
        /// <summary>
        /// 是否为窗口程序
        /// </summary>
        public static Boolean IsWinForm
        {
            get
            {
                return appMode == "winform";
            }
            set
            {
                if (value)
                {
                    appMode = "winform";
                }
            }
        }
        #endregion
    }
}