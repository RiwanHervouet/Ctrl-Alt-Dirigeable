using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RectTransform))]
public class Tools_UI_Grid_Set : EditorWindow
{
    #region Init
    public RectTransform gridParentObject;
    public RectTransform emptyUIElement;
    private Rect gridData;
    private RectTransform[,] itemsToArrange;
    private int mapLength = 128;
    private int _s_mapLength = 4;
    private int mapHeight = 64;
    private int _s_mapHeight = 2;
    private Sprite imageToArrange;
    private Vector2 imageSize;
    private bool preferToStretchImage;

    private RectTransform tempParent;

    GUILayoutOption[] layoutOptions;
    bool createAGrid = false;
    bool sliders = false;
    #endregion

    #region Displaying the tool
    [MenuItem("Tools/UI Grid Set Images")]
    public static void ShowWindow()
    {
        GetWindow<Tools_UI_Grid_Set>("UI Grid Set Window");
    }

    private void OnEnable()
    {
        #region Layout Options
        //layoutOptions = 
        #endregion
    }
    public void OnGUI()
    {
        createAGrid = EditorGUILayout.Toggle("Create a Grid (or Arrange existing one)", createAGrid, layoutOptions);
        gridParentObject = (RectTransform)EditorGUILayout.ObjectField("Grid Parent", gridParentObject, typeof(RectTransform), true, layoutOptions);

        #region Optional Parameters
        if (createAGrid)
        {
            sliders = EditorGUILayout.Toggle("Sliders ?", sliders, layoutOptions);
            if (!sliders)
            {
                mapLength = EditorGUILayout.IntField("Map Length", mapLength, layoutOptions);
                mapHeight = EditorGUILayout.IntField("Map Height", mapHeight, layoutOptions);
            }
            else
            {
                _s_mapLength = EditorGUILayout.IntSlider("Map Length (*32)", _s_mapLength, 1, 40, layoutOptions);
                mapLength = _s_mapLength * 32;
                _s_mapHeight = EditorGUILayout.IntSlider("Map Height (*32)", _s_mapHeight, 1, 20, layoutOptions);
                mapHeight = _s_mapHeight * 32;
            }

            emptyUIElement = (RectTransform)EditorGUILayout.ObjectField("Empty UI Element", emptyUIElement, typeof(RectTransform), false, layoutOptions);
            if (imageToArrange == null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("ATTENTION The image works best when proportions are squared.", layoutOptions);
                EditorGUILayout.Space();
            }
            imageToArrange = (Sprite)EditorGUILayout.ObjectField("Image to fill the Grid with", imageToArrange, typeof(Sprite), true, layoutOptions);
        }
        #endregion

        preferToStretchImage = EditorGUILayout.Toggle("Stretch image to fill grid better", preferToStretchImage, layoutOptions);

        #region Button
        EditorGUILayout.Space();
        if (createAGrid)
        {
            if (GUILayout.Button("Create a grid and arrange this image in it."))
            {
                CreateGrid();
            }
        }
        else
        {
            if (GUILayout.Button("Arrange all images in the existing grid"))
            {
                ArrangeStuff();
            }
        }
        #endregion
    }
    #endregion

    #region Used Methods
    private void ArrangeStuff()
    {
        #region Error Management
        if (gridParentObject == null)
        {
            Debug.LogWarning("You must select a Parent to arrange images in !");
            return;
        }
        #endregion
        #region Determine sizes of grid, image (display and gaps within the grid) 
        int numberOfVerticalItems = gridParentObject.childCount;
        int numberOfHorizontalItems = gridParentObject.GetChild(0).childCount;
        gridData = RectTransformUtility.PixelAdjustRect(gridParentObject, gridParentObject.parent.GetComponent<Canvas>());
        itemsToArrange = new RectTransform[numberOfVerticalItems, numberOfHorizontalItems];


        float xSizeOfImageInGrid = gridData.width / numberOfHorizontalItems;
        float ySizeOfImageInGrid = gridData.height / numberOfVerticalItems;

        float minimumSizeOfImageInGrid = xSizeOfImageInGrid <= ySizeOfImageInGrid ? xSizeOfImageInGrid : ySizeOfImageInGrid;
        imageSize = preferToStretchImage ?
            new Vector2(xSizeOfImageInGrid, ySizeOfImageInGrid) :
            new Vector2(minimumSizeOfImageInGrid, minimumSizeOfImageInGrid);

        float yCoordinateOfRow = 1f;
        float yCoordinateOfRowDelta = 1f / numberOfVerticalItems;
        float xCoordinateOfRow;
        float xCoordinateOfRowDelta = 1f / numberOfHorizontalItems;
        #endregion

        for (int i = 0; i < numberOfVerticalItems; i++)
        {
            tempParent = gridParentObject.GetChild(i).GetComponent<RectTransform>();
            //on bouge le pivot par rapport au pivot 
            //le pivot du parent est au milieu de la grille
            tempParent.anchorMax = new Vector2(1f, yCoordinateOfRow);
            yCoordinateOfRow = 1f - yCoordinateOfRowDelta * (i + 1f);
            tempParent.anchorMin = new Vector2(0f, yCoordinateOfRow);

            PutUIElementBetweenHisAnchors(tempParent);

            xCoordinateOfRow = 0f;

            for (int j = 0; j < numberOfHorizontalItems; j++)
            {
                itemsToArrange[i, j] = tempParent.GetChild(j).GetComponent<RectTransform>();
                itemsToArrange[i, j].anchorMin = new Vector2(xCoordinateOfRow, 0f);
                xCoordinateOfRow = xCoordinateOfRowDelta * (j + 1f);
                itemsToArrange[i, j].anchorMax = new Vector2(xCoordinateOfRow, 1f);

                PutUIElementBetweenHisAnchors(itemsToArrange[i, j]);

                SetSize(itemsToArrange[i, j], imageSize); //divisé par un truc petit qui le rend plus grand pour remplir la case. ou pas, parce que pour une raison incroyable, ça marche alors que ça marchait pas avant. YES Unity
            }
        }
    }

