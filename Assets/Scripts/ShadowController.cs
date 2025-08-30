using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowController : PlayerController
{
    public List<string> moves = new List<string>();
    private string currentMove;
    private int i = 0;

    protected override void Start()
    {
        EventsManager.instance.onShadowSpawn.Invoke();
        base.Start();
        moves = LevelManager.instance.GetShadowMoves();
    }

    protected override void Update()
    {
        if (SceneChanger.instance.GetLevelNumber() == 10) return;
        if (!playerAlive) return;
        if (inDialog) return;

        currentGridCell = grid.GetValue(transform.position);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0f)
        {
            if (currentGridCell == 0 || GameManager.instance.playerStartMove)
            {
                GetNewPosition();
            }
        }

        MoveToNewTile();
    }

    #region Shadow Direction
    protected override bool Left()
    {
        move = currentMove == "left";
        if (move)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            GetAnimator().TriggerDash();
        }
        return move;
    }

    protected override bool Right()
    {
        move = currentMove == "right";
        if (move)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            GetAnimator().TriggerDash();
        }
        return move;
    }

    protected override bool Up()
    {
        return currentMove == "up";
    }

    protected override bool Down()
    {
        return currentMove == "down";
    }
    #endregion

    public override void GetNewPosition()
    {
        if (i != moves.Count)
        {
            currentMove = moves[i];
            base.GetNewPosition();

            if (currentGridCell != 0)
            {
                MoveTrackingManager.instance.GetDirectionalTrackerObject(i).gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, .1f);
                i++;
            }
        }
        else if (currentGridCell == 0)
        {
            NoTile();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TriggerDeath();
            StartCoroutine(collision.gameObject.GetComponent<AnimatorController>().WaitToReload(2f));
            playerAlive = false;
        }
    }
}
