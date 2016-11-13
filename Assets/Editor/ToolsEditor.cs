//author:Kuribayashi

using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Assets.Script;

enum ToolTab
{
    select = 0,
    add = 1,
    editMode = 2
}

enum WindowType
{
    Audio,
    Event,
    Lut
}

public class ToolsEditor : EditorWindow
{


    private static WindowType windowType;
    private AudioClip audio;
    private string intro;
    private ToolTab tooltab;
    private int toolindex;
    private List<AudioData> audioDataList;
    private Vector2 scrollPosition = Vector2.zero;
    private AudioData currAudioData;
    private AudioClip temp_currentClip;
    private int currIndex;


    [MenuItem("资源事件配置/XML音频配置工具")]
    static void AddWindow()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 300, 300);
        ToolsEditor window = (ToolsEditor)EditorWindow.GetWindowWithRect(typeof(ToolsEditor), wr, true, "XML音频配置工具");
        windowType = WindowType.Audio;
        window.Show();

    }
    [MenuItem("资源事件配置/场景事件管理工具")]
    static void AddSceneWindow()
    {
        Rect sm = new Rect(0, 0, 300, 300);
        ToolsEditor windowsm = (ToolsEditor)EditorWindow.GetWindowWithRect(typeof(ToolsEditor), sm, true, "场景事件管理");
        windowType = WindowType.Event;
        windowsm.Show();
    }







    #region 资源管理
    //绘制窗口时调用
    void OnGUI()
    {

        switch (windowType)
        {
            case WindowType.Audio:
                DrawAudioManager();
                break;
            case WindowType.Event:
                DrawEventManager();
                break;
        }


    }

    void DrawAudioManager()
    {
        string[] toolbarNames = new string[2] { "音效列表", "新增音效" };
        tooltab = (ToolTab)GUILayout.Toolbar((int)tooltab, toolbarNames);
        switch (tooltab)
        {
            case ToolTab.add:
                DrawAdd();
                break;
            case ToolTab.select:
                DrawAudioDataList();
                break;
            case ToolTab.editMode:
                EditMode();
                break;

        }
    }


    void DrawEventManager()
    {
        if (GUILayout.Button("打雷"))
        {
            if (Application.isPlaying)
            {
                GameObject obj = GameObject.Find("Thunder");
                if (obj == null)
                    obj = (GameObject)(Instantiate(Resources.Load("Prefab/Thunder")));
                obj.name = "Thunder";
                FlickeringLight fliker = obj.GetComponent<FlickeringLight>();
                fliker.FluorescentFlickerMax = 1f;
                fliker.StartThunder();
            }
            else ShowNotification(new GUIContent("只能在游戏模式下运行！"));
        }

        if (GUILayout.Button("遮罩"))
        {
            if (Application.isPlaying)
            {
                GameObject obj = GameObject.Find("Mask");
                if (obj == null)
                {
                    obj = (GameObject)(Instantiate(Resources.Load("Prefab/Mask")));
                    obj.name = "Mask";
                }

            }
            else ShowNotification(new GUIContent("只能在游戏模式下运行！"));
        }
        if (GUILayout.Button("下雨"))
        {
            if (Application.isPlaying)
            {
                GameObject obj = GameObject.Find("Eff_Rain");
                if (obj == null)
                    obj = (GameObject)(Instantiate(Resources.Load("Prefab/Eff_Rain")));
                obj.name = "Eff_Rain";

            }
            else ShowNotification(new GUIContent("只能在游戏模式下运行！"));
        }
        if (GUILayout.Button("屏幕色调"))
        {
            if (Application.isPlaying)
            {
                GameObject obj = GameObject.Find("Tone");
                if (obj == null)
                    obj = (GameObject)(Instantiate(Resources.Load("Prefab/Tone")));
                obj.name = "Tone";
            }
            else ShowNotification(new GUIContent("只能在游戏模式下运行！"));
        }
    }





    void EditMode()
    {
        if (currAudioData != null)
        {
            EditorGUILayout.LabelField("ID:  " + currAudioData.ID.ToString());
            currAudioData.Name = EditorGUILayout.TextArea(currAudioData.Name);
            if (temp_currentClip == null) temp_currentClip = (AudioClip)Resources.Load("Audio/" + currAudioData.AudioName);
            temp_currentClip = (AudioClip)EditorGUILayout.ObjectField(temp_currentClip, typeof(AudioClip), true, GUILayout.Height(15));
            if (GUILayout.Button("保存修改"))
            {
                AudioClip clip = temp_currentClip;
                if (clip == null)
                {
                    this.ShowNotification(new GUIContent("资源名不能为空"));
                    return;
                }
                if (currAudioData.Name == string.Empty)
                {
                    this.ShowNotification(new GUIContent("名称不能为空"));
                    return;
                }
                currAudioData.AudioName = temp_currentClip.name;
                ReadXmlMgr<ToolsEditor>.ChanageAudioData(currAudioData);
                this.ShowNotification(new GUIContent("保存成功！"));
                currIndex = -1;
                currAudioData = null;
                tooltab = ToolTab.select;
            }
            if (GUILayout.Button("返回")) tooltab = ToolTab.select;
        }

    }

    void DrawAdd()
    {
        intro = EditorGUILayout.TextField("输入名称", intro);
        audio = EditorGUILayout.ObjectField("添加音效", audio, typeof(AudioClip), true) as AudioClip;
        if (GUILayout.Button("添加音效", GUILayout.Width(200), GUILayout.Height(30)))
        {
            //关闭窗口
            // this.Close();
            // Debug.Log(intro.Length);
            if (intro == string.Empty)
            {
                this.ShowNotification(new GUIContent("介绍不能为空!"));
                return;
            }
            if (audio == null)
            {
                this.ShowNotification(new GUIContent("音效不能为空!"));
                return;
            }
            int id = AddAudioNode(intro, audio.name);
            if (id >= 0)
            {
                this.ShowNotification(new GUIContent(string.Format("成功添加!\r\nID:{0}", id)));
                intro = null;
                audio = null;
            }
            else this.ShowNotification(new GUIContent("请喊程序大爷"));
        }
    }

    void DrawAudioDataList()
    {
        // Debug.Log(audioDataList.Count);
        if (audioDataList == null)
        {
            InitAudioDataList();

        }
        else
        {
            // GUI.VerticalScrollbar(new Rect(285, 30, 30, 270), 100, 1.0f, 10.0f, 0.0f);
            scrollPosition = GUI.BeginScrollView(new Rect(0, 30, 300, 260), scrollPosition, new Rect(0, 0, 280, audioDataList.Count * 53 + 120));
            foreach (AudioData ad in audioDataList)
            {
                // Debug.Log(ad.Name);
                if (GUILayout.Button(string.Format("ID:{0}           名称:{1}", ad.ID, ad.Name), GUILayout.Height(16), GUILayout.Width(263)))
                {
                    tooltab = ToolTab.editMode;
                    currAudioData = audioDataList[ad.ID - 1];
                    currIndex = ad.ID;
                }
                EditorGUILayout.ObjectField(Resources.Load("Audio/" + ad.AudioName), typeof(AudioClip), true, GUILayout.Height(15), GUILayout.Width(280));
                GUILayout.Label(string.Format(" "), GUILayout.Height(15), GUILayout.Width(200));
            }
            GUI.EndScrollView();
        }

    }

    #endregion 资源管理 资源管理

    void InitAudioDataList()
    {
        audioDataList = ReadXmlMgr<ToolsEditor>.GetAllAudioData();
    }


    public static int AddAudioNode(string audio_name, string resource)
    {
        string filepath = ReadXmlDataMgr.GetInstance().GetXmlPath(ReadXmlDataMgr.XmlName.AudioData);
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNode root = xmlDoc.SelectSingleNode("AudioData");
            int count = GetAudioNodeCount();
            XmlElement elmNew = xmlDoc.CreateElement("AudioData" + (count + 1));
            XmlElement elID = xmlDoc.CreateElement("ID");
            elID.InnerText = (count + 1).ToString();
            XmlElement elName = xmlDoc.CreateElement("Name");
            elName.InnerText = audio_name;
            XmlElement elAudioName = xmlDoc.CreateElement("AudioName");
            elAudioName.InnerText = resource;
            elmNew.AppendChild(elID);
            elmNew.AppendChild(elName);
            elmNew.AppendChild(elAudioName);
            root.AppendChild(elmNew);
            xmlDoc.AppendChild(root);
            Debug.Log(filepath);
            xmlDoc.Save(filepath);
            return count + 1;

        }
        else return -1;
    }

    public static int GetAudioNodeCount()
    {
        string filepath = ReadXmlDataMgr.GetInstance().GetXmlPath(ReadXmlDataMgr.XmlName.AudioData);
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            using (XmlReader reader = XmlReader.Create(filepath, settings))
            {
                xmlDoc.Load(reader);
                XmlNode root = xmlDoc.SelectSingleNode("AudioData");
                int count = root.ChildNodes.Count;
                return count;
            }
        }
        else return -1;
    }
}


