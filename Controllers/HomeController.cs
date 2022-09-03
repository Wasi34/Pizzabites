using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Pizzabites.Models;

namespace Pizzabites.Controllers
{
    public class HomeController : Controller
    {

        PizzabitesEntities db = new PizzabitesEntities();
        public static int contactE;
        public static List<int> CartItems = new List<int>();
        public static int search;

        public ActionResult HomePage()
        {
            List<Product> products = db.Products.ToList();
            return View(products);

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (!(customer.CUSEmail.EndsWith(".com") || customer.CUSEmail.EndsWith(".edu")))
            {
                ViewBag.Notification = "Email Format Incorrect.";
                return View();
            }else if(!Regex.IsMatch(customer.CUSContactNo, @"^\d+$"))
            {
                ViewBag.Notification = "Contact Number can not contain letters";
                return View();

            }
            else if (customer.CUSContactNo.Length != 11 || !customer.CUSContactNo.StartsWith("01"))
            {
                ViewBag.Notification = "Contact Number must be 11 digit.";
                return View();
            }
            else if (customer.CUSPassword.Length<6)
            {
                ViewBag.Notification = "Password must be 6 characters long.";
                return View();

            }
            else if (db.Customers.Any(x => x.CUSEmail == customer.CUSEmail ))
            {
                ViewBag.Notification = "This account already exists.";
                return View();
            }
            else if (db.Customers.Any(x => x.CUSEmail == "Pizzabites@gmail.com"))
            {
                ViewBag.Notification = "Try another email.";
                return View();
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                Session["CUSName"] = customer.CUSName.ToString();
                Session["CUSEmail"] = customer.CUSEmail.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Login()
        {
            if (Session["CUSName"] != null)
            {
                ViewBag.loggedIn = "User already logged in";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            if (customer.CUSEmail.Equals("Pizzabites@gmail.com") && customer.CUSPassword.Equals("pizzabites"))
            {
                ViewBag.Login = "Admin Logged In";
                Session["CUSName"] = "Admin";
                Session["CUSEmail"] = "Pizzabites@gmail.com";
                // ViewBag.Login = "Admin Logged In";
                return RedirectToAction("Index", "Home");
            }
            var checklogin = db.Customers.Where(x => x.CUSEmail.Equals(customer.CUSEmail) && x.CUSPassword.Equals(customer.CUSPassword)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["CUSName"] = checklogin.CUSName;
                Session["CUSEmail"] = customer.CUSEmail.ToString();
                ViewBag.Login = "Logged In";
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or Password";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProductPreview(int id)
        {

            Product product = db.Products.Where(temp => temp.PRID.Equals(id)).SingleOrDefault();
            List<Comment> comments = db.Comments.Where(temp => temp.ProductID.Equals(id)).ToList();
            return View(new object[] { product, comments });

        }

        [HttpPost]
        public ActionResult CommentPost(String ProductID, string CommentMessage)
        {
           
            
                int id = Int32.Parse(ProductID);
                Comment comment = new Comment
                {
                    ProductID = id,
                    CommentMessage = CommentMessage,
                    CustName = Session["CUSName"].ToString()
                };
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ProductPreview/" + ProductID);
            
        }

        public ActionResult ProductPage(string category, string page)
        {
            List<Product> products;
            var sql = "";
            int p;
            double resultPerPage;
            if (page == null)
            {
                p = 1;
            }
            else
            {
                p = Int32.Parse(page);
            }
            if (category == null)
            {

                sql = "Select * from Product";
                resultPerPage = 6;
                ViewBag.productCategory = "All Products";
            }
            else
            {
                //products = db.Products.Where(temp => temp.PRType.Equals(category)).ToList();
                sql = "Select * from Product where PRType = '" + category + "' ";
                ViewBag.Category = category;
                resultPerPage = 3;
                ViewBag.productCategory = category;
            }
            products = db.Products.SqlQuery(sql).ToList();

            var pageFirstresult = (p - 1) * resultPerPage;
            double numberOfresult = products.Count;
            double numberOfPage = Math.Ceiling(numberOfresult / resultPerPage);
            System.Diagnostics.Debug.WriteLine(numberOfPage);

            ViewBag.Page = p;
            ViewBag.NumberOfPages = numberOfPage;
            return View(products);
        }


        public ActionResult ContactUs()
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
               
            }

            ContactU contactUs = new ContactU();
                System.Diagnostics.Debug.WriteLine(contactE);
                if (contactE == 1)
                {
                    contactE = 0;
                    ViewBag.Noti = "Email Format Incorrect";
                }
                if (Session["CUSName"] != null)
                {
                    string email = Session["CUSEmail"].ToString();
                    Customer customer = db.Customers.Where(temp => temp.CUSEmail.Equals(email)).SingleOrDefault();
                    contactUs.Email = customer.CUSEmail;
                    contactUs.FullName = customer.CUSName;
                }
                else
                {
                    contactUs.Email = "";
                    contactUs.FullName = "";
                }
                return View(contactUs);
            
        }

        [HttpPost]
        public ActionResult ContactSummary()
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {
                List<ContactU> contacts;
                var sql = "";
                ContactU contactUs = new ContactU();
                // System.Diagnostics.Debug.WriteLine(contactE);

                if (Session["CUSName"] != null)
                {
                    string email = email = Session["CUSEmail"].ToString(); ;
                    if (email.Equals("Pizzabites@gmail.com"))
                    {
                        sql = "Select * from ContactUs";
                    }
                    else
                    {
                        sql = "Select * from ContactUs where Email = '" + email + "' ";
                    }
                }
                contacts = db.ContactUs.SqlQuery(sql).ToList();

                return View(contacts);
            }
        }

        [HttpPost]
        public ActionResult ContactUs(ContactU contactU)
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {
                if (!(contactU.Email.EndsWith(".com") || contactU.Email.EndsWith(".edu")))
                {
                    contactE = 1;
                }
                else
                {
                    db.ContactUs.Add(contactU);
                    db.SaveChanges();
                }
                return RedirectToAction("ContactUs");
            }

        }
        public ActionResult CustomerProfile()
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine(Session["CUSName"]);
                string username = Session["CUSEmail"].ToString();
                Customer customer = db.Customers.Where(temp => temp.CUSEmail.Equals(username)).SingleOrDefault();

                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(Customer customer)
        {
            
                customer.CUSEmail = Session["CUSEmail"].ToString();
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["CUSName"] = customer.CUSName;
                return RedirectToAction("CustomerProfile");
            
        }

        public ActionResult AddToCart(int id)
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {

                bool flag = true;
                foreach (var item in CartItems)
                {
                    if (item.Equals(id))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    CartItems.Add(id);
                    ViewBag.AddedItem = "Product Added to Cart";
                }
                else
                {
                    ViewBag.AddedItem = "Item Already in Cart";
                }
                return RedirectToAction("Orders");
            }
        }

