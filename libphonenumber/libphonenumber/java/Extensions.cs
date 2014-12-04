using java.io;
using com.google.i18n.phonenumbers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

using String = java.lang.String;
using StringBuilder = java.lang.StringBuilder;
using Math = java.lang.Math;
using IOException = java.io.IOException;
using NullPointerException = java.lang.NullPointerException;
using System.Reflection;
using java.lang;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]

namespace JavaPort
{
    public static class Extensions
    {
        /// <summary>
        /// Detects if we are running inside a unit test.
        /// </summary>
        public static class UnitTestDetector
        {
            static UnitTestDetector()
            {
                string testAssemblyName = "Microsoft.VisualStudio.QualityTools.UnitTestFramework";
                UnitTestDetector.IsInUnitTest = AppDomain.CurrentDomain.GetAssemblies()
                    .Any(a => a.FullName.StartsWith(testAssemblyName));
            }

            public static bool IsInUnitTest { get; private set; }
        }


        internal static InputStream getResourceAsStream(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullpath = "libphonenumber" + path.Replace('/', '.');

            byte[] buffer;
            using (Stream stream = assembly.GetManifestResourceStream(fullpath))
            {
                if (stream == null)
                {
                    if (UnitTestDetector.IsInUnitTest)
                    {
                        assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.FullName.StartsWith("Test,"));
                        fullpath = "Test" + path.Replace('/', '.');
                        var teswt = assembly.GetManifestResourceNames();
                        using (Stream test = assembly.GetManifestResourceStream(fullpath))
                        {
                            if (test == null)
                                throw new RuntimeException(path, new IOException());

                            buffer = new byte[test.Length];
                            test.Read(buffer, 0, (int)test.Length);
                        }
                    }
                    else throw new RuntimeException(path, new IOException());
                }
                else
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                }
            }
            return new ByteArrayInputStream(buffer);
        }

        public static T[] toArray<T>(this IEnumerable<T> value, T[] dest)
        {
            int index = 0;
            foreach (var item in value)
            {
                dest[index++] = item;
            }
            return dest;
        }

        public static T[] toArray<T>(this IEnumerable<T> value)
        {
            return value.ToArray();
        }

        internal static int hashCode(this Phonenumber.PhoneNumber.CountryCodeSource me)
        {
            return me.GetHashCode();
        }

        internal static bool equals(this Phonemetadata.PhoneMetadata me, Phonemetadata.PhoneMetadata other)
        {
            return me.Equals(other);
        }
    }
}
