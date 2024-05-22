using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindAnyObjectByType<Player>();
            }    
            return player;
        }
        private set
        {
            player = value;
        }
    }

    protected override void OnInitalize()
    {
        base.OnInitalize();

         
    }
}
