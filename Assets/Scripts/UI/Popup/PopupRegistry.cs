using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup
{
    public class PopupRegistry : MonoBehaviour
    {
        [SerializeField] private BasePopup[] prefabs;
        [SerializeField] private Transform parent;

        private readonly Dictionary<string, BasePopup> _prefabMap = new();
        private readonly Dictionary<string, BasePopup> _instances = new();

        // Pre-instantiate all registered popups to avoid runtime allocation and first-open lag
        private void Awake()
        {
            foreach (var prefab in prefabs)
            {
                _prefabMap[prefab.PopupName] = prefab;

                var instance = Instantiate(prefab, parent);
                instance.gameObject.SetActive(false);
                _instances[prefab.PopupName] = instance;
            }
        }

        public bool TryGet(string key, out BasePopup popup)
        {
            if (_instances.TryGetValue(key, out popup))
                return true;

            // Fallback: instantiate if not pre-loaded
            if (!_prefabMap.TryGetValue(key, out var prefab))
            {
                Core.Logger.Error("PopupRegistry", $"No prefab registered for key: {key}");
                return false;
            }

            popup = Instantiate(prefab, parent);
            popup.gameObject.SetActive(false);
            _instances[key] = popup;
            return true;
        }
    }
}
