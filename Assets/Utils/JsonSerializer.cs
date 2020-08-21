using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SimplySave {
	public static class JsonSerializer {

		static public string dataPath = Application.dataPath;

		/// <summary>
		/// This fonction loads a JSON file and deserialize into an object
		/// </summary>
		/// <param name="fileName">Name of the file you want to read without the extension</param>
		/// <param name="path">Path to your destination (example : "folder1/"</param>
		/// <param name="data">Object where to data will be deserialize to</param>
		/// <returns>Return true if the operation was successful otherwise it throws an error and return false</returns>
		public static bool SaveJSONFile(string fileName, string path, object data) {

			string destination = dataPath + "/" + path + fileName + ".json";
			FileStream file;
			if (File.Exists(destination)) file = File.Open(destination, FileMode.Create);
			else file = File.Create(destination);

			try {
				string json = JsonUtility.ToJson(data, true);
				byte[] info = new UTF8Encoding(true).GetBytes(json);
				file.Write(info, 0, info.Length);
				return true;
			}
			catch (IOException e) {
				Debug.LogError("Failed to serialize. Reason: " + e.Message);
				return false;
			}
			finally {
				file.Close();
			}
		}

		/// <summary>
		/// This fonction loads a JSON file and deserialize into an object
		/// </summary>
		/// <typeparam name="T">Type of the object you want to deserialize to</typeparam>
		/// <param name="fileName">Name of the file you want to read without the extension</param>
		/// <param name="path">Path to your destination (example : "folder1/"</param>
		/// <param name="data">object where to data will be deserialize to</param>
		/// <returns>Return true if the operation was successful otherwise it throws an error and return false</returns>
		public static bool LoadJSONFile<T>(string fileName, string path, out T data) {
			string destination = dataPath + "/" + path + fileName + ".json";
			data = default(T);
			FileStream file;

			if (File.Exists(destination)) file = File.OpenRead(destination);
			else {
				Debug.LogError("File not found");
				return false;
			}

			try {
				byte[] b = new byte[1024];
				string json = string.Empty;
				UTF8Encoding temp = new UTF8Encoding(true);
				while (file.Read(b, 0, b.Length) > 0) {
					json += temp.GetString(b);
				}
				data = JsonUtility.FromJson<T>(json);
				return true;
			}
			catch (IOException e) {
				Debug.LogError("Failed to serialize. Reason: " + e.Message);
				return false;
			}
			finally {
				file.Close();
			}
		}

	}
}
