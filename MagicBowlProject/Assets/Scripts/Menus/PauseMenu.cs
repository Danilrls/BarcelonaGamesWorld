	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.Networking;
	using UnityEngine.Networking.Match;


	public class PauseMenu : NetworkBehaviour {
		
		public NetworkManager manager;
		public bool server;
		public GameObject btnPause;

		public PlayerController myPlayer;
		// Use this for initialization

		void Start () {
			this.gameObject.SetActive(false); //note: the object has to be active in the viewport
			//manager = m.GetComponent<CustomNetworkManager>().manager;
			//COJER EL MANAGER DEL SCRIPT DE CUSTOMNETWORKMANAGER
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void mostraMenuPausa(){ //important to be public to see it in the onclick() from PauseButton
			if (!this.gameObject.activeInHierarchy) {
				this.gameObject.SetActive (true);
				btnPause.SetActive(false);
			} else {
				this.gameObject.SetActive (false);
				btnPause.SetActive(true);
			}
		} 

		public void clickContinue(){
			this.gameObject.SetActive (false); 
			btnPause.SetActive(true);
		}

		public void OnDestroyMatch(bool success, string extendedInfo)
		{	
			//Caso en el que el cliente sale de la partida por si mismo
			Debug.Log("CLIENT PLAYER DESTROYED");
			NetworkManager.singleton.StopMatchMaker();
			NetworkManager.Shutdown();
			Destroy(GameObject.Find("NetworkManager"));
		}

		public void clickExit(NetworkManager m){
			//Si es servidor checkea que jugador es para llamar el RPC que hace que todos cierren su partida
			if(server){
				Debug.Log("FJALAR KICKS");
				myPlayer.RpcKickEveryone();
			//Si es cliente, cierra su partida
			}else{
				Debug.Log("NOT SERVER");
				manager = m.GetComponent<CustomNetworkManager>().manager;
				manager.matchMaker.DestroyMatch(manager.matches[0].networkId, 0, OnDestroyMatch);
				Destroy(GameObject.Find("mc"));
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
