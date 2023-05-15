using Spectre.Console;
using System.Data;
using System.Data.SqlClient;

namespace ProductManagementReAssessment
{
    public class Product
    {
        SqlConnection con = new SqlConnection("Server=US-CJB79S3; database=Assessment; Integrated Security=true");
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
            SqlDataAdapter adp1 = new SqlDataAdapter("select * from ProductsManagement", con);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            DataRow newrow = ds.Tables[0].NewRow();
            newrow["ProductName"] = Pnam;
            newrow["ProductDescription"] = Pdes;
            newrow["Quantity"] = Pqua;
            newrow["Price"] = Ppri;
            ds.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp1);
            adp1.Update(ds);
            Console.WriteLine("Product Added Successfully");
        }
        public void ViewProduct()
        {
            Console.WriteLine("Enter Product ID to view:");
            int ID = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp2 = new SqlDataAdapter($"select * from ProductsManagement Where ProductID ={ID}", con);
            DataSet ds = new DataSet();
            adp2.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow rows = ds.Tables[0].Rows[0];
                Console.WriteLine("Product Name: {0}", rows["ProductName"]);
                Console.WriteLine("Product Description: {0} ", rows["ProductDescription"]);
                Console.WriteLine("Quantity: {0}", rows["Quantity"]);
                Console.WriteLine("Price: {0}", rows["Price"]);
            }
            else
            {
                Console.WriteLine("Product ID not Exists!");
            }
        }
        public void ViewAllProducts()
        {
            SqlDataAdapter adp3 = new SqlDataAdapter($"select * from ProductsManagement", con);
            DataSet ds = new DataSet();
            adp3.Fill(ds);
            Console.WriteLine("ProductID|ProductName|ProductDescription|Quantity|Price");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]}  | ");
                }
                Console.WriteLine();
            }
        }
        public void UpdateProduct() 
        {
            Console.WriteLine("Enter the ProductID to Update");
            int ID = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp4 = new SqlDataAdapter($"Select * from ProductsManagement where ProductID={ID}", con);
            DataSet ds = new DataSet();
            adp4.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {            
                Console.WriteLine("Enter Updated Product Name:");
                string Pnamu = Console.ReadLine();
                Console.WriteLine("Enter Updated Product Description:");
                string Pdesu = Console.ReadLine();
                Console.WriteLine("Enter the Quantity:");
                int Pquau = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Price:");
                decimal Ppriu = Convert.ToDecimal(Console.ReadLine());
                DataRow row = ds.Tables[0].Rows[0];            
                row["ProductName"] = Pnamu;
                row["ProductDescription"] = Pdesu;
                row["Quantity"] = Pquau;
                row["Price"] = Ppriu;
                SqlCommandBuilder builder = new SqlCommandBuilder(adp4);
                adp4.Update(ds);
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
            SqlDataAdapter adp5 = new SqlDataAdapter($"Select * from ProductsManagement where ProductID={ID}", con);
            DataSet ds = new DataSet();
            adp5.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                row.Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(adp5);
                adp5.Update(ds);
                Console.WriteLine("Product Details Deleted Successfully");
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
            do
            {
                try
                {
                    Console.WriteLine("--------------------------------------------------");
                    AnsiConsole.MarkupLine($"[Yellow] Welcome to Product Management App [/]");
                    AnsiConsole.MarkupLine($"[green] 1. Create Notes [/] ");
                    AnsiConsole.MarkupLine($"[green] 2. View Product [/] ");
                    AnsiConsole.MarkupLine($"[green] 3. View All Products [/] ");
                    AnsiConsole.MarkupLine($"[green] 4. Update Product [/] ");
                    AnsiConsole.MarkupLine($"[green] 5. Delete Product [/]");
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[DarkRed] Enter your Choice [/]");
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
                                Console.WriteLine("Wrong Choice Entered");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entered Values Will be only in Numbers");
                }
                Console.WriteLine("Do you wish to continue? [y/n] ");
                s = Console.ReadLine();
            } while (s.ToLower() == "y");
        }
    }
}