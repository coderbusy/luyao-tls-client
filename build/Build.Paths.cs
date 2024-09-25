using Nuke.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Build
{
    AbsolutePath CacheDirectory => RootDirectory / "cache";
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath PackageDirectory => OutputDirectory / "packages";
    AbsolutePath RuntimesDirectory => SourceDirectory / "LuYao.TlsClient" / "runtimes";
}
