//using Business.Concrete;
//using Business.Constants;
//using Core.Utilities.Results;
//using DataAccess.Concrete.EntityFramework;
//using DataAccess.Concrete.InMemory;

////ProductTest();

////CategoryTest();

//static void ProductTest()
//{
//    ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal));

//    var result = productManager.GetProductDetails();

//    if (result.Success == true)
//        foreach (var item in productManager.GetProductDetails().Data)
//        {
//            Console.WriteLine(item.ProductName + " : " + item.CategoryName);
//        }
//    else
//    {
//        Console.WriteLine(result.Message);
//    }
//}

//static void CategoryTest()
//{
//    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

//    var result = categoryManager.GetAll();

//    foreach (var item in result.Data)
//    {
//        Console.WriteLine(item.CategoryName + " / " + result.Message + " / " + result.Success);
//    }
//}