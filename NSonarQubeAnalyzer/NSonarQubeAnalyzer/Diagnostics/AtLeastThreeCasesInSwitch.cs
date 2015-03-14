using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Threading;

namespace NSonarQubeAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AtLeastThreeCasesInSwitch : DiagnosticAnalyzer
    {
        internal const string DiagnosticId = "S1301";
        internal const string Description = "\"Select Case\" statements should have at least 3 \"Case\" clauses";
        internal const string MessageFormat = "Replace this \"Select Case\" statement with \"if\" statements to increase readability.";
        internal const string Category = "SonarQube";
        internal const DiagnosticSeverity Severity = DiagnosticSeverity.Warning;

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Description, MessageFormat, Category, Severity, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
         
            context.RegisterSyntaxNodeAction(
                c =>
                {
                    SelectStatementSyntax switchNode = (SelectStatementSyntax)c.Node;
                    if (!HasAtLeastThreeLabels(switchNode))
                    {
                        c.ReportDiagnostic(Diagnostic.Create(Rule, c.Node.GetLocation()));
                    }
                },
                SyntaxKind.SelectStatement);
        }

        private static bool HasAtLeastThreeLabels(SelectStatementSyntax node)
        {
            // TODO: How do I check the number of case statements in vb?   
            return false;
            //return node.Sections.Sum(section => section.Labels.Count) >= 3;
        }
    }
}
