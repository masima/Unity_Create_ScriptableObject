using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

/// <summary>
/// ScriptableObjectを継承したクラス名のファイルを選択し、
/// 右クリックから「Create -> ScriptableObject」でアセットファイル作成。
/// </summary>
public class CreateScriptableObject {

	[MenuItem("Assets/Create/ScriptableObject")]
	static void Create()
	{
		foreach (var pair in GetTargetAssetPaths()) {
			Create(pair.Key, pair.Value);
		}
	}
	[MenuItem("Assets/Create/ScriptableObject", true)]
	static bool ValidateCreate()
	{
		return 0 < GetTargetAssetPaths().Count;
	}


	static Dictionary<string, Type> GetTargetAssetPaths()
	{
		var dict = new Dictionary<string, Type>();
		foreach (var guid in Selection.assetGUIDs) {
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var name = Path.GetFileNameWithoutExtension(path);
			var type = GetType(name);
			if (type != null) {
				dict[path] = type;
			}
		}
		return dict;
	}
	static Type GetType(string name)
	{
		foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
			foreach (var type in asm.GetTypes()) {
				if (type.Name == name && type.IsSubclassOf(typeof(ScriptableObject))) {
					return type;
				}
			}
		}
		return null;
	}


	public static void Create(string path, Type type)
	{
		var outputPath = AssetDatabase.GenerateUniqueAssetPath(Path.ChangeExtension(path, ".asset"));
		Debug.Log("create:" + outputPath);

		var instance = ScriptableObject.CreateInstance(type);
		AssetDatabase.CreateAsset(instance, outputPath);
	}
}
