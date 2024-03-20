using UnityEngine;

// !!!
// This whole feature is a prototype and is in need of deep optimizations
public class GridManager : MonoBehaviour
{
    private int _width;
    private int _height;

    private GameObject[,] _gridObjects;

    private GridCell selection;

    void Update()
    {
        ShowSelectedCell();
    }


    public void Init(LevelData levelData)
    {
        _width = levelData.GridWidth;
        _height = levelData.GridHeight;
        _gridObjects = new GameObject[_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject obj = levelData.GridPath.Contains(new Vector2Int(x, y)) ? levelData.PathCell : levelData.BasicCell;
                _gridObjects[x, y] = Instantiate(obj, new Vector3(x * levelData.GridSpacing + levelData.GridSpacing / 2, 0, y * levelData.GridSpacing + levelData.GridSpacing / 2), Quaternion.identity);
                _gridObjects[x, y].transform.parent = transform;
                _gridObjects[x, y].gameObject.name = "GridCell (X: " + x + " , Y: " + y + ")";
            }
        }
    }

    public void ProjectOntoSelectedCell(GameObject obj)
    {
        if (selection != null)
            obj.transform.position = selection.transform.position;
    }

    // TODO: Move raycast logic into a separate class
    private void ShowSelectedCell()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _gridObjects[x, y].GetComponent<GridCell>().SetSelected(false);
            }
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        selection = null;
        if (Physics.Raycast(ray, out hit, 100))
        {
            GridCell cell = hit.transform.GetComponent<GridCell>();
            if (cell != null)
            {
                selection = cell;
                cell.SetSelected(true);
            }
        }
    }
}
