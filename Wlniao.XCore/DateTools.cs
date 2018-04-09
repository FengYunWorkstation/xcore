/*==============================================================================
    �ļ����ƣ�DateTools.cs
    ���û�����CoreCLR 5.0,.NET Framework 2.0/4.0/5.0
    �������������õ�ʱ�������������
================================================================================
 
    Copyright 2014 XieChaoyi

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
namespace Wlniao
{
    /// <summary>
    /// ���õ�ʱ�������������
    /// </summary>
    public partial class DateTools
    {
        internal static int _TimeZone = 0;
        /// <summary>
        /// ��ǰ���е�ʱ��
        /// </summary>
        private static int TimeZone
        {
            get
            {
                return _TimeZone;
            }
        }
        /// <summary>
        /// ��ȡUnixʱ���
        /// </summary>
        /// <returns></returns>
        public static long GetUnix()
        {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000000;
        }
        /// <summary>
        /// ��ȡUnixʱ���
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetUnix(DateTime time)
        {
            return time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000000;
        }
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNow()
        {
            if (TimeZone == 0)
            {
                return DateTime.Now;// ֱ�ӷ��ر���ʱ��
            }
            else
            {
                var temp = DateTime.Now.AddHours(0 - TimeZone);
                return new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, temp.Second, temp.Millisecond, DateTimeKind.Utc).ToLocalTime();
            }
        }
        /// <summary>
        /// ��ȡ��ʱ���ĵ�ǰʱ��
        /// </summary>
        /// <returns></returns>
        public static DateTime GetUnspecifiedNow()
        {
            var utc = DateTime.UtcNow;
            return new DateTime(utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute, utc.Second, utc.Millisecond, DateTimeKind.Unspecified).AddHours(TimeZone);
        }
        /// <summary>
        /// ��Unixʱ���ת��Ϊ��ǰʱ�䣨��ͨ��WLN_TIMEZONE���ã�
        /// </summary>
        /// <param name="unixtime"></param>
        /// <returns></returns>
        public static DateTime Convert(long unixtime)
        {
            if (TimeZone == 0)
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(unixtime * 10000000)).ToLocalTime();
            }
            else
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).Add(new TimeSpan(unixtime * 10000000)).AddHours(TimeZone);
            }
        }
        /// <summary>
        /// ��ʱ���ַ���ת��Ϊ��ǰʱ��
        /// </summary>
        /// <param name="strtime"></param>
        /// <returns></returns>
        public static DateTime Convert(string strtime)
        {
            if (TimeZone == 0)
            {
                return System.Convert.ToDateTime(strtime);
            }
            else
            {
                var temp = System.Convert.ToDateTime(strtime).AddHours(0 - TimeZone);
                return new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, temp.Second, temp.Millisecond, DateTimeKind.Utc).ToLocalTime();
            }
        }
        /// <summary>
        /// ��Unixʱ���ת��ΪUTC����Э��ʱ��
        /// </summary>
        /// <param name="unixtime"></param>
        /// <returns></returns>
        public static DateTime ConvertToUtc(long unixtime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(unixtime * 10000000));
        }
        /// <summary>
        /// ��Unixʱ���ת��Ϊ��ʱ����ʱ��
        /// </summary>
        /// <param name="unixtime"></param>
        /// <returns></returns>
        public static DateTime ConvertToUnspecified(long unixtime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).Add(new TimeSpan(unixtime * 10000000)).AddHours(TimeZone);
        }
        /// <summary>
        /// ��Unixʱ���������ʱ�估ָ����ʽ���
        /// </summary>
        /// <param name="unixtime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ConvertToFormat(long unixtime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return Convert(unixtime).ToString(format);
        }
        /// <summary>
        /// ��Unixʱ���ת����GMT��ʽ
        /// </summary>
        /// <param name="unixtime"></param>
        /// <returns></returns>
        public static string ConvertToGMT(long unixtime = 0)
        {
            var dt = DateTime.UtcNow;
            if (unixtime > 0)
            {
                dt = ConvertToUtc(unixtime);
            }
            return dt.ToString("r");
        }
        /// <summary>
        /// ��GMT��ʽת��ΪUTC����Э��ʱ��
        /// </summary>
        /// <param name="gmt"></param>
        /// <returns></returns>
        public static DateTime ConvertGmtToUtc(string gmt)
        {
            var dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (string.IsNullOrEmpty(pattern))
                {
                    dt = System.Convert.ToDateTime(gmt);
                }
                else
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                }
            }
            catch { }
            return dt;
        }
        /// <summary>
        /// ����ǰʱ�䰴ָ����ʽ���
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Format(string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (TimeZone == 0)
            {
                return DateTime.Now.ToString(format);// ֱ�Ӹ�ʽ�������ʱ��
            }
            else
            {
                return DateTime.UtcNow.AddHours(TimeZone).ToString(format);// ��UTCʱ�����ʱ������
            }
        }
        /// <summary>
        /// ����ǰUTCʱ�䰴ָ����ʽ���
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatUtc(string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.UtcNow.ToString(format);
        }
        /// <summary>
        /// ��ʽ�������ʱ��
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatTimeZone(string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (TimeZone == 0)
            {
                return DateTime.Now.ToString(format); //ֱ�Ӹ�ʽ�������ʱ��
            }
            else
            {
                return DateTime.UtcNow.AddHours(TimeZone).ToString(format); //��UTCʱ�����ʱ������
            }
        }
        /// <summary>
        /// ��ʽ���ָ��ʱ����ʱ��
        /// </summary>
        /// <param name="timezone"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatTimeZone(int timezone, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.UtcNow.AddHours(timezone).ToString(format); //��UTCʱ�����ʱ������
        }
        /// <summary>
        /// ��Unixʱ���������ʱ����ָ����ʽ���
        /// </summary>
        /// <param name="unixtime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatUnix(long unixtime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return Convert(unixtime).ToString(format);
        }
        /// <summary>
        /// ��Unixʱ�����UTCЭ��ʱ��ָ����ʽ���
        /// </summary>
        /// <param name="unixtime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatUnixToUtc(long unixtime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var utc = ConvertToUtc(unixtime);
            return utc.ToString(format);
        }
        /// <summary>
        /// ��Unixʱ���������ʱ������ʽ���
        /// </summary>
        /// <param name="unixtime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatUnixToTimeZone(long unixtime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var utc = ConvertToUtc(unixtime);
            if (TimeZone == 0)
            {
                return utc.ToLocalTime().ToString(format); //ֱ���Ա���ʱ���ʽ���
            }
            else
            {
                return utc.AddHours(TimeZone).ToString(format); //��UTCʱ�����ʱ������
            }
        }
        /// <summary>
        /// ��Unixʱ�����ָ��ʱ������ʽ���
        /// </summary>
        /// <param name="unixtime"></param>
        /// <param name="timezone"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatUnixToTimeZone(long unixtime, int timezone, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var utc = ConvertToUtc(unixtime);
            if (timezone == 0)
            {
                return utc.ToLocalTime().ToString(format); //ֱ���Ա���ʱ���ʽ���
            }
            else
            {
                return utc.AddHours(timezone).ToString(format); //��UTCʱ�����ʱ������
            }
        }
    }
}