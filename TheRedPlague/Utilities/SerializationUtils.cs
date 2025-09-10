using System.Linq;
using TheRedPlague.Utilities.SerializableStructs;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class SerializationUtils
{
    public static SerializableVector3[] ToSerializable(this Vector3[] array)
        => array?.Select(v => new SerializableVector3(v)).ToArray();

    public static SerializableQuaternion[] ToSerializable(this Quaternion[] array)
        => array?.Select(q => new SerializableQuaternion(q)).ToArray();

    public static Vector3[] ToUnity(this SerializableVector3[] array)
        => array?.Select(v => v.ToVector3()).ToArray();

    public static Quaternion[] ToUnity(this SerializableQuaternion[] array)
        => array?.Select(q => q.ToQuaternion()).ToArray();
}