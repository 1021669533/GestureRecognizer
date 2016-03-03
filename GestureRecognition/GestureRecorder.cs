using System;
using System.Collections.Generic;
using GestureRecognition.Tools;
using UnityEngine;

namespace GestureRecognition
{
    public enum EventState
    {
        Free,
        Catched
    }
    public abstract class GestureRecorder
    {
        protected GestureRealTimeRecognizer RealTimeRecognizer;
        protected GestureNonRealTimeRecognizer NonRealTimeRecognizer;
        protected GestureSemiRealTimeRecognizer SemiRealTimeRecognizer;

        protected bool RealTimeRecognizeEnable { get; set; }
        protected bool NonRealTimeRecognizeEnable { get; set; }
        protected bool SemiRealTimeRecognizeEnable { get; set; }

        protected long LastTimestamp = 0;// 上一个有效的时间戳
        protected long CurrentTimestamp = 0;// 当前有效的时间戳
        // 提取手势点时最小的分辨距离
        public static readonly int MinGesturePointDistance = Screen.height * Screen.height / 0x32;
        public const long RealTimeReportInterval = 500000;// 最小提交间隔（纳秒）

        public EventState EventState { set; get; }

        public bool GestureRecognizeEnable { set; get; }
        public abstract void Record();
        public abstract void RealTimeReport();
        public abstract void NonRealTimeReport();
        public abstract void SemiRealTimeReport();

        protected abstract void OnTouchBegin();
        protected GestureRecorder()
        {
            GestureRecognizeEnable = true;
            EventState = EventState.Free;
            RealTimeRecognizeEnable = false;
            NonRealTimeRecognizeEnable = false;
            SemiRealTimeRecognizeEnable = false;
        }

        protected GestureRecorder(GestureRealTimeRecognizer realTimeRecognizer,
            GestureNonRealTimeRecognizer nonRealTimeRecognizer, GestureSemiRealTimeRecognizer semiRealTimeRecognizer)
        {
            RealTimeRecognizer = realTimeRecognizer;
            RealTimeRecognizeEnable = true;
            NonRealTimeRecognizer = nonRealTimeRecognizer;
            NonRealTimeRecognizeEnable = true;
            SemiRealTimeRecognizer = semiRealTimeRecognizer;
            SemiRealTimeRecognizeEnable = true;
        }

        public void SetRealTimeRecognizer(GestureRealTimeRecognizer recognizer)
        {
            RealTimeRecognizer = recognizer;
            RealTimeRecognizeEnable = true;
        }

        public void RemoveRealTimeRecognizer()
        {
            RealTimeRecognizer = null;
            RealTimeRecognizeEnable = false;
        }
        public void SetNonRealTimeRecognizer(GestureNonRealTimeRecognizer recognizer)
        {
            NonRealTimeRecognizer = recognizer;
            NonRealTimeRecognizeEnable = true;
        }

        public void RemoveNonRealTimeRecognizer()
        {
            NonRealTimeRecognizer = null;
            NonRealTimeRecognizeEnable = false;
        }
        public void SetSemiRealTimeRecognizer(GestureSemiRealTimeRecognizer recognizer)
        {
            SemiRealTimeRecognizer = recognizer;
            SemiRealTimeRecognizeEnable = true;
        }

        public void RemoveSemiRealTimeRecognizer()
        {
            SemiRealTimeRecognizer = null;
            SemiRealTimeRecognizeEnable = false;
        }
    }

    /// <summary>
    /// 针对移动设备的 Recorder ,可识别多点触摸手势
    /// </summary>
    public class GestureRecorderMobile : GestureRecorder
    {
        protected int LastTouchesCount = 0;// 最后一次触摸的触摸点数量
        protected GesturePoint[] LastRealTimePoints = new GesturePoint[GestureManager.MaxTouchCount];
        protected GesturePoint[] LastPoints = new GesturePoint[GestureManager.MaxTouchCount];
        protected Vector2[] LastVectors = new Vector2[GestureManager.MaxTouchCount];// 上一组向量
        protected Vector2[] LastTinyVectors = new Vector2[GestureManager.MaxTouchCount];// 上一组微小向量
        protected bool[] VectorAvaliable = new bool[GestureManager.MaxTouchCount];

