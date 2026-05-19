# dottest-static-analysis Skills Example Project (Windows)

This tutorial shows how to integrate the `dottest-analyzer` skill into a coding agent, then use it to fix static analysis violations and generate unit tests in an example project.

Skill sources are located in the repository: [parasoft/dottest-ai-agent-skills](https://github.com/parasoft/dottest-ai-agent-skills)

For this tutorial, we demonstrate integration with GitHub Copilot CLI agent. However, the skill can be used with any coding agent that supports `SKILL.md` skills and has access to the dotTEST MCP tools.

---

## Prerequisites

The following prerequisites apply to both the GitHub Actions and local workstation workflows.

### 1. Install and license Parasoft dotTEST

Parasoft dotTEST 2026.1 or later must be installed and licensed on the machine (or runner) where analysis will execute. Note the installation directory — it is required for the `DOTTEST_HOME` setting.

### 2. Install and configure GitHub Copilot CLI

The GitHub Copilot CLI agent must be installed and authenticated. Refer to the [GitHub Copilot CLI documentation](https://docs.github.com/en/copilot/how-tos/copilot-cli) for installation steps.

When selecting a model, choose one capable of multi-step reasoning and tool use.

### 3. Register the dotTEST MCP Server

Register the dotTEST MCP Server with the Copilot CLI agent so the `dottest-analyzer` skill can invoke dotTEST analysis tools:

```bat
copilot add dottest-mcp-server "[DOTTEST_INSTALL_DIR]\integration\mcb\dottestmcp.bat"
```

Replace `[DOTTEST_INSTALL_DIR]` with your dotTEST installation directory.

---

## Configure settings

The skill reads its configuration from the `.github/workflows/dottest-analyzer.config` file in the demo project, or from environment variables that override it. The full list of available settings is shown below.

> **Note:** Values defined in the config file are loaded first; any environment variable with the same name overrides the file value. You can also point the skill to an alternate config file using the `DOTTEST_ANALYZER_CONFIG` environment variable.

> **Tip:** Keep `DOTTEST_COMMIT_FIXES=false` on your first run. This lets you review proposed changes before they are committed to your branch.

| Setting | Description | Default / Example |
| --- | --- | --- |
| `DOTTEST_HOME` | Path to the dotTEST installation directory | `C:\Program Files\Parasoft\dotTEST\[VERSION]` |
| `SOLUTION_PATH` | Relative or absolute path to the solution file to analyze | `./BankExample.slnx` |
| `OUTPUT_DIR` | Directory where analysis and agent outputs are stored. *(Optional)* | `./.dottest/agent_output` |
| `DOTTEST_TEST_CONFIGURATION` | Test configuration set used by dotTEST for static analysis. *(Optional)* | `builtin://Recommended Rules` |
| `DOTTEST_COMMIT_FIXES` | When `true`, commits each successful fix as a Git commit. *(Optional)* | `false` |
| `DOTTEST_FILTER_RULE` | Comma-separated rule IDs to limit which violations are fixed; when unset all violations are processed. *(Optional)* | — |
| `DOTTEST_SETTINGS` | Absolute path to a `dottestcli.properties` file passed to all dottestcli commands. *(Optional)* | — |
| `DOTTEST_BASE_STATIC_ANALYSIS_REPORT` | Path to an existing baseline static analysis `report.xml` to reuse, skipping the initial analysis step. *(Optional)* | — |
| `DISABLE_UNIT_TEST_VERIFICATION` | If `true`, skips unit test execution during build and fix verification steps. *(Optional)* | `false` |
| `DOTTEST_BASE_UNIT_TEST_REPORT` | Path to an existing unit test `report.xml` baseline; enables Test Impact Analysis when combined with `DOTTEST_BASE_UNIT_TEST_COVERAGE`. *(Optional)* | — |
| `DOTTEST_BASE_UNIT_TEST_COVERAGE` | Path to an existing unit test `coverage.xml` baseline; enables Test Impact Analysis when combined with `DOTTEST_BASE_UNIT_TEST_REPORT`. *(Optional)* | — |
| `FIXES_BRANCH_NAME` | Branch to create and switch to before committing fixes; supports `[timestamp]` substitution. *(Optional)* | `dottest-autofix/[timestamp]` |
| `DOTTEST_FIX_ATTEMPTS` | Number of additional attempts to fix a violation when verification fails after the first attempt. *(Optional)* | `1` |
| `DOTTEST_STATIC_NO_OF_MAX_FIXES` | Maximum number of violations the agent will fix in a single run. *(Optional)* | `3` |
| `DOTTEST_REFERENCE_BRANCH` | Reference branch for diff-based analysis; only violations introduced relative to this branch are processed. *(Optional)* | `master` |
| `DOTTEST_ANALYZER_CONFIG` | Path to an alternate `dottest-analyzer.config` file. *(Optional)* | (defaults to `.github/workflows/dottest-analyzer.config`) |

---

## Running via GitHub Actions (CI/CD)

This section describes how to run the `dottest-analyzer` skill as part of a GitHub Actions workflow. The workflow automates the full cycle — from cloning skills to running analysis and committing fixes. The runner must have Parasoft dotTEST installed and the Copilot CLI installed and configured (see [Prerequisites](#prerequisites) above).

### Step 1: Prepare the repository

Fork or clone the demo project to your GitHub account so you can trigger workflows:

```bat
git clone https://github.com/parasoft/dottest-ai-agent-demo
```

Ensure settings in `.github/workflows/dottest-analyzer.config` are correct for your runner environment, in particular `DOTTEST_HOME` and `SOLUTION_PATH`. You may also override any setting via environment variables in the workflow YAML.

### Step 2: Trigger the workflow

Push a commit or manually dispatch the workflow from the **Actions** tab in your GitHub repository. The workflow will:

1. Check out the demo project.
2. Clone the `dottest-analyzer` skill into the workspace.
3. Run the agent with the configured prompt to detect and fix violations.
4. Optionally commit the fixes back to the repository.

### Step 3: Validate the results

After the workflow completes, review the results:

- Inspect the workflow run log in the **Actions** tab for a summary of violations found and fixes applied.
- If `DOTTEST_COMMIT_FIXES=true`, review the automatically created commits or pull request on your branch.
- If `DOTTEST_COMMIT_FIXES=false`, you can review changes in your `_work` folder of the GitHub Actions Runner.

---

## Running Locally on a Workstation

This section describes how to run the `dottest-analyzer` skill interactively on your local Windows workstation. This is the recommended approach for exploring the skill and reviewing fixes before committing. Ensure you have completed the [Prerequisites](#prerequisites) above before starting.

### Step 1: Check out the demo project

Clone the demo project repository to your local machine:

```bat
git clone https://github.com/parasoft/dottest-ai-agent-demo
cd dottest-ai-agent-demo
```

Verify the project builds successfully before proceeding:

```bat
dotnet build BankExample.slnx
```

> **Note:** Resolve any build errors before running the agent. The skill requires a compilable project to perform analysis.

### Step 2: Deploy the skill into the workspace

The `dottest-analyzer` skill must be present in the workspace so that the Copilot CLI agent can discover and load it automatically. Clone the skills repository into the `.github/skills` folder inside the demo project:

```bat
git clone https://github.com/parasoft/dottest-ai-agent-skills .github\copilot\skills
```

After cloning, your workspace should contain the following skill entry point:

```text
.github\skills\dottest-analyzer\SKILL.md
```

### Step 3: Run autofixes with the CLI agent

Start the GitHub Copilot CLI agent from the demo project root directory with the required permissions:

```bat
copilot -p "Use dottest-analyzer to fix violations in priority order, but fix at most 3 violations in this run. Do not suppress violations. After each fix, run tests and re-run dotTEST verification against the baseline report as required by the skill." ^
  --allow-all-tools ^
  --add-dir "your_workspace_dir" ^
  --add-dir "your_dottest_home_dir"
```

> **Note:** If you prefer to grant only the minimum necessary permissions, replace `--allow-all-tools` with `--allow-tool='tool_name'`.

The agent will:

1. Run `dottestcli` to analyze the solution and detect violations.
2. Prioritize violations by severity and confidence.
3. Propose and apply code fixes for the highest-priority violations.
4. Re-run tests and re-analyze after each fix to verify correctness.
5. Report a summary of all changes made.

> **Tip:** Limit the number of fixes per run (e.g., `at most 3`) so you can inspect each change carefully before proceeding further.

### Step 4: Validate the results

After the agent completes its run, review the changes it made before accepting them. Open the modified files in your IDE or Git client and confirm that each fix correctly addresses the reported violation without altering the intended behavior of the code. Pay particular attention to logic-sensitive areas such as conditionals, error handling, and data transformations.

If a fix looks correct, commit it to your branch. If a fix introduces unintended changes or breaks existing behavior, revert that file and consider adjusting the settings (e.g., `DOTTEST_FILTER_RULE` or `DOTTEST_FIX_ATTEMPTS`) before re-running the agent.

---

## Common failures and quick fixes

| Symptom | Resolution |
| --- | --- |
| `DOTTEST_HOME` not set or invalid | Set `DOTTEST_HOME` to the dotTEST installation directory and verify that `dottestcli.exe` exists at `%DOTTEST_HOME%\bin\dottestcli.exe`. |
| `ANALYZED_PROJECT_PATH` invalid | Ensure the path points to the root of the `dottest-ai-agent-demo` repository. |
| Build or tests fail | Resolve compilation errors and failing tests before running the agent. The skill cannot analyze a project that does not build. |
| Missing `DOTTEST_SETTINGS` file | Verify the path specified in `DOTTEST_SETTINGS` is correct, or unset the variable to use default analysis settings. |
| No violations found | This is a valid outcome. It means the project is already compliant with the configured rule set. |
| No tests generated | The code may already have sufficient test coverage, or the methods under analysis may not be well-suited for automated test generation. |
| MCP Server not found | Confirm that the `copilot add dottest-mcp-server` command completed successfully and that the path to `dottestmcp.bat` is correct. |

---

## References

- dotTEST skills used in this tutorial:
  - [`dotTEST Analyzer Skills`](https://github.com/parasoft/dottest-ai-agent-skills)
- Copilot skills docs:
  - [GitHub Copilot CLI skills documentation](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/create-skills)
- Codex CLI skills docs:
  - [OpenAI Codex CLI skills documentation](https://developers.openai.com/codex/skills)
