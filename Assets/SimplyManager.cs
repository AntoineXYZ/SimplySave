using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplySave {


public static class SimplyManager {
		public enum SaveType {
			Json,
			Binary
		}

		public static string baseDataPath = Application.dataPath;

		/// <summary>
		/// Simple save function.
		/// </summary>
		/// <param name="data">Your data object, must be serializable</param>
		/// <param name="additionalPath">Any additional path after the baseDataPath (e.g "Saves/")</param>
		/// <param name="fileName">Name of the save file</param>
		/// <param name="saveType">Type of the save (e.g SaveType.Json)</param>
		public static void Save(object data, string additionalPath, string fileName, SaveType saveType) {
			if(saveType == SaveType.Json) {
				JsonSerializer.dataPath = baseDataPath;
				JsonSerializer.SaveJSONFile(fileName, additionalPath, data);
			}
			else {
				BinarySerializer.Init(baseDataPath);
				BinarySerializer.SaveBinaryFile(fileName, additionalPath, data);
				BinarySerializer.Close();
			}
		}

		/// <summary>
		/// Simple load function.
		/// </summary>
		/// <typeparam name="T">Type of the object you want to load</typeparam>
		/// <param name="additionalPath">Any additional path after the baseDataPath (e.g "Saves/")</param>
		/// <param name="fileName">Name of the save file</param>
		/// <param name="saveType">Type of the save (e.g SaveType.Json)</param>
		/// <returns></returns>
		public static T Load<T>(string additionalPath, string fileName, SaveType saveType) {
			T data;
			if (saveType == SaveType.Json) {
				JsonSerializer.LoadJSONFile<T>(fileName, additionalPath, out data);
			}
			else {
				BinarySerializer.Init(baseDataPath);
				BinarySerializer.LoadBinaryFile<T>(fileName, additionalPath, out data);
				BinarySerializer.Close();
			}
			return data;
		}

	}
}
