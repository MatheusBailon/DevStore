using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DevStore.Domain;
using DevStore.Infra;

namespace DevStore.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class ProductController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        //Modificando o roteamento
        [Route("product")]

        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Colocando entre "{" chaves isso se torna um parametro da url
        [Route("Categories/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategoryId(int categoryId)
        {
            var result = db.Products.Include("Category").Where(x =>x.CategoryId== categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }



        //Post, quando é post preciso informar isso [HttpPost]. Detalhe: apenas o Get que não precisa desse "parametro" pois é o serviço padrão.
        [HttpPost]
        [Route("product")]
        public HttpResponseMessage PostProduct(Products product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,$"Erro ao incluir o produto: {e.Message}");
            }
        }


        //Patch
        [HttpPatch]
        [Route("product")]
        public HttpResponseMessage PatchProduct(Products product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Products>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var response = product;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError,$"Erro ao atualizar Produto.Erro: {e.Message}");
            }
        }

        //Put - igual ao Patch
        [HttpPut]
        [Route("product")]
        public HttpResponseMessage PutProduct(Products product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Products>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var response = product;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Erro ao atualizar Produto.Erro: {e.Message}");
            }
        }

        //Delete
        [HttpDelete]
        [Route("product")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            if (id<0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                Products product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, $"Produto {product} deletado com sucesso!!");
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Erro ao apagar Produto.Erro: {e.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}