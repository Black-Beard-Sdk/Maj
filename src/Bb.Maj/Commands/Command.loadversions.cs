using Bb.CommandLines;
using Bb.CommandLines.Outs;
using Bb.CommandLines.Validators;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Linq;

namespace Bb.Maj.Commands
{


    /*
    .\Json.exe schema --excelconfig C:\_perso\Src\Sdk\TransformJsonToJson\Src\DocTests\template1.json C:\_perso\Src\Sdk\TransformJsonToJson\Src\DocTests\source1.json | .\Json.exe format
     */

    /// <summary>
    /// 
    /// </summary>
    public static partial class Command
    {


        public static CommandLineApplication CommandVersions(this CommandLineApplication app)
        {

            app.Command("version", config =>
            {

                config.Description = "return the last version";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var packageName = validator.Argument("<github package name>", "github package name");

                config.OnExecute(() =>
                {

                    string name = null;
                    name = packageName.Value.TrimPath();

                    var version = name.ResolveLastVersion();

                    Output.WriteLineStandard($"last version of '{name}' is {version}");

                    return 0;

                });
            });

            app.Command("versions", config =>
            {

                config.Description = "return the last version";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var packageName = validator.Argument("<github package name>", "github package name");

                config.OnExecute(() =>
                {

                    string name = null;
                    name = packageName.Value.TrimPath();

                    var versions = name.GetUrls().ToLookup(c => c.Name);

                    foreach (var item in versions)
                    {

                        Output.WriteLineStandard(item.Key);

                        var i = item.OrderByDescending(c => c.Version);
                        foreach (var item2 in i)
                            Output.WriteLineStandard("  - " + item2.Version.ToString());

                    }

                    return 0;

                });
            });

            app.Command("install", config =>
            {

                config.Description = "return the last version";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var packageNameArgument = validator.Argument("<github package name>", "github package name");
                var artefactNameArgument = validator.Argument("<artefact name>", "specifiy the artifact name");
                var CurrentVersionArgument = validator.Argument("<current version>", "specifiy the current version");
                var targetDirArgument = validator.Argument("<target path>", "specifiy the target directory");

                var processToWaitOpt = validator.Option("--p", "specifiy a process to waiting to end");
                var versionOpt = validator.Option("--v", "specifiy the version to install");

                config.OnExecute(() =>
                {

                    int processId = 0;
                    if (processToWaitOpt.HasValue())
                        processId = int.Parse(processToWaitOpt.Value());

                    string name = null;
                    name = packageNameArgument.Value.TrimPath();

                    string version = null;
                    if (versionOpt.HasValue())
                        version = versionOpt.Value().TrimPath();

                    GitHubVersionHelper.WriteLineStandard = (a) => Output.WriteLineStandard(a);
                    GitHubVersionHelper.WriteLineError = (a) => Output.WriteLineError(a);

                    var _result = GitHubVersionHelper.RunUpdate(processId, name, version, artefactNameArgument.Value.TrimPath(), CurrentVersionArgument.Value, targetDirArgument.Value.TrimPath());

                    return _result;

                });
            });

            return app;

        }

    }
}
