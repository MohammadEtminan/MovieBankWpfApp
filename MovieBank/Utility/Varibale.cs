using System;

namespace MovieBank.Utility
{
    public static class Variable
    {
        public static readonly string ApplicationPath = "pack://application:,,,";
        public static readonly string ImageFullPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\Movie\\";
        public static readonly string DefaultPoster = "/Content/Images/noposter.jpg";
    }
}