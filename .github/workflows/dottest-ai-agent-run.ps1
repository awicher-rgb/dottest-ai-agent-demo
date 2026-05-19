$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent (Split-Path -Parent $scriptDir)

$env:DOTTEST_ANALYZER_CONFIG = Join-Path -Path $projectRoot -ChildPath ".github\workflows\dottest-analyzer.config"
$env:SOLUTION_PATH = Join-Path -Path $projectRoot -ChildPath "BankExample.slnx"
$env:OUTPUT_DIR = Join-Path -Path $projectRoot -ChildPath ".dottest\agent_output"

$prompt = @"
Use dottest-static-analysis to analyse and fix violations in the project. 
Create a summary and enlist in point what was changed in the project.
"@

Push-Location $projectRoot
& copilot -p $prompt --allow-all-tools --add-dir $projectRoot --add-dir "c:\Program Files\Parasoft\dotTEST\2026.1"
Pop-Location

