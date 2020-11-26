using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.InteropServices;





/*Suprisingly this is the best system i have ever writin.
 * It works fine dosent give a Error and has fixes
 * 90% of the Errors
 */
namespace System.SAL
{

    [ComVisible(true)]
    /// <summary>
    /// The save system of the game
    /// </summary>
    public class saveSystem : MonoBehaviour
    {
        /*This is the most efficient way to save files/data in game for now
         */

        

        /// <summary>
        /// Saves the data to the chosen path
        /// </summary>
        /// <param name="Filename">The chosen file name</param>
        /// <param name="filepath">The custom file path</param>
        /// <param name="filetype">The custom file type</param>
        /// <param name="replacable">Is the file Replacable by a new Version of the file</param>
        /// <param name="data">The data that need's to be saved</param>
        public static void save(string Filename, string filepath, string filetype,bool replacable, object data)
        {
            if(replacable == true)
            {
                if (File.Exists(filepath + "/" + Filename + "." + filetype))
                {
                    File.Delete(filepath + "/" + Filename + "." + filetype);
                }
            }
            

            FileStream fs = new FileStream(filepath + "/" + Filename + "." + filetype,FileMode.OpenOrCreate);

            BinaryFormatter formater = new BinaryFormatter();

            
            try
            {
                formater.Serialize(fs, data);
            }
            catch (Exception)
            {
               
              
            }finally
            {
                fs.Close();
            }

        }

        /// <summary>
        /// Create's a custom empty file 
        /// </summary>
        /// <param name="Filename">The file name</param>
        /// <param name="filepath">Where the file should be located</param>
        /// <param name="filetype">What should the file type be</param>
        public static void CreatEmptyFile(string Filename, string filepath, string filetype)
        {
            FileStream fs = new FileStream(filepath + "/" + Filename + "." + filetype, FileMode.OpenOrCreate);

            BinaryFormatter formater = new BinaryFormatter();

            try
            {
                formater.Serialize(fs, "");
            }
            catch (SerializationException)
            {
           
            }
            finally
            {
                fs.Close();
            }
        }

        

