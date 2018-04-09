/*==============================================================================
    �ļ����ƣ�DataPage.cs
    ���û�����CoreCLR 5.0,.NET Framework 2.0/4.0/5.0
    �������������ݷ�ҳģ��
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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace Wlniao.Data
{

    /// <summary>
    /// ���ݷ�ҳģ��
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    [Wlniao.Serialization.Serializable]
    public class DataPage<T>
    {
        private int _recordCount;
        private int _pageCount;
        private int _pageSize;
        private int _pageIndex;
        private List<T> _results;

        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        /// <summary>
        /// ÿҳ������
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
        /// <summary>
        /// ��ѯ�����������б�
        /// </summary>
        public List<T> Results
        {
            get { return _results; }
            set { _results = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DataPage()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageSize"></param>
        public DataPage(List<T> list, Int32 pageSize)
        {
            var _pager = GetPage(list, pageSize);
            this.RecordCount = _pager.RecordCount;
            this.PageCount = _pager.PageCount;
            this.PageIndex = _pager.PageIndex;
            this.PageSize = _pager.PageSize;
            this.Results = _pager.Results;
        }

        /// <summary>
        /// ���ؿյķ�ҳ�����
        /// </summary>
        /// <returns></returns>
        public static DataPage<T> GetEmpty(int pageSize = 10)
        {
            var pager = new DataPage<T>();
            pager._pageSize = pageSize;
            pager._pageIndex = 1;
            pager._recordCount = 0;
            pager._results = new List<T>();
            return pager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static DataPage<T> GetPage(List<T> list, int pageSize, int pageIndex = 1)
        {
            var pager = new DataPage<T>();
            if (pageSize <= 0) pageSize = 20;
            pager._pageSize = pageSize;
            pager._pageIndex = pageIndex;
            pager._recordCount = list.Count;
            pager._results = new List<T>();
            // ����ҳ��
            int pcount = pager._recordCount / pager._pageSize;
            int imod = pager._recordCount % pager._pageSize;
            if (imod > 0)
            {
                pager._pageCount = pcount + 1;
            }
            else
            {
                pager._pageCount = pcount;
            }
            if (pager._pageIndex <= 1)
            {
                pager._pageIndex = 1;
            }
            else if (pager._pageIndex > pager._pageCount)
            {
                pager._pageIndex = pager._pageCount;
            }

            // �õ������
            int start = (pager._pageIndex - 1) * pager._pageSize;
            int count = 1;
            for (int i = start; i < list.Count; i++)
            {
                if (count > pager._pageSize) break;
                pager._results.Add(list[i]);
                count++;
            }
            return pager;
        }
    }
}
