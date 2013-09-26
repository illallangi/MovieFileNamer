using System.Collections.Generic;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer
{
    public interface IResultSource
    {
        ResultCollection Get();
    }

    public interface IHtmlResultSource
    {
        HtmlResultCollection Get();
    }
}