using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Assets.SourceCode.Strategies;
using CSharpCompiler;
using Microsoft.CSharp;
using UnityEngine;

class ScriptInjector : MonoBehaviour {

    public AbstractBoxingStrategy RedStrategy { get; private set; }
    public AbstractBoxingStrategy BlueStrategy { get; private set; }

    private void Awake() {
        string redBoxerClass = "Player1Strategy";
        string blueBoxerClass = "Player2Strategy";
        Compile(redBoxerClass, blueBoxerClass);
    }

    private string readme = "";            // Displayed on the GUI
    private string compilerErrorMessages = "";        // Displayed on the GUI

    private void Compile(string redBoxerClass, string blueBoxerClass) {
        try {
            compilerErrorMessages = "";  // clear any previous messages

            // ********** Create an instance of the C# compiler   
            //CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CSharpCompiler.CodeCompiler codeCompiler = new CSharpCompiler.CodeCompiler();

            // ********** add compiler parameters
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.CompilerOptions = "/target:library /optimize /warn:0";
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.IncludeDebugInformation = true;
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Core.dll");
            compilerParams.ReferencedAssemblies.Add(Assembly.GetCallingAssembly().Location);

            string redBoxerSource = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, redBoxerClass) + ".cs");
            string blueBoxerSource = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, blueBoxerClass) + ".cs");

            // ********** actually compile the code  ??????? THIS LINE WORKS IN UNITY EDITOR --- BUT NOT IN BUILD ??????????
            CompilerResults resultsRedBoxer = codeCompiler.CompileAssemblyFromSource(compilerParams, redBoxerSource);
            CompilerResults resultsBlueBoxer = codeCompiler.CompileAssemblyFromSource(compilerParams, blueBoxerSource);

            // ********** Do we have any compiler errors
            if (resultsRedBoxer.Errors.Count > 0 || resultsBlueBoxer.Errors.Count > 0) {
                foreach (CompilerError error in resultsRedBoxer.Errors) {
                    compilerErrorMessages = compilerErrorMessages + error.ErrorText + '\n';
                }
            }
            else {
                // ********** get a hold of the actual assembly that was generated
                Assembly generatedRedAssembly = resultsRedBoxer.CompiledAssembly;
                Assembly generatedBlueAssembly = resultsBlueBoxer.CompiledAssembly;

                if (generatedRedAssembly != null && generatedBlueAssembly != null) {
                    // get type of class Calculator from just loaded assembly
                    Type redClassType = generatedRedAssembly.GetType(redBoxerClass);
                    Type blueClassType = generatedBlueAssembly.GetType(blueBoxerClass);

                    RedStrategy = (AbstractBoxingStrategy)Activator.CreateInstance(redClassType);
                    BlueStrategy = (AbstractBoxingStrategy)Activator.CreateInstance(blueClassType);

                    // Say success!
                    compilerErrorMessages = "Success!";
                }
            }
        }
        catch (Exception o) {
            Debug.LogError("" + o.Message + "\n" + o.Source + "\n" + o.StackTrace + "\n");
            throw o;
        }

        Debug.Log(compilerErrorMessages);
    }
}