using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
namespace Assembly
{
    public class AssemSolutionManager
    {
        private static AssemSolutionManager asM = null;
        public int iSolutionNum = 3;//组装方案设定个数
        public Dictionary<int, AssemSolution> dic_Solution = new Dictionary<int, AssemSolution>();
      
        private AssemSolutionManager()
        {
           

        }
        public static AssemSolutionManager getInstance()
        {
            if (asM == null)
            {
                asM = new AssemSolutionManager();
            }
            return asM;
        }
        public void initSolutionParam(string file)
        {
            dic_Solution.Clear();
            for (int i = 0; i < iSolutionNum; i++)
            {
                AssemSolution solution = new AssemSolution(i + 1);
                solution.initParam(file, (i + 1).ToString());
                dic_Solution.Add((i + 1), solution);
            }
            CommonSet.bDirect = Convert.ToBoolean(IniOperate.INIGetStringValue(file, "Other", "bDirect", "true"));
        }
        public bool saveSolutionParam(string file)
        {
            bool bFlag = true;
            foreach (KeyValuePair<int, AssemSolution> pair in dic_Solution)
            {
                bFlag = bFlag && pair.Value.saveParam(file, pair.Key.ToString());
            }
            bFlag = bFlag && IniOperate.INIWriteValue(file, "Other", "bDirect", CommonSet.bDirect.ToString());
            return bFlag;
        }
        /// <summary>
        /// 寻找相应穴号的组装方案，如果没有找到，则默认为第一种组装方案
        /// </summary>
        /// <param name="num">穴号</param>
        /// <returns>返回组装方案号</returns>
        public int getSolution(int num)
        {
            foreach (KeyValuePair<int, AssemSolution> pair in dic_Solution)
            {
                if (pair.Value.IsThisSolution(num))
                    return pair.Key;
            }
            return 1;
        }
    }
}
