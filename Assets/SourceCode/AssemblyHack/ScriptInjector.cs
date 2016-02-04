using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Assets.SourceCode.Strategies;
using Microsoft.CSharp;
using UnityEngine;

class ScriptInjector : MonoBehaviour {

    private void Awake() {
        Compile();
    }

    private string scriptText =
@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
    class InjectionStrategy : AbstractBoxingStrategy {
        public override void Act() {
            Do.ChangeStance(Boxer.Stance.BLOCKING);
        }
    }

     ";

    private string readme = "";            // Displayed on the GUI
    private string compilerErrorMessages = "";        // Displayed on the GUI

    private Assembly generatedAssembly;                    // Compiled code is called an "Assembly"
    public AbstractBoxingStrategy myScript_Instance = null;            // These two variables are used run the compiled code.

    private void Compile() {
        try {
            compilerErrorMessages = "";  // clear any previous messages

            // ********** Create an instance of the C# compiler   
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            // ********** add compiler parameters
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.CompilerOptions = "/target:library /optimize /warn:0";
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.IncludeDebugInformation = true;
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Core.dll");
            compilerParams.ReferencedAssemblies.Add(@"C:\Users\Kru\Unity\code-boxing\obj\Debug\Assembly-CSharp.dll");

            // ********** actually compile the code  ??????? THIS LINE WORKS IN UNITY EDITOR --- BUT NOT IN BUILD ??????????
            CompilerResults results = codeProvider.CompileAssemblyFromSource(compilerParams, scriptText);

            // ********** Do we have any compiler errors
            if (results.Errors.Count > 0) {
                foreach (CompilerError error in results.Errors)
                    compilerErrorMessages = compilerErrorMessages + error.ErrorText + '\n';
            }
            else {
                // ********** get a hold of the actual assembly that was generated
                generatedAssembly = results.CompiledAssembly;

                if (generatedAssembly != null) {
                    // get type of class Calculator from just loaded assembly
                    myScript_Type = generatedAssembly.GetType("InjectionStrategy");
                    Debug.Log(myScript_Type);
                    // create instance of class MyScript
                    myScript_Instance = (AbstractBoxingStrategy)Activator.CreateInstance(myScript_Type);

                    // Say success!
                    compilerErrorMessages = "Success!";
                }
            }
        }
        catch (Exception o) {
            Debug.LogError("" + o.Message + "\n" + o.Source + "\n" + o.StackTrace + "\n");
        }

        Debug.Log(compilerErrorMessages);
    }


    public Type myScript_Type { get; set; }
}