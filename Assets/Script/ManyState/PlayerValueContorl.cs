
//功能：
//创建者: 胡海辉
//创建时间：


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ManyState
{
    public struct ValueCompoent
    {
        public int powerValue;             //体力值
        public int hungryValue;            //饥饿值
        public int waterValue;             //体内水分
        public int tiredValue;             //疲劳度

    }

    public class PlayerValueContorl
    {
        public ValueCompoent PlayerValueCompoent = default(ValueCompoent);
    }
}
