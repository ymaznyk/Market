using eMarket.Controllers;
using eMarket.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eMarket.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void LoginTest()
        {
            AccountController controller = new AccountController();
            ViewResult viewRes = new ViewResult();
            LoginViewModel model = new LoginViewModel();
            model.Email = "bkhoronzhuk@gmail.com";
            model.Password = "bkhoronzhuk";
            Assert.IsNotNull(controller.Login(model.Email + model.Password));
        }
        [TestMethod]
        public void LoginFailedTest()
        {
            AccountController controller = new AccountController();
            ViewResult viewRes = new ViewResult();
            LoginViewModel model = new LoginViewModel();
            model.Email = "";
            model.Password = "";
            Assert.AreNotEqual(controller.ViewBag, controller.Login(" "));
        }
        [TestMethod]
        public void AddCartTest()
        {
            int cartId = 1;
            ShoppingCartController controller = new ShoppingCartController();
            //Assert.IsNotNull(controller.AddToCart(5));
        }
        [TestMethod]
        public void DeleteCartTest()
        {
            int cartId = 5;
            ShoppingCartController controller = new ShoppingCartController();
            //Assert.IsNotNull(controller.Delete(5));
        }
    }
}
