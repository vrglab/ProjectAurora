using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

using UnityEngine.UI;
using System.SAL;


public class Player : Entity
{
    public override int Health { get; set; } = 10;
    public override int Hunger { get; set; } = 20;
    public override float Speed { get; protected set; } = 19.8f;
    public override float RunSpeed { get; protected set; } = 28.65f;
    public override World world { get; set; }
    public override WorldTime WTime { get; set; }

    public Sprite Heart, DamagedHeart, MoreDamagedHeart, EvenMoreDamagedHeart, NearlyDead;

    public Image HeartImage;
    public Text HeartText, CPUtext, GPUText, FPSText, PTtext, UTtext, SLLtext, PLtext, TLtext, WItext, WStext, MIOtext;

    public GameObject SystemInfoPanel;

    public Rigidbody2D rg;

    public GameObject HandToHoldItems;

    public static Player instance { get; protected set; }

    public MinigType mine = MinigType.Hand;
    public MinigType Minetype { get; protected set; }

    public bool ShowSysemInfo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            print("Player set");
        }
    }

    void Start()
    {
        Minetype = mine;
        world = World.Instance;


    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        HeartUISet();

        ToggleSystemInfo(ShowSysemInfo);


        try
        {
            if (ShowSysemInfo == true)
            {
                Info();
            }
        }
        catch (Exception)
        {
            if (ShowSysemInfo == true)
            {
                TileSafeInfo();
            }
        }
       
            
       

        if (Input.GetKeyDown(KeyCode.F1))
        {

            AutoSavePlayer(transform.position.x, transform.position.y, Health, Hunger);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (ShowSysemInfo == true)
            {
                ShowSysemInfo = false;
            }
            else
            {
                ShowSysemInfo = true;
            }
        }
    }

    public void AutoSavePlayer(float x, float y, float Health, float Hunger)
    {
        string path = world.WorldSavePath;
        PlayerData p = new PlayerData(x, y, Health, Hunger);

        saveSystem.save("player", path, DirectoryManager.Savetype, true, p);

        Debug.Log("Data saved with: " + p.Data() + ". To the path: " + path);

    }

    public override void Init()
    {
        base.Init();
    }


    public void ToggleSystemInfo(bool Toggle)
    {
        if (Toggle == true)
        {
            SystemInfoPanel.SetActive(true);
        }
        else if (Toggle == false)
        {
            SystemInfoPanel.SetActive(false);
        }
    }

    public void HoldItem(Item item)
    {
        if (item.Type == ItemTypeSlot.Holdable)
        {
            Minetype = item.Minnig;
        }
    }


    public void Info()
    {
        int fps = (int)(1.0f / Time.deltaTime);
        Vector3 pl = new Vector3(transform.position.x, transform.position.y, transform.position.z);



        Vector3 tl = new Vector3(world.getTileAt("Ground", (int)transform.position.x, (int)transform.position.y).x, world.getTileAt("Ground", (int)transform.position.x, (int)transform.position.y).y, world.getTileAt("Ground", (int)transform.position.x, (int)transform.position.y).z);
    
       
        
        CPUtext.text = SystemInfo.processorType;
        GPUText.text = SystemInfo.graphicsDeviceName;
        FPSText.text = fps.ToString();
        PTtext.text = WorldTime.Instance.PhysicalTime.ToString();
        UTtext.text = WorldTime.Instance.UniversalTime.ToString();
        SLLtext.text = WorldTime.Instance.SunLightLevel.ToString();
        PLtext.text = " "+pl.x.ToString() + "/" + pl.y.ToString() + "/" + pl.z.ToString();
        WItext.text = world.ID.ToString();
        WStext.text = world.x.ToString() + "/" + world.y.ToString();



        try
        {
            TLtext.text = tl.x.ToString() + "/" + tl.y.ToString() + "/" + tl.z.ToString();
        }
        catch (NullReferenceException)
        {

            TLtext.text = "Information not found";
        }

    
        try
        {
            MIOtext.text = MousIsOverTile().name;
        }
        catch (DirectoryNotFoundException)
        {

            MIOtext.text = "Information not found";
        }
        
    }

    public void TileSafeInfo()
    {
        int fps = (int)(1.0f / Time.deltaTime);
        Vector3 pl = new Vector3(transform.position.x, transform.position.y, transform.position.z);



       


        CPUtext.text = SystemInfo.processorType;
        GPUText.text = SystemInfo.graphicsDeviceName;
        FPSText.text = fps.ToString();
        PTtext.text = WorldTime.Instance.PhysicalTime.ToString();
        UTtext.text = WorldTime.Instance.UniversalTime.ToString();
        SLLtext.text = WorldTime.Instance.SunLightLevel.ToString();
        PLtext.text = " " + pl.x.ToString() + "/" + pl.y.ToString() + "/" + pl.z.ToString();
        WItext.text = world.ID.ToString();
        WStext.text = world.x.ToString() + "/" + world.y.ToString();

        TLtext.text = "Information not found";
        MIOtext.text = "Information not found";
        

    }

    public void Movement()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {

            if (Input.GetKey(KeyCode.D))
            {
                Vector2 v2 = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 v2 = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 v2 = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector2 v2 = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
                rg.MovePosition(v2);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 v2 = new Vector2(transform.position.x + RunSpeed * Time.deltaTime, transform.position.y);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 v2 = new Vector2(transform.position.x - RunSpeed * Time.deltaTime, transform.position.y);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 v2 = new Vector2(transform.position.x, transform.position.y + RunSpeed * Time.deltaTime);
                rg.MovePosition(v2);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector2 v2 = new Vector2(transform.position.x, transform.position.y - RunSpeed * Time.deltaTime);
                rg.MovePosition(v2);
            }
        }
    }

    public void HeartUISet()
    {
        HeartText.text = Health.ToString();
        if (Health == 10)
        {
            HeartImage.sprite = Heart;
        }
        if (Health == 8)
        {
            HeartImage.sprite = DamagedHeart;
        }
        if (Health == 6)
        {
            HeartImage.sprite = MoreDamagedHeart;
        }
        if (Health == 4)
        {
            HeartImage.sprite = EvenMoreDamagedHeart;
        }
        if (Health == 2)
        {
            HeartImage.sprite = NearlyDead;
        }
    }

    public Tile MousIsOverTile()
    {
        Vector2 MousePose2D = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        Tile t;

        
            //If ground exists and nothing is on it return tile to to
            if (world.getTileAt("Ground", (int)MousePose2D.x, (int)MousePose2D.y) != null && world.getTileAt("Ores", (int)MousePose2D.x, (int)MousePose2D.y) == null || world.getTileAt("Trees", (int)MousePose2D.x, (int)MousePose2D.y) == null)
            {
                t = world.getTileAt("Ground", (int)MousePose2D.x, (int)MousePose2D.y);
                return t;
            }
            else if (world.getTileAt("Ores", (int)MousePose2D.x, (int)MousePose2D.y) != null)
            {
                t = world.getTileAt("Ores", (int)MousePose2D.x, (int)MousePose2D.y);
                return t;
            }
            else if (world.getTileAt("Trees", (int)MousePose2D.x, (int)MousePose2D.y) != null)
            {
                t = world.getTileAt("Trees", (int)MousePose2D.x, (int)MousePose2D.y);
                return t;
            }
      
        

        return null;
     
    }  

}

[Serializable]
public class PlayerData
{
   public  float x, y;

  public  float health;
   public float hunger;

    public PlayerData(float x, float y, float Health, float Hunger)
    {
        //this sets the world position of the player
        this.x = x;
        this.y = y;

        //this sets the player health and hunger
        health = Health;
        hunger = Hunger;
    }

    public object Data()
    {
        return  "Health: " + health.ToString() + " ,Hunger: " + hunger.ToString() + " ,Position: " + "x " + x.ToString() + " y " + y.ToString(); 
    }
}


