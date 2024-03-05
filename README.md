## Contribution Guidelines

To safeguard the quality and integrity of the `main` branch, we enforce restricted push access. All contributors must adhere to a branch-based workflow, pushing their changes via pull requests (PRs). This mandatory review process ensures that every contribution is thoroughly vetted and tested before integration, thereby upholding the project's code quality and stability.

### Workflow for Making Contributions

1. **Fork and Clone:**
   Begin by forking the repository. Once forked, clone it to your local machine.

2. **Set Upstream Remote:**
   Establish a link to the original repository by adding it as an upstream remote with the following command:
   ```sh
   git remote add upstream git@github.com:Aarsh2001/RythmiVerse.git
   ```

3. **Branch Off from `main`:**
   Ensure you're working with the latest `main` branch. Create a new feature or fix branch:
   ```sh
   git checkout -b <your-branch-name>
   ```

4. **Implement Your Changes:**
   After making your changes locally, stage them for commit:
   ```sh
   git add . // or specific filename
   ```
   Commit your changes with a descriptive message:
   ```sh
   git commit -m "<your-commit-message>"
   ```

5. **Push to Your Fork:**
   Push your branch and changes to your fork on GitHub:
   ```sh
   git push origin <your-branch-name>
   ```

6. **Create a Pull Request:**
   Navigate to the original repository and open a pull request from your recently pushed branch against the `main` branch. Ensure your PR description clearly describes the changes and any other pertinent details.

Your adherence to these guidelines is greatly appreciated and vital in helping us create a stellar application together. Thank you for your contributions!
