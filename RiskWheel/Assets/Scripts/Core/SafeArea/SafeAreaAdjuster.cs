using System;
using UnityEngine;

namespace ScreenAreaAdjuster
{
    //*************************************************************************************************
    /// <summary>
    /// Safe Area Adjuster
    /// </summary>
    //*************************************************************************************************
    public class SafeAreaAdjuster : MonoBehaviour
    {
        private RectTransform _panelRectTrans;
        private bool _isInitScreenSizeSimulate;

        /// <summary>Target RectTransfrom</summary>
        public RectTransform PanelRectTrans
        {
            get
            {
                if (_panelRectTrans == null)
                {
                    _panelRectTrans = GetComponent<RectTransform>();
                }

                return _panelRectTrans;
            }
        }

        [SerializeField] private bool isAutoScale;
        [SerializeField] private bool isBannerAreaDisabled = false;
        [SerializeField] private bool isScaleForIphone = false;

        private Rect _safeArea;
        private Vector2Int _screenSize;

        public bool IsBannerAreaDisabled
        {
            get => isBannerAreaDisabled;
            set => isBannerAreaDisabled = value;
        }

        public bool IsAutoScale
        {
            get => isAutoScale;
            set => isAutoScale = value;
        }

        public bool IsScaleForIphone
        {
            get => isScaleForIphone;
            set => isScaleForIphone = value;
        }

        private void Awake()
        {
            Calculate();
        }

        //*************************************************************************************************
        /// <summary>
        /// Initialize
        /// </summary>
        //*************************************************************************************************
        public void Calculate()
        {
#if UNITY_EDITOR
            // For Debug at Play Mode
            if (simulateOnPlay)
            {
                _orientationType = OrientationType.Auto;
                simulateType = GetSimulateTypeFromCurrentResolution();
                if (simulateType == SimulateData.SimulateType.None)
                {
                    _isInitScreenSizeSimulate = true;
                    Apply();
                    return;
                }

                _safeArea = GetSimulationSafeArea(simulateType);
                _screenSize = GetSimulationResolution(simulateType);
                _isInitScreenSizeSimulate = false;
                Apply();
                return;
            }
#endif
            _isInitScreenSizeSimulate = true;
            Apply();
        }

        public void ReapplyBannerArea(bool isBannerAreaDisabled)
        {
            this.isBannerAreaDisabled = isBannerAreaDisabled;
#if UNITY_EDITOR
            // For Debug at Play Mode
            if (simulateOnPlay)
            {
                _orientationType = OrientationType.Auto;
                simulateType = GetSimulateTypeFromCurrentResolution();
                if (simulateType == SimulateData.SimulateType.None)
                {
                    _isInitScreenSizeSimulate = true;
                    Apply();
                    return;
                }

                _safeArea = GetSimulationSafeArea(simulateType);
                _screenSize = GetSimulationResolution(simulateType);
                _isInitScreenSizeSimulate = false;
                Apply();
                return;
            }
#endif
            _isInitScreenSizeSimulate = true;
            Apply();
        }

        //*************************************************************************************************
        /// <summary>
        /// Apply Safe Area to Screen
        /// </summary>
        /// <param name="isInitScreenSize">True: Initialize ScreenSize</param>
        //*************************************************************************************************
        //[Conditional("UNITY_EDITOR"), Conditional("UNITY_IOS")]
        public void Apply(float relativeSafeHeight = 0f, float relativeBannerHeight = 0)
        {
            if (_isInitScreenSizeSimulate)
            {
                _safeArea = Screen.safeArea;
#if UNITY_EDITOR || Unity2018_3_2_or_Older
                var display = Display.displays[0];
                _screenSize = new Vector2Int(display.systemWidth, display.systemHeight);
#else
                _screenSize = new Vector2Int(Screen.width, Screen.height);
#endif
            }

            if (!isBannerAreaDisabled)
                _safeArea = SetSafeArea(relativeSafeHeight);
            var anchorMin = _safeArea.position;
            var anchorMax = _safeArea.position + _safeArea.size;
            anchorMin.x /= _screenSize.x;
            if (isScaleForIphone) anchorMin.y = 0f;
            else anchorMin.y /= _screenSize.y;
            anchorMax.x /= _screenSize.x;
            anchorMax.y /= _screenSize.y;

            PanelRectTrans.anchorMin = anchorMin;
            PanelRectTrans.anchorMax = anchorMax;

            if (isAutoScale)
            {
                var oldSize = PanelRectTrans.rect;
                AdjustScale();
                var heightRate = GetHeightRate();
                var newSize = new Rect(PanelRectTrans.rect.x * heightRate, PanelRectTrans.rect.y * heightRate,
                    PanelRectTrans.rect.width * heightRate, PanelRectTrans.rect.height * heightRate);
                // Expand the size of RectTransform by the shrink scale
                PanelRectTrans.sizeDelta =
                    new Vector2((oldSize.width - newSize.width), (oldSize.height - newSize.height));
            }
        }

