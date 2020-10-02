using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(RectTransform))]
public class Mapplacement : EditorWindow
{
    public RectTransform parent;
    public RectTransform[,] itemsToArrange;

    GUILayoutOption[] layoutOptions;

    [MenuItem("Window/Arrange dots")]
    public static void ShowWindow()
    {
        GetWindow<Mapplacement>("Map Placement Window");
    }

    public void OnGUI()
    {
        parent = (RectTransform)EditorGUILayout.ObjectField("Grid Parent", parent, typeof(RectTransform), true, layoutOptions);
        if (GUILayout.Button("Arrange all dots in the map grid"))
        {
            ArrangeStuff();
        }
    }

    public void ArrangeStuff()
    {
        itemsToArrange = new RectTransform[parent.childCount, parent.GetChild(0).childCount];

        //for chaque enfant, on le bouge selon le nombre d'enfants par rapport à la taille de la grille ou plutôt par rapport aux ancres
    }
}
