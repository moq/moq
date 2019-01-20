﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.VisualBasic.SyntaxFactory;

namespace Stunts.Processors
{
    /// <summary>
    /// Adds the <c>auto-generated</c> file header that flags the 
    /// document as a generated one.
    /// </summary>
    public class VisualBasicFileHeader : IDocumentProcessor
    {
        const string header = @"'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

";

        /// <summary>
        /// Applies to <see cref="LanguageNames.CSharp"/> only.
        /// </summary>
        public string[] Languages { get; } = new[] { LanguageNames.VisualBasic };

        /// <summary>
        /// Runs in the final phase of codegen, <see cref="ProcessorPhase.Fixup"/>.
        /// </summary>
        public ProcessorPhase Phase => ProcessorPhase.Fixup;

        /// <summary>
        /// Adds the <c>auto-generated</c> file header to the document.
        /// </summary>
        public async Task<Document> ProcessAsync(Document document, CancellationToken cancellationToken = default)
        {
            var syntax = await document.GetSyntaxRootAsync(cancellationToken);

            return document.WithSyntaxRoot(syntax.WithLeadingTrivia(CommentTrivia(header)));
        }
    }
}
