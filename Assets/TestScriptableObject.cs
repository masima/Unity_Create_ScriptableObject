using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestScriptableObject : ScriptableObject {

	public string testString = "Test!";
	public string[] testStrings =
	{
		"a","b","c"
	};
	public int number = 12345;
}
