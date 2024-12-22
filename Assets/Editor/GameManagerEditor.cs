using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
     public override void OnInspectorGUI()
    {
        bool showFoldout = false;

        base.OnInspectorGUI();

        FSM fsm = (FSM)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(fsm.stateMachine == null) return;

        if(fsm.stateMachine.CurrentState != null){
            EditorGUILayout.LabelField("Current Level: *", fsm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if(showFoldout){
            if(fsm.stateMachine.dictionaryState != null){
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for(int i = 0; i < keys.Length; i++){
                    EditorGUILayout.LabelField(string.Format("{0} :: {i}", keys[i], vals[i]));
            }
            }

        }
    }

}