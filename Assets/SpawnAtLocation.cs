using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtLocation : SceneController
{
    public Transform player;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

		/*if (prevScene == "Inside House Scene")
        {
            player.position = new Vector2(-3f, -0.5f);
            //Camera.main.transform.position = new Vector3(0f, 3f, -10f);
        }*/

		if (prevScene == "Inside House Scene" && currentScene == "Outside House Scene") {
			player.position = new Vector2(-3f, -0.5f);
		}

		if (prevScene == "Town Scene" && currentScene == "Outside House Scene")
		{
			player.position = new Vector2(10f, -3.5f);
		}

		if (prevScene == "Outside House Scene" && currentScene == "Town Scene")
		{
			player.position = new Vector2(10f, 8f);
		}

		if (prevScene == "LevelGenV2" && currentScene == "Town Scene")
		{
			player.position = new Vector2(70f, 10f);
		}
	}
}
