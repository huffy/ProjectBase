//author:kuribayashi  2016年7月20日 00:45:24
using UnityEditor;

[CustomEditor(typeof(OutlineEffect))]
public class OutlineEditor : Editor {

    OutlineEffect outline;

    public override void OnInspectorGUI() {
        //元界面
       // base.DrawDefaultInspector();
        outline = (OutlineEffect)target;
        outline.lineThickness = EditorGUILayout.FloatField("线宽度",outline.lineThickness);
        outline.lineIntensity = EditorGUILayout.FloatField("线强度",outline.lineIntensity);
        outline.lineColor0 = EditorGUILayout.ColorField("线颜色",outline.lineColor0);
    }
	
}
