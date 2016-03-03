using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Tools;
using UnityEngine;

namespace GestureRecognition
{
    public enum InflectionPointFeature
    {
        Unknown,
        Start,
        Straight,// 直线
        ClockwiseAcuteAngle,// 顺时针锐角
        AntiClockwiseAcuteAngle,// 逆时针锐角
        ClockwiseRightAngle,// 直角
        AntiClockwiseRightAngle,// 直角
        ClockwiseObtuseAngle,// 钝角
        AntiClockwiseObtuseAngle,// 钝角
        Turn,// 转向
        Break,// 断开
        End,
        TouchCountChanged
    }
    public struct GesturePoint
    {
        public float x;
        public float y;

        public InflectionPointFeature Feature;

        public Vector2 Vector2
        {
            get
            {
                return new Vector2(x, y);
            }
        }
        public long Timestamp;

        public GesturePoint(float x, float y, long timestamp)
        {
            this.x = x;
            this.y = y;
            this.Timestamp = timestamp;
            this.Feature = InflectionPointFeature.Unknown;
        }

        public GesturePoint(Vector2 vector2, long timestamp)
        {
            this.x = vector2.x;
            this.y = vector2.y;
            this.Timestamp = timestamp;
            this.Feature = InflectionPointFeature.Unknown;
        }

        public GesturePoint(Vector2 vector2, long timestamp, InflectionPointFeature feature)
        {
            this.x = vector2.x;
            this.y = vector2.y;
            this.Timestamp = timestamp;
            this.Feature = feature;
        }

        public GesturePoint(Vector2 vector2, long timestamp, InflectionPointFeature feature, float coss)
        {
            this.x = vector2.x;
            this.y = vector2.y;
            this.Timestamp = timestamp;
            this.Feature = feature;
        }

        public static float Distance(GesturePoint point1, GesturePoint point2)
        {
            return Mathf.Sqrt(GesturePoint.DistanceSquare(point1, point2));
        }

        public static float DistanceSquare(GesturePoint point1, GesturePoint point2)
        {
            var dx = point2.x - point1.x;
            var dy = point2.y - point1.y;
            return dx * dx + dy * dy;
        }

        public static Vector2 operator +(GesturePoint point1, GesturePoint point2)
        {
            return new Vector2(point1.x + point2.x, point1.y + point2.y);
        }
        public static Vector2 operator -(GesturePoint point1, GesturePoint point2)
        {
            return new Vector2(point1.x - point2.x, point1.y - point2.y);
        }
    }

    public struct GestureVector
    {
        public float x;
        public float y;
        public long TimestampStart;
        public long TimestampEnd;

        public Direction Direction
        {
            get
            {
                return MathExtension.JudgeDirection(new Vector2(x, y));
            }
        }

        public long Duration
        {
            get
            {
                return TimestampEnd - TimestampStart;
            }
        }

        public GestureVector(float x, float y, long timestampStart, long timestampEnd)
        {
            this.x = x;
            this.y = y;
            this.TimestampStart = timestampStart;
            this.TimestampEnd = timestampEnd;
        }
    }

    /// <summary>
    /// 线段
    /// </summary>
    public struct Segment
    {
        public Vector2 Start;
        public Vector2 End;
        public Segment(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
        }
        // 求得两线段是否相交，并不求得交点
        public static bool IsCrossing(Segment s1, Segment s2)
        {
            return Segment.IsCrossing(s1.Start, s1.End, s2.Start, s2.End);
        }
        // 求得p11、p12组成的线段是否与p21、p22组成的线段相交
        public static bool IsCrossing(Vector2 p11, Vector2 p12, Vector2 p21, Vector2 p22)
        {
            var v1 = p12 - p11;
            if (((int)MathExtension.Cross(v1, p21 - p11) ^ (int)MathExtension.Cross(v1, p22 - p11)) > 0)
                return false;
            var v2 = p22 - p21;
            return ((int)MathExtension.Cross(v2, p11 - p21) ^ (int)MathExtension.Cross(v1, p12 - p21)) <= 0;
        }

        public static bool IsCrossing(GesturePoint p11, GesturePoint p12, GesturePoint p21, GesturePoint p22)
        {
            var v1 = p12 - p11;
            if (((int)MathExtension.Cross(v1, p21 - p11) ^ (int)MathExtension.Cross(v1, p22 - p11)) > 0)
                return false;
            var v2 = p22 - p21;
            return ((int)MathExtension.Cross(v2, p11 - p21) ^ (int)MathExtension.Cross(v2, p12 - p21)) <= 0;
        }
    }

    /// <summary>
    /// 手势信息，由一个包含二维点坐标的集合生成
    /// 手势信息包含硬件设备采集用户输入的原始信息
    /// 初始化手势信息对象时，输入的原始数据会被做预处理
    /// </summary>
    public class GesturePath
    {
        public long StartTimestamp { get; set; }
        public long EndTimestamp { get; set; }

