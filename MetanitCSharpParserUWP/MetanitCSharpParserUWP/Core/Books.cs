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

        #region CSharp

        private static string BaseName = "/sharp/";

        public static string CSharpBasics     { get => BaseName + "tutorial"; }
        public static string CSharpPatterns   { get => BaseName + "patterns"; }
        public static string CSharpNet        { get => BaseName + "net"; }
        public static string CSharpAlgoritms  { get => BaseName + "algoritm"; }
        public static string CSharpASP        { get => BaseName + "aspnet5"; }
        public static string CSharpAspMvc     { get => BaseName + "mvc5"; }
        public static string CSharpAspNetCore { get => BaseName + "aspnetcore"; }

        #endregion


    }
}
