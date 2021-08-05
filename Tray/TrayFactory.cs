using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigureFile;
using System.Windows.Forms;
using System.Drawing;
namespace Tray
{
    public class TrayFactory
    {
        public static Dictionary<string, Tray> dic_Tray = new Dictionary<string, Tray>();//存放所有托盘对象
      
        /// <summary>
        /// 通过指定字符串获取对象
        /// </summary>
        /// <param name="key">字符串</param>
        /// <returns>返回托盘对象</returns>
        public static Tray getTrayFactory(string key)
        {
            if (dic_Tray.ContainsKey(key))
                return dic_Tray[key];
            else
                return null;
        }
        public static Label CreateLabel(int i)
        {
            Label lbl = new Label();
            //Button btn = new Button();
            lbl.Anchor = AnchorStyles.None;
            lbl.Margin = new Padding(0, 0, 0, 0);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            lbl.BackgroundImageLayout = ImageLayout.Stretch;
            lbl.Name = i.ToString();



            return lbl;
        }
        /// <summary>
        /// 读取文件初始化对象
        /// </summary>
        /// <param name="strPath"></param>
        public static void initTrayFactory(string strPath)
        {
          
            dic_Tray.Clear();
            string[] ids = IniOperate.INIGetAllSectionNames(strPath);
            foreach (string sid in ids) {
                int id = Convert.ToInt32(IniOperate.INIGetStringValue(strPath, sid, "Id", sid));
                int Row = Convert.ToInt32(IniOperate.INIGetStringValue(strPath, sid, "Row", "10"));
                int Column = Convert.ToInt32(IniOperate.INIGetStringValue(strPath, sid, "Column", "10"));
                string strName = IniOperate.INIGetStringValue(strPath, sid, "Name", "托盘"+sid);
                string strStart = IniOperate.INIGetStringValue(strPath, sid, "StartPose", "左上角");
                string strDirect = IniOperate.INIGetStringValue(strPath, sid, "Direction", "行");
                string strEmpty = IniOperate.INIGetStringValue(strPath,sid,"Empty","");
                bool bUnregular = Convert.ToBoolean( IniOperate.INIGetStringValue(strPath, sid, "IsRegular", "false"));
                string strChangeRowType = IniOperate.INIGetStringValue(strPath, sid, "strChangeRowType", "Z型");
                Tray tray = new Tray(id, strName, Row, Column);
                tray.bUnregular = bUnregular;
                tray.strChangeRowType = strChangeRowType;
                tray.initTray(strStart, strDirect, strEmpty);
                setTray(sid, tray);
            }
         
       
        }
        /// <summary>
        /// 设置或添加对象
        /// </summary>
        /// <param name="key">托盘对应的字符串</param>
        /// <param name="t">托盘对象</param>
        public static void setTray(string key, Tray t)
        {
            if (dic_Tray.ContainsKey(key))
            {
                dic_Tray[key] = t;
            }
            else {
                dic_Tray.Add(key, t);
            }
        }

        /// <summary>
        /// 保存所有托盘对象参数到文件
        /// </summary>
        /// <param name="strSaveFile">文件名,以.ini结尾</param>
        /// <returns>返回保存状态</returns>
        public static bool saveTrayFactoryParam(string strSaveFile)
        {
            bool bResult = false;
            foreach (KeyValuePair<string, Tray> dic in dic_Tray)
            {
                Tray t = dic.Value;
                string strSection = dic.Key;
               bResult = IniOperate.INIWriteValue(strSaveFile, strSection, "Id", t.TID.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "Row", t.TMaxRow.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "Column", t.TMaxCol.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "Name", t.TName);
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "StartPose", t.eStart.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "Direction", t.eDirect.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "Empty", t.getStringEmpty());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "IsRegular", t.bUnregular.ToString());
               bResult = bResult && IniOperate.INIWriteValue(strSaveFile, strSection, "strChangeRowType", t.strChangeRowType);
            }
            return bResult;
        }
        /// <summary>
        /// 获取托盘的个数
        /// </summary>
        /// <returns>返回托盘个数</returns>
        public static int getTrayCount()
        {
            return dic_Tray.Count;
        }
    }
}
