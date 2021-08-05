using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Tray
{
    
    /// <summary>
    /// 托盘类，包含托盘类型，计数，移位，坐标等
    /// </summary>
    [Serializable]
     public class Tray
    {
         public int TID = 0;//托盘id
         public string TName = "";//名称
        // public int TType = 0;//0代表行列都对齐的盘，1代表行或列错开的异形盘
         public int TMaxRow = 3;//托盘行数 
         public int TMaxCol = 3;//托盘列数
         public bool bUnregular = true;//是否为异形盘
         public string strChangeRowType = "Z型";//换行方式，S型和Z型
       
         //public int iNext=0;//下一个有效位置
         //public Index TFront;//上一个位置
         public List<Index> lstEmpty = new List<Index>();//盘中无效位置
         //private List<Index> lstAll = new List<Index>();//盘中所有位置
         public Dictionary<int, Index> dic_Index = new Dictionary<int, Index>();//有效排序位置
         public EStartPos eStart = EStartPos.左上角; //起始位置
         public EIndexDirect eDirect = EIndexDirect.行;//排列方向
         
         public Action updateColor;//更新点位时的委托
         private int iCurrentPos = 1;//当组装位置
        public bool bFinish = false;//盘满标志

         public int CurrentPos
         {
             get { return iCurrentPos; }
             set { 
                 iCurrentPos = value;
                 if (CurrentPos < StartPos)
                     iCurrentPos = StartPos;
                if (iCurrentPos > EndPos)
                {
                    iCurrentPos = EndPos;
                    bFinish = true;
                }

             }
         }
         private int iStartPos = 1;//起始位置

         public int StartPos
         {
             get { return iStartPos; }
             set {
                 
                 iStartPos = value;
                 if (CurrentPos < StartPos)
                 {
                     CurrentPos = StartPos;
                 }
             }
         }
         private int iEndPos = 100;//结束位置

         public int EndPos
         {
             get { return iEndPos; }
             set { 
                 
                 iEndPos = value;
                 if (iCurrentPos > EndPos)
                 {
                     iCurrentPos = EndPos;
                     
                 }
             }
         }




         /// <summary>
         /// 建立托盘
         /// </summary>
         /// <param name="id">托盘序号</param>
         /// <param name="name">托盘命名</param>
         /// <param name="Row">托盘行数</param>
         /// <param name="Column">托盘列数</param>
         public Tray(int id,string name, int Row, int Column)
         {
             TID = id;
             TName = name;

             TMaxRow = Row;
             TMaxCol = Column;

             //for (int r = 0; r < TMaxRow; r++)
             //{
             //    for (int c = 0; c < TMaxCol; c++)
             //    {
             //        Index i = new Index();
             //        i.Col = c;
             //        i.Row = r;
             //        lstAll.Add(i);
             //    }
             //}
             //initTray();

         }
         /// <summary>
         /// 初始化托盘,加载盘时调用
         /// </summary>
         public void initTray(string start,string direct,string strEmpty)
         {
             lstEmpty.Clear();
             eStart = (EStartPos)Enum.Parse(typeof(EStartPos), start);
             eDirect = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), direct);
             List<string> lst = new List<string>();
             lst.AddRange(strEmpty.Split(','));
             lst.Remove("");
             foreach (string value in lst)
             {
                 Index pos = new Index(value);
                 lstEmpty.Add(pos);
             }
             //iCurrent = FindPos(0);
             //iNext = FindPos(iCurrent + 1);           
             sortTray(eStart, eDirect);
         }
         public void initTrayValue(Color c)
         {           

             for (int i = StartPos; i <= EndPos; i++)
             {
                 setNumColor(i, c);
             }
             CurrentPos = StartPos;
            bFinish = false;
         }

         /// <summary>
         /// 找出盘中有效点，按起始位置和方向排列点位
         /// </summary>
         public void sortTray(EStartPos start, EIndexDirect direct)
         {
             dic_Index.Clear();
             int i = 1;
             switch (start)
             {
                 #region"左上角"
                 case EStartPos.左上角:
                     if (direct == EIndexDirect.行)
                     {
                         for (int r = 0; r < TMaxRow; r++)
                         {
                             
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int c = 0; c < TMaxCol; c++)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = 0;
                                 if (r % 2 == flag)
                                 {
                                     for (int c = 0; c < TMaxCol; c++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int c = TMaxCol-1; c >=0 ; c--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }

                     }
                     else {
                         for (int c = 0; c < TMaxCol; c++)
                         {
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int r = 0; r < TMaxRow; r++)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = 0;
                                 if (c % 2 == flag)
                                 {
                                     for (int r = 0; r < TMaxRow; r++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int r = TMaxRow - 1; r >= 0; r--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }
                     }
                     break;
                 #endregion
                 #region"左下角"
                 case EStartPos.左下角:
                     if (direct == EIndexDirect.行)
                     {
                         for (int r = TMaxRow - 1; r >= 0; r--)
                         {
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int c = 0; c < TMaxCol; c++)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = (TMaxRow-1)%2;
                                 if (r % 2 == flag)
                                 {
                                     for (int c = 0; c < TMaxCol; c++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int c = TMaxCol - 1; c >= 0; c--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }

                     }
                     else {
                         for (int c = 0; c < TMaxCol; c++)
                         {
                             
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int r = TMaxRow - 1; r >= 0; r--)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = 0;
                                 if (c % 2 == flag)
                                 {
                                     for (int r = TMaxRow - 1; r >= 0; r--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int r = 0; r < TMaxRow; r++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }

                         }
                     }
                     break;
                 #endregion
                 #region"右上角"
                 case EStartPos.右上角:
                     if (direct == EIndexDirect.行)
                     {
                         for (int r = 0; r < TMaxRow; r++)
                         {
                            

                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int c = TMaxCol - 1; c >= 0; c--)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = 0;
                                 if (r % 2 == flag)
                                 {
                                     for (int c = TMaxCol - 1; c >= 0; c--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int c = 0; c <TMaxCol; c++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }
                     }
                     else {
                         for (int c = TMaxCol - 1; c >= 0; c--)
                         {
                            
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int r = 0; r < TMaxRow; r++)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }

                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = (TMaxCol - 1) / 2;
                                 if (c % 2 == flag)
                                 {
                                     for (int r = 0; r < TMaxRow; r++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int r = TMaxRow-1; r>=0 ; r--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }
                     }
                     break;
                 #endregion
                 #region"右下角"
                 case EStartPos.右下角:
                     if (direct == EIndexDirect.行)
                     {
                         for (int r = TMaxRow - 1; r >= 0; r--)
                         {
                             
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int c = TMaxCol - 1; c >= 0; c--)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = (TMaxRow - 1) % 2;
                                 if (r % 2 == flag)
                                 {
                                     for (int c = TMaxCol - 1; c >= 0; c--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int c = 0; c < TMaxCol; c++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }
                         }
                     }
                     else {
                         for (int c = TMaxCol - 1; c >= 0; c--)
                         {
                            
                             if (strChangeRowType.Equals("Z型"))
                             {

                                 #region"Z"
                                 for (int r = TMaxRow - 1; r >= 0; r--)
                                 {
                                     Index pos = new Index(r, c);
                                     if (IsExistEmpty(pos))
                                     {
                                         continue;
                                     }
                                     else
                                     {
                                         dic_Index.Add(i, pos);
                                         i++;
                                     }

                                 }
                                 #endregion
                             }
                             else
                             {
                                 #region"S"
                                 int flag = (TMaxCol - 1) % 2;
                                 if (c % 2 == flag)
                                 {
                                     for (int r = TMaxRow - 1; r >= 0; r--)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }
                                 else
                                 {
                                     for (int r = 0; r < TMaxRow; r++)
                                     {
                                         Index pos = new Index(r, c);
                                         if (IsExistEmpty(pos))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             dic_Index.Add(i, pos);
                                             i++;
                                         }

                                     }
                                 }


                                 #endregion
                             }

                            
                         }
                     }
                     break;
                 #endregion
             }
             StartPos = 1;
             EndPos = dic_Index.Count;
         }


         /// <summary>
         /// 从指定的索引位置开始查找有效穴号，并返回该穴号位置
         /// </summary>
         /// <param name="_pos">开始查找的位置</param>
         /// <returns>返回有效穴号位置,如果返回-1则代表没有找到</returns>
         public int FindPos(Index _pos)
         {
             int result = -1;
             foreach (int i in dic_Index.Keys)
             {
                 if (dic_Index[i].Equals(_pos)) {
                     result = i;
                     break;
                 }
             }
             return result;
         }
         
         /// <summary>
         /// 往盘中添加屏蔽位置
         /// </summary>
         /// <param name="row">行</param>
         /// <param name="col">列</param>
         public void addEmptyPos(int row, int col)
         {
             Index pos = new Index(row, col);          
             if (!IsExistEmpty(pos)) {
                 lstEmpty.Add(pos);
             }
         }
         /// <summary>
         /// 往盘中添加屏蔽位置
         /// </summary>
         /// <param name="r_c">以字符口串"R_C"的形式赋值</param>        
         public void addEmptyPos(string r_c)
         {
             Index pos = new Index(r_c);
             if (!IsExistEmpty(pos))
             {
                 lstEmpty.Add(pos);
             }
         }
         /// <summary>
         /// 移除屏蔽位置
         /// </summary>
         /// <param name="r_c">以字符口串"R_C"的形式赋值</param>
         public void removeEmptyPos(string r_c)
         {
             Index pos = new Index(r_c);
             if (IsExistEmpty(pos))
             {
                 lstEmpty.Remove(pos);
             }
         }
         //判断屏蔽位置是否存在
         public bool IsExistEmpty(Index _pos)
         {
             if (lstEmpty.Contains(_pos))
             {
                 return true;
             }

             return false;
         }
         /// <summary>
         /// 获取所有屏蔽点的字符串形式
         /// </summary>
         /// <returns>以"r_c,r_c,...,r_c"形式返回字符串</returns>
         public string getStringEmpty()
         {
             string strReturn = "";
             int iLen = lstEmpty.Count;
             for (int i = 0; i < iLen; i++)
             {
                 
                 if (i == iLen - 1)
                 {
                     strReturn += lstEmpty[i].ToString();
                     break;
                 }
                 strReturn += lstEmpty[i].ToString()+",";
             }
             return strReturn;
         }
         
         /// <summary>
         /// 设置指定序号控件的背景颜色
         /// </summary>
         /// <param name="num">穴号</param>
         /// <param name="bColor">颜色</param>
         public void setNumColor(int num, Color bColor)
         {
             Index index = dic_Index[num];
             index.color = bColor;
             dic_Index[num] = index;
             //updateColor();
         }
         /// <summary>
         /// 设置托盘起始结束位
         /// </summary>
         /// <param name="_startPos">起始位置</param>
         /// <param name="_endPos">结束位置</param>
         /// <param name="fillColor">起始位到结束位的显示颜色</param>
         /// <param name="fillColor2">无效位置的显示颜色</param>
         public void setStartEndPos(int _startPos,int _endPos, Color fillColor, Color fillColor2)
         {
             if (_startPos > _endPos)
                 return;
             iCurrentPos = _startPos;
             StartPos = _startPos;
             EndPos = _endPos;
             for (int i = 1; i < _startPos; i++)
             {
                 setNumColor(i, fillColor2);
             }
             int count = dic_Index.Count;
             for (int i = _endPos; i < count + 1; i++)
             {
                 setNumColor(i, fillColor2);
             }
             for (int i = _startPos; i <= _endPos; i++)
             {
                 setNumColor(i, fillColor);
             }
             // UpdateColor(null);
         }

         public void RemoveDelegate()
         {
             try
             {
                 if (updateColor != null)
                 {
                     Delegate[] list = updateColor.GetInvocationList();
                     foreach (Delegate d in list)
                     {
                         updateColor -= d as Action;
                     }
                 }
             }
             catch (Exception ex)
             {
                 string info = ex.ToString();
             }
         }
         public void setPosShowAlone(List<int> lstPoint,Color initColor,Color showColor)
         {
             initTrayValue(initColor);
             foreach (int value in lstPoint)
             {
                 setNumColor(value, showColor);
             }
             updateColor();
         }
          public Tray Clone() //深clone         
          {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as Tray;
        }
         
    }
    [Serializable]
    public struct Index{
        public int Row;//行
        public int Col;//列
        public Color color;//在工作状态起作用，工位状态；Gray代表初始化，green代表OK,red代表NG,其它代表被屏蔽
        public Index(int r, int c) { Row = r; Col = c; color = Color.Gray; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式传递值</param>
        public Index(string r_c) {
            try {
                string[] value = r_c.Trim().Split('_');
                Row = Convert.ToInt32(value[0]);
                Col = Convert.ToInt32(value[1]);
            }
            catch (Exception ex) {
                Row = -1;
                Col = -1;
            }
            color = Color.Gray;
        }
        public override string ToString()
        {
            return Row.ToString()+"_"+Col.ToString();
        }
        public void setColor(Color c)
        {
            color = c;
        }
    }
    public enum EStartPos{
        左上角,
        左下角,
        右上角,
        右下角
    }
    public enum EIndexDirect{
        行,
        列
    }



}
