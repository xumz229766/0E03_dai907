using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using HalconDotNet;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    /// <summary>
    /// 组装吸笔刚接触平台时的位置高度，此高度为组装零位高度
    /// </summary>
   public class AssembleHeightRelation
    {
       public static List<MeasurePosZ> lstRelationAssem1Station1 = new List<MeasurePosZ>();//组装1部工位1数据
       public static List<MeasurePosZ> lstRelationAssem1Station2 = new List<MeasurePosZ>();//组装1部工位2数据
       public static List<MeasurePosZ> lstRelationAssem2Station1 = new List<MeasurePosZ>();//组装2部工位1数据
       public static List<MeasurePosZ> lstRelationAssem2Station2 = new List<MeasurePosZ>();//组装2部工位2数据
       public static Point p1 = new Point();//吸笔1标定工位1时的坐标
       public static Point p2 = new Point();//吸笔1标定工位2时的坐标
       public static Point p3 = new Point();//吸笔10标定工位1时的坐标
       public static Point p4 = new Point();//吸笔10标定工位2时的坐标

       public static void InitParam(string file)
       {
           string section = "AssembleHeightRelation";
           lstRelationAssem1Station1.Clear();
           lstRelationAssem1Station2.Clear();
           lstRelationAssem2Station1.Clear();
           lstRelationAssem2Station2.Clear();

           for (int i = 0; i < 9; i++)
           {
               MeasurePosZ m = new MeasurePosZ();
               m.InitParam(file, section, "Assem1Station1" + i.ToString());
               lstRelationAssem1Station1.Add(m);

               m = new MeasurePosZ();
               m.InitParam(file, section, "Assem1Station2" + i.ToString());
               lstRelationAssem1Station2.Add(m);

               m = new MeasurePosZ();
               m.InitParam(file, section, "Assem2Station1" + i.ToString());
               lstRelationAssem2Station1.Add(m);

               m = new MeasurePosZ();
               m.InitParam(file, section, "Assem2Station2" + i.ToString());
               lstRelationAssem2Station2.Add(m);
           }
           p1.initParam(file, section, "p1");
           p2.initParam(file, section, "p2");
           p3.initParam(file, section, "p3");
           p4.initParam(file, section, "p4");
       }

       public static bool SaveParam(string file)
       {
           string section = "AssembleHeightRelation";
           bool bFlag = true;

           for (int i = 0; i < 9; i++)
           {

               bFlag = bFlag && lstRelationAssem1Station1[i].SaveParam(file, section, "Assem1Station1" + i.ToString());
               bFlag = bFlag && lstRelationAssem1Station2[i].SaveParam(file, section, "Assem1Station2" + i.ToString());
               bFlag = bFlag && lstRelationAssem2Station1[i].SaveParam(file, section, "Assem2Station1" + i.ToString());
               bFlag = bFlag && lstRelationAssem2Station2[i].SaveParam(file, section, "Assem2Station2" + i.ToString());

           }
           bFlag = bFlag && p1.saveParam(file, section, "p1");
           bFlag = bFlag && p2.saveParam(file, section, "p2");
           bFlag = bFlag && p3.saveParam(file, section, "p3");
           bFlag = bFlag && p4.saveParam(file, section, "p4");
           return bFlag;
       }

       
       public static void WriteFile(string strPath, List<MeasurePosZ> lst, string fileName)
       {
           if (!Directory.Exists(strPath))
           {
               try
               {
                   DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
               }
               catch (Exception)
               {

               }
           }
           string file = strPath + fileName + ".csv";
           try
           {

               if (!File.Exists(file))
               {
                   FileStream fs = File.Create(file);
                   fs.Close();
                   string strHead = " ,Z轴位置,测量高度";
                  
                   StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                   sw.WriteLine(strHead);
                   sw.Close();
               }

           }
           catch (Exception)
           {
           }
           try
           {
               StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
               int len = lst.Count;
               for (int i = 0; i < len; i++)
               {
                   sw.WriteLine("吸笔" + (i + 1).ToString() + "," + lst[i].PosZ.ToString("0.000") + "," + lst[i].MeasureZ.ToString("0.000"));
               }
               sw.Close();
           }
           catch (Exception)
           {


           }

       }

       //public void WriteResult(string strPath, string strContent)
       //{
       //    Action<string, string> dele = WriteFile;
       //    dele.BeginInvoke(strPath, strContent, WriteCallBack, null);
       //}
       //private void WriteCallBack(IAsyncResult _result)
       //{
       //    AsyncResult result = (AsyncResult)_result;
       //    Action<string, string> dele = (Action<string, string>)result.AsyncDelegate;
       //    dele.EndInvoke(result);
       //}

    }
    /// <summary>
    /// 测量Z轴刚接触平台时的Z高度和对应测量仪器的高度，各吸笔在组装平台上的零位高度
    /// </summary>
   public class MeasurePosZ
   {
       public double PosZ { get; set; }//Z轴的位置高度
       public double MeasureZ { get; set; }//测量的高度值

       public void InitParam(string file, string section,string keyHead)
       {
           PosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file,   section, keyHead + "_PosZ","0"));
           MeasureZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, keyHead + "_MeasureZ", "0"));
       }
       public bool SaveParam(string file, string section, string keyHead)
       {
           bool bFlag = true;
           bFlag = bFlag && (IniOperate.INIWriteValue(file, section, keyHead + "_PosZ",PosZ.ToString()));
           bFlag = bFlag && (IniOperate.INIWriteValue(file, section, keyHead + "_MeasureZ", MeasureZ.ToString()));
           return bFlag;
       }

   }
    /// <summary>
    /// 组装吸笔在单个工位的数据
    /// </summary>
   public class AssemblePressureRelation
   {
       public static Dictionary<int,List<MeasurePressure>> dic_RelationPressAndV1 = new Dictionary<int,List<MeasurePressure>>();//组装1部压力和电压关系数据
       public static Dictionary<int, List<MeasurePressure>> dic_RelationPressAndV2 = new Dictionary<int, List<MeasurePressure>>();//组装1部压力和电压关系数据
       public static Dictionary<int, HTuple> dic_Homat1 = new Dictionary<int, HTuple>();
       public static Dictionary<int, HTuple> dic_Homat2 = new Dictionary<int, HTuple>();
       public static Point p1 = new Point();//吸笔1标定工位1时的坐标
       public static Point p2 = new Point();//吸笔10标定工位1时的坐标


       public static void InitParam(string file)
       {
           string section = "AssemblePressureRelation";
           dic_RelationPressAndV1.Clear();
           dic_RelationPressAndV2.Clear();
           for (int i = 0; i < 9; i++)
           {
               List<MeasurePressure> lst1 = new List<MeasurePressure>();
               List<MeasurePressure> lst2 = new List<MeasurePressure>();
               for (int k = 0; k < 20; k++)
               {
                   MeasurePressure m = new MeasurePressure();
                   m.InitParam(file, section, "PressureV1" + i.ToString()+k.ToString());
                   lst1.Add(m);
                   m = new MeasurePressure();
                   m.InitParam(file, section, "PressureV2" + i.ToString() + k.ToString());
                   lst2.Add(m);
               }
               dic_RelationPressAndV1.Add(i+1, lst1);
               dic_RelationPressAndV2.Add(i+10, lst2);
               dic_Homat1.Add(i + 1, null);
               dic_Homat2.Add(i + 1, null);
           }
           p1.initParam(file, section, "p1");
           p2.initParam(file, section, "p2");
         
       }
       public static bool SaveParam(string file)
       {
           string section = "AssemblePressureRelation";
           bool bFlag = true;
           for (int i = 0; i < 9; i++)
           {
               for (int k = 0; k < 20; k++)
               {
                   bFlag = bFlag && dic_RelationPressAndV1[i+1][k].SaveParam(file,section,"PressureV1" + i.ToString()+k.ToString());
                   bFlag = bFlag && dic_RelationPressAndV2[i+10][k].SaveParam(file, section, "PressureV2" + i.ToString() + k.ToString());
               }
               dic_Homat1[i + 1] = null;
               dic_Homat2[i + 10] = null;
           }
           bFlag = bFlag && p1.saveParam(file, section, "p1");
           bFlag = bFlag && p2.saveParam(file, section, "p2");
           
           return bFlag;
       }

       public static float GetVBySuction1(int iSuctionNum,double pressure)
       {

         
           HTuple outV=1;
           try
           {
               if (dic_Homat1[iSuctionNum] == null)
               {
                   createFun1(iSuctionNum);
               }
             HOperatorSet.GetYValueFunct1d(dic_Homat1[iSuctionNum],pressure,"constant",out outV);
           }
           catch (Exception)
           {

               
           }
           
           return (float)outV.D;
            
       }
       public static void createFun1(int iSuctionNum)
       { 
           HTuple x = new HTuple();
           HTuple y = new HTuple();
            int count = dic_RelationPressAndV1[iSuctionNum].Count;
           for(int i = 0;i<count;i++)
           {
               if(dic_RelationPressAndV1[iSuctionNum][i].V != 0)
               {
                    x= x.TupleConcat(dic_RelationPressAndV1[iSuctionNum][i].Pressure);
                    y =y.TupleConcat(dic_RelationPressAndV1[iSuctionNum][i].V);
               }

           }
           HTuple func;
           HOperatorSet.CreateFunct1dPairs(x, y, out func);
           dic_Homat1[iSuctionNum] = func;
       }
       public static float GetVBySuction2(int iSuctionNum, double pressure)
       {
           HTuple outV=1;
           try
           {

               if (dic_Homat2[iSuctionNum] == null)
               {
                   createFun2(iSuctionNum);
               }
           
               HOperatorSet.GetYValueFunct1d(dic_Homat2[iSuctionNum], pressure, "constant", out outV);

           }
           catch (Exception)
           {

              
           }
           return (float)outV.D;

       }
       public static void createFun2(int iSuctionNum)
       {
           HTuple x = new HTuple();
           HTuple y = new HTuple();
           int count = dic_RelationPressAndV2[iSuctionNum].Count;
           for (int i = 0; i < count; i++)
           {
               if (dic_RelationPressAndV2[iSuctionNum][i].V != 0)
               {
                   x = x.TupleConcat(dic_RelationPressAndV2[iSuctionNum][i].Pressure);
                   y = y.TupleConcat(dic_RelationPressAndV2[iSuctionNum][i].V);
               }

           }
           HTuple func;
           HOperatorSet.CreateFunct1dPairs(x, y, out func);
           dic_Homat2[iSuctionNum] = func;
       }


       public static void WriteFile(string strPath, string fileName)
       {
           if (!Directory.Exists(strPath))
           {
               try
               {
                   DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
               }
               catch (Exception)
               {

               }
           }
           string file = strPath + fileName + ".csv";
           try
           {

               if (!File.Exists(file))
               {
                   FileStream fs = File.Create(file);
                   fs.Close();
                   string strHead = "电压,";
                   for (int i = 0; i < 18; i++)
                   {
                       strHead += "吸笔" + (i + 1).ToString() + "压力(N),";

                   }
                   strHead.Remove(strHead.LastIndexOf(','));
                   StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                   sw.WriteLine(strHead);
                   sw.Close();
               }

           }
           catch (Exception)
           {
           }
           try
           {
               StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
               int len = dic_RelationPressAndV1[1].Count;
               for (int i = 0; i < len; i++)
               {
                   int len2 = 9;
                   string strOut = dic_RelationPressAndV1[1][i].V.ToString()+"V";
                   for(int k = 0;k<len2;k++)
                   {
                       strOut += "," + (dic_RelationPressAndV1[k + 1][i].Pressure.ToString("0.0"));
                         //sw.WriteLine("吸笔" + (i + 1).ToString() + "," + dic_RelationPressAndV1[i].ToString("0.000") + "," + lst[i].MeasureZ.ToString("0.000"));
                   }
                   for (int k = 9; k < len2+9; k++)
                   {
                       strOut += "," + (dic_RelationPressAndV2[k + 1][i].Pressure.ToString("0.0"));
                       //sw.WriteLine("吸笔" + (i + 1).ToString() + "," + dic_RelationPressAndV1[i].ToString("0.000") + "," + lst[i].MeasureZ.ToString("0.000"));
                   }
                   sw.WriteLine(strOut);

                 }
               sw.Close();
           }
           catch (Exception)
           {


           }

       }

   }
   public class MeasurePressure
   {
       public double V { get; set; }
       public double Pressure { get; set; }

       public void InitParam(string file, string section, string keyHead)
       {
           V = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, keyHead + "_V", "0"));
           Pressure = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, keyHead + "_Pressure", "0"));
       }
       public bool SaveParam(string file, string section, string keyHead)
       {
           bool bFlag = true;
           bFlag = bFlag && (IniOperate.INIWriteValue(file, section, keyHead + "_V", V.ToString()));
           bFlag = bFlag && (IniOperate.INIWriteValue(file, section, keyHead + "_Pressure", Pressure.ToString()));
           return bFlag;
       }
   }


}
