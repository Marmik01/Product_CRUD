using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example.DAL;
using ADO_Example.Models;

namespace ADO_Example.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL _productDAL = new ProductDAL(); 
        // GET: Product
        public ActionResult Index()
        {
            var productList = _productDAL.GetAllProducts();
            
            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently product not available in the Database.";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully...!";
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available/unable to save the product details.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = _productDAL.GetProductByID(id).FirstOrDefault();

            if(products == null )
            {
                TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available/unable to update the product details.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error Message"] = ex.Message;
                return View(); 
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["ErrorMessage"] = "Product not deleted...";
                    
                }
                else
                {
                    TempData["SuccessMessage"] = "Product deleted successfully...";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
                
            }
        }

    }
}
