namespace CleanCodeExercises.Tests.E06;

public class Tests
{
    [Fact]
    public void Should_handle_fail_if_name_is_empty()
    {
        var request = new NewProductRequest()
        {
            Name = string.Empty,
            Price = .8M,
        };
        var newProductHandler = new NewProductHandler();

        Action handle = () => newProductHandler.Handle(request, default);

        Assert.Throws<ArgumentException>(handle);
    }

    [Fact]
    public void Should_handle_new_product()
    {
        var request = new NewProductRequest()
        {
            Name = "Pen",
            Price = .8M,
        };
        var newProductHandler = new NewProductHandler();

        newProductHandler.Handle(request, default);
    }
}

public class NewProductHandler
{
    public void Handle(NewProductRequest request, CancellationToken cancellationToken)
    {
        bool isNotValid = false;

        if (request.Price <= 0)
            isNotValid = true;

        if (string.IsNullOrEmpty(request.Name))
            isNotValid = true;

        if (isNotValid)
        {
            throw new ArgumentException();
        }

        Save(cancellationToken, request, out var success);

        if (success == false)
        {
            throw new Exception();
        }
    }

    private void Save(CancellationToken cancellationToken, NewProductRequest request, out bool success)
    {
        //...
        success = true;
    }
}

public class NewProductRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}