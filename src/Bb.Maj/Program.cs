using Bb.CommandLines.Outs;
using Bb.Maj.Commands;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Bb.Maj
{

    public partial class Program
    {

        static Program()
        {

            // ensure all assemblies are loaded.
            //var type = typeof(Bb.Jslt.Services.Excels.Column);

        }

        public static int ExitCode { get; private set; }

        public static void Main(params string[] args)
        {

            CommandLineApplication app = null;
            try
            {

                app = new CommandLineApplication()
                    .Initialize()
                    .CommandVersions()
                    
                ;

                int result = app.Execute(args);

                Output.Flush();

                Environment.ExitCode = Program.ExitCode = result;

            }
            catch (System.FormatException e2)
            {
                FormatException(app, e2);
            }
            catch (CommandParsingException e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = Program.ExitCode = e.HResult;

                app.ShowHelp();

                Environment.ExitCode = Program.ExitCode = 1;

            }
            catch (Exception e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = Program.ExitCode = e.HResult;

                Environment.ExitCode = Program.ExitCode = 1;

            }

        }

        private static void FormatException(CommandLineApplication app, FormatException e2)
        {
            Output.WriteLineError(e2.Message);
            Output.Flush();
            app.ShowHelp();
            Environment.ExitCode = Program.ExitCode = 2;
        }

    }



    //class Program
    //{

    //    // commandline "Black-Beard-Sdk/jslt" 1.0.25

    //    // {0} = https://github.com/
    //    // {1} = "Black-Beard-Sdk/jslt"

    //    // {0}{1} --> https://github.com/Black-Beard-Sdk/jslt

    //    // href="{0}{1}/releases/tag/1.0.27


    //    // <title>Release 1.0.27 · {0}</title>

    //    // https://github.com/{0}/releases/download/1.0.27/cli.zip
    //    // https://github.com/{0}/releases/download/1.0.27/evaluator.zip


    //    // href="/{0}/releases/download/1.0.27/cli.zip"

    //    static void Main(string[] args)
    //    {

    //        string[] a = args.Select(c => Trim(c)).ToArray();

    //        //var parameters = new Parameters()
    //        //{
    //        //    Url = new Uri($"https://github.com/{a[0]}"),
    //        //    //Url = new Uri($"https://github.com/" + $"{a[0]}/releases/latest"),
    //        //    CurrentVersion = new Version(a[1]),
    //        //};

    //        string name = "Black-Beard-Sdk/jslt";

    //        var result = name.GetUrls();
    //        var items = result.ToLookup(c => c.Name).ToList();


    //    }


    //    private static string Trim(string self)
    //    {
    //        var o = self.Trim();

    //        if (o.StartsWith('"') && o.EndsWith('"'))
    //            o = self.Trim().Trim('"');

    //        if (o.StartsWith('\'') && o.EndsWith('\''))
    //            o = self.Trim().Trim('\'');

    //        return o;
    //    }

    //}

}
