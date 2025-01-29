namespace CleanCodeExercises.Tests.E08;

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
}

public class NewProductHandler
{
    public void Handle(NewProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Price <= 0)
                throw new ArgumentException();

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException();

            Save(request, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void Save(NewProductRequest request, CancellationToken cancellationToken)
    {
        //...
    }
}

public class NewProductRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}