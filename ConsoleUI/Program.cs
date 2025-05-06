using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductTest();

//CategoryTest();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal());

    var result = productManager.GetProductDetails();

    if (result.Success == true)
        foreach (var item in productManager.GetProductDetails().Data)
        {
            Console.WriteLine(item.ProductName + " : " + item.CategoryName);
        }
    else
    {
        Console.WriteLine(result.Message);
    }
}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

    foreach (var item in categoryManager.GetAll())
    {
        Console.WriteLine(item.CategoryName);
    }
}