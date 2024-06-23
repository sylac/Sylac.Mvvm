using Microsoft.CodeAnalysis;

namespace Sylac.MVVM.Generators.Diagnostics
{
    public static class DiagnosticsDescriptors
    {
        public static readonly DiagnosticDescriptor PartialClassDiagnostic = new(
            "SYLAC001",
            "Non-partial class",
            "ConnectWithViewModelAttribute cannot be used on non-partial classes",
            "Usage",
            DiagnosticSeverity.Error,
            true
        );

        public static readonly DiagnosticDescriptor InvalidConstructorDiagnostic = new(
            "SYLAC002",
            "Invalid constructor",
            "ConnectWithViewModelAttribute cannot be used on classes without a constructor that takes the view model as a parameter",
            "Usage",
            DiagnosticSeverity.Error,
            true
        );
    }
}
