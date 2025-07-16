using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "ProductAdded";
        public static string ProductNameInvalid = "Product Name can not be less than 2 chracters";
        public static string MaintenanceTime = "No queries after 22pm";
        public static string ProductsListed = "ProductsListed";
        public static string CategoryListed = "CategoryListed";
        public static string ProductCountOfCategoryError = "ProductCountOfCategoryError";
        public static string ProductNameExistsError = "ProductNameExistsError";
        public static string CategoryLimitExceded = "CategoryLimitExceded";
        public static string AuthorizationDenied = "AuthorizationDenied";
        public static string UserRegistered = "UserRegistered";
        public static string UserNotFound = "UserNotFound";
        public static string PasswordError = "PasswordError";
        public static string SuccessfulLogin = "SuccessfulLogin";
        public static string UserAlreadyExists = "UserAlreadyExists";
        public static string AccessTokenCreated = "AccessTokenCreated";
    }
}
