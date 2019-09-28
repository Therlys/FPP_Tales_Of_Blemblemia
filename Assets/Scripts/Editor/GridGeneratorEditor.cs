using UnityEditor;
using UnityEngine;

namespace Game
{
    [CustomEditor(typeof(GridGenerator))]
    //Author: Mike Bédard
    public class GridGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            GridGenerator gridGenerator = (GridGenerator)target;
            
            if(GUILayout.Button("Generate Grid"))
            {
                gridGenerator.GenerateGrid();
            }
            
            if(GUILayout.Button("Clear Grid"))
            {
                gridGenerator.ClearGrid();
            }
        }
    }
}