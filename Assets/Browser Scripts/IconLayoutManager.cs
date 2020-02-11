using UnityEngine;

public class IconLayoutManager : MonoBehaviour
{
    //If there's a more elegant way of checking that
    //the mouse is dropping an icon on top of another,
    //pls change this cause it's pretty ugly.
    public bool[,] iconGrid;
    private RectTransform canvasRect;
    Vector3[] corners = new Vector3[4]; //BottomLeft, TopLeft, TopRight, BottomRight


    [SerializeField]
    private int gridX = 100; //Perhaps it makes more sense to change this to padding
    [SerializeField]
    private int gridY = 100;
    private int offsetX;
    private int offsetY;

    void Start()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        canvasRect.GetWorldCorners(corners);
        offsetX = gridX / 2;
        offsetY = gridY / 2;
        //Assert.IsNotNull(canvasRect);
        ForceLayout();
    }

    private Vector2 ComputePosition(int x, int y)
    {
        float left = corners[1].x; float top = corners[1].y;
        return new Vector2(left + x * gridX + gridX / 2, top - (y * gridY + gridY / 2));
    }

    private void ComputeIndices(Vector2 position, out int x, out int y)
    {
        Vector2 scaled = new Vector2(position.x - corners[1].x, corners[1].y - position.y);
        x = Mathf.Clamp(Mathf.RoundToInt((scaled.x - gridX / 2) / gridX), 0, iconGrid.GetLength(0) - 1);
        y = Mathf.Clamp(Mathf.RoundToInt((scaled.y - gridY / 2) / gridY), 0, iconGrid.GetLength(1) - 1);
    }

    private void ResetGrid()
    {
        for(int x = 0; x < iconGrid.GetLength(0); x++)
        {
            for(int y = 0; y < iconGrid.GetLength(1); y++)
            {
                iconGrid[x, y] = false;
            }
        }
    }

    private void ForceLayout()
    {
        float width = canvasRect.rect.width; float height = canvasRect.rect.height;
        //Assert.IsTrue(width > gridX); //Assert.IsTrue(height > gridY);
        iconGrid = new bool[(int)(width / gridX), (int)(height / gridY)];
        ResetGrid();
        GameObject[] icons = GameObject.FindGameObjectsWithTag("Icon");
        int x = 0; int y = 0;

        //Jank af
        for (int i = 0; i < icons.Length; i++)
        {
            iconGrid[x, y] = true;
            icons[i].transform.position = ComputePosition(x, y);
            y++;
            if (y == iconGrid.GetLength(1))
            {
                y = 0;
                x++;
            }
        }
    }

    //Keeps the icons on the screen
    public bool InBounds(Vector2 mousePos)
    {
        if (corners[0].x <= mousePos.x && corners[0].y <= mousePos.y &&
            corners[1].x <= mousePos.x && corners[1].y >= mousePos.y &&
            corners[2].x >= mousePos.x && corners[2].y >= mousePos.y &&
            corners[3].x >= mousePos.x && corners[3].y <= mousePos.y)
        {
            return true;
        }

        return false;
    }

    public Vector2 CheckEndPosition(Vector2 starting, Vector2 mousePos)
    {

        int x = 0; int y = 0; ComputeIndices(mousePos, out x, out y);
        if (!iconGrid[x, y])
        {
            iconGrid[x, y] = true;
            Vector2 ret = ComputePosition(x, y);
            ComputeIndices(starting, out x, out y); //Icon moved, note that in the grid
            iconGrid[x, y] = false;
            return ret;
        } else
        {
            return starting;
        }
    }

    //TODO: Check if the screen changes size. If it does, update corners and reforce layout.
}
