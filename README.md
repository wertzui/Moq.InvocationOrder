# Moq.InvocationOrder
Provides an easy way to verify the invocation order of your mocks!

## Example
```
// Arrange
var order = new MockInvocationOrder();
var mock1 = new Mock<object>();
var mock2 = new Mock<object>();

mock1.SetupWithOrder(o => o.ToString(), order);
mock2.SetupWithOrder(o => o.GetHashCode(), order);

// Act
mock1.Object.ToString();
mock2.Object.GetHashCode();

// Assert
mock1.VerifyWasCalledAfter(o => o.GetHashCode(), mock2, o => o.ToString(), order);
```
