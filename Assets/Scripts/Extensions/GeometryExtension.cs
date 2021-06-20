using UnityEngine;

namespace Extensions
{
    public static class GeometryExtension
    {
        public static Vector3 RandomPointInBox(Vector3 center, Vector3 size, float yPosition)
        {
            return new Vector3(
                center.x + (Random.value - 0.5f) * size.x,
                yPosition,
                center.z + (Random.value - 0.5f) * size.z
            );
        }
    }
}
