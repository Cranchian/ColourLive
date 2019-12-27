using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RegionCapture))]
public class RegionCaptureEditor : Editor
{
	override public void OnInspectorGUI()
	{
		var Region_Capture_UI = target as RegionCapture;

		GUIStyle style = new GUIStyle (EditorStyles.label);
		style.normal.textColor = Color.gray;
		style.fontSize = 9;

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(20));
		EditorGUILayout.EndVertical();

		Region_Capture_UI.ARCamera = (EditorGUILayout.ObjectField(Region_Capture_UI.ARCamera, typeof(Camera), true)) as Camera;
		EditorGUILayout.LabelField("If not setted - it will be found by name", style);

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(15));
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginHorizontal();
        Region_Capture_UI.UseCustomBackgroundMaterial = GUILayout.Toggle(Region_Capture_UI.UseCustomBackgroundMaterial, "", GUILayout.Width(15));
        EditorGUILayout.LabelField("Use custom background material", GUILayout.Width(230));
		EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("", GUILayout.Height(15));
        EditorGUILayout.EndVertical();

        if (Region_Capture_UI.UseCustomBackgroundMaterial)
        {
            Region_Capture_UI.CustomBackgroundMaterial = (EditorGUILayout.ObjectField(Region_Capture_UI.CustomBackgroundMaterial, typeof(Material), true)) as Material;
            EditorGUILayout.LabelField("Please add custom (ARCore / ARKit) background material", style);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("", GUILayout.Height(5));
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            Region_Capture_UI.UseBackgroundPlane = GUILayout.Toggle(Region_Capture_UI.UseBackgroundPlane, "", GUILayout.Width(15));
            EditorGUILayout.LabelField("Use the background plane in a scene", GUILayout.Width(230));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("", GUILayout.Height(5));
            EditorGUILayout.EndVertical();

            if (Region_Capture_UI.UseBackgroundPlane)
            {
                Region_Capture_UI.BackgroundPlane = (EditorGUILayout.ObjectField(Region_Capture_UI.BackgroundPlane, typeof(GameObject), true)) as GameObject;
                EditorGUILayout.LabelField("If not setted - it will be found by name", style);
            }
            else
            {
                Region_Capture_UI.VideoBackgroundTexure = (EditorGUILayout.ObjectField(Region_Capture_UI.VideoBackgroundTexure, typeof(Texture), true)) as Texture;
                EditorGUILayout.LabelField("Please add WebCamTexture", style);
            }
        }

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(30));
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Flip texture X", GUILayout.Width(80));
		Region_Capture_UI.FlipX = GUILayout.Toggle(Region_Capture_UI.FlipX, "", GUILayout.Width(35));
		EditorGUILayout.LabelField("Flip texture Y", GUILayout.Width(80));
		Region_Capture_UI.FlipY = GUILayout.Toggle(Region_Capture_UI.FlipY, "", GUILayout.Width(35));
        EditorGUILayout.LabelField("Rotate 90", GUILayout.Width(60));
        Region_Capture_UI.Rotate90 = GUILayout.Toggle(Region_Capture_UI.Rotate90, "", GUILayout.Width(20));
        EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(3));
		EditorGUILayout.EndVertical();

		EditorGUILayout.LabelField("Allow to flip a texture on the capture plane", style);

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(30));
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginHorizontal();
		Region_Capture_UI.HideFromARCamera = GUILayout.Toggle(Region_Capture_UI.HideFromARCamera, "", GUILayout.Width(15));
		EditorGUILayout.LabelField("Hide this layer from the ARCamera", GUILayout.Width(230));
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(20));
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginHorizontal();
		Region_Capture_UI.Check_OutOfBounds = GUILayout.Toggle(Region_Capture_UI.Check_OutOfBounds, "", GUILayout.Width(15));
		EditorGUILayout.LabelField("Check if the plane is out of bounds", GUILayout.Width(230));
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("", GUILayout.Height(10));
		EditorGUILayout.EndVertical();


		if (Region_Capture_UI.Check_OutOfBounds) 
		{
			SerializedProperty S_Property_OutOfBounds = serializedObject.FindProperty("OutOfBounds");
			EditorGUILayout.PropertyField(S_Property_OutOfBounds);

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("", GUILayout.Height(10));
			EditorGUILayout.EndVertical();

			SerializedProperty S_Property_ReturnInBounds = serializedObject.FindProperty("ReturnInBounds");
			EditorGUILayout.PropertyField(S_Property_ReturnInBounds);

			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("", GUILayout.Height(15));
			EditorGUILayout.EndVertical();
		} 

		if (GUI.changed) { EditorUtility.SetDirty(Region_Capture_UI); }
	}
}