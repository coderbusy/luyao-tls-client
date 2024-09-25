using Nuke.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Build
{
    Target Print => _ => _
        .Executes(() =>
        {
            Log.Information("GitVersion.Major = {Value}", GitVersion.Major);
            Log.Information("GitVersion.Minor = {Value}", GitVersion.Minor);
            Log.Information("GitVersion.Patch = {Value}", GitVersion.Patch);
            Log.Information("GitVersion.PreReleaseTag = {Value}", GitVersion.PreReleaseTag);
            Log.Information("GitVersion.PreReleaseTagWithDash = {Value}", GitVersion.PreReleaseTagWithDash);
            Log.Information("GitVersion.PreReleaseLabel = {Value}", GitVersion.PreReleaseLabel);
            Log.Information("GitVersion.PreReleaseLabelWithDash = {Value}", GitVersion.PreReleaseLabelWithDash);
            Log.Information("GitVersion.PreReleaseNumber = {Value}", GitVersion.PreReleaseNumber);
            Log.Information("GitVersion.WeightedPreReleaseNumber = {Value}", GitVersion.WeightedPreReleaseNumber);
            Log.Information("GitVersion.BuildMetaData = {Value}", GitVersion.BuildMetaData);
            Log.Information("GitVersion.BuildMetaDataPadded = {Value}", GitVersion.BuildMetaDataPadded);
            Log.Information("GitVersion.FullBuildMetaData = {Value}", GitVersion.FullBuildMetaData);
            Log.Information("GitVersion.MajorMinorPatch = {Value}", GitVersion.MajorMinorPatch);
            Log.Information("GitVersion.SemVer = {Value}", GitVersion.SemVer);
            Log.Information("GitVersion.LegacySemVer = {Value}", GitVersion.LegacySemVer);
            Log.Information("GitVersion.LegacySemVerPadded = {Value}", GitVersion.LegacySemVerPadded);
            Log.Information("GitVersion.AssemblySemVer = {Value}", GitVersion.AssemblySemVer);
            Log.Information("GitVersion.AssemblySemFileVer = {Value}", GitVersion.AssemblySemFileVer);
            Log.Information("GitVersion.FullSemVer = {Value}", GitVersion.FullSemVer);
            Log.Information("GitVersion.InformationalVersion = {Value}", GitVersion.InformationalVersion);
            Log.Information("GitVersion.BranchName = {Value}", GitVersion.BranchName);
            Log.Information("GitVersion.EscapedBranchName = {Value}", GitVersion.EscapedBranchName);
            Log.Information("GitVersion.Sha = {Value}", GitVersion.Sha);
            Log.Information("GitVersion.ShortSha = {Value}", GitVersion.ShortSha);
            Log.Information("GitVersion.NuGetVersionV2 = {Value}", GitVersion.NuGetVersionV2);
            Log.Information("GitVersion.NuGetVersion = {Value}", GitVersion.NuGetVersion);
            Log.Information("GitVersion.NuGetPreReleaseTagV2 = {Value}", GitVersion.NuGetPreReleaseTagV2);
            Log.Information("GitVersion.NuGetPreReleaseTag = {Value}", GitVersion.NuGetPreReleaseTag);
            Log.Information("GitVersion.VersionSourceSha = {Value}", GitVersion.VersionSourceSha);
            Log.Information("GitVersion.CommitsSinceVersionSource = {Value}", GitVersion.CommitsSinceVersionSource);
            Log.Information("GitVersion.CommitsSinceVersionSourcePadded = {Value}", GitVersion.CommitsSinceVersionSourcePadded);
            Log.Information("GitVersion.UncommittedChanges = {Value}", GitVersion.UncommittedChanges);
            Log.Information("GitVersion.CommitDate = {Value}", GitVersion.CommitDate);
        });
}
