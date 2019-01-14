using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePokeFrameAnimAttack : FrameAnim {
    public LancePokeFrameAnimAttack(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
        GameObject lanceAnim = ((LancePokeDescriptor)action.descriptor).lancePrefab;
        Unit unit = sim.displacement.unit;
        Vector2 attackDir = Action.GetDirectionVector(dir);

        if(attackDir == Vector2.zero) {
        	// bad direction input
        	return false;
        }
       
        //Spawn Anims
        if (board.CheckCoord(sim.result + attackDir)) {
        	GameObject lance = GameObject.Instantiate(lanceAnim, (sim.result + attackDir), Quaternion.identity);
        	Tile tile = board.GetTile(sim.result + attackDir);

            lance.transform.position = tile.transform.position - new Vector3(0, 0.3f, 0);   //TODO: fix hardcoded sprite offset
        	foreach(SpriteRenderer sprite in lance.GetComponentsInChildren<SpriteRenderer>()) {
        		sprite.sortingOrder = tile.GetSortingOrder();
        	}
        	
            ((LancePoke)action).spawnedLanceAnims.Add(tile, lance);
        }
        // Ok spot: 2nd tile from user
        if (board.CheckCoord(sim.result + attackDir * 2)) {
            GameObject lance = GameObject.Instantiate(lanceAnim, (sim.result + attackDir * 2), Quaternion.identity);
            Tile tile = board.GetTile(sim.result + attackDir * 2);

            lance.transform.position = tile.transform.position - new Vector3(0, 0.3f, 0);   //TODO: fix hardcoded sprite offset
        	foreach(SpriteRenderer sprite in lance.GetComponentsInChildren<SpriteRenderer>()) {
        		sprite.sortingOrder = tile.GetSortingOrder();
        	}
        	
            ((LancePoke)action).spawnedLanceAnims.Add(tile, lance);
        }
        // Sweetspot: 3rd tile from user
        if (board.CheckCoord(sim.result + attackDir * 3)) {
            GameObject lance = GameObject.Instantiate(lanceAnim, (sim.result + attackDir * 3), Quaternion.identity);
            Tile tile = board.GetTile(sim.result + attackDir * 3);

            lance.transform.position = tile.transform.position - new Vector3(0, 0.3f, 0);   //TODO: fix hardcoded sprite offset
        	foreach(SpriteRenderer sprite in lance.GetComponentsInChildren<SpriteRenderer>()) {
        		sprite.sortingOrder = tile.GetSortingOrder();
        	}
        	
            ((LancePoke)action).spawnedLanceAnims.Add(tile, lance);
        }

        //Enable all animations
        foreach(KeyValuePair<Tile, GameObject> pair in ((LancePoke)action).spawnedLanceAnims) {
        	pair.Value.SetActive(true);
        }

        //Play Audio TODO: Probably should move this elsewhere
        AudioSource source = unit.audioManager.source;
        AudioClip attackClip = unit.audioManager.library["Attack"][Random.Range(0, unit.audioManager.library["Attack"].Count)];
        source.clip = attackClip;
        source.PlayDelayed(Random.Range(0.0f, 1.0f));

        return true;
	}
}