        public long Duration
        {
            get
            {
                return EndTimestamp - StartTimestamp;
            }
        }
        public GestureType Type { get; set; }
        // 向量特征枚举
        public enum GestureFeatureType
        {
            Undefined, // 未定义
            Line, // 直线
            Arc, // 弧线
            SingleString, // 单弦
            DoubleString, // 双弦线
            MutipleString // 多弦线
        }

        // 所有手势点
        public List<GesturePoint> AllPoints = new List<GesturePoint>();
        // 所有拐点
        public List<GesturePoint> InflectionPoints = new List<GesturePoint>();
        // 所有手势拐点的夹角余弦平方值
        public List<float> AngleCosSquare = new List<float>();
        // 所有手势向量
        public List<Vector2> AllVectors = new List<Vector2>();
        public List<Vector2> AllNormalizedVectors = new List<Vector2>();
        // 当前向量
        public Vector2 CurrentVector;
        // 所有向量的模的平方
        public List<float> VectorSquareMagnitude = new List<float>();
        // 异变点
        public List<GesturePoint> MutationPoints = new List<GesturePoint>();

        public const float TinyVectorThresholds = GestureManager.MaxTouchCount;

        public Vector2 MiddlePoint
        {
            get
            {
                return InflectionPoints.Count > 0 ? InflectionPoints[InflectionPoints.Count/2].Vector2 : Vector2.zero;
            }
        }

        public int CrossingCount { get; set; }

        public GesturePath()
        {
            StartTimestamp = 0;
            EndTimestamp = 0;

            AllPoints = new List<GesturePoint>();
            InflectionPoints = new List<GesturePoint>();
            AngleCosSquare = new List<float>();
            AllVectors = new List<Vector2>();
            AllNormalizedVectors = new List<Vector2>();
            VectorSquareMagnitude = new List<float>();
            MutationPoints = new List<GesturePoint>();
        }
        // 初始化手势信息
        public void Initialize(long timestamp, Vector2 lastPoint)
        {
            StartTimestamp = timestamp;
            AllPoints.Clear();
            AllPoints.Add(new GesturePoint(lastPoint, timestamp, InflectionPointFeature.Start));
            InflectionPoints.Clear();
            InflectionPoints.Add(new GesturePoint(lastPoint, timestamp, InflectionPointFeature.Start));
            AngleCosSquare.Clear();
            AngleCosSquare.Add(float.PositiveInfinity);
            AllVectors.Clear();
            AllNormalizedVectors.Clear();
            VectorSquareMagnitude.Clear();
            MutationPoints.Clear();
        }

        public void OnTouchEnd(long timeStamp, GesturePoint lastPoint, Vector2 lastVector)
        {
            EndTimestamp = timeStamp;
            lastPoint.Feature = InflectionPointFeature.End;
            AllPoints.Add(lastPoint);
            InflectionPoints.Add(lastPoint);
            AngleCosSquare.Add(float.PositiveInfinity);
            AllNormalizedVectors.Add(lastVector);
            VectorSquareMagnitude.Add(lastVector.sqrMagnitude);

            // 判断异变点数量
            JudgeMutationPoints();
        }

        // 解算异变点
        protected void JudgeMutationPoints()
        {
            if (AllVectors.Count < 2) return;
            var tendency = MathExtension.Cross(AllVectors[0], AllVectors[1]) > 0;
            for (var i = 1; i < AllVectors.Count - 1; i++)
            {
                if (!(tendency ^ (MathExtension.Cross(AllVectors[i], AllVectors[i + 1]) > 0))) continue;
                var coss = MathExtension.CosSquare(AllVectors[i], AllVectors[i + 1]);
                if (coss > GestureConstant.coss10 || coss < GestureConstant.coss170) continue;
                MutationPoints.Add(AllPoints[i + 1]);
                tendency = !tendency;
            }
        }
        // 解算交点
        protected void JudgeCrossingPoint()
        {
            for (var i = 0; i < InflectionPoints.Count - 3; i++)
            {
                for (var j = i + 2; j < InflectionPoints.Count - 1; j++)
                {
                    if (Segment.IsCrossing(InflectionPoints[i], InflectionPoints[i + 1], InflectionPoints[j], InflectionPoints[j + 1]))
                        CrossingCount++;
                    Debug.Log(Segment.IsCrossing(InflectionPoints[i], InflectionPoints[i + 1], InflectionPoints[j], InflectionPoints[j + 1]));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder("+手势信息");
            builder.Append("\n经过时间:").Append(Duration / 10000).Append("毫秒")
                .Append("\n点数:").Append(AllPoints.Count)
                .Append("\n异变点数:").Append(MutationPoints.Count)
                .Append("\n向量数:").Append(AllNormalizedVectors.Count)
                .Append("\n拐点数:").Append(InflectionPoints.Count());
            return builder.ToString();
        }
    }
}
