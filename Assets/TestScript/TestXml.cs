
//功能：
//创建者: 胡海辉
//创建时间：


using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
public class TestXML : MonoBehaviour
{
    // 新建菜单项   
    [MenuItem("Editor/CreateXML")]
    static void ExportSceneToXML()
    {
        //获取XML保存路径,引号中的可以自己指定，前提是指定文件夹必须存在      
        string filePath = Application.dataPath + @"/StreamingAssets/TestXML.xml";
        // 如果此文件已存，则删掉      
        if (File.Exists(filePath))
            File.Delete(filePath);
        // 新建一个XML      
        XmlDocument xmlDoc = new XmlDocument();
        // 为这个XML创建两个元素节点     
        XmlElement config = xmlDoc.CreateElement("Config");
        XmlElement scene = xmlDoc.CreateElement("Scene");
        // 设置scene元素的属性      
        scene.SetAttribute("name", filePath);
        // 创建另一个新元素节点     
        XmlElement position = xmlDoc.CreateElement("position");
        // 同上        
        XmlElement x = xmlDoc.CreateElement("x");
        // 设置元素内容 
        x.InnerText = "InnerText";
        // 指定各节点层级关系    
        position.AppendChild(x);
        scene.AppendChild(position);
        config.AppendChild(scene);
        xmlDoc.AppendChild(config);
        // 保存      
        xmlDoc.Save(filePath);
        // 刷新，作用相当于在unity中的资源面板中右击-刷新（Refresh） 
        AssetDatabase.Refresh();
    }

    private void Start() 
    {
    }
}
