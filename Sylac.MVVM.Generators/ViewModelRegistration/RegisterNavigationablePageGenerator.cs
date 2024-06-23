using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Sylac.MVVM.Generators.Diagnostics;
using System.Collections.Immutable;
using System.Text;

namespace Sylac.MVVM.Generators.ViewModelRegistration;

[Generator(LanguageNames.CSharp)]
public class RegisterNavigationablePageGenerator : IIncrementalGenerator
{
    private const string BASE_LIB_NAMESPACE = "Sylac.MVVM";
    private const string CONNECT_WITH_VM_ATTRIBUTE = $"{BASE_LIB_NAMESPACE}.Navigation.ConnectWithViewModelAttribute`2";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                CONNECT_WITH_VM_ATTRIBUTE,
                IsSyntaxTargetForGeneration,
                static (context, _) => context
            );

        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());


        context.RegisterSourceOutput(compilationAndClasses, Execute);
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node, CancellationToken _)
    {
        return node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };
    }

    static void Execute(SourceProductionContext context, (Compilation Compilation, ImmutableArray<GeneratorAttributeSyntaxContext> SyntaxContexts) source)
    {
        if (source.SyntaxContexts.IsDefaultOrEmpty)
            return;

        List<string> viewModelsToRegister = [];
        var distinctSyntaxContexts = source.SyntaxContexts.Distinct();
        foreach (var syntaxContext in distinctSyntaxContexts)
        {
            if (syntaxContext.TargetNode is not ClassDeclarationSyntax classDeclarationSyntax ||
                syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.TargetNode) is not ISymbol classSymbol)
                continue;

            // Check if the class is partial
            if (!classDeclarationSyntax.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticsDescriptors.PartialClassDiagnostic, syntaxContext.TargetNode.GetLocation()));
                continue;
            }

            foreach (var attributeData in syntaxContext.Attributes)
            {
                if (attributeData is null ||
                    attributeData.AttributeClass is null ||
                    !attributeData.AttributeClass.IsGenericType ||
                    attributeData.AttributeClass.TypeArguments.Length != 2 ||
                    classDeclarationSyntax.Members.FirstOrDefault(m => m.IsKind(SyntaxKind.ConstructorDeclaration)) is not ConstructorDeclarationSyntax constructor)
                    continue;

                var viewModelType = attributeData.AttributeClass.TypeArguments[0];
                var paramsType = attributeData.AttributeClass.TypeArguments[1];

                // Check if class constructor has a parameter of the view model type
                var hasValidParameter = constructor.ParameterList.Parameters
                    .Any(param =>
                    {
                        if (param.Type is null)
                        {
                            return false;
                        }

                        var paramType = syntaxContext.SemanticModel.GetTypeInfo(param.Type).Type;
                        return SymbolEqualityComparer.Default.Equals(paramType, viewModelType);
                    });

                if (!hasValidParameter)
                {

                    context.ReportDiagnostic(Diagnostic.Create(DiagnosticsDescriptors.InvalidConstructorDiagnostic, constructor.GetLocation()));
                    continue;
                }

                viewModelsToRegister.Add(GenerateViewModelRegistration(classSymbol, viewModelType, paramsType));
            }
        }

        if (source.Compilation.AssemblyName is null)
            return;

        context.AddSource($"App.xaml.sg.cs", SourceText.From(GenerateClassCode(source.Compilation.AssemblyName, viewModelsToRegister), Encoding.UTF8));
    }

    static string GenerateClassCode(string assemblyName, IEnumerable<string> vmToRegister)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"namespace {assemblyName}");
        sb.AppendLine("{");
        sb.AppendLine($"    public partial class App");
        sb.AppendLine("    {");
        sb.AppendLine($"        private static void RegisterRoutes({BASE_LIB_NAMESPACE}.Navigation.Abstractions.INavigationService navigationService)");
        sb.AppendLine("        {");
        foreach (var vms in vmToRegister.Distinct())
        {
            sb.AppendLine($"            {vms};");
        }
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }

    static string GenerateViewModelRegistration(ISymbol pageName, ITypeSymbol viewModelType, ITypeSymbol paramsType)
    {
        return $"navigationService.RegisterNavigationView<{pageName}, {viewModelType}, {paramsType}>();";
    }
}
