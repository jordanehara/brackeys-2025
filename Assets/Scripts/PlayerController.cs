using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected GameObject gridControl;
    [SerializeField] protected Transform movePoint;

    private float moveSpeed = 15f;
    private float tileSize = 2f;
    protected bool inDialog = true;
    protected int currentGridCell;
    protected Grid grid;

    protected virtual void Start()
    {
        grid = gridControl.GetComponent<GridController>().grid;
        movePoint.parent = null;

        EventsManager.instance.onDialogStarted.AddListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.AddListener(EndDialogMode);
    }

    void OnDestroy()
    {
        EventsManager.instance.onDialogStarted.RemoveListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.RemoveListener(EndDialogMode);
    }

    protected virtual void Update()
    {
        currentGridCell = grid.GetValue(transform.position);
        GameManager.instance.playerMoving = IsMovementInput();

        if (Vector3.Distance(transform.position, movePoint.position) <= 0f)
        {
            if (inDialog) return;
            GetNewPosition();
        }

        MoveToNewTile();
        CheckPlayerOutOfBounds();
    }

    public virtual void GetNewPosition()
    {
        switch (currentGridCell)
        {
            case 0:
                NoTile();
                break;
            case 1:
                Platform();
                break;
            case 2:
                Ladder();
                break;
            case 3:
                TopLadder();
                break;
            case 4: // button
            case 5: // door
            case 6: // goal
            case 7: // start
            case 8: // teleport button
            case 9: // teleport door
                Platform();
                break;
        }
    }

    protected void MoveToNewTile()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    protected void CheckPlayerOutOfBounds()
    {
        if (currentGridCell == -1) // fell off the grid
        {
            SceneChanger.instance.ReloadScene();
        }
    }

    #region Player Direction
    public void ResetMovePoint()
    {
        movePoint.position = transform.position;
    }

    protected bool IsMovementInput()
    {
        return Left() || Right() || Up() || Down();
    }

    protected virtual bool Left()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
    }

    protected virtual bool Right()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }

    protected virtual bool Up()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    protected virtual bool Down()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }
    #endregion

    #region Tile Movement
    protected void NoTile()
    {
        grid.GetXY(transform.position, out int x, out int y);
        for (int i = y; i > -1; i--)
        {
            if (grid.GetValue(x, i) > 0) // if there's literally any tile to land on
            {
                break;
            }
            movePoint.position += new Vector3(0, -tileSize, 0);
        }
    }

    protected void Platform()
    {
        if (Left())
        {
            LevelManger.instance.AddMove("left");
            movePoint.position += new Vector3(-tileSize, 0, 0);
        }

        if (Right())
        {
            LevelManger.instance.AddMove("right");
            movePoint.position += new Vector3(tileSize, 0, 0);
        }
    }

    protected void Ladder()
    {
        if (Up())
        {
            LevelManger.instance.AddMove("up");
            grid.GetXY(transform.position, out int x, out int y);
            for (int i = y; i < grid.height - 1; i++)
            {
                if (grid.GetValue(x, i) == 3) // top ladder found
                {
                    break;
                }
                movePoint.position += new Vector3(0, tileSize, 0);
            }
        }
        else
        {
            Platform();
        }
    }

    protected void TopLadder()
    {
        if (Down())
        {
            LevelManger.instance.AddMove("down");
            grid.GetXY(transform.position, out int x, out int y);
            for (int i = y; i > -1; i--)
            {
                if (grid.GetValue(x, i - 1) != 2) // last ladder tile
                {
                    break;
                }
                movePoint.position += new Vector3(0, -tileSize, 0);
            }
        }
        else
        {
            Platform();
        }
    }
    #endregion

    #region  Dialog Listeners
    public void StartDialogMode()
    {
        inDialog = true;
    }

    public void EndDialogMode()
    {
        inDialog = false;
    }
    #endregion
}
