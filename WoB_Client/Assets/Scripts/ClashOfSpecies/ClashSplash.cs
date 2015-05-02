using UnityEngine;

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

public class ClashSplash : MonoBehaviour {

	// Other
    private ClashGameManager manager;

	public Texture animals;
	private Rect windowRect;
	
	void Awake() {
		var main = GameObject.Find("MainObject");
        manager = main.AddComponent<ClashGameManager>();
	}
	
	// Use this for initialization
	IEnumerator Start() {
        manager.currentPlayer = GameState.player;
        
		yield return StartCoroutine(Execute(ClashSpeciesListProtocol.Prepare(), (res) => {
            var response = res as ResponseClashSpeciesList;
            manager.availableSpecies = response.speciesList;
        }));

        yield return StartCoroutine(Execute(ClashEntryProtocol.Prepare(), (res) => {
            var response = res as ResponseClashEntry;
            if (response.config != null) {
                foreach (var pair in response.config) {
                    var species = manager.availableSpecies.Single((el) => el.id == pair.Key);
                    manager.lastDefenseConfig.layout.Add(species, pair.Value);
                }
                Game.LoadScene("ClashMain");
            } else {
                Game.LoadScene("ClashShop");
            }
        }));
	}

    IEnumerator Execute(NetworkRequest req, NetworkManager.Callback cb) {
        bool done = false;
		NetworkManager.Send(req, (res) => {
			cb(res);
			done = true;
		});

		while (!done) yield return null;
	}

	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), animals);
		
		// Client Version Label
		GUI.Label(new Rect(Screen.width - 75, Screen.height - 30, 65, 20), "v" + Constants.CLIENT_VERSION + " Test");
		
	}
	
	// Update is called once per frame
	void Update() {}
}
