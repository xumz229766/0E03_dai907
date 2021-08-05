using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly
{
    /// <summary>
    /// 点胶测试
    /// </summary>
    public class GlueTest:BarrelAndGlueModule
    {

        public GlueTest()
        {
            lstAction.Clear();
            //点胶
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY到点胶拍照位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40Z轴到点胶拍照位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40上相机拍照);
            lstAction.Add(ActionName._40上相机拍照完成);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY轴到点胶位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40Z轴到点胶位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40点胶气缸下降);
            lstAction.Add(ActionName._40开始点胶);
            lstAction.Add(ActionName._40点胶判断);
            lstAction.Add(ActionName._40点胶到位);
            lstAction.Add(ActionName._40点胶气缸上升);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY到UV位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40开始UV);
            lstAction.Add(ActionName._40UV);
            lstAction.Add(ActionName._40XY轴到点胶位);
            lstAction.Add(ActionName._40UV完成);
            lstAction.Add(ActionName._40点胶XY到位完成);
           
        }
        public override void Reset()
        {
            
        }

        public override void Action(ActionName action, ref int step)
        {
            base.Action(action, ref step);
        }

        public override void Action2()
        {
            //int iTargetStep = lstAction.IndexOf(ActionName._40开始点胶);
            //if ((IStep >= iTargetStep) && (IStep <= iTargetStep + 1))
            //{
            //    int i = 0;
            //    base.Action(ActionName._40点胶判断, ref i);

            //}
        }
    }
}
