using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(FlickeringLight))]
public class ThunderLightEditor : Editor {

    FlickeringLight flicker;

    public override void OnInspectorGUI() {
        flicker = (FlickeringLight)target;
        if (flicker.flickeringLightStyle == FlickeringLight.flickerinLightStyles.Thunder)
        {
            flicker.thunderDelay = EditorGUILayout.FloatField("雷声音效延迟", flicker.thunderDelay);
            flicker.thunderTime = EditorGUILayout.FloatField("雷光持续长度", flicker.thunderTime);
            flicker.intervalMin = EditorGUILayout.FloatField("雷光最小间隔", flicker.intervalMin);
            flicker.intervalMax = EditorGUILayout.FloatField("雷光最大间隔", flicker.intervalMax);
            flicker.FluorescentFlickerMin = EditorGUILayout.FloatField("雷光最小强度", flicker.FluorescentFlickerMin);
            flicker.FluorescentFlickerMax= EditorGUILayout.FloatField("雷光最大强度", flicker.FluorescentFlickerMax);
            flicker.lightColor = EditorGUILayout.ColorField("雷光颜色",flicker.lightColor);
            flicker.FluorescentFlickerAudioClip = EditorGUILayout.ObjectField("雷声音效",flicker.FluorescentFlickerAudioClip,typeof(AudioClip),true,GUILayout.Height(15))as AudioClip;
            if (GUILayout.Button("开始打雷"))
            {
                if (Application.isPlaying)
                    flicker.StartThunder();
            }
        }
        else
            base.DrawDefaultInspector();
        
    }
}
