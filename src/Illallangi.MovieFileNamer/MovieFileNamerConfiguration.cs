using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer
{
    public interface IDirectory
    {
        string Path { get; }
        string Name { get; }
    }

    public sealed class DirectoryConfigurationElement : ConfigurationElement, IDirectory
    {
        [ConfigurationProperty("Path", IsRequired = true)]
        public string Path
        {
            get { return (String)this["Path"]; }
            set { this["Path"] = value; }
        }

        [ConfigurationProperty("Name")]
        public string Name
        {
            get { return (String)(this["Name"] ?? System.IO.Path.GetFileName(this.Path)); }
            set { this["Name"] = value; }
        }
    }

    public sealed class DirectoryConfigurationElementCollection : ConfigurationElementCollection
    {
        public DirectoryConfigurationElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as DirectoryConfigurationElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        protected
            override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new DirectoryConfigurationElement();
        }

        protected override object GetElementKey(
            System.Configuration.ConfigurationElement element)
        {
            return ((DirectoryConfigurationElement)element).Path;
        }

    }

    public sealed class MovieFileNamerConfiguration : ConfigurationSection, IConfig
    {
        public static MovieFileNamerConfiguration GetConfig()
        {
            var c = (MovieFileNamerConfiguration)ConfigurationManager.GetSection("MovieFileNamerConfiguration");
            if (null == c)
            {
                throw new ConfigurationErrorsException("MovieFileNamerConfiguration element not found");
            }
            return c;
        }

        public IEnumerable<IDirectory> Directories
        {
            get
            {
                foreach (var d in this.DirectoryCollection)
                {
                    yield return (IDirectory)d;
                }
            }
        }
        [System.Configuration.ConfigurationProperty("Directories")]
        public DirectoryConfigurationElementCollection DirectoryCollection
        {
            get
            {
                return (DirectoryConfigurationElementCollection)this["Directories"] ??
                   new DirectoryConfigurationElementCollection();
            }
        }

        [ConfigurationProperty("Template", DefaultValue = "Email.cshtml")]
        public string Template
        {
            get { return (String)this["Template"]; }
            set { this["Template"] = value; }
        }

        [ConfigurationProperty("From", IsRequired = true)]
        public string FromAddress
        {
            get { return (String)this["From"]; }
            set { this["From"] = value; }
        }

        [ConfigurationProperty("To", IsRequired = true)]
        public string ToAddress
        {
            get { return (String)this["To"]; }
            set { this["To"] = value; }
        }

        [ConfigurationProperty("TheMovieDbApiKey", IsRequired = true)]
        public string TheMovieDbApiKey
        {
            get { return (String)this["TheMovieDbApiKey"]; }
            set { this["TheMovieDbApiKey"] = value; }
        }

        [ConfigurationProperty("TheMovieDbApiUri", DefaultValue = @"https://api.themoviedb.org/3/search/movie?api_key={0}&query={1}&year={2}")]
        public string TheMovieDbApiUri
        {
            get { return (String)this["TheMovieDbApiUri"]; }
            set { this["TheMovieDbApiUri"] = value; }
        }

        [ConfigurationProperty("Directory", IsRequired = true)]
        public string Directory
        {
            get { return Environment.ExpandEnvironmentVariables((String)this["Directory"]); }
            set { this["Directory"] = value; }
        }

        [ConfigurationProperty("JsonPath", DefaultValue = @"%temp%\Illallangi.MovieFileNamer.json")]
        public string JsonPath
        {
            get { return Environment.ExpandEnvironmentVariables((String)this["JsonPath"]); }
            set { this["JsonPath"] = value; }
        }

        [ConfigurationProperty("HtmlPath", DefaultValue = @"%temp%\Illallangi.MovieFileNamer.html")]
        public string HtmlPath
        {
            get { return Environment.ExpandEnvironmentVariables((String)this["HtmlPath"]); }
            set { this["HtmlPath"] = value; }
        }

        [ConfigurationProperty("CachePath", DefaultValue = @"%temp%\Illallangi.MovieFileNamer")]
        public string CachePath
        {
            get { return Environment.ExpandEnvironmentVariables((String)this["CachePath"]).CreatePath(); }
            set { this["CachePath"] = value; }
        }

        [ConfigurationProperty("Subject", DefaultValue = @"Errors detected in {0}")]
        public string Subject
        {
            get { return (String)this["Subject"]; }
            set { this["Subject"] = value; }
        }

        [ConfigurationProperty("Interval", DefaultValue = 60)]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }

        [ConfigurationProperty("SmtpPort", DefaultValue = @"25")]
        public int SmtpPort
        {
            get { return (int)this["SmtpPort"]; }
            set { this["SmtpPort"] = value; }
        }

        [ConfigurationProperty("SmtpServer", IsRequired = true)]
        public string SmtpServer
        {
            get
            {
                var result = (String)this["SmtpServer"];
                return result;
            }
            set { this["SmtpServer"] = value; }
        }
    }
}