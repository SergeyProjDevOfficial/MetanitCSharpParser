using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitCSharpParserUWP.Core
{
    class Books
    {
        protected string MainPage { get => "https://metanit.com"; }

        public class CSharp
        {
            private static string BaseName = "/sharp/";

            public static string Basics     { get => BaseName + "tutorial"; }
            public static string Patterns   { get => BaseName + "patterns"; }
            public static string Net        { get => BaseName + "net"; }
            public static string Algoritms  { get => BaseName + "algoritm"; }
            public static string ASP        { get => BaseName + "aspnet5"; }
            public static string AspMvc     { get => BaseName + "mvc5"; }
            public static string AspNetCore { get => BaseName + "aspnetcore"; }
        }
    }
}
