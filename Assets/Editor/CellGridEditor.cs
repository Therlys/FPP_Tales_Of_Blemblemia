using UnityEditor;
using UnityEngine;

namespace Game
{
    
    [CustomEditor(typeof(GridGenerator))]
    public class CellGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            GridGenerator gridGenerator = (GridGenerator)target;
            
            if(GUILayout.Button("Generate Grid"))
            {
                gridGenerator.CreateCellsDependingOnTilemap();
            }
        }
    }
}