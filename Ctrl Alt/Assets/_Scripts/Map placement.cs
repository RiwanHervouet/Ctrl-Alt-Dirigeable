using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RectTransform))]
public class Mapplacement : EditorWindow
{
    private RectTransform parent;
    private Rect gridData;
    private RectTransform[,] itemsToArrange;
    private Image imageToArrange;

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

    private void ArrangeStuff()
    {
        int numberOfVerticalItems = parent.childCount;
        int numberOfHorizontalItems = parent.GetChild(0).childCount;
        gridData = RectTransformUtility.PixelAdjustRect(parent, parent.parent.GetComponent<Canvas>());
        itemsToArrange = new RectTransform[numberOfVerticalItems, numberOfHorizontalItems];
        imageToArrange = parent.GetComponentInChildren<Image>() == null ? parent.GetChild(0).GetComponentInChildren<Image>() : parent.GetComponentInChildren<Image>();

        float xSizeOfImageInGrid = gridData.width / (imageToArrange.preferredWidth * numberOfHorizontalItems);
        float ySizeOfImageInGrid = gridData.height / (imageToArrange.preferredHeight * numberOfVerticalItems);

        float sizeOfImageInGrid = xSizeOfImageInGrid <= ySizeOfImageInGrid ? xSizeOfImageInGrid : ySizeOfImageInGrid;

        Debug.Log(RectTransformUtility.PixelAdjustRect(parent, parent.parent.GetComponent<Canvas>()));

        //for chaque enfant, on le bouge selon le nombre d'enfants par rapport à la taille de la grille ou plutôt par rapport aux ancres
    }

    private void DetermineSizes()
    {
        //mettre tout le début du code en initialisant bien comme il faut
    }
}
