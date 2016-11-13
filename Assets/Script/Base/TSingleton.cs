
//功能：
//创建者: 胡海辉
//创建时间：

using System;
namespace Assets.Script.Base
{
    public class TSingleton<T> : BaseCommpoent  where T : IDisposable, new()
    {
        private static T _instance;
        static TSingleton()
        {
           _instance = default(T);
        }

        public static void Destory()
        {
            if (_instance != null)
            {
               TSingleton<T>._instance.Dispose();
              _instance = default(T);
            }
        }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                T local = default(T);
               _instance = (local == null) ? Activator.CreateInstance<T>() : default(T);
            }
            return _instance;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }


}
