using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

//ProductManager productManager = new ProductManager(new EfProductDal());

ProductManager productManager = new ProductManager(new EfProductDal());

foreach (var item in productManager.GetByUnitPrice(40,200))
{
    Console.WriteLine(item.ProductName);
}