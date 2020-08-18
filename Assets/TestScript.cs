using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplySave {
	public class TestScript : MonoBehaviour {

		void Start() {

			var testObject = new MyObject();
			testObject.name = "MyAwesomeName";
			testObject.intTest = 5;
			testObject.vector3Test = new Vector3(10.6f, 5, 7f);
			testObject.vector4Test = new Vector4(10.6f, 5, 7f, 0f);
			testObject.floatTest = 895.454f;

			FileSystemIO.Init(Application.dataPath);

			FileSystemIO.SaveBinaryFile("test", "", ".bin", testObject);

			object obj;
			FileSystemIO.LoadBinaryFile("test", "", ".bin", out obj);

			FileSystemIO.Close();
		
			MyObject myObj = (MyObject)obj;
			Debug.Log(myObj.ToString());
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
			return
				"Name : " + name + "\n" +
				"IntTest : " + intTest + "\n" +
				"Vector3 Test : " + vector3Test + "\n" +
				"Vector4 Test : " + vector4Test + "\n" +
				"float Test : " + floatTest + "\n";
		}
	}
}