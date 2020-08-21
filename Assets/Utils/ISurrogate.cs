using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class ISurrogate : ISerializationSurrogate {
	public virtual Type type { get; protected set; }
	public abstract void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context);
	public abstract object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
}
