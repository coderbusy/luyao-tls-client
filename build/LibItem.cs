using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LibItem
{
    public LibItem()
    {

    }

    public LibItem(string keyword, string dir)
    {
        this.Keyword = keyword;
        this.Directory = dir;
    }
    public string Keyword { get; set; }
    public string Directory { get; set; }
    public string Format { get; set; } = "${name}${ext}";

    public static List<LibItem> List()
    {
        return new List<LibItem>
            {
                new LibItem("darwin-amd64","osx-x64"),
                new LibItem("darwin-arm64","osx-arm64"),
                new LibItem("linux-alpine-amd64","alpine-x64") { Format = "lib${name}${ext}"},
                new LibItem("linux-arm64","linux-arm64"){ Format = "lib${name}${ext}"},
                new LibItem("ubuntu-amd64","linux-x64"){ Format = "lib${name}${ext}"},
                new LibItem("windows-32","win-x86"),
                new LibItem("windows-64","win-x64")
            };
    }
}
