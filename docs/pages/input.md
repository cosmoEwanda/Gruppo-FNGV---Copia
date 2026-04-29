# Input System 
Input is decoupled from every system logic to facilitate testing and allow the addition of other controllers (e.g., Gamepad). This decoupling is achieved through **Dependency Injection**.

*   **Interface:** `IInputProvider`
*   **Current Class:** `KeyboardInput`
*   **Mockup Class:** `MockInput`

Every component that relies on the user input **must** implement the `IRequireInput` interface.

## InputInjector
The class `InputInjector` is responsible of change the way components recieve input commands. 
Through the method `InputInjector.ApplyGlobally(IInputProvider)` it will fetch all the `GameObjects` in the scene and apply `SetInputProvider` function on the components that implement `IRequireInput` interface. **Note**: this method is pretty expensive, it should be called only few times during the runtime (i.e. just at scene startup).


