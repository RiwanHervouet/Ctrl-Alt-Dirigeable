using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//[CustomEditor(typeof(RectTransform))]
public class MapPlacement : MonoBehaviour
{
    public RectTransform gridObject;
    private Rect gridData;
    private RectTransform[,] itemsToArrange;
    private Image imageToArrange;

    private RectTransform tempParent;

    GUILayoutOption[] layoutOptions;

    [MenuItem("Window/Arrange dots")]
    /*public static void ShowWindow()
    {
        GetWindow<Mapplacement>("Map Placement Window");
    }*/

    public void OnGUI()
    {
        gridObject = (RectTransform)EditorGUILayout.ObjectField("Grid Parent", gridObject, typeof(RectTransform), true, layoutOptions);
        /*pour faire progresser ce tool, je peux demander quelle image doit remplir la matrice et selon un boolean qui préfère garder les proportions  de l'image ou opti au mieux l'espace
        imageToArrange = (Image)EditorGUILayout.ObjectField("Image to fill the Grid with", imageToArrange, typeof(Image), true, layoutOptions);
        preferToKeepProportionsRight = EditorGUILayout.Toggle(preferToKeepProportionsRight, layoutOptions);
        */
        EditorGUILayout.Space();
        if (GUILayout.Button("Arrange all dots in the map grid"))
        {
            ArrangeStuff();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
        imageToArrange = gridObject.GetComponentInChildren<Image>() == null ? gridObject.GetChild(0).GetComponentInChildren<Image>() : gridObject.GetComponentInChildren<Image>();

        //if(preferToKeepProportionsRight)
        float xSizeOfImageInGrid = gridData.width / numberOfHorizontalItems;
        float ySizeOfImageInGrid = gridData.height / numberOfVerticalItems;

        float minimumSizeOfImageInGrid = xSizeOfImageInGrid <= ySizeOfImageInGrid ? xSizeOfImageInGrid : ySizeOfImageInGrid;
        Vector2 imageSize = new Vector2(minimumSizeOfImageInGrid, minimumSizeOfImageInGrid);

        Debug.Log(RectTransformUtility.PixelAdjustRect(gridObject, gridObject.parent.GetComponent<Canvas>()));
        #endregion

        for (int i = 0; i < numberOfVerticalItems; i++)
        {
            tempParent = gridObject.GetChild(i).GetComponent<RectTransform>();
            //on bouge le pivot par rapport au pivot 
            //le pivot du parent est au milieu de la grille
            tempParent.anchoredPosition = new Vector2(0, (-gridData.height * 0.5f) + ySizeOfImageInGrid * (i + 0.5f));
            
            for (int j = 0; j < numberOfHorizontalItems; j++)
            {
                itemsToArrange[i, j] = tempParent.GetChild(j).GetComponent<RectTransform>();
                itemsToArrange[i, j].anchoredPosition = new Vector2((-gridData.width * 0.5f) + xSizeOfImageInGrid * (j + 0.5f), 0);
                SetSize(itemsToArrange[i, j], new Vector2(minimumSizeOfImageInGrid, minimumSizeOfImageInGrid)); //divisé par un truc petit qui le rend plus grand pour remplir la case.
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
}
