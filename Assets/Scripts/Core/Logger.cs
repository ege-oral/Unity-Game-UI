using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Core
{
    public static class Logger
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string tag, string message)
        {
            Debug.Log($"<color=cyan>[{tag}]</color> {message}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void Warning(string tag, string message)
        {
            Debug.LogWarning($"<color=yellow>[{tag}]</color> {message}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void Error(string tag, string message)
        {
            Debug.LogError($"<color=red>[{tag}]</color> {message}");
        }
    }
}
