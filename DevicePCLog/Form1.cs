using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevicePCLog
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }









        /// <summary>
        /// mac to ipv6 mac
        /// </summary>
        /// <param name="ipv4mac"></param>
        /// <returns></returns>
        private string Mac2Ipv6Mac(string ipv4mac)
        {
            string result = string.Empty;

            if (ipv4mac.Contains("-"))
            {
                string[] tempmac = ipv4mac.Split('-');
                tempmac = InsertValue(tempmac, "ff", 3);
                tempmac = InsertValue(tempmac, "ee", 4);
                string  oldhead = ConverString(tempmac[0], 16, 2).PadLeft(8, '0');
               // string newhead = InvertString(oldhead);
                tempmac[0] = InvertString(oldhead);

                result = "fe80::" + tempmac[0] + tempmac[1] + ":" + tempmac[2] + tempmac[3] + ":" +
                    tempmac[4] + tempmac[5] + ":" + tempmac[6] + tempmac[7];

            }


            return result.ToLower();
        }








        /// <summary>
        /// 将value 插入到指定数组的指定的位置
        /// </summary>
        /// <param name="a">指定数组</param>
        /// <param name="value">待插入的元素</param>
        /// <param name="index">插入的位置</param>
        /// <returns>插入后的数组</returns>
        private  string[] InsertValue(string[] a, string value, int index)
        {
            try
            {
                //转换成List<int>集合
                List<string> list = new List<string>(a);
                //插入
                list.Insert(index, value);
                //从List<int>集合，再转换成数组
                return list.ToArray();
            }
            catch (Exception e)  // 捕获由插入位置非法而导致的异常
            {
                throw e;
            }
        }




        private string InvertString(string oldstr)
        {
            string newstr = string.Empty;

            for (int i = 0; i < oldstr.Length-1; i++)
            {
                if (i == 6)
                {
                    if (oldstr.Substring(i, 1) == "1")
                        newstr = newstr + "0";
                    if (oldstr.Substring(i, 1) == "0")
                        newstr = newstr + "1";
                }
                newstr = newstr + oldstr.Substring(i, 1);
            }
            return ConverString(newstr, 2, 16);

        }




        /// <summary>
        /// 隨機生成一個mac地址
        /// </summary>
        /// <returns></returns>
        private string GetMac()
        {
            Random r = new Random();
            //int iResult = r.Next(0, 255);
            string hex= string.Empty ;
            for (int i = 0; i < 6; i++)
            {
                hex = hex + "-" + string.Format("{0:X}", r.Next(0, 255)).PadLeft(2, '0');
            }

            if (hex.StartsWith("-"))
              hex= hex.Remove(0, 1);
            return hex;

        }


        /// <summary>
        /// 進制轉換
        /// </summary>
        /// <param name="value">需要轉換的值</param>
        /// <param name="fromBase">原進制</param>
        /// <param name="toBase">需要轉換的進制</param>
        /// <returns>返回的結果</returns>
        public  string ConverString(string value, int fromBase, int toBase)
        {
            Int32 n = Convert.ToInt32(value, fromBase);
            return Convert.ToString(n, toBase);
        }






        private void btnStart_Click(object sender, EventArgs e)
        {
            string ipv6 = Mac2Ipv6Mac(GetMac());

            MessageBox.Show(ipv6);
        }


    }
}
