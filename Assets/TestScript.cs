using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestScript : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
		var TestObject = new TestObject();
		TestObject.name = "TestName";
		TestObject.intTest = 5;
		TestObject.vector3Test = new Vector3(10.6f, 5, 7f);
		TestObject.floatTest = 895.454f;
		TchouSaveSystem.FileSystemIO.SaveBinaryFile("test", "", ".bin", TestObject);
		object obj;
		TchouSaveSystem.FileSystemIO.LoadBinaryFile("test", "", ".bin", out obj);

		var TestObject2 = (TestObject)obj;
		Debug.Log(TestObject2.name);
		Debug.Log(TestObject2.intTest);
		Debug.Log(TestObject2.vector3Test);
		Debug.Log(TestObject2.floatTest);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
class TestObject {
	public string name;
	public int intTest;
	public Vector3 vector3Test;
	public float floatTest;
}