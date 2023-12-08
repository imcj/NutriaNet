using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.Loader;

namespace NutriaNet.Data.Code;

public class Compiler
{
    protected AppAssemblies assemblies = new();

    protected string name;

    public Compiler(string name)
    {
        this.name = name; 
    }

    public Stream Compile(string code)
    {
        

        var compilation = CSharpCompilation.Create(
            name,
            new [] { CSharpSyntaxTree.ParseText(code) },
            references: GetReferences(),
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

        var stream = new MemoryStream();
        var compiled = compilation.Emit(stream);

        if (!compiled.Success) throw new Exception();

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    protected IEnumerable<MetadataReference> GetReferences()
    {
        var references = new List<MetadataReference>()
        {
            GetReference("mscorlib"),
            GetReference("System"),
            GetReference("System.Runtime"),
            GetReference("System.Core"),
            GetReference("System.Private.CoreLib"),
            GetReference("Microsoft.EntityFrameworkCore"),
            GetReference("NutriaNet.Data"),
        };

        return references;
    }

    protected MetadataReference GetReference(string name)
    {
        return MetadataReference.CreateFromFile(assemblies.Get(name).Location);
    }

    public AssemblyLoadContext MakeAssemblyLoadContext(Stream stream)
    {
        var context = new AssemblyLoadContext(name);
        context.LoadFromStream(stream);
        return context;
    }
}
