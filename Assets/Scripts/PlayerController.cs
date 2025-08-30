using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected GameObject gridControl;
    [SerializeField] protected Transform movePoint;

    private float moveSpeed = 15f;
    private float tileSize = 2f;
    protected bool inDialog = false;
    protected int currentGridCell;
    protected Grid grid;
    protected bool playerAlive = true;
    protected bool move;

    protected void Awake()
    {
        EventsManager.instance.onDialogStarted.AddListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.AddListener(EndDialogMode);
    }

    protected virtual void Start()
    {
        grid = gridControl.GetComponent<GridController>().grid;
        movePoint.parent = null;

        EventsManager.instance.onPlayerWin.AddListener(LevelManager.instance.SetLevelComplete);
    }

    protected void OnDestroy()
    {
        EventsManager.instance.onDialogStarted.RemoveListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.RemoveListener(EndDialogMode);
        EventsManager.instance.onPlayerWin.RemoveListener(LevelManager.instance.SetLevelComplete);
    }

    protected virtual void Update()
    {
        if (LevelManager.instance.GetLevelComplete()) return;
        if (!playerAlive) return;

        currentGridCell = grid.GetValue(transform.position);
        GameManager.instance.playerStartMove = false;

        if (ReachedDestination())
        {
            if (inDialog) return;
            GetNewPosition();
        }

        MoveToNewTile();
        CheckPlayerOutOfBounds();
    }

    private bool ReachedDestination()
    {
        return Vector3.Distance(transform.position, movePoint.position) <= 0f;
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

    public AnimatorController GetAnimator()
    {
        return GetComponent<AnimatorController>();
    }

    public void TriggerDeath()
    {
        AudioManager.instance.PlayHurtSound();
        playerAlive = false;
        GetAnimator().TriggerDeath();
    }

    #region Player Direction
    public void ResetMovePoint()
    {
        movePoint.position = transform.position;
    }

    protected virtual bool Left()
    {
        move = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        if (move)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            GetAnimator().TriggerDash();
        }
        return move;
    }

    protected virtual bool Right()
    {
        move = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        if (move)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            GetAnimator().TriggerDash();
        }
        return move;
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
            LevelManager.instance.AddMove("left");
            movePoint.position += new Vector3(-tileSize, 0, 0);
            GameManager.instance.playerStartMove = true;
        }

        if (Right())
        {
            LevelManager.instance.AddMove("right");
            movePoint.position += new Vector3(tileSize, 0, 0);
            GameManager.instance.playerStartMove = true;
        }
    }

    protected void Ladder()
    {
        if (Up())
        {
            int numLadders = 0;
            LevelManager.instance.AddMove("up");
            grid.GetXY(transform.position, out int x, out int y);
            for (int i = y; i < grid.height - 1; i++)
            {
                if (grid.GetValue(x, i) == 3) // top ladder found
                {
                    break;
                }
                numLadders++;
            }
            movePoint.position += new Vector3(0, tileSize * numLadders, 0);
            GameManager.instance.playerStartMove = true;
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
            LevelManager.instance.AddMove("down");
            grid.GetXY(transform.position, out int x, out int y);
            for (int i = y; i > -1; i--)
            {
                if (grid.GetValue(x, i - 1) != 2) // last ladder tile
                {
                    break;
                }
                movePoint.position += new Vector3(0, -tileSize, 0);
            }
            GameManager.instance.playerStartMove = true;
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
