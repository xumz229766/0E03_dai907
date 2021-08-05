using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HalconDotNet;
using ConfigureFile;
namespace ImageProcess
{
    public class ImageProcessManager
    {
        private Dictionary<string, ProcessFatory> dic_Factory = new Dictionary<string, ProcessFatory>();
        private static ImageProcessManager manager = null;
        public static string strFilePath = System.Windows.Forms.Application.StartupPath + "\\Param\\";
        public static string strProduct = "Test";
        private ImageProcessManager()
        {
           // InitParam();
        }
        public static ImageProcessManager GetInstance()
        {
            if (manager == null)
            {
                manager = new ImageProcessManager();
            }
            return manager;
        }
        public void AddImageFactory(string key, ProcessFatory processFatory)
        {
            if (dic_Factory.ContainsKey(key))
            {
                dic_Factory[key] = processFatory;
            }
            else
            {
                dic_Factory.Add(key, processFatory);
            }
        }
        public bool RemoveImageFactory(string key)
        {
            if (dic_Factory.ContainsKey(key))
            {
               return dic_Factory.Remove(key);
            }
            return false;
        }
        public string[] GetFactoryNames()
        {
            return dic_Factory.Keys.ToArray();
        }
        public ProcessFatory GetProcessFactory(string name)
        {
            if (dic_Factory.ContainsKey(name))
            {
                return dic_Factory[name];
            }
            return null;
        }
        //初始化参数
        public void InitParam(string product)
        {
            dic_Factory.Clear();
            string strPath = strFilePath;
            strFilePath = strPath + product + "\\Project\\";
            string[] directs = Directory.GetDirectories(strFilePath);           
            foreach (string name in directs)
            {
                string item = name.Substring(name.LastIndexOf("\\") + 1);
                ProcessFatory pf = new ProcessFatory(item);
                AddImageFactory(item, pf);
            }
            
        }
        public bool SaveParam()
        {
           
            bool bResult = true;
       
            foreach (KeyValuePair<string, ProcessFatory> pair in dic_Factory)
            {
                bResult = bResult && pair.Value.save();
            }
            return bResult;
        }
        //保存指定项目
        public bool SaveParam(string projectName)
        {
            if (dic_Factory.ContainsKey(projectName))
            {
                return dic_Factory[projectName].save();
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 设置图像处理的显示窗口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hWindow"></param>
        public void SetWindow(string name,HWindow hWindow)
        {
            if (dic_Factory.ContainsKey(name))
            {
                dic_Factory[name].hwin = hWindow;
            }

        }
      
        

    }
}