        protected List<GesturePath> InfoList = new List<GesturePath>();

        protected bool[] VectorRecordEnable = new bool[GestureManager.MaxTouchCount];

        protected GesturePath[] GesturePaths = new GesturePath[GestureManager.MaxTouchCount];

        public GestureRecorderMobile()
        {
            Input.multiTouchEnabled = true;
            for (var i = 0; i < GestureManager.MaxTouchCount; i++)
            {
                GesturePaths[i] = new GesturePath();
            }
        }

        /// <summary>
        /// You should call this method every frame
        /// 
        /// </summary>
        public override void Record()
        {
            // 触摸点数量大于最大判断数量，直接返回
            if (!GestureRecognizeEnable || Input.touchCount > GestureManager.MaxTouchCount) return;
            // 开始触摸，处理完成后将结束事件
            if (LastTouchesCount == 0 && Input.touchCount > 0)
            {
                EventState = EventState.Free;
                OnTouchBegin();
                return;
            }
            // 事件结束
            if (Input.touchCount == 0)
            {
                if (LastTouchesCount != 0)
                {
                    OnTouchEnd();
                }
                return;
            }
            // 若触摸点数目改变，则立即提交本次事件，并初始化手势信息
            if (LastTouchesCount != Input.touchCount)
            {
                OnTouchCountChanged(Input.touchCount);
                return;
            }
            CurrentTimestamp = DateTime.Now.Ticks;
            OnTouchMove();
            LastTimestamp = CurrentTimestamp;
        }

        protected override void OnTouchBegin()
        {
            CurrentTimestamp = DateTime.Now.Ticks;
            LastTouchesCount = Input.touchCount;
            LastTimestamp = DateTime.Now.Ticks;
            for (var i = 0; i < Input.touchCount; i++)
            {
                VectorRecordEnable[i] = false;
                LastPoints[i] = new GesturePoint(Input.touches[i].position, LastTimestamp);
                LastRealTimePoints[i] = LastPoints[i];
                GesturePaths[i].Initialize(CurrentTimestamp, Input.touches[i].position);

            }
            // 触摸开始，通知手势点
            SemiRealTimeRecognizer.OnTouchBegin(LastTouchesCount);
        }

        public void OnTouchMove()
        {
            SemiRealTimeReport();
            if (CurrentTimestamp - LastRealTimePoints[0].Timestamp < RealTimeReportInterval) return;
            RealTimeReport();
            for (var i = 0; i < LastTouchesCount; i++)
            {
                LastRealTimePoints[i] = new GesturePoint(Input.touches[i].position, CurrentTimestamp);
            }
        }