    private void CreateGrid()
    {
        #region Error Management
        if (gridParentObject == null || emptyUIElement == null || imageToArrange == null)
        {
            if (gridParentObject == null)
                Debug.LogWarning("You must select a Parent to arrange images in !");
            if (emptyUIElement == null)
                Debug.LogWarning("You must select the \"Empty UI Element\" prefab for this tool to work !");
            if (imageToArrange == null)
                Debug.LogWarning("You must select an image to fill the grid with !");
            return;
        }
        if (mapLength < 1 || mapHeight < 1)
        {
            Debug.LogError("You might want to set some valid numbers for the height and length of the grid !");
            return;
        }
        #endregion
        #region Determine sizes of grid, image (display and gaps within the grid) 
        gridData = RectTransformUtility.PixelAdjustRect(gridParentObject, gridParentObject.parent.GetComponent<Canvas>());
        itemsToArrange = new RectTransform[mapHeight, mapLength];

        float xSizeOfImageInGrid = gridData.width / mapLength;
        float ySizeOfImageInGrid = gridData.height / mapHeight;

        float minimumSizeOfImageInGrid = xSizeOfImageInGrid <= ySizeOfImageInGrid ? xSizeOfImageInGrid : ySizeOfImageInGrid;

        imageSize = preferToStretchImage ?
            new Vector2(xSizeOfImageInGrid, ySizeOfImageInGrid) :
            new Vector2(minimumSizeOfImageInGrid, minimumSizeOfImageInGrid);

        float yCoordinateOfRow = 1f;
        float yCoordinateOfRowDelta = 1f / mapHeight;
        float xCoordinateOfRow;
        float xCoordinateOfRowDelta = 1f / mapLength;

        RectTransform[] children = new RectTransform[mapHeight];
        #endregion

        if (gridParentObject.childCount >= 1)
        {
            GameObject[] go = new GameObject[gridParentObject.childCount];
            for (int i = 0; i < gridParentObject.childCount; i++)
            {
                go[i] = gridParentObject.GetChild(i).gameObject;
            }

            for (int i = 0; i < go.Length; i++)
            {
                DestroyImmediate(go[i].gameObject);
            }
        }
        for (int i = 0; i < mapHeight; i++)
        {
            children[i] = Instantiate<RectTransform>(emptyUIElement, gridParentObject);
            children[i].name = "Row " + i;
        }

        for (int i = 0; i < mapHeight; i++)
        {
            tempParent = children[i];
            //on bouge le pivot par rapport au pivot 
            //le pivot du parent est au milieu de la grille
            tempParent.anchorMax = new Vector2(1f, yCoordinateOfRow);
            yCoordinateOfRow = 1f - yCoordinateOfRowDelta * (i + 1f);
            tempParent.anchorMin = new Vector2(0f, yCoordinateOfRow);

            PutUIElementBetweenHisAnchors(tempParent);

            xCoordinateOfRow = 0f;

            for (int j = 0; j < mapLength; j++)
            {
                itemsToArrange[i, j] = Instantiate(emptyUIElement, tempParent);
                itemsToArrange[i, j].name = ("Item " + j + ", " + i);
                itemsToArrange[i, j].gameObject.AddComponent<CanvasRenderer>();
                itemsToArrange[i, j].gameObject.AddComponent<Image>().sprite = imageToArrange;

                itemsToArrange[i, j].anchorMin = new Vector2(xCoordinateOfRow, 0f);
                xCoordinateOfRow = xCoordinateOfRowDelta * (j + 1f);
                itemsToArrange[i, j].anchorMax = new Vector2(xCoordinateOfRow, 1f);

                PutUIElementBetweenHisAnchors(itemsToArrange[i, j]);

                SetSize(itemsToArrange[i, j], imageSize); //divisé par un truc petit qui le rend plus grand pour remplir la case. ou pas, parce que pour une raison incroyable, ça marche alors que ça marchait pas avant. YES Unity
            }
        }
    }
    #endregion

    #region Assistance Methods
    public static void SetSize(RectTransform imageTransform, Vector2 newSize)
    {
        Vector2 oldSize = imageTransform.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        imageTransform.offsetMin = imageTransform.offsetMin - new Vector2(deltaSize.x * imageTransform.pivot.x, deltaSize.y * imageTransform.pivot.y);
        imageTransform.offsetMax = imageTransform.offsetMax + new Vector2(deltaSize.x * (1f - imageTransform.pivot.x), deltaSize.y * (1f - imageTransform.pivot.y));
    }

    public static void PutUIElementBetweenHisAnchors(RectTransform imageToClean)
    {
        imageToClean.anchoredPosition = new Vector2(0f, 0f);
        imageToClean.offsetMin = new Vector2(0f, 0f);
        imageToClean.offsetMax = new Vector2(0f, 0f);
    }
    #endregion
}
