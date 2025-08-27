using System.Collections.Generic;
using UnityEngine;

public class ShadowController : PlayerController
{
    public List<string> moves = new List<string>();
    private string currentMove;
    private int i = 0;
    private bool moveShadow = false;

    protected override void Start()
    {
        EventsManager.instance.onShadowSpawn.Invoke();
        base.Start();
        moves = LevelManger.instance.GetShadowMoves();
    }

    protected override void Update()
    {
        if (inDialog) return;

        currentGridCell = grid.GetValue(transform.position);

        moveShadow = GameManager.instance.playerMoving;

        if (Vector3.Distance(transform.position, movePoint.position) <= 0f)
        {
            if (currentGridCell == 0 || moveShadow)
            {
                if (inDialog) return;
                GetNewPosition();
            }
        }

        MoveToNewTile();
    }

    #region Shadow Direction
    protected override bool Left()
    {
        return currentMove == "left";
    }

    protected override bool Right()
    {
        return currentMove == "right";
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
        if (i == moves.Count) return;

        currentMove = moves[i];

        base.GetNewPosition();

        if (currentGridCell != 0)
            i++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TriggerDeath();
            EventsManager.instance.onPlayerLose.Invoke();
        }
    }
}
