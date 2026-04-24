using System;
using UnityEngine;

namespace UI.Configs
{ 
    public enum IconType
    {
        Coin,
        Crown,
        Star,
        Heart,
    }
    [CreateAssetMenu(fileName = "IconDatabase", menuName = "UI Config/Icon Database")]
    public class IconDatabase : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public IconType type;
            public Sprite sprite;
        }

        [SerializeField] private Entry[] entries;

        public Sprite Get(IconType type)
        {
            for (var i = 0; i < entries.Length; i++)
            {
                if (entries[i].type == type)
                    return entries[i].sprite;
            }
            return null;
        }
    }
}
