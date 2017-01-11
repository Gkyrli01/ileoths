using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;


public class network1 : MonoBehaviour
{
    const float STANDART = 10f;
    float time = 0;
    public MatchInfo matchInfo;
    public bool host = false;
    public bool client = false;
    public bool started = false;
    // [SyncVar]
    // bool empikaBro;
    void Start()
    {
        NetworkManager.singleton.StartMatchMaker();
        matchMaker = gameObject.AddComponent<NetworkMatch>();
        migrationManager = gameObject.GetComponent<NetworkMigrationManager>();
       // ClientScene.SetReconnectId(0, null);

    }
    NetworkMatch matchMaker;
    NetworkMigrationManager migrationManager;

    void Awake()
    {
        //matchMaker = gameObject.AddComponent<NetworkMatch>();
    }

    //call this method to request a match to be created on the server
    public void CreateInternetMatch(string matchName)
    {
        NetworkPlayer ok = new NetworkPlayer();

        matchMaker.CreateMatch("", 2, true, "", ok.externalIP, ok.ipAddress, 0, 0, OnMatchCreate);

    }

    //this method is called when your request for creating a match is returned
    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
          
            Debug.Log(matchInfo.networkId+" | "+matchInfo.address);
            //Debug.Log("Create match succeeded");
            this.matchInfo = matchInfo;
            NetworkServer.Listen(matchInfo, 9000);
            host = true;
            ///StartCoroutine("waitPlayer");
            //PlayerPrefs.SetInt("Hosting", 1);
            NetworkManager.singleton.StartHost(matchInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }
 
    //call this method to find a match through the matchmaker
    public void FindInternetMatch(string matchName)
    {
        // migrationManager.
        
        //ClientScene.SetReconnectId(ClientScene.ReconnectIdHost, null);
        matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }

    //this method is called when a list of matches is returned
    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                if (PlayerPrefs.GetInt("Hosting") == 1)
                {
                    Debug.Log("empika re");
                    PlayerPrefs.SetInt("Hosting", 0);
                }
                //Debug.Log("A list of matches was returned");
                NetworkPlayer ok = new NetworkPlayer();
               // ok.
                //join the last server (just in case there are two...)
                //NetworkManager.singleton.matchMaker.JoinMatch(matchListResponse.matches[matchListResponse.matches.Count - 1].networkId, "", OnJoinInternetMatch);
               
           
                matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", ok.externalIP, ok.ipAddress, 0, 0, OnMatchJoined);
               // ClientScene.ReconnectIdHost
            }
            else
            {
                CreateInternetMatch("");
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    //this method is called when your request to join a match is returned
    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {

            NetworkManager.singleton.StartClient(matchInfo);

        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    /*
    [Command]
    void CmdStart()
    {
        NetworkManager.singleton.StartHost(matchInfo);
        started = true;
        RpcStartClient();
    }
    [ClientRpc]
    void RpcStartClient()
    {
        if (started)
            return;
        NetworkManager.singleton.StartClient(matchInfo);
    }*/
    /*
    void CheckIP()
    {
        WWW myExtIPWWW = new WWW("http://checkip.dyndns.org");
        if (myExtIPWWW == null) return;
        yield return myExtIPWWW;
        myExtIP = myExtIPWWW.text;
        myExtIP = myExtIP.Substring(myExtIP.IndexOf(":") + 1);
        myExtIP = myExtIP.Substring(0, myExtIP.IndexOf("<"));
        // print(myExtIP);
    }


  */  /*
    class EmployeeTCPServer
    {
        static TcpListener listener;
        const int LIMIT = 5; //5 concurrent clients

        public static void Main()
        {
            listener = new TcpListener(2055);
            listener.Start();
#if LOG
            Console.WriteLine("Server mounted, 
                            listening to port 2055");
#endif
            for (int i = 0; i < LIMIT; i++)
            {
                Thread t = new Thread(new ThreadStart(Service));
                t.Start();
            }
        }
        public static void Service()
        {
            while (true)
            {
                Socket soc = listener.AcceptSocket();
                //soc.SetSocketOption(SocketOptionLevel.Socket,
                //        SocketOptionName.ReceiveTimeout,10000);
#if LOG
                Console.WriteLine("Connected: {0}", 
                                         soc.RemoteEndPoint);
#endif
                try
                {
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true; // enable automatic flushing
                    sw.WriteLine("{0} Employees available",
                          ConfigurationSettings.AppSettings.Count);
                    while (true)
                    {
                        string name = sr.ReadLine();
                        if (name == "" || name == null) break;
                        string job =
                            ConfigurationSettings.AppSettings[name];
                        if (job == null) job = "No such employee";
                        sw.WriteLine(job);
                    }
                    s.Close();
                }
                catch (Exception e)
                {
#if LOG
                    Console.WriteLine(e.Message);
#endif
                }
#if LOG
                Console.WriteLine("Disconnected: {0}", 
                                        soc.RemoteEndPoint);
#endif
                soc.Close();
            }
        }
    }
    */
}