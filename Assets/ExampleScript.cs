using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour {

	void Start() {
		var testObject = new MyObject();
		testObject.name = "MyAwesomeName";
		testObject.intTest = 5;
		testObject.vector3Test = new Vector3(10.6f, 5, 7f);
		testObject.vector4Test = new Vector4(10.6f, 5, 7f, 0f);
		testObject.floatTest = 895.454f;
	
	
		SaveBinaryTest(testObject);
		SaveJsonTest(testObject);

		LoadBinaryTest();
		LoadJsonTest();
	}

	void SaveBinaryTest(MyObject testObject) {
		SimplySave.SimplyManager.baseDataPath = Application.dataPath;
		SimplySave.SimplyManager.Save(testObject, "Saves/", "saveBin1", SimplySave.SimplyManager.SaveType.Binary);
	}

	void SaveJsonTest(MyObject testObject) {
		SimplySave.SimplyManager.baseDataPath = Application.dataPath;
		SimplySave.SimplyManager.Save(testObject, "Saves/", "saveJson1", SimplySave.SimplyManager.SaveType.Json);

	}

	void LoadBinaryTest() {
		SimplySave.SimplyManager.baseDataPath = Application.dataPath;
		MyObject obj = SimplySave.SimplyManager.Load<MyObject>("Saves/", "saveBin1", SimplySave.SimplyManager.SaveType.Binary);
		Debug.Log(obj.ToString());
	}

	void LoadJsonTest() {
		SimplySave.SimplyManager.baseDataPath = Application.dataPath;
		MyObject obj = SimplySave.SimplyManager.Load<MyObject>("Saves/", "saveJson1", SimplySave.SimplyManager.SaveType.Json);
		Debug.Log(obj.ToString());
	}
		
}

	

[Serializable]
class MyObject {
	public string name;
	public int intTest;
	public Vector3 vector3Test;
	public Vector4 vector4Test;
	public float floatTest;

	public override string ToString() {
		return (
			"Name : " + name + "\n" +
			"IntTest : " + intTest + "\n" +
			"Vector3 Test : " + vector3Test + "\n" +
			"Vector4 Test : " + vector4Test + "\n" +
			"float Test : " + floatTest + "\n");
	}
}