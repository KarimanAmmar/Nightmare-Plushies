using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    SerializedProperty wanderingStateProp;
    SerializedProperty chasingStateProp;
    SerializedProperty attackingStateProp;
    SerializedProperty attackDistanceProp;
    SerializedProperty chaseDistanceProp;
    SerializedProperty enemyTypeProp;
    SerializedProperty attackBehaviorProp;

    private void OnEnable()
    {
        wanderingStateProp = serializedObject.FindProperty("wanderingState");
        chasingStateProp = serializedObject.FindProperty("chasingState");
        attackingStateProp = serializedObject.FindProperty("attackingState");
        attackDistanceProp = serializedObject.FindProperty("attackDistance");
        chaseDistanceProp = serializedObject.FindProperty("chaseDistance");
        enemyTypeProp = serializedObject.FindProperty("enemyType");
        attackBehaviorProp = serializedObject.FindProperty("attackBehavior");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(wanderingStateProp);
        EditorGUILayout.PropertyField(chasingStateProp);
        EditorGUILayout.PropertyField(attackingStateProp);
        EditorGUILayout.PropertyField(attackDistanceProp);
        EditorGUILayout.PropertyField(chaseDistanceProp);
        EditorGUILayout.PropertyField(enemyTypeProp);

        EditorGUILayout.PropertyField(attackBehaviorProp);

        serializedObject.ApplyModifiedProperties();
    }
}
