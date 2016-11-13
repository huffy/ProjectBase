//author:kuribayashi  2016年7月20日 00:38:39
using UnityEngine;

public class InstanceBase<T> : MonoBehaviour where T : class 
{
    private static T Instance;

    public InstanceBase() {      
        Instance = this as T;            
    }

    public static T GetInstace() {       
        return Instance;
    }
  
}
