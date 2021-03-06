﻿using System.Collections.Generic;

namespace Illallangi.MovieFileNamer.Model
{
    public interface IResult
    {
        string Name { get; set; }
        string Directory { get; set; }
        IDictionary<string, IList<string>> Movies { get; }
        IDictionary<string, IList<string>> PassedMovies { get; }
        IDictionary<string, IList<string>> FailedMovies { get; }
        IEnumerable<string> Errors { get; }
        bool HasErrors { get; }
    }
}