        #region crash save
        public static void CrashSave(Exception e)
        {
            

            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/data" + "/Crashes" + "/" + "Crash On " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year +  "    " + DateTime.UtcNow.ToLocalTime().Hour + "_" + DateTime.UtcNow.ToLocalTime().Minute + "_" + DateTime.UtcNow.ToLocalTime().Second + ".txt", false);
            writer.WriteLine(e);
            writer.WriteLine("Get help about this error at: " + e.HelpLink);
            writer.WriteLine(e.StackTrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("User: " + Environment.UserName);
            writer.WriteLine("Machine name: " + Environment.MachineName);
            writer.WriteLine("Target frame rate: " + Application.targetFrameRate);
            writer.WriteLine("OS version: " + Environment.OSVersion);
            writer.WriteLine("ENV Version: " + Environment.Version);
            writer.WriteLine("Game Version: " + Application.version);
            writer.WriteLine("FPS: " + Environment.TickCount);
            writer.WriteLine("OS: " + Application.platform);
            writer.WriteLine();
            writer.WriteLine("Date of creation: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + "    " + DateTime.UtcNow.ToLocalTime().Hour + ":" + DateTime.UtcNow.ToLocalTime().Minute + ":" + DateTime.UtcNow.ToLocalTime().Second);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Stack trace: ");
            writer.WriteLine();
            writer.Write(Environment.StackTrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Executed/opened files: ");
            writer.WriteLine();
            writer.Write(Environment.CommandLine); 
            writer.Close();
        }

        public static void CrashSave(Exception e, string Stacktrace)
        {

           


            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/data" + "/Crashes" + "/" + "Crash On " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_"+ DateTime.UtcNow.ToLocalTime().Hour + "_" + DateTime.UtcNow.ToLocalTime().Minute + "_" + DateTime.UtcNow.ToLocalTime().Second + ".txt", false);
            writer.WriteLine(e);
            writer.WriteLine(e.Message);
            writer.WriteLine(Stacktrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("User: " + Environment.UserName);
            writer.WriteLine("Machine name: " + Environment.MachineName);
            writer.WriteLine("Target frame rate: " + Application.targetFrameRate);
            writer.WriteLine("OS version: " + Environment.OSVersion);
            writer.WriteLine("ENV Version: " + Environment.Version);
            writer.WriteLine("Game Version: " + Application.version);
            writer.WriteLine("FPS: " + Environment.TickCount);
            writer.WriteLine("OS: " + Application.platform);
            writer.WriteLine();
            writer.WriteLine("Date of creation: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + "    " + DateTime.UtcNow.ToLocalTime().Hour + ":" + DateTime.UtcNow.ToLocalTime().Minute + ":" + DateTime.UtcNow.ToLocalTime().Second);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Stack trace: ");
            writer.WriteLine();
            writer.WriteLine(Environment.StackTrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Executed/opened files: ");
            writer.WriteLine();
            writer.WriteLine(Environment.CommandLine);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Executed/opened files: ");
            writer.WriteLine();
            writer.WriteLine(Debug.unityLogger.ToString());
            writer.Close();
        }

        public static void CrashSave(Exception e, string Stacktrace , string Message)
        {
            


            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/data" + "/Crashes" + "/" + "Crash On " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "  " + DateTime.UtcNow.ToLocalTime().Hour + "_" + DateTime.UtcNow.ToLocalTime().Minute + "_" + DateTime.UtcNow.ToLocalTime().Second +  ".txt", false);
            writer.WriteLine(e);
            writer.WriteLine(Message);
            writer.WriteLine(Stacktrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("User: " + Environment.UserName);
            writer.WriteLine("Machine name: " + Environment.MachineName);
            writer.WriteLine("Target frame rate: " + Application.targetFrameRate);
            writer.WriteLine("OS version: " + Environment.OSVersion);
            writer.WriteLine("ENV Version: " + Environment.Version);
            writer.WriteLine("Game Version: " + Application.version);
            writer.WriteLine("FPS: " + Environment.TickCount);
            writer.WriteLine("OS: " + Application.platform);
            writer.WriteLine();
            writer.WriteLine("Date of creation: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + "    " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Stack trace: ");
            writer.WriteLine();
            writer.Write(Environment.StackTrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Executed/opened files: ");
            writer.WriteLine();
            writer.Write(Environment.CommandLine); 
            writer.Close();
        }

        public static void CrashSave(object e, string ehelpLink)
        {
            


            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/data" + "/Crashes" + "/" + "Crash On " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "  "  + DateTime.UtcNow.ToLocalTime().Hour + "_" + DateTime.UtcNow.ToLocalTime().Minute + "_" + DateTime.UtcNow.ToLocalTime().Second + ".txt", false);
            writer.WriteLine(e);
            writer.WriteLine("Get help about this error at: " + ehelpLink);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("User: " + Environment.UserName);
            writer.WriteLine("Machine name: " + Environment.MachineName);
            writer.WriteLine("Target frame rate: " + Application.targetFrameRate);
            writer.WriteLine("OS version: " + Environment.OSVersion);
            writer.WriteLine("ENV Version: " + Environment.Version);
            writer.WriteLine("Game Version: " + Application.version);
            writer.WriteLine("FPS: " + Environment.TickCount);
            writer.WriteLine("OS: " + Application.platform);
            writer.WriteLine();
            writer.WriteLine("Date of creation: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + "    " + DateTime.UtcNow.ToLocalTime().Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Stack trace: ");
            writer.WriteLine();
            writer.WriteLine(Environment.StackTrace);
            writer.WriteLine("---------------------------");
            writer.WriteLine();
            writer.WriteLine("Executed/opened files: ");
            writer.WriteLine();
            writer.WriteLine(Environment.CommandLine); 
            writer.Close();
        }
        #endregion

    }

    [ComVisible(true)]
    /// <summary>
    /// The load system of the game
    /// </summary>
    public class loadSystem : MonoBehaviour
    {
        /// <summary>
        /// this loads the saved file
        /// </summary>
        /// <typeparam name="t">data type</typeparam>
        /// <param name="Filename">The Files name</param>
        /// <param name="filepath">Where the wanted file is saved</param>
        /// <param name="filetype">what type of file this is</param>
        /// <param name="data">The data that needs to be loeaded</param>
        /// <returns></returns>
        public static t load<t>(string Filename, string filepath, string filetype)
        {
           t data = default(t);

            FileStream fs = new FileStream(filepath + "/" + Filename + "." + filetype, FileMode.Open);

            BinaryFormatter formater = new BinaryFormatter();
            try
            {
                

                data = (t)formater.Deserialize(fs);
                return data;
            }
            catch (SerializationException)
            {

               throw new FileLoadException();
               
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// this loads the saved file
        /// </summary>
        /// <typeparam name="t">data type</typeparam>
        /// <param name="Filename">The Files name</param>
        /// <param name="filepath">Where the wanted file is saved</param>
        /// <param name="filetype">what type of file this is</param>
        /// <param name="data">The data that needs to be loeaded</param>
        /// <returns></returns>
        public static t load<t>(string filepath)
        {
            t data = default(t);

            FileStream fs = new FileStream(filepath , FileMode.OpenOrCreate);

            BinaryFormatter formater = new BinaryFormatter();
            try
            {


                data = (t)formater.Deserialize(fs);
                return data;
            }
            catch (SerializationException)
            {

                throw new FileLoadException();

            }
            finally
            {
                fs.Close();
            }
        }
    }

    [ComVisible(true)]
    /// <summary>
    /// Jumping jet Io Utility system
    /// </summary>
    public class Utilsystem : MonoBehaviour
    {
        /// <summary>
        /// this finds the wanted file
        /// </summary>
        /// <param name="Filename">The name of the file</param>
        /// <param name="filepath">Where the file is located</param>
        /// <param name="filetype">What is the file type</param>
        /// <returns>If the file is found we return the file</returns>
        public static string Find(string Filename, string filepath, string filetype)
        {
            string path = filepath + "/" + Filename + "." + filetype;

            if (File.Exists(filepath + "/" + Filename + "." + filetype))
            {
                string file = path;

                return file;
            }
            else
            {
                System.Diagnostics.Debug.Fail("File does not exist");
                return null;
            }
                

        }

        /// <summary>
        /// This checks to see if the file exists
        /// </summary>
        /// <param name="Filename">The File name</param>
        /// <param name="filepath">Where the file is located</param>
        /// <param name="filetype">What is the type of the file</param>
        /// <returns>Returns true if the file exists</returns>
        public static bool exists(string Filename, string filepath, string filetype)
        {

            if (File.Exists(filepath + "/" + Filename + "." + filetype))
            { 

                return true;
            }
            else
            {
                System.Diagnostics.Debug.Fail("File does not exist");
                return false;
            }
        }

        /// <summary>
        /// This checks to see if the file exists
        /// </summary>
        /// <param name="path">The bath of the file/Foldor</param>
        /// <returns>Returns true if the file exists</returns>
        public static bool exists(string path)
        {

            if (File.Exists(path))
            {

                return true;
            }
            else
            {
                System.Diagnostics.Debug.Fail("File does not exist");
                return false;
            }
        }

        /// <summary>
        /// The Delets the file 
        /// </summary>
        /// <param name="Filename">What is the Targeted file name</param>
        /// <param name="filepath">Where is the Targeted file</param>
        /// <param name="filetype">What is the type of the Targeted file</param>
        public static void Remove(string Filename, string filepath, string filetype)
        {
            if (File.Exists(filepath + "/" + Filename + "." + filetype))
            {
                File.Delete(filepath + "/" + Filename + "." + filetype);
            }
            else
            {
                System.Diagnostics.Debug.Fail("This file does not exists");
              
            }

        }

        /// <summary>
        /// Creates a Directory/Foldor based on the bath and name given
        /// </summary>
        /// <param name="Filename">Foldor name</param>
        /// <param name="filepath">the path to the directory/Foldor</param>
        /// <param name="dfp">Where the path is located</param>
        public static void CreateDirectory(string Filename, string filepath, DataFoldorPlace dfp)
        {
            string path = filepath + "/" + Filename;

            if(dfp == DataFoldorPlace.Pictures)
            {
                if(exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + path);
                }
            }

            if (dfp == DataFoldorPlace.Ducumants)
            {
                if (exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + path);
                }
            }

            if (dfp == DataFoldorPlace.Videos)
            {
                if (exists(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + path);
                }
            }


            if (dfp == DataFoldorPlace.PDP)
            {
                if(exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
            }

        }

        /// <summary>
        /// Creates a Directory/Foldor based on the bath and name given
        /// </summary>
        /// <param name="Filename">Foldor name</param>
        /// <param name="filepath">the path to the directory/Foldor</param>
        public static void CreateDirectory(string Filename, string filepath)
        {
            string path = filepath + "/" + Filename;

                    Directory.CreateDirectory(path);

        }

        /// <summary>
        /// Creates a Directory/Foldor based on the bath and name given
        /// </summary>
        /// <param name="Filename">Foldor name</param>
        /// <param name="dfp">Where the path is located</param>
        public static void CreateDirectory(string Filename, DataFoldorPlace dfp)
        {
            string path = Filename;

            if (dfp == DataFoldorPlace.Pictures)
            {
                if (exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + path);
                }
            }

            if (dfp == DataFoldorPlace.Ducumants)
            {
                if (exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + path);
                }
            }

            if (dfp == DataFoldorPlace.Videos)
            {
                if (exists(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + path);
                }
            }


          

        }

        public enum DataFoldorPlace
        {
            Ducumants = 0,
            Pictures = 1,
            Videos = 2,
            PDP = 3
        }
       
    }


   


}




