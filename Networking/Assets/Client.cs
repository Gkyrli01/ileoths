using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

    /// <summary>
    /// This constant is used for indicating which camera is going to be enables.if cam=PLAYER->player camera is used
    /// </summary>
    public static readonly int PLAYER = 1;
    /// <summary>
    /// This is used both for camera enabling and as mystate variable.
    /// </summary>
    public static readonly int SUMMON = 2;
    /// <summary>
    /// When someone is looking on the Arena is in myState=Playing.
    /// </summary>
    public static readonly int PLAYING = 4;
    /// <summary>
    /// If someone clicked on the sealCase,myState=Sealcase.
    /// </summary>
    public static readonly int SEALCASE = 5;
    /// <summary>
    /// Frames used for the animations.Should change to dynamic.
    /// </summary>
    public static readonly int FRAMES=60;
    /// <summary>
    /// Secondary state takes this variable,when myState is PLAYING.Above means that you are watching the arena from above.(Always check myState==Playing to use this).
    /// </summary>
    public static readonly int ABOVE = 10;
    /// <summary>
    /// Secondary state takes this variable,when myState is PLAYING.Normal means that you are watching the arena from the gods perspective.(Always check myState==Playing to use this).
    /// </summary>
    public static readonly int NORMAL = 11;
    /// <summary>
    /// Not in use for now.
    /// </summary>
    public static readonly int RETURNING = 12;
    /// <summary>
    /// Spell1 is used for SpellActions,so they can determine whether they are valid to do their job.
    /// </summary>
    public static readonly int SPELL1 = 13;
    /// <summary>
    /// Spell2 is used for SpellActions,so they can determine whether they are valid to do their job.
    /// </summary>
    public static readonly int SPELL2 = 14;
    /// <summary>
    /// Spell3 is used for SpellActions,so they can determine whether they are valid to do their job.
    /// </summary>
    public static readonly int SPELL3 = 15;



    //public static readonly int FRAMES = 60;


    float maximum;
    float angle;

    public static int customAnimating=0;
    static int id;
    public static Cameras myCameras=new Cameras();
    static Vector3[] initialPositions;
    static Quaternion[] initialQuaternions;

    public static int myState=PLAYING;
    public static int secondaryState=NORMAL;
    /// <summary>
    /// You assign this variable only Client.SPELL1,Client.SPELL2,Client.SPELL3 or -1 values.
    /// </summary>
    static int spellState = -1;

    changeBy toUse = new changeBy();
    [System.Serializable]
     public class Cameras
    {
        public static Camera current;
        public static GameObject player1;
        public static GameObject player2;
        public static GameObject summon;
    }

    Vector3 ps = new Vector3();
    class changeBy
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
        public Quaternion fromRotation;
        public Quaternion toRotation;

    }

    void Start()
    {

    }

    public static void setInitialTransforms()
    {
        initialPositions = new Vector3[5];
        initialPositions[0] = Cameras.player1.transform.position;
        initialPositions[1] = Cameras.player2.transform.position;
        initialPositions[2] = Cameras.summon.transform.position;

        initialQuaternions = new Quaternion[5];
        initialQuaternions[0] = Cameras.player1.transform.rotation;
        initialQuaternions[1] = Cameras.player2.transform.rotation;
        initialQuaternions[2] = Cameras.summon.transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (myState == PLAYING)
        {
            var y = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 135.0f; 
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 35.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;
            //Vector3 y = Input.mousePosition;
            if (Cameras.current == null)
                return;
        Cameras.current.transform.Rotate(z,x,0);
        Cameras.current.transform.Translate(0, 0, y);

        }

        if (customAnimating!=0)
        {
            if (customAnimating == FRAMES)
            {  
                calculate();
            }            
            Cameras.current.transform.rotation = Quaternion.RotateTowards(Cameras.current.transform.rotation, toUse.toRotation,angle);
            Cameras.current.transform.position = Vector3.MoveTowards(Cameras.current.transform.position, toUse.toPosition,maximum);
            customAnimating--;
            if (customAnimating == 0)
            {
                
            }
        }
    }


    /**
     * Lion polla tis poutsas alla asenie
     * 
     * */
    void calculate()
    {

        toUse.fromPosition = Cameras.current.transform.position;
        toUse.fromRotation = Cameras.current.transform.rotation;

        if (PLAYING == myState&& secondaryState==ABOVE)
        {
            toUse.toPosition = Cameras.summon.transform.position;
            toUse.toRotation = Cameras.summon.transform.rotation;
            toUse.toPosition.y += 3;
        }
        else if(PLAYING == myState&&secondaryState==NORMAL)
        {
            toUse.toPosition = initialPositions[id-1];
            toUse.toRotation = initialQuaternions[id-1];
        }
        else if (myState == PLAYING && secondaryState == RETURNING)
        {

        }
        else if(myState==SEALCASE){
            if (id == 1)
            {
                toUse.toPosition = GameObject.Find("SealCase1Pos").transform.position;
               
                toUse.toRotation = GameObject.Find("SealCase1Pos").transform.rotation;
            }
            else
            {
                toUse.toPosition = GameObject.Find("SealCase2Pos").transform.position;
                toUse.toRotation = GameObject.Find("SealCase2Pos").transform.rotation;
            }
            toUse.toPosition.z += 5;
            toUse.toPosition.y += 2;
        }

        maximum = Vector3.Distance(Cameras.current.transform.position, toUse.toPosition) / FRAMES;
        angle = Quaternion.Angle(Cameras.current.transform.rotation, toUse.toRotation) / FRAMES;

    }

    public  void animate(int target)
    {
        if (SUMMON == target)
        {

        }
    }

    public static Camera enableCamera(int Cam)//1 playerCamera 2 Summon Camera
    {
        Camera tmp=null;
        if (Cam == PLAYER)
        {
            Cell.Pause = true;
            if (Id1 == 1)
            {
                Cameras.current = Cameras.player1.GetComponent<Camera>();
                Cameras.player1.GetComponent<Camera>().enabled = true;
                Cameras.player2.GetComponent<Camera>().enabled = false;
                Cameras.summon.GetComponent<Camera>().enabled = false;
                


            }
            else
            {
                Cameras.current = Cameras.player2.GetComponent<Camera>();
                Cameras.player2.GetComponent<Camera>().enabled = true;
                Cameras.player1.GetComponent<Camera>().enabled = false;
                Cameras.summon.GetComponent<Camera>().enabled = false;
                

            }
        }
        else if (Cam == SUMMON)
        {
            Cameras.player1.GetComponent<Camera>().enabled = false;
            Cameras.player2.GetComponent<Camera>().enabled = false;
            Cameras.summon.GetComponent<Camera>().enabled = true;
            Cameras.current = Cameras.summon.GetComponent<Camera>();
            myState = SUMMON;
            Cell.Pause = false;

        }

        return tmp;
    }


   

    public int Id
    {
        get
        {
            return Id1;
        }

        set
        {
            Id1 = value;
        }
    }

    public static int Id1
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }
    /// <summary>
    /// You assign this variable only Client.SPELL1,Client.SPELL2,Client.SPELL3 or -1 values.
    /// </summary>
    public static int SpellState
    {
        get
        {
            return spellState;
        }

        set
        {
             spellState = value;
        }
    }

    public  void setState(int value)
    {
        if (value == 1)
            spellState = SPELL1;
        else if (value == 2)
            spellState = SPELL2;
        else if (value == 3)
            spellState = SPELL3;
    }
}
