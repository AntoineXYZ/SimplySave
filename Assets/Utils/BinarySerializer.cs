using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;


namespace SimplySave {

	public static class BinarySerializer {

		public static Dictionary<string, ISerializationSurrogate> surrogateDictionary;

		public static SurrogateSelector ss;

		static bool inited = false;
		static string dataPath = Application.dataPath;


		/// <summary>
		/// Initialize the BinarySerializer
		/// </summary>
		/// <param name="_dataPath">Should be the base dataPath used by the BinarySerializer</param>
		/// <returns>Return true if the init was successful otherwise throw an error and return false</returns>
		public static bool Init(string _dataPath) {

			try {
				if (!inited) {
					ss = new SurrogateSelector();
					dataPath = _dataPath;
				}
				else {
					Debug.LogWarning("The FileSytemIO was already initialized, make sure to close it first using FileSystemIO.Close()");
					Close();
				}
				foreach (ISurrogate surrogate in Extensions.GetSurrogates()) {
					ss.AddSurrogate(surrogate.type, new StreamingContext(StreamingContextStates.All), surrogate);
				}
				inited = true;
				return true;
			}
			catch(System.Exception e) {
				Debug.LogError("Init failed : " + e);
				return false;
			}

		}

		public static bool Close() {
			try {
				if (inited) {
					foreach (ISurrogate surrogate in Extensions.GetSurrogates()) {
						ss.RemoveSurrogate(surrogate.type, new StreamingContext(StreamingContextStates.All));
					}
					dataPath = String.Empty;
					inited = false;
				}
				return true;
			}
			catch(System.Exception e) {
				Debug.LogError("Cleared failed : " + e);
				return false;
			}	
		}

		/// <summary>
		/// This fonction takes an object, serialize it and write it into a binary file
		/// </summary>
		/// <param name="fileName">Name of the file you want to write to without the extension</param>
		/// <param name="path">Path to your destination (example : "folder1/"</param>
		/// <param name="data">A serializable object</param>
		/// <returns>Return true if the operation was successful otherwise it throws an error and return false</returns>
		public static bool SaveBinaryFile(string fileName, string path, object data) {

			string destination = dataPath + "/" + path + fileName + ".bin";
			FileStream file;
			if (File.Exists(destination)) file = File.Open(destination, FileMode.Create);
			else file = File.Create(destination);

			BinaryFormatter bf = new BinaryFormatter();
			bf.SurrogateSelector = ss;

			try {
				bf.Serialize(file, data);
				return true;
			}
			catch (SerializationException e) {
				Debug.LogError("Failed to serialize. Reason: " + e.Message);
				return false;
			}
			finally {
				file.Close();
			}
		}

		/// <summary>
		/// This fonction loads a binary file and deserialize into an object
		/// </summary>
		/// <typeparam name="T">Type of the object you want to deserialize to</typeparam>
		/// <param name="fileName">Name of the file you want to read without the extension</param>
		/// <param name="path">Path to your destination (example : "folder1/"</param>
		/// <param name="data">object where to data will be deserialize to</param>
		/// <returns>Return true if the operation was successful otherwise it throws an error and return false</returns>
		public static bool LoadBinaryFile<T>(string fileName, string path, out T data) {
			string destination = dataPath + "/" + path + fileName + ".bin";
			data = default(T);
			FileStream file;
			if (File.Exists(destination)) file = File.OpenRead(destination);
			else {
				Debug.LogError("File not found");
				return false;
			}

			BinaryFormatter bf = new BinaryFormatter();
			bf.SurrogateSelector = ss;

			try {
				data = (T)bf.Deserialize(file);
				return true;
			}
			catch (SerializationException e) {
				Debug.LogError("Failed to serialize. Reason: " + e.Message);
				return false;
			}
			finally {
				file.Close();
			}
		}

	}
}