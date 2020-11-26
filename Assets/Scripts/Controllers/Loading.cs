using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    private bool loadScene = false;

    public int x, y;
    public string sceneName;
    public Text task;
    public Text loadingText;
    public Slider loadingslider;
    public Sprite Grass, stone, tree;


    public static bool NewWorld = true;
    public static int WorldID;

    private AsyncOperation ao;


    void Update()
    {



        // If the player has pressed the space bar and a new scene is not loading yet...

        if (loadScene == false)
        {



            // ...set the loadScene boolean to true to prevent loading a new scene more than once...

            loadScene = true;



            // ...change the instruction text to read "Loading..."

            loadingText.text = "Loading...";

          

            // ...and start a coroutine that will load the desired scene.

            StartCoroutine(LoadNewScene());



        }



        // If the new scene has started loading...

        if (loadScene == true)
        {

            

           
            
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.

            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

            task.text = ProjectAurora.Diagnostics.Debug.Task;
            loadingslider.value = ao.progress;

        }



    }





    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.

    IEnumerator LoadNewScene()
    {




        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        ao = SceneManager.LoadSceneAsync(sceneName);


        

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.

        while (!ao.isDone)
        {
            
            yield return null;

        }
            
        



    }

}