        public ActionResult Orders()
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {
                List<Product> products = new List<Product>();

                foreach (var item in CartItems)
                {
                    products.Add(db.Products.Where(temp => temp.PRID.Equals(item)).SingleOrDefault());
                }

                return View(products);
            }
        }
        public ActionResult ConfirmOrder(int? totalvalue,String AdressDetails,String PaymentMethod)
        {
            
            OrderList orderlist = new OrderList();
            orderlist.cusEmail = Session["CUSEmail"].ToString();
            orderlist.orderDate = DateTime.Now;
            orderlist.totalPrice = totalvalue;
            orderlist.cusAddress = AdressDetails;
            orderlist.PaymentMethod=PaymentMethod;


            string items = "";
            Product prod;

            foreach (var item in CartItems)
            {
                prod = db.Products.Where(temp => temp.PRID.Equals(item)).SingleOrDefault();
                items = items + prod.PRName + "";
            }

            orderlist.itemName = items;
            if (totalvalue == null)
            {
                ViewBag.Orders = "Order something";
            }
            else
            {
                db.OrderLists.Add(orderlist);
                db.SaveChanges();
                ViewBag.Orders = "Order Placed";
            }
            CartItems.Clear();
         
            return RedirectToAction("OrderHistory");
        }
      

        public ActionResult RemoveFromCart(Product product)
        {
            var item = CartItems.Single(temp => temp == product.PRID);
            if (item != 0)
            {
                CartItems.Remove(item);
            }
            return RedirectToAction("Orders");
        }

        public ActionResult OrderHistory()
        {
            if (Session["CUSEmail"] == null)
            {
                ViewBag.UserLogin = "Log in first";
                return RedirectToAction("Index");
            }
            else
            {

                List<OrderList> orderList;

                if (Session["CUSEmail"].ToString().Equals("Pizzabites@gmail.com"))
                {
                    orderList = db.OrderLists.ToList();

                }
                else
                {
                    string email = Session["CUSEmail"].ToString();

                    orderList = db.OrderLists.Where(temp => temp.cusEmail.Equals(email)).ToList();
                }

                return View(orderList);
            }
        }

        [HttpPost]
        public ActionResult ProductSearch(FormCollection form)
        {
            string pName = form["search"];
            List<Product> products;
            var sql = "";

            if (pName == null)
            {
                sql = "Select * from Product";
            }
            else
            {
                sql = "Select * from Product where PRName Like '%" + pName + "%' ";
            }
            products = db.Products.SqlQuery(sql).ToList();

            if (products.Count == 0)
            {
                ViewBag.Message = string.Format(pName + " Not Found");
            }
            return View(products);
        }

    }
}