        Rect SetSafeArea(float relativeBannerHeight = 0)
        {
            Rect a = Screen.safeArea;

#if UNITY_EDITOR
            if (Screen.height == 2436 && Screen.width == 1125)
            {
                a.size = new Vector2(1125.0f, 2202.0f);
                a.x = 0f;
                a.y = 102f;
            }
#endif
            float s = a.size.y;
            s *= relativeBannerHeight;
            Vector2 size = a.size;
            size.y -= s;
            a.size = size;
            a.position += Vector2.up * s;
            return a;
        }

        //*************************************************************************************************
        /// <summary>
        /// Returns the ratio of SafeArea to height
        /// </summary>
        /// <returns>The ratio of SafeArea to height</returns>
        //*************************************************************************************************
        private float GetHeightRate()
        {
            return Mathf.Clamp01(_safeArea.height / _screenSize.y);
        }

        //*************************************************************************************************
        /// <summary>
        /// Stretch Local Scale to fit SafeArea size
        /// </summary>
        //*************************************************************************************************
        private void AdjustScale()
        {
            var heightRate = GetHeightRate();
            PanelRectTrans.localScale = new Vector3(heightRate, heightRate, 1.0f);
        }

#if UNITY_EDITOR

        #region ForDebug

        private enum OrientationType
        {
            Auto = 0,
            Portrait,
            Landscape
        }

        private OrientationType _orientationType = OrientationType.Auto;

        [SerializeField, Header("Debug")] private bool simulateOnPlay = true;

        [SerializeField, Header("Debug"), Tooltip("Ignores when Play")]
        private SimulateData.SimulateType simulateType;

        [SerializeField, Header("Debug"), Tooltip("Ignores when Play")]
        private bool isPortrait;

        private bool isLandscape
        {
            get
            {
                if (_orientationType != OrientationType.Auto)
                {
                    return !isPortrait;
                }

                // Judge by Resolution at Play Mode
                var width = UnityEngine.Screen.width;
                var height = UnityEngine.Screen.height;

                var resolution = SimulateData.Resolutions[(int) simulateType];
                return width == resolution.y && height == resolution.x;
            }
        }

        //*************************************************************************************************
        /// <summary>
        /// Simulaties at Editor without Play
        /// </summary>
        //*************************************************************************************************
        public void SimulateAtEditor()
        {
            if (simulateType == SimulateData.SimulateType.None)
            {
                return;
            }

            _orientationType = isPortrait ? OrientationType.Portrait : OrientationType.Landscape;
            _safeArea = GetSimulationSafeArea(simulateType);
            _screenSize = GetSimulationResolution(simulateType);
            _isInitScreenSizeSimulate = false;
            Apply();
        }

        //*************************************************************************************************
        /// <summary>
        /// Returns Resolution for Simulate
        /// </summary>
        /// <param name="type">Simulate Type</param>
        /// <returns>Resolution for Simulate</returns>
        //*************************************************************************************************
        private Vector2Int GetSimulationResolution(SimulateData.SimulateType type)
        {
            var index = (int) type;
            var resolutions = SimulateData.Resolutions;
            var width = isLandscape ? resolutions[index].y : resolutions[index].x;
            var height = isLandscape ? resolutions[index].x : resolutions[index].y;
            return new Vector2Int(width, height);
        }

        //*************************************************************************************************
        /// <summary>
        /// Returns Safe Area for Simulate
        /// </summary>
        /// <param name="type">Simulate Type</param>
        /// <returns>Safe Area for Simulate</returns>
        //*************************************************************************************************
        private Rect GetSimulationSafeArea(SimulateData.SimulateType type)
        {
            return SimulateData.SafeAreaResolutions[(int) type, isLandscape ? 1 : 0];
        }

        //*************************************************************************************************
        /// <summary>
        /// Identify the Simulate Type from the resolution on the current editor
        /// </summary>
        /// <returns>Target Simulate Type</returns>
        //*************************************************************************************************
        private SimulateData.SimulateType GetSimulateTypeFromCurrentResolution()
        {
            var width = UnityEngine.Screen.width;
            var height = UnityEngine.Screen.height;

            var resolutions = SimulateData.Resolutions;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (width == resolutions[i].y && height == resolutions[i].x ||
                    width == resolutions[i].x && height == resolutions[i].y)
                {
                    return (SimulateData.SimulateType) i;
                }
            }

            return SimulateData.SimulateType.None;
        }

        #endregion

#endif
    }
}