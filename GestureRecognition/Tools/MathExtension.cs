using System;
using UnityEngine;

namespace GestureRecognition.Tools
{

    public static class MathExtension
    {
        /// <summary>
        /// 计算两二维向量夹角的余弦值平方，同时保持符号不变性
        /// </summary>
        /// <param name="from">起始2维向量</param>
        /// <param name="to">终止2维向量</param>
        /// <returns></returns>
        public static float CosSquare(Vector2 from, Vector2 to)
        {
            var xy = from.x * to.x + from.y * to.y;
            var f = from.sqrMagnitude;
            if (Math.Abs(f) < 0.0001f) return xy > 0 ? float.MaxValue : float.MinValue;
            var t = to.sqrMagnitude;
            if (Math.Abs(t) < 0.0001f) return xy > 0 ? float.MaxValue : float.MinValue;
            return xy * Mathf.Abs(xy) / (f * t);
        }

        public static float CosSquare(Vector3 from, Vector3 to)
        {
            var xyz = from.x * to.x + from.y * to.y + from.z * to.z;
            var f = from.sqrMagnitude;
            if (Math.Abs(f) < 0.0001f) return xyz > 0 ? float.MaxValue : float.MinValue;
            var t = to.sqrMagnitude;
            if (Math.Abs(t) < 0.0001f) return xyz > 0 ? float.MaxValue : float.MinValue;
            return xyz * Mathf.Abs(xyz) / (f * t);
        }
        /// <summary>
        /// 计算一个二维向量的斜率
        /// </summary>
        /// <param name="vector2">一个二维向量</param>
        /// <returns></returns>
        public static float Slope(Vector2 vector2)
        {
            if (Mathf.Abs(vector2.x) < 0.0001)
            {
                return Mathf.Infinity;
            }
            return vector2.y / vector2.x;
        }
        public static float DistanceSquare(Vector2 from, Vector2 to)
        {
            var dx = from.x - to.x;
            var dy = from.y - to.y;
            return dx * dx + dy * dy;
        }
        public static float DistanceSquare(GesturePoint from, GesturePoint to)
        {
            var dx = from.x - to.x;
            var dy = from.y - to.y;
            return dx * dx + dy * dy;
        }

        public static float DistanceSquare(Vector3 from, Vector3 to)
        {
            var dx = from.x - to.x;
            var dy = from.y - to.y;
            var dz = from.z - to.z;
            return dx * dx + dy * dy + dz * dz;
        }

        public static float Cross(Vector2 from, Vector2 to)
        {
            return from.x * to.y - from.y * to.x;
        }

        public static Direction JudgeDirection(Vector2 vector2)
        {
            return JudgeDirection(Vector2.zero, vector2);
        }
        public static Direction JudgeDirection(Vector2 from, Vector2 to)
        {
            var slope = Slope(to - from);
            if (slope > GestureConstant.tan67p5 || slope < -GestureConstant.tan67p5)
            {
                return to.y > from.y ? Direction.Up : Direction.Down;
            }
            if (slope > GestureConstant.tan22p5)
            {
                return to.y > @from.y ? Direction.Diagonal45 : Direction.Diagonal225;
            }
            if (slope > -GestureConstant.tan22p5)
            {
                return to.x > @from.x ? Direction.Right : Direction.Left;
            }
            return to.x > @from.x ? Direction.Diagonal315 : Direction.Diagonal135;
        }
        public static Direction JudgeDirection(GesturePoint from, GesturePoint to)
        {
            var slope = Slope(to - from);
            if (slope > GestureConstant.tan67p5 || slope < -GestureConstant.tan67p5)
            {
                return to.y > from.y ? Direction.Up : Direction.Down;
            }
            if (slope > GestureConstant.tan22p5)
            {
                return to.y > @from.y ? Direction.Diagonal45 : Direction.Diagonal225;
            }
            if (slope > -GestureConstant.tan22p5)
            {
                return to.x > @from.x ? Direction.Right : Direction.Left;
            }
            return to.x > @from.x ? Direction.Diagonal315 : Direction.Diagonal135;
        }

        public static InflectionPointFeature JudgeAngle(float coss, bool isClockwise)
        {
            if (coss > GestureConstant.coss22p5)
            {
                return InflectionPointFeature.Straight;
            }
            if (coss > GestureConstant.coss75)
            {
                return isClockwise ? InflectionPointFeature.ClockwiseAcuteAngle : InflectionPointFeature.AntiClockwiseAcuteAngle;
            }
            if (coss > GestureConstant.coss105)
            {
                return isClockwise ? InflectionPointFeature.ClockwiseRightAngle : InflectionPointFeature.AntiClockwiseRightAngle;
            }
            if (coss > GestureConstant.coss157p5)
            {
                return isClockwise ? InflectionPointFeature.ClockwiseObtuseAngle : InflectionPointFeature.AntiClockwiseObtuseAngle;
            }
            return InflectionPointFeature.Turn;
        }

        /// <summary>
        /// 判断拐点类型
        /// </summary>
        /// <param name="vector1">输入向量1</param>
        /// <param name="vector2">输入向量2</param>
        /// <returns></returns>
        public static InflectionPointFeature JudgeAngle(Vector2 vector1, Vector2 vector2)
        {
            return JudgeAngle(MathExtension.CosSquare(vector1, vector2), MathExtension.Cross(vector1, vector2) > 0);
        }

        /// <summary>
        /// 标准化，返回一个数最接近指定粒度的值
        /// </summary>
        /// <param name="num">输入数</param>
        /// <param name="unit">步长</param>
        /// <returns></returns>
        public static float Normalize(float num, float unit)
        {
            var x = ((int)(num / unit)) * unit;
            var r = num - x;
            return r < unit / 2 ? r : r + unit;
        }

        public static float Normalize(float num)
        {
            return Normalize(num, 1f);
        }

        public static Vector3 Normalize(Vector3 vector3, Vector3 unit)
        {
            return new Vector3(Normalize(vector3.x, unit.x), Normalize(vector3.y, unit.y), Normalize(vector3.z, unit.z));
        }

        public static Vector3 Normalize(Vector3 vector3)
        {
            return new Vector3(Normalize(vector3.x), Normalize(vector3.y), Normalize(vector3.z));
        }
    }
}