        protected void OnTouchEnd()
        {
            for (var i = 0; i < GestureManager.MaxTouchCount; i++)
            {
                GesturePaths[i].OnTouchEnd(CurrentTimestamp, LastPoints[i], LastVectors[i]);
            }
            if (SemiRealTimeRecognizeEnable)
            {
                SemiRealTimeRecognizer.OnTouchEnd();
            }
            NonRealTimeReport();
            LastTouchesCount = 0;
        }
        protected void OnTouchCountChanged(int count)
        {
            if (SemiRealTimeRecognizeEnable)
            {
                SemiRealTimeRecognizer.OnTouchesCountChanged(LastTouchesCount, count);
            }
            LastTouchesCount = count;
            OnTouchBegin();
        }
        // 实时报告
        public override void RealTimeReport()
        {
            if (!RealTimeRecognizeEnable) return;
            switch (LastTouchesCount)
            {
                case 1:
                    RealTimeRecognizer.Recognize(LastRealTimePoints[0], new GesturePoint(Input.touches[0].position, CurrentTimestamp));
                    break;
                case 2:
                    RealTimeRecognizer.Recognize(LastRealTimePoints[0], new GesturePoint(Input.touches[0].position, CurrentTimestamp), LastRealTimePoints[1], new GesturePoint(Input.touches[1].position, CurrentTimestamp));
                    break;
                case 3:
                    RealTimeRecognizer.Recognize(LastRealTimePoints[0], new GesturePoint(Input.touches[0].position, CurrentTimestamp), LastRealTimePoints[1], new GesturePoint(Input.touches[1].position, CurrentTimestamp), LastRealTimePoints[2], new GesturePoint(Input.touches[2].position, CurrentTimestamp));
                    break;
                default:
                    break;
            }
        }
        // 半实时报告
        public override void SemiRealTimeReport()
        {
            if (!SemiRealTimeRecognizeEnable) return;
            var notifyEnable = false;
            for (var i = 0; i < LastTouchesCount; i++)
            {
                // 当前触摸点与上一个有效向量点组成的向量
                var vector2 = Input.touches[i].position - LastPoints[i].Vector2;
                // 当前向量的模平方
                var squareMagnitude = vector2.sqrMagnitude;
                if (squareMagnitude < GestureManager.MinRecognizeOffset) continue;
                // 添加手势点（所有）
                GesturePaths[i].AllPoints.Add(LastPoints[i]);
                GesturePaths[i].AllVectors.Add(vector2);
                // 此时形成第一段手势向量，不参与夹角运算
                if (!VectorRecordEnable[i])
                {
                    VectorRecordEnable[i] = true;
                    LastVectors[i] = vector2;
                    SemiRealTimeRecognizer.OnTouchStartMove(LastVectors, LastTouchesCount);
                    LastPoints[i] = new GesturePoint(Input.touches[i].position, CurrentTimestamp);
                    continue;
                }
                // 不接受只含有2个点的微小向量
                if (!VectorAvaliable[i])
                {
                    VectorAvaliable[i] = true;
                    LastVectors[i] = vector2;
                    LastPoints[i] = new GesturePoint(Input.touches[i].position, CurrentTimestamp);
                    continue;
                }
                // 判断手势向量夹角，小于45度则视为同一段向量
                var coss = MathExtension.CosSquare(vector2, LastVectors[i]);
                if (coss > GestureConstant.coss45)
                {
                    LastVectors[i] = LastVectors[i] + vector2;
                    LastPoints[i] = new GesturePoint(Input.touches[i].position, CurrentTimestamp);
                    continue;
                }
                // 计算拐点类型
                notifyEnable = true;
                LastPoints[i].Feature = MathExtension.JudgeAngle(coss, MathExtension.Cross(vector2, LastVectors[i]) > 0);
                GesturePaths[i].InflectionPoints.Add(LastPoints[i]);
                GesturePaths[i].AllNormalizedVectors.Add(LastVectors[i]);
                GesturePaths[i].CurrentVector = vector2;
                GesturePaths[i].VectorSquareMagnitude.Add(squareMagnitude);
                LastPoints[i] = new GesturePoint(Input.touches[i].position, CurrentTimestamp);
                LastVectors[i] = vector2;
                VectorAvaliable[i] = false;
            }
            if (notifyEnable)
            {
                SemiRealTimeRecognizer.SemiNotify(GesturePaths, LastTouchesCount);
            }
        }
        /// <summary>
        /// 非实时报告，报告与当前触摸点数相当的路径
        /// </summary>
        public override void NonRealTimeReport()
        {
            if (!NonRealTimeRecognizeEnable) return;
            var array = new GesturePath[LastTouchesCount];
            for (var i = 0; i < LastTouchesCount; i++)
            {
                array[i] = GesturePaths[i];
            }
            NonRealTimeRecognizer.NotifyInfo(array);
        }
    }
    /// <summary>
    /// 针对 PC 端的 Recorder ,不识别多点触控
    /// </summary>
    public class GestureRecorderPC : GestureRecorder
    {
        // 上次记录的手势点，其最小间距为MinRecognizeOffset
        protected GesturePoint LastPoint;
        // 上次记录的最小实时手势点，其最小间距为MinRealTimeOffset
        protected GesturePoint LastRealTimePoint;
        protected Vector2 LastVector;
        protected bool LastMutationTendency;

