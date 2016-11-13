
//功能：
//创建者: 胡海辉
//创建时间：


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.BallMgr.HostCenter
{
  public  class NormalHostCenter :HostCenterCreator
    {
      public override void LogicCollision(BaseCreator _Ball)
      {
          base.LogicCollision(_Ball);
          DebugHelper.DebugLog("NormalHostCenter");
      }
    }
}
