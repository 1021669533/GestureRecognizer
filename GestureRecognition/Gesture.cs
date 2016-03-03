using System.Collections.Generic;
using UnityEngine;

namespace GestureRecognition
{
    public enum GestureType
    {
        Void,
        Click1,
        LongClick1,
        LineUpward,
        DoubleLineUpward,
        TripleLineUpward,
        LineDownward,
        DoubleLineDownward,
        TripleLineDownward,
        LineLeftward,
        DoubleLineLeftward,
        TripleLineLeftward,
        LineRightward,
        DoubleLineRightward,
        TripleLineRightward,
        LineDiagonal45,
        DoubleLineDiagonal45,
        TripleLineDiagonal45,
        LineDiagonal135,
        DoubleLineDiagonal135,
        TripleLineDiagonal135,
        LineDiagonal225,
        DoubleLineDiagonal225,
        TripleLineDiagonal225,
        LineDiagonal315,
        DoubleLineDiagonal315,
        TripleLineDiagonal315,
        ArrowUpward1, ArrowUpward2,
        ArrowDownward1, ArrowDownward2,
        ArrowLeftward1, ArrowLeftward2,
        ArrowRightward1, ArrowRightward2,
        SingleKnotUpward1,
        SingleKnotDownward1,
        SingleKnotLeftward1,
        SingleKnotRightward1,
        TriangularUpward,
        TriangularDownward,
        TriangularLeftward,
        TriangularRightward,
        M1, M2,
        N1, N2,
    }
    public enum RealTimeGestureType
    {
        Void,                           //空
        Stay1,                          //单指停留
        Stay2,                          //双指停留
        Stay3,
        Upwards1,                       //单指向上
        Upwards2,                       //双指向上
        Upwards3,                       //三指向上
        Downwards1,                     //单指向下
        Downwards2,                     //双指向下
        Downwards3,
        Rightwards1,                    //单指向右
        Rightwards2,                    //双指向右
        Rightwards3,
        Leftwards1,                     //单指向左
        Leftwards2,                     //单指向右
        Leftwards3,
        Diagonal45_1,
        Diagonal45_2,
        Diagonal45_3,
        Diagonal135_1,
        Diagonal135_2,
        Diagonal135_3,
        Diagonal225_1,
        Diagonal225_2,
        Diagonal225_3,
        Diagonal315_1,
        Diagonal315_2,
        Diagonal315_3,
        Pinch,                          // 双指聚拢
        Expand                          // 双指张开
    }

    public enum Platform
    {
        Windows,
        OSX,
        Android,
        iOS,
    }
    public class GestureManager : MonoBehaviour, IRealTimeGestureListener, ISemiRealTimeGestureListener, INonRealTimeGestureListener
    {
        private static GestureManager _instance;

        public Platform Platform = Platform.Windows;

        private List<IRealTimeGestureListener> _realTimeGestureListeners;
        private List<ISemiRealTimeGestureListener> _semiRealTimeGestureListeners;
        private List<INonRealTimeGestureListener> _nonRealTimeGestureListeners;

        public GestureRecorder Recorder { protected set; get; }
        private GestureRealTimeRecognizer _realTimeRecognizer;
        private GestureSemiRealTimeRecognizer _semiRealTimeRecognizer;
        private GestureNonRealTimeRecognizer _nonRealTimeRecognizer;

        public const int MaxTouchCount = 3;
        public static readonly float MinRecognizeOffset = Screen.height * Screen.height / 400f;
        public static readonly float MinRealTimeOffset = Screen.height * Screen.height / 6400f;

        public bool GestureRecognizeEnable
        {
            set
            {
                Recorder.GestureRecognizeEnable = value;
            }
            get
            {
                return Recorder.GestureRecognizeEnable;
            }
        }
        public EventState EventState
        {
            set
            {
                Recorder.EventState = value;
            }
            get
            {
                return Recorder.EventState;
            }
        }