        protected bool VectorAvaliable = false;

        protected bool VectorRecordEnable = false;

        protected GesturePath GesturePath = new GesturePath();
        public override void Record()
        {
            // 事件开始
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchBegin();
                return;
            }
            // 事件结束
            if (Input.GetMouseButtonUp(0))
            {
                // 终止端点
                GesturePath.OnTouchEnd(CurrentTimestamp, LastPoint, LastVector);
                if (SemiRealTimeRecognizeEnable)
                {
                    SemiRealTimeRecognizer.OnTouchEnd();
                }
                NonRealTimeReport();
            }
            CurrentTimestamp = DateTime.Now.Ticks;
            if (Input.GetMouseButton(0))
            {
                SemiRealTimeReport();
                if (CurrentTimestamp - LastRealTimePoint.Timestamp <= RealTimeReportInterval) return;
                RealTimeReport();
                LastRealTimePoint = new GesturePoint(Input.mousePosition, CurrentTimestamp);
            }
            LastTimestamp = CurrentTimestamp;
        }

        protected override void OnTouchBegin()
        {
            // 初始化时间戳
            CurrentTimestamp = DateTime.Now.Ticks;
            LastTimestamp = DateTime.Now.Ticks;
            // 向量记录将在至少两个点被记录后启动
            VectorRecordEnable = false;
            // 初始化向量点
            LastPoint = new GesturePoint(Input.mousePosition, LastTimestamp, InflectionPointFeature.Start);
            LastRealTimePoint = LastPoint;
            // 初始化手势信息对象
            GesturePath.Initialize(CurrentTimestamp, LastPoint.Vector2); 
            SemiRealTimeRecognizer.OnTouchBegin(1);
        }

        public override void RealTimeReport()
        {
            if (!RealTimeRecognizeEnable) return;
            RealTimeRecognizer.Recognize(LastRealTimePoint, new GesturePoint(Input.mousePosition, CurrentTimestamp));
        }

        public override void SemiRealTimeReport()
        {
            if (!SemiRealTimeRecognizeEnable) return;
            var vector2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - LastPoint.Vector2;
            // 与上个手势点的模平方
            var squareMagnitude = vector2.sqrMagnitude;
            if (squareMagnitude < GestureManager.MinRecognizeOffset) return;
            GesturePath.AllPoints.Add(LastPoint);
            GesturePath.AllVectors.Add(vector2);
            if (!VectorRecordEnable)
            {
                VectorRecordEnable = true;
                LastVector = vector2;
                SemiRealTimeRecognizer.OnTouchStartMove(new []{vector2}, 1);
                LastPoint = new GesturePoint(Input.mousePosition, CurrentTimestamp);
                return;
            }
            if (!VectorAvaliable)
            {
                VectorAvaliable = true;
                LastVector = vector2;
                LastPoint = new GesturePoint(Input.mousePosition, CurrentTimestamp);
                return;
            }
            var coss = MathExtension.CosSquare(vector2, LastVector);
            if (coss > GestureConstant.coss45)
            {
                LastVector = LastVector + vector2;
                LastPoint = new GesturePoint(Input.mousePosition, CurrentTimestamp);
                return;
            }
            // 计算拐点类型
            LastPoint.Feature = MathExtension.JudgeAngle(coss, MathExtension.Cross(vector2, LastVector) > 0);
            GesturePath.InflectionPoints.Add(LastPoint);
            GesturePath.AllNormalizedVectors.Add(LastVector);
            GesturePath.CurrentVector = vector2;
            GesturePath.VectorSquareMagnitude.Add(squareMagnitude);
            LastPoint = new GesturePoint(Input.mousePosition, CurrentTimestamp);
            LastVector = vector2;
            VectorAvaliable = false;
            SemiRealTimeRecognizer.SemiNotify(new[] { GesturePath }, 1);
        }
        public override void NonRealTimeReport()
        {
            if (!NonRealTimeRecognizeEnable) return;
            NonRealTimeRecognizer.NotifyInfo(new[] { GesturePath });
        }
    }
}
