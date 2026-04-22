using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Logger = Core.Logger;

namespace UI.Popup
{
    [RequireComponent(typeof(RawImage))]
    public class BlurredBackdrop : MonoBehaviour
    {
        private const string LogTag = "BlurredBackdrop";

        [SerializeField] private Shader blurShader;
        [SerializeField, Range(1, 16)] private int iterations = 6;
        [SerializeField, Range(1, 8)] private int downsample = 2;
        [SerializeField, Range(0.5f, 4f)] private float offset = 1.5f;
        [SerializeField] private bool verboseLogging;

        private static readonly int OffsetId = Shader.PropertyToID("_Offset");

        private RawImage _rawImage;
        private Material _blurMaterial;
        private RenderTexture _capture;
        private RenderTexture _ping;
        private RenderTexture _pong;
        private RenderTexture _result;
        private Coroutine _captureRoutine;

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();

            if (blurShader == null)
            {
                Logger.Error(LogTag, "Shader 'Hidden/UI/KawaseBlur' not found.");
                return;
            }

            if (!blurShader.isSupported)
            {
                Logger.Error(LogTag, "Blur shader is not supported on this platform/GPU.");
                return;
            }

            _blurMaterial = new Material(blurShader) { hideFlags = HideFlags.HideAndDontSave };
        }

        private void OnEnable()
        {
            if (_blurMaterial == null)
                return;

            _rawImage.enabled = false;
            _captureRoutine = StartCoroutine(CaptureAtEndOfFrame());
        }

        private void OnDisable()
        {
            if (_captureRoutine != null)
            {
                StopCoroutine(_captureRoutine);
                _captureRoutine = null;
            }
            ReleaseTextures();
        }

        private void OnDestroy()
        {
            ReleaseTextures();
            if (_blurMaterial != null)
                Destroy(_blurMaterial);
        }

        private IEnumerator CaptureAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            var w = Mathf.Max(1, Screen.width);
            var h = Mathf.Max(1, Screen.height);
            var bw = Mathf.Max(1, w / downsample);
            var bh = Mathf.Max(1, h / downsample);

            ReleaseTextures();

            _capture = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.Default);
            _ping = RenderTexture.GetTemporary(bw, bh, 0, RenderTextureFormat.Default);
            _pong = RenderTexture.GetTemporary(bw, bh, 0, RenderTextureFormat.Default);

            ScreenCapture.CaptureScreenshotIntoRenderTexture(_capture);

            Graphics.Blit(_capture, _ping, new Vector2(1f, -1f), new Vector2(0f, 1f));

            var src = _ping;
            var dst = _pong;
            for (var i = 0; i < iterations; i++)
            {
                var o = offset + i;
                _blurMaterial.SetFloat(OffsetId, o);
                Graphics.Blit(src, dst, _blurMaterial, 0);
                (src, dst) = (dst, src);
            }

            RenderTexture.active = null;

            _result = src;
            if (src == _ping)
            {
                RenderTexture.ReleaseTemporary(_pong);
                _pong = null;
            }
            else
            {
                RenderTexture.ReleaseTemporary(_ping);
                _ping = null;
            }

            RenderTexture.ReleaseTemporary(_capture);
            _capture = null;

            _rawImage.texture = _result;
            _rawImage.color = Color.white;
            _rawImage.enabled = true;

            if (verboseLogging)
                Logger.Log(LogTag, $"Screen backbuffer captured {w}x{h}, blurred at {bw}x{bh}, {iterations} iterations.");

            _captureRoutine = null;
        }

        private void ReleaseTextures()
        {
            if (_rawImage != null)
                _rawImage.texture = null;

            RenderTexture.active = null;

            if (_capture != null) { RenderTexture.ReleaseTemporary(_capture); _capture = null; }
            if (_ping != null) { RenderTexture.ReleaseTemporary(_ping); _ping = null; }
            if (_pong != null) { RenderTexture.ReleaseTemporary(_pong); _pong = null; }
            _result = null;
        }
    }
}
