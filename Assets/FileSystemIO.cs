using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnityEngine;


namespace TchouSaveSystem{

	public static class FileSystemIO {


		/// <summary>
		/// This fonction takes an object, serialize it and write it into a file
		/// </summary>
		/// <param name="fileName">Name of the file you want to write to without the extension</param>
		/// <param name="url">Path to your destination (example : "folder1/"</param>
		/// <param name="format">The extension of the file (example : ".bin")</param>
		/// <param name="data">A serializable object</param>
		/// <returns>Return true if the operation was succesful else, it throws an error</returns>
		public static bool SaveBinaryFile(string fileName, string path, string format, object data) {

			string destination = Consts.DataPath + "/" + path + fileName + format;

			Debug.Log(destination);

			FileStream file;
			if (File.Exists(destination)) file = File.OpenWrite(destination);
			else file = File.Create(destination);
			BinaryFormatter bf = new BinaryFormatter();

			SurrogateSelector ss = new SurrogateSelector();
			CustomSerializer v3ss = new CustomSerializer();
			ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);
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

		public static object LoadBinaryFile(string fileName, string path, string format, out object data) {
			string destination = Consts.DataPath + "/" + path + fileName + format;
			data = null;
			FileStream file;

			if (File.Exists(destination)) file = File.OpenRead(destination);
			else {
				Debug.LogError("File not found");
				return null;
			}

			BinaryFormatter bf = new BinaryFormatter();
			SurrogateSelector ss = new SurrogateSelector();
			CustomSerializer v3ss = new CustomSerializer();
			ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);
			bf.SurrogateSelector = ss;


			try {
				data = (object)bf.Deserialize(file);
				return data;
			}
			catch (SerializationException e) {
				Debug.LogError("Failed to serialize. Reason: " + e.Message);
				return null;
			}
			finally {
				file.Close();
			}
		}

	}
}