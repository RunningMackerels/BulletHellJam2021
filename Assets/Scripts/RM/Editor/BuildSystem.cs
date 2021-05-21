using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace RM.Editor
{
    /// <summary>
    /// Editor tool to make builds and zip them
    /// </summary>
    public class BuildSystem : EditorWindow
    {
        [Serializable]
        private class AvailableTarget
        {
            public BuildTarget Target;
            [HideInInspector]
            public bool Available;

            public void BuildIt(string buildFolder, bool createZip, bool copyToDocs)
            {
                if (!Available || EditorUserBuildSettings.activeBuildTarget != Target)
                {
                    return;
                }
                
                EditorUserBuildSettings.SwitchActiveBuildTargetAsync(ConvertBuildTarget(Target), Target);
            
                string targetBuildFolder = Application.productName + "_" + Target.ToString();
                string targetBuildFolderPath = Path.Combine(".", buildFolder, targetBuildFolder);
                
                string[] scenes = new string[EditorBuildSettings.scenes.Length];
                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {
                    scenes[i] = EditorBuildSettings.scenes[i].path;
                }
                
                BuildPlayerOptions options = new BuildPlayerOptions();
                options.target = Target;
                options.scenes = scenes;

                 
                
                if (Target == BuildTarget.WebGL)
                {
                    options.locationPathName = targetBuildFolderPath;
                }
                else
                {
                    options.locationPathName =
                        Path.Combine(targetBuildFolderPath, Application.productName);
                }

                options.options = BuildOptions.ShowBuiltPlayer;
                
                BuildReport report = BuildPipeline.BuildPlayer(options);

                if (report.summary.result == BuildResult.Succeeded)
                {
                    Debug.Log("Build successful for target " +Target.ToString() + " in folder " + targetBuildFolder);
                }
                else
                {
                    Debug.LogError("Build fail for target " + Target.ToString());
                }

                if (createZip)
                {
                    CompressDirectory(targetBuildFolderPath, Path.Combine(".", buildFolder, targetBuildFolder + ".zip"));
                    Debug.Log("Build successfully zipped to " + targetBuildFolder + ".zip");
                }

                if (copyToDocs)
                {
                    string docsFolder = Application.dataPath + "/../docs";
                    FileUtil.DeleteFileOrDirectory(docsFolder);

                    FileUtil.CopyFileOrDirectory(targetBuildFolderPath, docsFolder);
                }
            }

            private void CompressDirectory(string inDir, string outputFile)
            {
                string[] files = Directory.GetFiles(inDir, "*.*", SearchOption.AllDirectories);
                int dirLen = inDir[inDir.Length - 1] == Path.DirectorySeparatorChar ? inDir.Length : inDir.Length + 1;

                using FileStream outFile = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None);
                using GZipStream str = new GZipStream(outFile, CompressionMode.Compress);
                foreach (string filePath in files)
                {
                    string relativePath = filePath.Substring(dirLen);
                    CompressFile(inDir, relativePath, str);
                }
            }
            
            private void CompressFile(string dir, string relativePath, GZipStream zipStream)
            {
                //Compress file name
                char[] chars = relativePath.ToCharArray();
                zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
                foreach (char c in chars)
                    zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

                //Compress file content
                byte[] bytes = File.ReadAllBytes(Path.Combine(dir, relativePath));
                zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
                zipStream.Write(bytes, 0, bytes.Length);
            }
            
            public void SwitchTo()
            {
                if (!Available)
                {
                    return;
                }
                
                EditorUserBuildSettings.SwitchActiveBuildTargetAsync(ConvertBuildTarget(Target), Target);
            }
        }
        
        private string _buildFolder = "Builds";

        private bool _createZip = true;

        private bool _copyToDocs = false;
        
        private List<AvailableTarget> _targets = new List<AvailableTarget>();

         
        
        [MenuItem("RM/Build System")]
        private static void CreateWizard()
        {
            BuildSystem window = (BuildSystem)EditorWindow.GetWindow(typeof(BuildSystem));
            window.Show();
        }

        private void OnEnable()
        {
            _targets = new List<AvailableTarget>();
            _targets.Add(new AvailableTarget()
            {
                Available = IsPlatformAvailable(BuildTarget.StandaloneWindows64),
                Target = BuildTarget.StandaloneWindows64,
            });
            _targets.Add(new AvailableTarget()
            {
                Available = IsPlatformAvailable(BuildTarget.StandaloneOSX),
                Target = BuildTarget.StandaloneOSX
            });
            _targets.Add(new AvailableTarget()
            {
                Available = IsPlatformAvailable(BuildTarget.WebGL),
                Target = BuildTarget.WebGL
            });
            
            foreach (AvailableTarget target in _targets)
            {
                target.Available = IsPlatformAvailable(target.Target);
            }
        }

        private void OnGUI()
        {
            _buildFolder = EditorGUILayout.TextField("Build Folder", _buildFolder);

            AvailableTarget activeTarget = null;
            
            foreach (AvailableTarget target in _targets)
            {
                if (!target.Available)
                {
                    continue;
                }

                if (EditorUserBuildSettings.activeBuildTarget != target.Target)
                {
                    if (GUILayout.Button("Switch to " + target.Target.ToString()))
                    {
                        target.SwitchTo();
                    }
                }
                else
                {
                    activeTarget = target;
                }
            }

            if (activeTarget == null)
            {
                return;
            }
            
            _createZip = EditorGUILayout.Toggle("Create Zip", _createZip);
            _copyToDocs = EditorGUILayout.Toggle("Copy to /Docs", _copyToDocs);

            
            if (GUILayout.Button("Build " + activeTarget.Target.ToString()))
            {
                activeTarget.BuildIt(_buildFolder, _createZip, _copyToDocs);
            }
        }

        private bool IsPlatformAvailable(BuildTarget target)
        {
            Type moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
            MethodInfo isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            MethodInfo getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
     
            return (bool)isPlatformSupportLoaded.Invoke(null,new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {target})});
        }
        
        static BuildTargetGroup ConvertBuildTarget(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                case BuildTarget.iOS:
                    return BuildTargetGroup.iOS;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneLinux64:
                    return BuildTargetGroup.Standalone;
                case BuildTarget.Android:
                    return BuildTargetGroup.Android;
                case BuildTarget.WebGL:
                    return BuildTargetGroup.WebGL;
                case BuildTarget.WSAPlayer:
                    return BuildTargetGroup.WSA;
                case BuildTarget.PS4:
                    return BuildTargetGroup.PS4;
                case BuildTarget.XboxOne:
                    return BuildTargetGroup.XboxOne;
                case BuildTarget.tvOS:
                    return BuildTargetGroup.tvOS;
                case BuildTarget.Switch:
                    return BuildTargetGroup.Switch;
                case BuildTarget.NoTarget:
                default:
                    Debug.LogError("No BuildTarget found for " + buildTarget.ToString());
                    return BuildTargetGroup.Unknown;
            }
        }
    }
}