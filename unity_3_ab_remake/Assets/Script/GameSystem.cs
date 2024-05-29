using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace STORYGAME
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSysyem))]

    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSysyem gameSysyem = (GameSysyem)target;

            if (GUILayout.Button("Rest Story Modes"))
            {
                gameSysyem.ResetStroyModles();
            }
        }
    }
#endif



    public class GameSysyem : MonoBehaviour
    {
        public static GameSysyem instance;

        private void Awake()
        {
            instance = this;
        }

        public enum GAMESTATE
        {
            STORYSHOW,
            WAITSELECT,
            STORYEND,
            BATTLEMODE,
            BATTLEDONE,
            SHOPMODE,
            ENDMODE,
        }

        public StroyTableObject[] stroymodels;
#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]

        public void ResetStroyModles()
        {
            storyModels = Resources.LoadAll<StroyTableObject>("");
        }
#endif
    }
}