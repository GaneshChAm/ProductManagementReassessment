﻿using Spectre.Console;
using System.Data;
using System.Data.SqlClient;

namespace ProductManagementReAssessment
{
    public class Product
    {
        SqlConnection con = new SqlConnection("Server=US-CJB79S3; database=Assessment; Integrated Security=true");       
        DataSet ds;
        SqlDataAdapter adp;
        public Product()
        {          
            ds = new DataSet();
            adp = new SqlDataAdapter("select * from ProductsManagement", con);         
            adp.Fill(ds);
           
        }

        public void AddProduct()
        {
            Console.WriteLine("Enter the Product Name:");
            string Pnam = Console.ReadLine();
            Console.WriteLine("Enter the Product Description:");
            string Pdes = Console.ReadLine();
            Console.WriteLine("Enter the Quantity:");
            int Pqua =Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Price:");
            decimal Ppri = Convert.ToDecimal(Console.ReadLine());

            DataRow newrow = ds.Tables[0].NewRow();

            var x = ds.Tables[0].AsEnumerable().Max(x => x["ProductID"]);
            newrow["ProductID"] = Convert.ToInt16(x) +1;
            newrow["ProductName"] = Pnam;
            newrow["ProductDescription"] = Pdes;
            newrow["Quantity"] = Pqua;
            newrow["Price"] = Ppri;
            ds.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Product Added Successfully");
        }
        public void ViewProduct()
        {
            Console.WriteLine("Enter Product ID to view:");
            int ID = Convert.ToInt16(Console.ReadLine());

            var row = ds.Tables[0].Select($"ProductID={ID}");
            if (row.Length > 0)
            {
                var rows = row[0];
                Console.WriteLine($"ProductID : {rows["ProductID"]}\n ProductName : {rows["ProductName"]}\nProductDescription : {rows["ProductDescription"]}\nQuantity : {rows["Quantity"]}\nPrice : {rows["Price"]}");
            }
            else
            {
                Console.WriteLine("Product ID not Exists!");
            }
        }
        public void ViewAllProducts()
        {
            Console.WriteLine("ProductID | ProductName | ProductDescription | Quantity | Price");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($" {ds.Tables[0].Rows[i][j]} |");
                }
                Console.WriteLine();
            }
        }
        public void UpdateProduct() 
        {
            Console.WriteLine("Enter the ProductID to Update");
            int ID = Convert.ToInt32(Console.ReadLine());

            var rows = ds.Tables[0].Select($"ProductID={ID}");
            if (rows.Length > 0)
            {            
                Console.WriteLine("Enter Updated Product Name:");
                string Pnamu = Console.ReadLine();
                Console.WriteLine("Enter Updated Product Description:");
                string Pdesu = Console.ReadLine();
                Console.WriteLine("Enter the Quantity:");
                int Pquau = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Price:");
                decimal Ppriu = Convert.ToDecimal(Console.ReadLine());

                rows[0]["ProductName"] = Pnamu;
                rows[0]["ProductDescription"] = Pdesu;
                rows[0]["Quantity"] = Pquau;
                rows[0]["Price"] = Ppriu;
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                Console.WriteLine("Product Details Updated Successfully");
            }
            else
            {
                Console.WriteLine("Product ID not Exists!");
            }
        }
        public void DeleteProduct()
        {
            Console.WriteLine("Enter the ProductID to Delete");
            int ID = Convert.ToInt32(Console.ReadLine());

            var row = ds.Tables[0].Select($"ProductID={ID}");
            if (row.Length > 0)
            {
                row[0].Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                Console.WriteLine("Product Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Product ID not Exists!");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            string s;
            AnsiConsole.Write(new FigletText(" Product Management App ").Centered().Color(Color.Blue));
            do
            {
                try
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");              
                    AnsiConsole.MarkupLine($"[green] 1. Create Notes [/] ");
                    AnsiConsole.MarkupLine($"[green] 2. View Product [/] ");
                    AnsiConsole.MarkupLine($"[green] 3. View All Products [/] ");
                    AnsiConsole.MarkupLine($"[green] 4. Update Product [/] ");
                    AnsiConsole.MarkupLine($"[green] 5. Delete Product [/]");
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[Red] Enter your Choice [/]");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                p.AddProduct();
                                break;
                            }
                        case 2:
                            {
                                p.ViewProduct();
                                break;
                            }
                        case 3:
                            {
                                p.ViewAllProducts();
                                break;
                            }
                        case 4:
                            {
                                p.UpdateProduct();
                                break;
                            }
                        case 5:
                            {
                                p.DeleteProduct();
                                break;
                            }
                        default:
                            {
                                AnsiConsole.MarkupLine($"[Red] Wrong Choice Entered [/]");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    AnsiConsole.MarkupLine($"[Red] Entered Values Will be only in Numbers [/]");                  
                }
                AnsiConsole.MarkupLine($"[Yellow] Do you wish to continue? (y/n) [/]");
                s = Console.ReadLine();
            } while (s.ToLower() == "y");
        }
    }
}