        public static GestureManager GetInstance()
        {
            return _instance ?? FindObjectOfType<GestureManager>();
        }
        public void Awake()
        {
            _instance = this;
            switch (Platform)
            {
                case Platform.Windows:
                case Platform.OSX:
                    Recorder = new GestureRecorderPC();
                    break;
                case Platform.Android:
                case Platform.iOS:
                    Recorder = new GestureRecorderMobile();
                    break;
            }
            _realTimeGestureListeners = new List<IRealTimeGestureListener>();
            _semiRealTimeGestureListeners = new List<ISemiRealTimeGestureListener>();
            _nonRealTimeGestureListeners = new List<INonRealTimeGestureListener>();

            _realTimeRecognizer = new GestureRealTimeRecognizer(this);
            _semiRealTimeRecognizer = new GestureSemiRealTimeRecognizer(this);
            _nonRealTimeRecognizer = new GestureNonRealTimeRecognizer(this);
            Recorder.SetRealTimeRecognizer(_realTimeRecognizer);
            Recorder.SetSemiRealTimeRecognizer(_semiRealTimeRecognizer);
            Recorder.SetNonRealTimeRecognizer(_nonRealTimeRecognizer);
        }

        public void AddNonRealGestureParser(NonRealTimeGestureParser parser)
        {
            _nonRealTimeRecognizer.AddParser(parser);
        }

        public void Update()
        {
            if (!GestureRecognizeEnable) return;
            Recorder.Record();
        }

        public void AddListener(IRealTimeGestureListener listener)
        {
            if (!_realTimeGestureListeners.Contains(listener))
            {
                _realTimeGestureListeners.Add(listener);
            }
        }

        public void AddListener(ISemiRealTimeGestureListener listener)
        {
            if (!_semiRealTimeGestureListeners.Contains(listener))
            {
                _semiRealTimeGestureListeners.Add(listener);
            }
        }

        public void AddListener(INonRealTimeGestureListener listener)
        {
            if (!_nonRealTimeGestureListeners.Contains(listener))
            {
                _nonRealTimeGestureListeners.Add(listener);
            }
        }

        public void RemoveListener(IRealTimeGestureListener listener)
        {
            _realTimeGestureListeners.Remove(listener);
        }

        public void RemoveListener(ISemiRealTimeGestureListener listener)
        {
            _semiRealTimeGestureListeners.Remove(listener);
        }

        public void RemoveListener(INonRealTimeGestureListener listener)
        {
            _nonRealTimeGestureListeners.Remove(listener);
        }

        public void NotifyType(RealTimeGestureType type)
        {
            foreach (var listener in _realTimeGestureListeners)
            {
                listener.NotifyType(type);
            }
        }

        public void NotifyState(InflectionPointFeature state)
        {
            foreach (var listener in _realTimeGestureListeners)
            {
                listener.NotifyState(state);
            }
        }

        public void SemiNotify(GesturePath[] paths, int touchCount)
        {
            foreach (var listener in _semiRealTimeGestureListeners)
            {
                listener.SemiNotify(paths, touchCount);
            }
        }

        public void OnTouchBegin(int touchCount)
        {
            foreach (var listener in _semiRealTimeGestureListeners)
            {
                listener.OnTouchBegin(touchCount);
            }
        }

        public void OnTouchStartMove(Vector2[] vectors, int touchCount)
        {
            foreach (var listener in _semiRealTimeGestureListeners)
            {
                listener.OnTouchStartMove(vectors, touchCount);
            }
        }

        public void OnTouchesCountChanged(int lastTouchesCount, int currentTouchesCount)
        {
            foreach (var listener in _semiRealTimeGestureListeners)
            {
                listener.OnTouchesCountChanged(lastTouchesCount, currentTouchesCount);
            }
        }

        public void OnTouchEnd()
        {
            foreach (var listener in _semiRealTimeGestureListeners)
            {
                listener.OnTouchEnd();
            }
        }

        public void NonNotify(GesturePath[] paths, GestureType type)
        {
            foreach (var listener in _nonRealTimeGestureListeners)
            {
                listener.NonNotify(paths, type);
            }
        }

        public static int GetExpectGestureCount(GestureType type)
        {
            return 1;
        }
    }

    public interface IRealTimeGestureListener
    {
        void NotifyType(RealTimeGestureType type);
        void NotifyState(InflectionPointFeature state);
    }

    public interface ISemiRealTimeGestureListener
    {
        // 当手势中的任何向量发生一定程度的转向之后，通知当前向量的方向
        void SemiNotify(GesturePath[] paths, int touchCount);
        void OnTouchBegin(int touchCount);
        void OnTouchStartMove(Vector2[] vectors, int touchCount);
        void OnTouchesCountChanged(int lastTouchesCount, int currentTouchesCount);
        void OnTouchEnd();
    }

    public interface INonRealTimeGestureListener
    {
        void NonNotify(GesturePath[] paths, GestureType type);
    }

}
