using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Jobs;
using System.SAL;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




public class World : MonoBehaviour
{
    [Header("other mannagers or controllers")]
    public DirectoryManager dm;

    [Header("World generation")]
    public Material SpriteMat;
    public Item stick, Stone;
    public Sprite Grass, stone,tree, CaveStone;
    




    [Header("World controller options")]
    public bool WorldGen = false;
    public bool newWorld;
    public int loadingId;


    private TileData tdG;
    private int worldId;
    private int id;
    public int x, y;

    public int ID { get; protected set; }

    public string WorldSavePath { get; protected set; }

    public string Name { get; set; }

    private Dictionary<int, string> world = loadSystem.load<Dictionary<int, string>>("world", "C:/Users/arad8/AppData/LocalLow/vrglab/projekt aurora/worldsLoad", "pa");


    public static World Instance { get; protected set; } = null;

    bool hasquit;


    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (WorldGen == true)
        {
            ProjectAurora.Diagnostics.Debug.log("Entered unity awake", gameObject);
            if (newWorld == true)
            {
                ProjectAurora.Diagnostics.Debug.task("Creating new world");

                NewWorld();

                ProjectAurora.Diagnostics.Debug.log("New world created", gameObject);
            }

            if (newWorld == false)
            {
                ProjectAurora.Diagnostics.Debug.task("Loading world");
                LoadWorld(loadingId);

                ProjectAurora.Diagnostics.Debug.log("World" + id.ToString() + " Is loaded", gameObject);
            }
        }
    }

    private void Update()
    {
       

        GameObject go = GameObject.Find("New Game Object");
        Destroy(go);
    }

    

    //This creates a new world for the player to play in.
    public void NewWorld()
    {
        ProjectAurora.Diagnostics.Debug.task("Generating a new World id");
        //We create the id of the world
        id = RandomizeWorldid();

        //We check to see the id is in use
        if (world.ContainsKey(id))
        {
            //If a world with this id exists we Re-randomize the id
            id = RandomizeWorldid();
        }

        ProjectAurora.Diagnostics.Debug.log(id);
        //We chekc to see if the world exists
        worldExists(id);

        ProjectAurora.Diagnostics.Debug.task("Creating save Directories");
        //We create the save folder of the world
        Directory.CreateDirectory(Application.persistentDataPath + "/" + "world/" + "world" + id.ToString());

        ProjectAurora.Diagnostics.Debug.log(Application.persistentDataPath + "/" + "world/" + "world" + id.ToString());

        //We set the universal save path and world id
        WorldSavePath = Application.persistentDataPath + "/" + "world/" + "world" + id.ToString();
        ID = id;

        if(!Directory.Exists(WorldSavePath + "/Tiles"))
        {
            Directory.CreateDirectory(WorldSavePath + "/Tiles");
        }
        if (!Directory.Exists(WorldSavePath + "/Tiles/Ground"))
        {
            Directory.CreateDirectory(WorldSavePath + "/Tiles/Ground");
        }
        if (!Directory.Exists(WorldSavePath + "/Tiles/Ores"))
        {
            Directory.CreateDirectory(WorldSavePath + "/Tiles/Ores");
        }
        if (!Directory.Exists(WorldSavePath + "/Tiles/Trees"))
        {
            Directory.CreateDirectory(WorldSavePath + "/Tiles/Trees");
        }

        world.Add(id, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString());


        ProjectAurora.Diagnostics.Debug.task("Saving Directories");
        saveSystem.save("world", dm.worldsdataPath, dm.SaveType, false, world);


        //Debug.Log("Worlds in data base: " + world.Count);
        ProjectAurora.Diagnostics.Debug.task("Generating world Surface");

      
            GenerateWorldSurface();
    
        ProjectAurora.Diagnostics.Debug.task("Creating new world");
       

        ProjectAurora.Diagnostics.Debug.task("Saving world data");
        worldData w = new worldData(id,WorldSavePath, Name, x, y);
        saveSystem.save("worldData", WorldSavePath, dm.SaveType, false, w);
        ProjectAurora.Diagnostics.Debug.task("Entering world");

    }

    //This loads a world based on there id
    public void LoadWorld(int id)
    {
        if (world.ContainsKey(id))
        {
            ProjectAurora.Diagnostics.Debug.task("Entering world directory");
            //We set the universal save path to the worlds path
            WorldSavePath = Application.persistentDataPath + "/" + "world/" + "world" + id.ToString();

            ProjectAurora.Diagnostics.Debug.task("Loading world data");
            //We load the data of the world with the id
            worldData wd = loadSystem.load<worldData>("worldData", Application.persistentDataPath + "/" + "world/" + "world" + id.ToString(), dm.SaveType);

            ///We set the universal id.
            ID = wd.ID;

            ProjectAurora.Diagnostics.Debug.task("Seting world size");
            x = wd.x;
            y = wd.y;

            ProjectAurora.Diagnostics.Debug.task("Loading world Tiles");
            LoadWorldTile(id, wd.x , wd.y, wd.Path + "/Tiles/");

            //Debug.Log("World-" + ID.ToString() + " Was loaded from the path: " + WorldSavePath);
        }
        else
        {
            throw new WorldNotFoundException();
        }

    }

    public void DeleteWorld(int id)
    {
        if (world.ContainsKey(id))
        {
            File.Delete(Application.persistentDataPath + "/" + "world/" + "world" + id.ToString());
            world.Remove(id);


        }

    }


    #region Surface genoration

    /*
     * This is the most slowest thing i have made. God help please help oh please help
     * I can not thread this. So it is huyper slow.
     */


    public void GenerateWorldSurface()
    {
        GameObject g = Instantiate(new GameObject("Surface"), transform, true);

        int X = x;
        int Y = y;

            for (int i = 0; i < X; i++)
            {
                for (int b = 0; b < Y; b++)
                {


                //genorate the ground
                SurfaceGroundGenorator(i, b, g);

                //Genorate the trees
                int treeGen = UnityEngine.Random.Range(0, 60);
                if (treeGen == 2)
                {
                    TreeGenorator(i, b, g);
                }

                //Genorate the items
                int itemGen = UnityEngine.Random.Range(0, 40);
                if (itemGen == 9)
                {
                    ItemGenorator(i, b, g);
                }


                }
            }
        
            if(g.name == "Surface(Clone)")
            {
                    g.name = "Surface";
            }
        
    }

    

    public void TreeGenorator(int x, int y, GameObject parent)
    {
        TileData T = LoadTileDataAt(x, y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Ground");

        if (T.GoName == "Grass")
        {
            Tile t = new Tile(x, y, -1);

            t.SetTiletype(TileBlockType.Tree);
            t.HasCollider2D = true;
            t.HasRidigdbody2D = true;
            t.go = Instantiate(new GameObject(), parent.transform, true);
            t.go.name = "Tree";
            t.Name = t.go.name;
            t.go.AddComponent<PolygonCollider2D>();
            t.go.AddComponent<OptimiesTile>();
            t.GetCollider().isTrigger = true;
            t.go.AddComponent<Rigidbody2D>();
            t.rg = t.go.GetComponent<Rigidbody2D>();
            t.go.AddComponent<SpriteRenderer>();
            t.go.GetComponent<SpriteRenderer>().sortingLayerName = "Tree";
            t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
            t.go.GetComponent<OptimiesTile>().main = Camera.main;
            t.rg.gravityScale = 0f;
            t.SetPos(new Vector3(x, y, -1));
            t.SetSprite(tree);


            if (t.go.name != "New Game Object(Clone)")
            {

                tdG = new TileData(
                t.tt,
                t.HasRidigdbody2D,
                t.HasCollider2D,
                t.x,
                t.y,
                t.z,
                t.GetSprite(),
                t.go,
                t.GetCollider().isTrigger,
                t.GetRigidbody().gravityScale,
                t.HasSpriteRenderer,
                t.GetSpRenderer().sortingLayerName
                );
                string potion = JsonUtility.ToJson(tdG);
                saveSystem.save("tile " + t.x + " " + t.y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Trees", "pa", true, potion);
            }

            if (t.go.name == "New Game Object(Clone)")
            {
                Destroy(t.go);
            }
            if (t.go.name == "New Game Object")
            {
                Destroy(t.go);
            }
        }

       

    }

    public void SurfaceGroundGenorator(int x, int y, GameObject parent)
    {
        int Tile = UnityEngine.Random.Range(-1, 20);
        Tile t = new Tile(x, y);

        switch (Tile)
        {
            default:

                t.go = Instantiate(new GameObject(), parent.transform, true);

                t.SetTiletype(TileBlockType.grass);
                t.HasCollider2D = false;
                t.HasRidigdbody2D = true;
                t.go.name = "Grass";
                t.Name = t.go.name;
                t.go.AddComponent<Rigidbody2D>();
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.SetPos(new Vector2(x, y));
                t.SetSprite(Grass);
                t.go.AddComponent<Tile>();
                t.go.GetComponent<Tile>().go = t.go;

                break;
            case 2:

                t.go = Instantiate(new GameObject(), parent.transform, true);

                t.SetTiletype(TileBlockType.grass);
                t.HasCollider2D = false;
                t.HasRidigdbody2D = true;
                t.go.name = "Grass";
                t.Name = t.go.name;
                t.go.AddComponent<Rigidbody2D>();
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.SetPos(new Vector2(x, y));
                t.SetSprite(Grass);
                t.go.AddComponent<Tile>();
                t.go.GetComponent<Tile>().go = t.go;
                break;

            case 4:

                t.go = Instantiate(new GameObject(), parent.transform, true);

                t.SetTiletype(TileBlockType.grass);
                t.HasCollider2D = false;
                t.HasRidigdbody2D = true;
                t.go.name = "Grass";
                t.Name = t.go.name;
                t.go.AddComponent<Rigidbody2D>();
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.SetPos(new Vector2(x, y));
                t.SetSprite(Grass);
                t.go.AddComponent<Tile>();
                t.go.GetComponent<Tile>().go = t.go;
                break;
            case 5:
                t.go = Instantiate(new GameObject(), parent.transform, true);

                t.SetTiletype(TileBlockType.grass);
                t.HasCollider2D = false;
                t.HasRidigdbody2D = true;
                t.go.name = "Grass";
                t.Name = t.go.name;
                t.go.AddComponent<Rigidbody2D>();
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.SetPos(new Vector2(x, y));
                t.SetSprite(Grass);
                t.go.AddComponent<Tile>();
                t.go.GetComponent<Tile>().go = t.go;
                break;
            case 3:
                t.SetTiletype(TileBlockType.stone);
                t.go = Instantiate(new GameObject(), parent.transform, true);
                t.go.name = "TallGrass";
                t.go.AddComponent<BoxCollider2D>();
                t.GetCollider().isTrigger = true;
                t.go.AddComponent<Rigidbody2D>();
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                t.go.GetComponent<SpriteRenderer>().sortingLayerName = "Ores";
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.Name = t.go.name;
                t.HasCollider2D = true;
                t.HasRidigdbody2D = true;
                t.go.transform.localScale = new Vector3(0.6034f, 0.6034f, 0.6034f);
                t.SetSprite(stone);
                t.z = t.go.transform.position.z;
                t.SetPos(new Vector2(x, y));

                break;


        }

        try
        {
            tdG = new TileData(
            t.tt,
            t.HasRidigdbody2D,
            t.HasCollider2D,
            t.x,
            t.y,
            t.z,
            t.GetSprite(),
            t.go,
            t.GetCollider().isTrigger,
            t.GetRigidbody().gravityScale,
            t.HasSpriteRenderer,
            t.GetSpRenderer().sortingLayerName
            );
        }
        catch (Exception)
        {
            try
            {
            tdG = new TileData(
          t.tt,
          t.HasRidigdbody2D,
          t.HasCollider2D,
          t.x,
          t.y,
          t.z,
          t.GetSprite(),
          t.go,
          t.GetRigidbody().gravityScale,
          t.HasSpriteRenderer,
          t.GetSpRenderer().sortingLayerName
          );
            }
            catch (Exception)
            {
                tdG = new TileData(
          t.tt,
          t.HasRidigdbody2D,
          t.HasCollider2D,
          t.x,
          t.y,
          t.z,
          t.GetSprite(),
          t.go,
          t.HasSpriteRenderer,
          t.GetSpRenderer().sortingLayerName
          );

            }
            
        }
        

            string potion = JsonUtility.ToJson(tdG);
            saveSystem.save("tile " + t.x + " " + t.y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Ground", "pa", true, potion);

      

       


    }

    public void ItemGenorator(int x, int y, GameObject parent)
    {
        TileData T = LoadTileDataAt(x, y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Ground");

        int num = UnityEngine.Random.Range(0, 4);
        Tile t = new Tile(x, y, -2);

        switch (num)
        {
            default:
                if (T.GoName == "Grass")
                {
                    
                    t.SetTiletype(TileBlockType.Tree);
                    t.HasCollider2D = true;
                    t.HasRidigdbody2D = true;
                    t.go = Instantiate(new GameObject(), parent.transform, true);
                    t.go.AddComponent<ItemObject>();
                    t.go.name = "Stick";
                    t.Name = t.go.name;
                    t.go.AddComponent<BoxCollider2D>();
                    t.go.AddComponent<SpriteRenderer>();
                    t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                    t.go.GetComponent<SpriteRenderer>().sortingLayerName = "Ores";
                    t.go.GetComponent<ItemObject>().item = stick;
                    t.go.AddComponent<OptimiesTile>();
                    t.GetCollider().isTrigger = true;
                    t.bc2 = t.go.GetComponent<BoxCollider2D>();
                    t.go.GetComponent<BoxCollider2D>().size = new Vector2(0.83f, 2.75f);
                    t.go.AddComponent<Rigidbody2D>();
                    t.go.GetComponent<Rigidbody2D>().SetRotation(-41.013f);
                    t.rg = t.go.GetComponent<Rigidbody2D>();
                    t.go.GetComponent<OptimiesTile>().main = Camera.main;
                    t.rg.gravityScale = 0f;
                    t.SetPos(new Vector3(x, y, -1));
                    t.SetSprite(stick.Image);
                    
                    

                }
                break;

            case 2:
                if (T.GoName == "Grass")
                {

                    t.SetTiletype(TileBlockType.Tree);
                    t.HasCollider2D = true;
                    t.HasRidigdbody2D = true;
                    t.go = Instantiate(new GameObject(), parent.transform, true);
                    t.go.AddComponent<ItemObject>();
                    t.go.name = "Stone";
                    t.go.AddComponent<EdgeCollider2D>();
                    t.go.AddComponent<SpriteRenderer>();
                    t.go.GetComponent<SpriteRenderer>().material = SpriteMat;
                    t.go.GetComponent<SpriteRenderer>().sortingLayerName = "Ores";
                    t.go.GetComponent<ItemObject>().item = Stone;
                    t.go.AddComponent<OptimiesTile>();
                    t.GetCollider().isTrigger = false;

                    List<Vector2> points = new List<Vector2>();
                    points.Add(new Vector2(-0.4151745f, -0.2330685f));
                    points.Add(new Vector2(-0.4630175f, -0.2069778f));
                    points.Add(new Vector2(-0.4761019f, -0.1578255f));
                    points.Add(new Vector2(-0.4704857f, -0.09151268f));
                    points.Add(new Vector2(-0.4207792f, 0.009716034f));
                    points.Add(new Vector2(-0.2311592f, 0.2142944f));
                    points.Add(new Vector2(-0.162501f, 0.2783765f)); 
                    points.Add(new Vector2(-0.02504349f, 0.2798567f));
                    points.Add(new Vector2(0.01332998f, 0.3093252f));
                    points.Add(new Vector2(0.07708457f, 0.2664242f));
                    points.Add(new Vector2(0.1212483f, 0.1212483f));
                    points.Add(new Vector2(0.1578369f, 0.235693f));
                    points.Add(new Vector2(0.1592903f, 0.1064749f));
                    points.Add(new Vector2(0.1109095f, -0.01511193f));
                    points.Add(new Vector2(0.01754665f, -0.1273861f));
                    points.Add(new Vector2(-0.09866714f, -0.1900043f));
                    points.Add(new Vector2(-0.2447052f, -0.2304869f));
                    points.Add(new Vector2(-0.4149628f, -0.2335644f));
                    t.go.GetComponent<EdgeCollider2D>().SetPoints(points);

                    //t.GetComponent<EdgeCollider2D>().edgeRadius = 0.15f;
                    t.go.AddComponent<Rigidbody2D>();
                    t.rg = t.go.GetComponent<Rigidbody2D>();
                    t.go.GetComponent<OptimiesTile>().main = Camera.main;
                    t.rg.gravityScale = 0f;
                    t.SetPos(new Vector3(x, y, -1));
                    t.SetSprite(Stone.Image);

                }
                break;



        }
        if(t != null)
        {
            if (t.go.name != "New Game Object(Clone)" || t.go.name != "New Game Object" || t.go.name != null)
            {

                tdG = new TileData(
                t.tt,
                t.HasRidigdbody2D,
                t.HasCollider2D,
                t.x,
                t.y,
                t.z,
                t.GetSprite(),
                t.go,
                t.GetCollider().isTrigger,
                t.GetRigidbody().gravityScale,
                t.HasSpriteRenderer,
                t.GetSpRenderer().sortingLayerName
                );
                string potion = JsonUtility.ToJson(tdG);
                saveSystem.save("tile " + t.x + " " + t.y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Ores", "pa", true, potion);
            }

            if (t.go.name == "New Game Object(Clone)")
            {
                Destroy(t.go);
            }
            if (t.go.name == "New Game Object")
            {
                Destroy(t.go);
            }
        }
        







    }

    #endregion

    #region cave genoration
    public void GenerateWorldCave()
    {
        GameObject g = Instantiate(new GameObject("Cave"), transform, true);
        for (int i = 0; i < x; i++)
        {
            for (int b = 0; b < y; b++)
            {
                CaveGroundGenorator(i, b, g);
               


            }
        }
        g.SetActive(false);
    }

    public void CaveGroundGenorator(int x, int y, GameObject parent)
    {
        int Tile = UnityEngine.Random.Range(-1, 9);
        Tile t = new Tile(x, y);

        switch (Tile)
        {
            default:

                t.go = Instantiate(new GameObject(), parent.transform, true);

                t.SetTiletype(TileBlockType.stone);
                t.HasCollider2D = true;
                t.HasRidigdbody2D = true;
                t.go.name = "Stone";
                t.go.AddComponent<PolygonCollider2D>();
                t.GetCollider().isTrigger = true;
                t.go.AddComponent<Rigidbody2D>();
                
                t.rg = t.go.GetComponent<Rigidbody2D>();
                t.go.AddComponent<SpriteRenderer>();
                t.go.AddComponent<OptimiesTile>();
                t.go.GetComponent<OptimiesTile>().main = Camera.main;
                t.rg.gravityScale = 0f;
                t.SetPos(new Vector2(x, y));
                t.SetSprite(CaveStone);



                break;

           
        }




        if (t.go.name != "New Game Object(Clone)")
        {

            tdG = new TileData(
            t.tt,
            t.HasRidigdbody2D,
            t.HasCollider2D,
            t.x,
            t.y,
            t.z,
            t.GetSprite(),
            t.go,
            t.GetCollider().isTrigger,
            t.GetRigidbody().gravityScale,
            t.HasSpriteRenderer,
            t.GetSpRenderer().sortingLayerName
            );

            string potion = JsonUtility.ToJson(tdG);
            saveSystem.save("tile " + t.x + " " + t.y, Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/Ground", "pa", true, potion);

        }

        if (t.go.name == "New Game Object(Clone)")
        {
            Destroy(t.go);
        }

        if (t.go.name == "New Game Object")
        {
            Destroy(t.go);
        }


    }
    #endregion

    public Tile getTileAt(string TileTypeFolder, int x, int y)
    {
        try
        {
            Tile t = new Tile(x, y);


            string td = loadSystem.load<string>(Application.persistentDataPath + "/" + "world/" + "world" + id.ToString() + "/Tiles/" + TileTypeFolder + "/" + "tile " + t.x + " " + t.y + "." + DirectoryManager.Savetype);

            TileData data = JsonUtility.FromJson<TileData>(td);

            t.go = new GameObject(data.GoName);

            return t;
        }
        catch (Exception)
        {

            return null;
        }
      
    }


    public TileData LoadTileDataAt(int x,int y, string TileDataPath)
    {



        FileStream fs = new FileStream(TileDataPath + "/" + "tile " + x + " " + y + "." + "pa", FileMode.Open);

        BinaryFormatter formater = new BinaryFormatter();

       string td = (string)formater.Deserialize(fs);

        TileData data = JsonUtility.FromJson<TileData>(td);

        return  data;
    }

    public void LoadWorldTile(int id, int worldx, int worldy, string path)
    {
        
        //Load ground
        for (int x   = 0; x < worldx; x++)
            {
             
            for (int y = 0; y < worldy; y++)
            {
                Tile t = new Tile(x, y);

          
                string td = loadSystem.load<string>(path + "/Ground" + "/" + "tile " + t.x + " " + t.y + "." + "pa");

                TileData data = JsonUtility.FromJson<TileData>(td);



                t.go = Instantiate(new GameObject(data.GoName), transform, true);

                t.rg = t.go.GetComponent<Rigidbody2D>();

                t.go.AddComponent<SpriteRenderer>();
                t.go.GetComponent<SpriteRenderer>().sprite = data.sprite;
         
                if (data.HasRidigdbody2D == true)
                {
                    t.go.AddComponent<Rigidbody2D>();

                    t.go.GetComponent<Rigidbody2D>().gravityScale = data.GravetyScale;
                    
                }

                if (data.HasCollider2D == true)
                {
                    t.go.AddComponent<PolygonCollider2D>();

                    t.go.GetComponent<PolygonCollider2D>().isTrigger = data.IsC2DTrriger;

                }

                t.go.transform.localScale = new Vector3(data.xScale, data.yScale, data.zScale);


                t.SetPos(new Vector3(data.pos3D.x, data.pos3D.y, data.pos3D.z));
                t.go.AddComponent<OptimiesTile>();

                if (t.go.name == "New Game Object(Clone)")
                {
                    Destroy(t.go);
                }



            }
        }

        //Load trees
        /*for (int x = 0; x < worldx; x++)
        {

            for (int y = 0; y < worldy; y++)
            {
                Tile t = new Tile(x, y);

                if (File.Exists(path + "/Trees" + "/" + "tile " + t.x + " " + t.y + "." + "pa"))
                {
                    string td = loadSystem.load<string>(path + "/Trees" + "/" + "tile " + t.x + " " + t.y + "." + "pa");

                    TileData data = JsonUtility.FromJson<TileData>(td);



                    t.go = Instantiate(new GameObject(data.GoName), transform, true);

                    t.rg = t.go.GetComponent<Rigidbody2D>();


                    t.go.AddComponent<SpriteRenderer>();
                    t.go.GetComponent<SpriteRenderer>().sprite = data.sprite;
                    t.go.GetComponent<SpriteRenderer>().sortingLayerName = data.sortingLayername;


                    t.go.AddComponent<OptimiesTile>();
                    if (data.HasRidigdbody2D == true)
                    {
                        t.go.AddComponent<Rigidbody2D>();

                        t.go.GetComponent<Rigidbody2D>().gravityScale = data.GravetyScale;

                    }

                    if (data.HasCollider2D == true)
                    {
                        t.go.AddComponent<PolygonCollider2D>();

                        t.go.GetComponent<PolygonCollider2D>().isTrigger = data.IsC2DTrriger;

                    }

                    t.go.transform.localScale = new Vector3(data.xScale, data.yScale, data.zScale);


                    t.SetPos(new Vector3(data.pos3D.x, data.pos3D.y, data.pos3D.z));


                    if (t.go.name == "New Game Object(Clone)")
                    {
                        Destroy(t.go);
                    }
             }*/





    }
  



    //This gets the world data of the chose world
    public static worldData GetWorldData(int id)
    {
        worldData wd = loadSystem.load<worldData>("worldData", Application.persistentDataPath + "/" + "world/" + "world" + id.ToString(), "pa");

        return wd;
    }

    private int RandomizeWorldid()
    {
        worldId = UnityEngine.Random.Range(0, 900000000);

        return worldId;
    }

    private bool worldExists(int id)
    {
        if (!File.Exists(Application.persistentDataPath + "/" + "world" + id.ToString()))
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}

[Serializable]
public class worldData
{
   public int ID;
   public string Path;
   public string name;
   public Tile[] tiles;
   public int x, y;
 
   

    public  worldData(int id, string path)
    {
        ID = id;
        Path = path;
    }

    public worldData(int id, string path, string name)
    {
        ID = id;
        Path = path;
        this.name = name;
    }
    public worldData(int id, string path, int x, int y)
    {
        ID = id;
        Path = path;
       
        this.x = x;
        this.y = y;
    }
    public worldData(int id, string path, string name, int x, int y)
    {
        ID = id;
        Path = path;
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public worldData(int id, string path, int x, int y, Tile[] tiles)
    {
        ID = id;
        Path = path;
        this.x = x;
        this.y = y;
        this.tiles = tiles;
    }
    public worldData(int id, string path, string name, int x, int y, Tile[] tiles)
    {
        ID = id;
        Path = path;
        this.name = name;
        this.x = x;
        this.y = y;
        this.tiles = tiles;
    }
}

public class WorldNotFoundException : Exception
{
    public WorldNotFoundException()
    {
        ProjectAurora.Diagnostics.Debug.logError(Message);
    }

    public WorldNotFoundException(string message) : base(message)
    {
    }

    public WorldNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected WorldNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override IDictionary Data => base.Data;

    public override string HelpLink { get => base.HelpLink; set => base.HelpLink = value; }

    public override string Message => "World Was not found";

    public override string Source { get => base.Source; set => base.Source = value; }

    public override string StackTrace => base.StackTrace;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override Exception GetBaseException()
    {
        return base.GetBaseException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}



