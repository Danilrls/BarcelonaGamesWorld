#if ENABLE_UNET
using UnityEngine.SceneManagement;
namespace UnityEngine.Networking

{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]

    public class CustomNetworkManager : NetworkBehaviour
    {
        
        public NetworkManager manager;
        private bool matchFound;
        private bool disconnect;
        private int iterations;
        private int maxIterations;
        public int minPlayersPerRoom;
        public bool startGame = false;
        public int selectionIndex;
        public bool stopIterating = false;

        
        private bool server;
        private bool listTheMatch = true;

        void Start()
        {
            matchFound = true;
            iterations = 0;

            manager = this.GetComponent<NetworkManager>();
            manager.StartMatchMaker();
            manager.matchMaker.ListMatches(0, 1, "", true, 0, 0, manager.OnMatchList);

            //Accede al indice de seleccion para saber que personaje spawnear
            GameObject md = GameObject.Find("mc");
            selectionIndex = md.GetComponentInChildren<CharacterCreation>().selectionIndex;
            //Selecciona el player prefab
            switch (selectionIndex)
            {
                case 1:
                    manager.playerPrefab = manager.spawnPrefabs[1];
                    break;
                default:
                    manager.playerPrefab = manager.spawnPrefabs[0];
                    break;
            }
            maxIterations = 100;

        }

        void Update()
        {

           // Debug.Log(iterations);
            Debug.Log(matchFound);
            // Si la lista de matches no es nula && Si el count de la lista de matches es mas grande que 0 &&
            // iterations es mas pequeño que maxIterations, cosa
            // que implica que lo acota a las primeras maxIterations interaciones, coge el primer match de la
            // lista y la asigna al manager, se une.
            if (manager.matches != null && manager.matches.Count > 0 && matchFound &&
                iterations < maxIterations && manager.matches[0].currentSize < minPlayersPerRoom)
            {
                matchFound = false;
                
                manager.matchName = manager.matches[0].name;
                manager.matchSize = (uint)manager.matches[0].currentSize;

                stopIterating = true;
                manager.matchMaker.JoinMatch(manager.matches[0].networkId, "", "", "", 0, 0,
                    manager.OnMatchJoined);

                // manager.matchName = "MagicBowl Game";
                // manager.matchSize = 4;
                // manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0,
                //      0, manager.OnMatchCreate);
                // matchFound = false;

                manager.matchMaker.ListMatches(0, 1, "", true, 0, 0, manager.OnMatchList);
            }
            //Si ha hecho mas de maxIterations iteraciones, y no ha encontrado partida (matchFound == true), 
            // crea la partida.
            if (iterations >= maxIterations && matchFound)
            {
                manager.matchName = "MagicBowl Game";
                manager.matchSize = 4;
                stopIterating = true;
                manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0,
                     0, manager.OnMatchCreate);
                matchFound = false;
                
                manager.matchMaker.ListMatches(0, 1, "", true, 0, 0, manager.OnMatchList);
            }
            // Ha de ser igual a 4
            if (manager.numPlayers >= minPlayersPerRoom)
            {
                startGame = true;
                //Needed to destry the match
                if(listTheMatch){
                     manager.matchMaker.ListMatches(0, 1, "", true, 0, 0, manager.OnMatchList);
                }
                listTheMatch = false;
            }
            if(!stopIterating){
                iterations++;
            }
            
        }
        public void closeMatch(){
            manager.matchMaker.DestroyMatch(manager.matches[0].networkId, 0, OnDestroyMatch);
        }
        private void OnPlayerDisconnected(NetworkPlayer player)
        {
            Debug.Log("PLAYER DISCONECTED");
            //Reset iterations for next game
            iterations = 0;
            //Reset for next game
            matchFound = true;

            
            if (player == GetServerPlayer())
            {
                server = (player == GetServerPlayer());
                Debug.Log("Server player disconnected");
                manager.matchMaker.DestroyMatch(manager.matches[0].networkId, 0, OnDestroyMatch);
            }
        }
        
        public void OnDestroyMatch(bool success, string extendedInfo)
		{
            //Reset iterations for next game
            iterations = 0;
            //Reset for next game
            matchFound = true;

            Debug.Log("ON DESTROY MATCH TRIGGERED");
			if(server){
				Debug.Log("SERVER PLAYER DESTROYED");
				NetworkManager.singleton.StopHost(); 
				NetworkManager.singleton.StopMatchMaker(); 
				NetworkManager.Shutdown(); 
				Destroy(GameObject.Find("NetworkManager"));
				NetworkTransport.Shutdown();
                Destroy(GameObject.Find("mc"));
			    SceneManager.LoadScene("MainMenu");
			}else{
				Debug.Log("CLIENT PLAYER DESTROYED");
				NetworkManager.singleton.StopMatchMaker();
				NetworkManager.Shutdown();
				Destroy(GameObject.Find("NetworkManager"));
                Destroy(GameObject.Find("mc"));
			    SceneManager.LoadScene("MainMenu");
			}
            
		}
       

        public static NetworkPlayer GetServerPlayer()
        {
            if (Network.isClient)
                return Network.connections[0];
            if (Network.isServer)
                return Network.player;
            // not connected or not running as server
            return Network.connections[0];
        }

      
    }
};

#endif //ENABLE_UNET