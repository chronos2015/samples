using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeDom
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // static void Main(string[] args) {
            //     Console.WriteLine("hello,world!");
            // }
            var methodDesc = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Main")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.StaticKeyword))
                .AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier("args")).WithType(SyntaxFactory.ParseTypeName(typeof(string[]).FullName)))
                .AddBodyStatements(SyntaxFactory.ParseStatement("Console.WriteLine(\"hello,world!\");"));

            // public class Program : Object
            var classDesc = SyntaxFactory
                .ClassDeclaration("Program")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Object")))
                .AddMembers(methodDesc);

            // namespace sample {}
            var nameapace = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName(" sample"))
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
                .AddMembers(classDesc);

            var code = nameapace.NormalizeWhitespace().ToFullString();

            // Output new code to the console.
            Console.WriteLine(code);
        }
    }
}
