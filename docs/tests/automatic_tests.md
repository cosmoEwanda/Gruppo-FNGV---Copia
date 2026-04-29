# Automatic Tests
Run automatic tests by the Unity Test Runner framework.

Put reusable `scripts` for testing into `Assets/Tests/_CommonScripts/` folder in order to avoid code duplication.

## Create autmatic tests.
Steps to create new automatic tests:

1. Create a folder in `Assets/Tests/`.
2. According to your needs create either `EditMode` or `PlayMode` subfolders.
3. Each subfolder must have its own assembly definition.
4. Link each assembly definition to the `GameCode` module.
5. If needed link also the `CommonScript` module.
