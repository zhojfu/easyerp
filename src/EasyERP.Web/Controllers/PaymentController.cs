using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Payments;
    using EasyERP.Web.Models.Payments;
    using EasyERP.Web.Models.Products;

    public class PaymentController : Controller
    {
        private IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        // GET: Payment
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult PayInfo(int Id)
        {
            var payment = paymentService.GetPaymentByInventoryId(Id);
            if (payment == null)
            {
                return new HttpNotFoundResult();
            }

            var paymentModel = new PaymentModel()
            {
                Id = payment.Id,
                DueDateTime = payment.DueDateTime,
                TotalAmount = payment.TotalAmount,
                Paid = payment.Items.Sum(i => i.Paid),
                PayItems = payment.Items.Select(
                    i => new PaymentModel.PayItemModel
                    {
                        PayAmount = i.Paid,
                        PayDate = i.PayDataTime
                    }).ToList()
            };

            return View(paymentModel);
        }
    }
}