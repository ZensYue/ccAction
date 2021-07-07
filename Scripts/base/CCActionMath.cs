

using UnityEngine;

namespace ccAction
{
    public static class CCActionMath
    {
		/// <summary>
		/// 二阶贝塞尔曲线
		/// </summary>
		public static Vector3 QuadBezier(Vector3 p1, Vector3 c, Vector3 p2, float progress)
		{
			float t = progress;
			float d = 1f - t;
			return d * d * p1 + 2f * d * t * c + t * t * p2;
		}

		/// <summary>
		/// 三阶贝塞尔曲线
		/// </summary>
		public static Vector3 CubicBezier(Vector3 p1, Vector3 c1, Vector3 c2, Vector3 p2, float progress)
		{
			float t = progress;
			float d = 1f - t;
			return d * d * d * p1 + 3f * d * d * t * c1 + 3f * d * t * t * c2 + t * t * t * p2;
		}

		/// <summary>
		/// 样条曲线
		/// </summary>
		public static Vector3 CatmullRoom(Vector3 p1, Vector3 c1, Vector3 c2, Vector3 p2, float progress)
		{
			float t = progress;
			return .5f *
			(
				(-c1 + 3f * p1 - 3f * p2 + c2) * (t * t * t)
				+ (2f * c1 - 5f * p1 + 4f * p2 - c2) * (t * t)
				+ (-c1 + p2) * t
				+ 2f * p1
			);
		}

		/// <summary>
		/// 震动采样
		/// </summary>
		public static Vector3 Shake(Vector3 magnitude, Vector3 position, float progress)
		{
			float x = Random.Range(-magnitude.x, magnitude.x) * progress;
			float y = Random.Range(-magnitude.y, magnitude.y) * progress;
			float z = Random.Range(-magnitude.z, magnitude.z) * progress;
			return position + new Vector3(x, y, z);
		}

		/// <summary>
		/// 椭圆运动 
		/// VecA与VecB垂直
		/// a和b相等，2D就是一个圆，3D就是一个球
		/// </summary>
		/// <param name="centerPos">中心点</param>
		/// <param name="vecA">向量A，如果是2D，可以当做x或者y轴，与向量B垂直</param>
		/// <param name="VecB">向量B，如果是2D，可以当做x或者y轴，与向量A垂直</param>
		/// <param name="a">向量A长度</param>
		/// <param name="b">向量B长度</param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static Vector3 Ellipse(Vector3 centerPos, Vector3 vecA,Vector3 vecB,float a,float b,float t)
        {
			Vector3 vector3 = new Vector3();
			float angle = t * 360;
			float hudu = (angle / 180) * Mathf.PI;
			vector3.x = centerPos.x + a * vecA.x * Mathf.Cos(hudu) + b * vecB.x * Mathf.Sin(hudu);
			vector3.y = centerPos.y + a * vecA.y * Mathf.Cos(hudu) + b * vecB.y * Mathf.Sin(hudu);
			vector3.z = centerPos.z + a * vecA.z * Mathf.Cos(hudu) + b * vecB.z * Mathf.Sin(hudu);
			return vector3;

		}
	}
}
