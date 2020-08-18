using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace SimplySave {
	static class Surrogates {
		class Vector3Surrogate : Surrogate {

			public override Type type { get; protected set; } = typeof(Vector3);

			// Method called to serialize a Vector3 object
			public override void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context) {

				Vector3 v3 = (Vector3)obj;
				info.AddValue("x", v3.x);
				info.AddValue("y", v3.y);
				info.AddValue("z", v3.z);
			}

			// Method called to deserialize a Vector3 object
			public override System.Object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

				Vector3 v3 = (Vector3)obj;
				v3.x = (float)info.GetValue("x", typeof(float));
				v3.y = (float)info.GetValue("y", typeof(float));
				v3.z = (float)info.GetValue("z", typeof(float));
				obj = v3;
				return obj;
			}
		}

		class Vector4Surrogate : Surrogate {

			public override Type type { get; protected set; } = typeof(Vector4);

			// Method called to serialize a Vector4 object
			public override void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context) {

				Vector4 v4 = (Vector4)obj;
				info.AddValue("x", v4.x);
				info.AddValue("y", v4.y);
				info.AddValue("z", v4.z);
				info.AddValue("w", v4.w);
			}

			// Method called to deserialize a Vector4 object
			public override System.Object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

				Vector4 v4 = (Vector4)obj;
				v4.x = (float)info.GetValue("x", typeof(float));
				v4.y = (float)info.GetValue("y", typeof(float));
				v4.z = (float)info.GetValue("z", typeof(float));
				v4.w = (float)info.GetValue("w", typeof(float));
				obj = v4;
				return obj;
			}
		}

		public static List<ISerializationSurrogate> GetSurrogates() {
			var list = new List<ISerializationSurrogate>();

			list.Add(new Vector3Surrogate());
			list.Add(new Vector4Surrogate());

			return list;
		}

	}
}