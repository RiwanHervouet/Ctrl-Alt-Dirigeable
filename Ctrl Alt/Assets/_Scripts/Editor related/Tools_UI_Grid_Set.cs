using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RectTransform))]
public class Tools_UI_Grid_Set : EditorWindow
{
    public RectTransform gridObject;
    private Rect gridData;
    private RectTransform[,] itemsToArrange;
    //private Image imageToArrange; besoin dès que je voudrai savoir quelles proportions a l'image pour pouvoir les garder toussa

    private RectTransform tempParent;

    GUILayoutOption[] layoutOptions;

    [MenuItem("Tools/UI Grid Set Images")]
    public static void ShowWindow()
    {
        GetWindow<Tools_UI_Grid_Set>("UI Grid Set Window");
    }

    public void OnGUI()
    {
        gridObject = (RectTransform)EditorGUILayout.ObjectField("Grid Parent", gridObject, typeof(RectTransform), true, layoutOptions);
        /*pour faire progresser ce tool, je peux demander quelle image doit remplir la matrice et selon un boolean qui préfère garder les proportions  de l'image ou opti au mieux l'espace
        imageToArrange = (Image)EditorGUILayout.ObjectField("Image to fill the Grid with", imageToArrange, typeof(Image), true, layoutOptions);
        preferToKeepProportionsRight = EditorGUILayout.Toggle(preferToKeepProportionsRight, layoutOptions);
        */
        EditorGUILayout.Space();
        if (GUILayout.Button("Arrange all images in the grid"))
        {
            ArrangeStuff();
        }
    }

    private void ArrangeStuff()
    {
        #region Determine sizes of grid, image (display and gaps within the grid) 
        int numberOfVerticalItems = gridObject.childCount;
        int numberOfHorizontalItems = gridObject.GetChild(0).childCount;
        gridData = RectTransformUtility.PixelAdjustRect(gridObject, gridObject.parent.GetComponent<Canvas>());
        itemsToArrange = new RectTransform[numberOfVerticalItems, numberOfHorizontalItems]; 
        //imageToArrange = gridObject.GetComponentInChildren<Image>() == null ? gridObject.GetChild(0).GetComponentInChildren<Image>() : gridObject.GetComponentInChildren<Image>();


        //if(preferToKeepProportionsRight)
        float xSizeOfImageInGrid = gridData.width / numberOfHorizontalItems;
        float ySizeOfImageInGrid = gridData.height / numberOfVerticalItems;

        float minimumSizeOfImageInGrid = xSizeOfImageInGrid <= ySizeOfImageInGrid ? xSizeOfImageInGrid : ySizeOfImageInGrid;
        Vector2 imageSize = new Vector2(minimumSizeOfImageInGrid, minimumSizeOfImageInGrid);

        float yCoordinateOfRow = 1f;
        float yCoordinateOfRowDelta = 1f / numberOfVerticalItems;
        float xCoordinateOfRow;
        float xCoordinateOfRowDelta = 1f / numberOfHorizontalItems;
        #endregion

        for (int i = 0; i < numberOfVerticalItems; i++)
        {
            tempParent = gridObject.GetChild(i).GetComponent<RectTransform>();
            //on bouge le pivot par rapport au pivot 
            //le pivot du parent est au milieu de la grille
            tempParent.anchorMax = new Vector2(1f, yCoordinateOfRow);
            yCoordinateOfRow = 1f - yCoordinateOfRowDelta * (i + 1f);
            tempParent.anchorMin = new Vector2(0f, yCoordinateOfRow);

            xCoordinateOfRow = 0f;

            for (int j = 0; j < numberOfHorizontalItems; j++)
            {
                itemsToArrange[i, j] = tempParent.GetChild(j).GetComponent<RectTransform>();
                itemsToArrange[i, j].anchorMin = new Vector2(xCoordinateOfRow, 0f);
                xCoordinateOfRow = xCoordinateOfRowDelta * (j + 1f);
                itemsToArrange[i, j].anchorMax = new Vector2(xCoordinateOfRow, 1f);

                CleanNumbersOnInspector(itemsToArrange[i, j]);

                SetSize(itemsToArrange[i, j], imageSize); //divisé par un truc petit qui le rend plus grand pour remplir la case. ou pas, parce que pour une raison incroyable, ça marche alors que ça marchait pas avant. YES Unity
            }
        }
    }
    public static void SetSize(RectTransform imageTransform, Vector2 newSize)
    {
        Vector2 oldSize = imageTransform.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        imageTransform.offsetMin = imageTransform.offsetMin - new Vector2(deltaSize.x * imageTransform.pivot.x, deltaSize.y * imageTransform.pivot.y);
        imageTransform.offsetMax = imageTransform.offsetMax + new Vector2(deltaSize.x * (1f - imageTransform.pivot.x), deltaSize.y * (1f - imageTransform.pivot.y));
    }

    public static void CleanNumbersOnInspector(RectTransform imageToClean)
    {
        imageToClean.anchoredPosition = new Vector2(0f, 0f);
        imageToClean.offsetMin = new Vector2(0f, 0f);
        imageToClean.offsetMax = new Vector2(0f, 0f);
    }
}
