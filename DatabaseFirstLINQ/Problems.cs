using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            var users = _context.Users;

            int count = users.Count();
            Console.WriteLine($"The Count of users is {count}");

            // HINT: .ToList().Count

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            var products = _context.Products;

            var productsGreaterThan150 = products.Where(p => p.Price > 150);
            // Then print the name and price of each product from the above query to the console.
            foreach (var product in productsGreaterThan150)
            {
                Console.WriteLine(product.Name + " " + product.Price );
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var products = _context.Products;
            var productsWithS = products.Where(p => p.Name.Contains("s"));
            foreach (var product in productsWithS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var users = _context.Users;
            var usersRegisteredBefore2016 = users.Where(u => u.RegistrationDate.Value.Year < 2016);

            foreach (var user in usersRegisteredBefore2016)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }
            

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            var users = _context.Users;
            var usersRegisteredBetwen2016and2018 = users.Where(u => u.RegistrationDate.Value.Year > 2016 && u.RegistrationDate.Value.Year < 2018);

            foreach (var user in usersRegisteredBetwen2016and2018)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }


        }
        // Then print each user's email and registration date to the console.

    

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            var shopingcart = _context.ShoppingCarts.Include(us => us.Product).Include(us => us.User).Where(us => us.User.Email == "afton@gmail.com");
            
            foreach (ShoppingCart usercart in shopingcart)
            {
                Console.WriteLine($"Product: {usercart.Product.Name} Price: {usercart.Product.Price} Quantity: {usercart.Quantity}");
            }

            // Then print the product's name, price, and quantity to the console.

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var cartSum = _context.ShoppingCarts.Include(us => us.Product).Include(us => us.User).Where(us => us.User.Email == "oda@gmail.com").Select(us => us.Product.Price).Sum();

            Console.WriteLine(cartSum);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employeeUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Employee").ToList();
            

            foreach (UserRole employee in employeeUsers)
            {
                var shoppingCart = _context.ShoppingCarts.Include(us => us.Product).Include(us => us.User).Where(us => us.User == employee.User);
                foreach (ShoppingCart employeeCart in shoppingCart)
                {

                    Console.WriteLine($"Email: {employee.User.Email} Product: { employeeCart.Product.Name} Price: { employeeCart.Product.Price} Quantity: { employeeCart.Quantity}");
                }
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Nintendo64",
                Description = "Best Gaming console",
                Price = 150,

            };
            try
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                Console.WriteLine("The new Product has been saved");
            }
            catch {
                Console.WriteLine("There was an error, please try again");
            }
           

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Nintendo64").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(user => user.Email == "david@gmail.com").Select(user => user.Id).SingleOrDefault();

            ShoppingCart newShoppingCart = new ShoppingCart()

            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1

            };
            try { 
                _context.ShoppingCarts.Add(newShoppingCart);
                _context.SaveChanges();
                Console.WriteLine("New Item Added");
            }
            catch
               {
                Console.WriteLine("There was an error procesing your order, please try again");
            }


        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Nintendo64").SingleOrDefault();
            product.Price = 200;
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var userToDelete = _context.Users.Where(user => user.Email == "oda@gmail.com").SingleOrDefault();

            try 
            { 
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                Console.WriteLine("User has been deleted");
            }
            catch
            {
                Console.WriteLine("There was an error, please trry again");
            }

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            Console.WriteLine("Please Enter your email");
            string userEmail = Console.ReadLine();
            Console.WriteLine("\nPlease Enter your Password");
            string userPassword = Console.ReadLine();

            // Take the email and password and check if the there is a person that matches that combination.

            var userInput = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).SingleOrDefault();
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            try
            {
                if (userInput != null)
                {
                    Console.WriteLine("Signed In!");
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
            }
            catch
            {
                Console.WriteLine("There is an Error, please try again ");
            }
           
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var cartSum = _context.ShoppingCarts.Include(us => us.Product).Include(us => us.User).Where(us => us.User.Email == "oda@gmail.com").Select(us => us.Product.Price).Sum();
            var userList = _context.Users.ToList();
            decimal grandTotals = 0; 
            foreach(User user in userList)
            {
                decimal userTotals = _context.ShoppingCarts.Include(ut => ut.Product).Include(ut => ut.User).Where(ut => ut.User.Email == user.Email).Select(ut => ut.Product.Price).Sum();
                grandTotals += userTotals;
                Console.WriteLine(user.Email +": $" + userTotals);
            }
            Console.WriteLine("Grand Total :" + grandTotals);
        }

        private void DisplayShoppingCart(List<ShoppingCart> shoppingCartProducts)
        {
            foreach (var shoppingCartRow in shoppingCartProducts)
            {
                Console.WriteLine($"ID: {shoppingCartRow.Product.Id}  Name: {shoppingCartRow.Product.Name} Quantity: {shoppingCartRow.Quantity}");
            }

        }
        private void SelectAddProduct(List<ShoppingCart> shoppingCartProducts) 
        {
            Console.WriteLine("Press the id number of a product to add it to your cart\nEnter '0' to exit the selection menu.");
            int itemToAdd = int.Parse(Console.ReadLine());
           
            
            while (itemToAdd != 0)
                foreach (var shoppingCartRow in shoppingCartProducts)
                {

                    if (shoppingCartRow.Product.Id == itemToAdd)
                        {
                            shoppingCartRow.Quantity += 1;
                            try
                                {
                                    _context.ShoppingCarts.Update(shoppingCartRow);
                                    _context.SaveChanges();
                                    Console.WriteLine("Item added to cart");
                                    itemToAdd = int.Parse(Console.ReadLine());
                                    DisplayShoppingCart(shoppingCartProducts);

                        }
                            catch
                                {
                                    Console.WriteLine("There was an error adding the item, please try again");
                                    DisplayShoppingCart(shoppingCartProducts);
                        }
                        }
                    /*else
                    {
                        ShoppingCart newShoppingCart = new ShoppingCart()
                        {
                            UserId = userId,
                            ProductId = itemToAdd,
                            Quantity = 1
                        };
                        _context.ShoppingCarts.Add(newShoppingCart);
                        _context.SaveChanges();
                        Console.WriteLine("Item added to cart");
                        itemToAdd = int.Parse(Console.ReadLine());
                    }*/

                }

        }


        // BIG ONE
        private void BonusThree()
        {

           

            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            Console.WriteLine("Please Enter your email");
            string userEmail = Console.ReadLine();
            Console.WriteLine("\nPlease Enter your Password");
            string userPassword = Console.ReadLine();
            int userId = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).Select(user => user.Id).SingleOrDefault();

            var userInput = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).SingleOrDefault();
         
                if (userInput != null)
                {
                    Console.WriteLine("Signed In!");
                    // a. Give them a menu where they perform the following actions within the console
                    // View the products in their shopping cart
                    // View all products in the Products table
                    List<ShoppingCart> shoppingCartProducts = _context.ShoppingCarts.Include(shoppingCart => shoppingCart.User).Include(shoppingCart => shoppingCart.Product).Where(shoppingCart => shoppingCart.User.Email == userEmail).ToList();
                    Console.WriteLine("Press 1 for View your Shopping Cart\nPress 2 for view all Products\n");
                    int userOption = int.Parse(Console.ReadLine());


                    switch (userOption) 
                    {
                        case 1:
                            DisplayShoppingCart(shoppingCartProducts);
                            SelectAddProduct(shoppingCartProducts);



                        break;
                        case 2:
                            var allProducts = _context.Products.ToList();
                            foreach (var product in allProducts)
                            {
                                Console.WriteLine($"{product.Id}: Product: {product.Name}  Price: {product.Price}");
                            }
                           break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }



                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
               

            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

        }

    }
}
