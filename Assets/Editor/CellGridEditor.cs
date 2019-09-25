using UnityEditor;
using UnityEngine;

namespace Game
{
    
    [CustomEditor(typeof(CellGridCreator))]
    public class CellGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            CellGridCreator cellGridCreator = (CellGridCreator)target;
            
            if(GUILayout.Button("Build Cell Grid"))
            {
                cellGridCreator.CreateCellsDependingOnTilemap();
            }
        }
    }
}