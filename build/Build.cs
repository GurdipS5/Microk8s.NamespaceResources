using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode


    /// <summary>
    ///
    /// </summary>
    public string CodeCoverage = "true";

    /// <summary>
    ///
    /// </summary>
    public string Space = " ";

    #region NetCoreBuild

    public string framework = "net8.0";
    string SelfContained = string.Empty;
    string runtime = "win-x64";

    #endregion

    #region Secrets

    /// <summary>
    ///  ProGet server key.
    /// </summary>
    [Parameter][Secret] readonly string NuGetApiKey;

    /// <summary>
    /// Codecov test token.
    /// </summary>
    [Secret][Parameter] readonly string CODECOV_SECRET;

    /// <summary>
    /// Sonarqube API Key.
    /// </summary>
    [Secret][Parameter] readonly string SonarKey;

    /// <summary>
    /// GitHub PAT token.
    /// </summary>
    [Secret][Parameter] readonly string GitHubToken;

    /// <summary>
    /// Sny API Token.
    /// </summary>
    [Secret][Parameter] readonly string SNYK_TOKEN;

    /// <summary>
    ///     License key for Report Generator.
    /// </summary>
    [Parameter][Secret] readonly string ReportGeneratorLicense;

    /// <summary>
    ///     Dependency Track API Key.
    /// </summary>
    [Parameter][Secret] readonly string DTrackApiKey2;

    #endregion


    #region Paths

    /// <summary>
    ///
    /// </summary>
    readonly AbsolutePath UtilsDir = RootDirectory / "Utilities";

    /// <summary>
    /// s.
    /// </summary>
    readonly AbsolutePath BuildDir = RootDirectory / "Nuke" / "Output" / "Build";

    /// <summary>
    /// Codecov config file.
    /// </summary>
    readonly AbsolutePath CodecovYml = RootDirectory / "codecov.yml";

    /// <summary>
    /// Artifacts directory.
    /// </summary>
    readonly AbsolutePath Artifacts = RootDirectory / "Nuke" / "Artifacts";

    /// <summary>
    ///  Output of coverlet code  coverage report.
    /// </summary>
    readonly AbsolutePath QodanaOut = RootDirectory / "Nuke" / "Qodana" / "Results";

    /// <summary>
    ///  Output of coverlet code  coverage report.
    /// </summary>
    readonly AbsolutePath QodanaReport = RootDirectory / "Nuke" / "Qodana" / "Report";

    /// <summary>
    ///  Output of coverlet code  coverage report.
    /// </summary>
    readonly AbsolutePath QodanaCache = @"D:\QodanaCache";

    /// <summary>
    ///  Output of coverlet code  coverage report.
    /// </summary>
    readonly AbsolutePath CoverletOutput = RootDirectory / "Nuke" / "Output" / "Coverlet";

    /// <summary>
    /// NDependOutput folder.
    /// </summary>
    readonly AbsolutePath NukeOut = RootDirectory / "Nuke";

    /// <summary>
    ///  NDependOutput folder.
    /// </summary>
    readonly AbsolutePath NDependOutput = RootDirectory / "Nuke" / "Output" / "NDependOut";

    /// <summary>
    /// GGShield config file.
    /// </summary>
    readonly AbsolutePath GgConfig = RootDirectory / "gitguardian.yml";

    /// <summary>
    /// Dotnet publish output directory
    /// </summary>
    readonly AbsolutePath PublishFolder = RootDirectory / "Nuke" / "Output" / "Publish";

    /// <summary>
    /// PVS Studio log output folder.
    /// </summary>
    readonly AbsolutePath PvsStudio = RootDirectory / "Nuke" / "Output" / "PVS";

    /// <summary>
    ///     Path to nupkg file from the project
    /// </summary>
    readonly AbsolutePath NupkgPath = RootDirectory / "Nuke" / "Output" / "Nuget";

    /// <summary>
    /// Coverlet report folder.
    /// </summary>
    readonly AbsolutePath ReportOut = RootDirectory / "Nuke" / "Output" / "Coverlet" / "Report";

    /// <summary>
    ///  Output directory of the SBOM file from CycloneDX
    /// </summary>
    readonly AbsolutePath Sbom = RootDirectory / "Nuke" / "Output" / "SBOM";


    /// <summary>
    /// Filename of changelog file.
    /// </summary>
    string ChangeLogFile => RootDirectory / "changelog.md";

    /// <summary>
    ///  Docfx folder.
    /// </summary>

    AbsolutePath DocFxLibrary => RootDirectory / "docfx_project";

    /// <summary>
    /// Directory of MSTests project.
    /// </summary>
    AbsolutePath TestsDirectory => RootDirectory / "Tests";

    /// <summary>
    /// Directory of MSTests project.
    /// </summary>
    AbsolutePath CoverletSrc => RootDirectory / "Tests" / "unittestresults";


    /// <summary>
    /// Target path.
    /// </summary>
    readonly AbsolutePath TargetPath = RootDirectory / "Nuke" / "Output" / "Coverlet" / "Report";



    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });

}
