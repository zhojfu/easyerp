using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Payments;
    using Domain.Model.Payments;
    using EasyERP.Web.Models.Payments;
    using EasyERP.Web.Models.Products;
    using System.Web.Routing;

    public class PaymentController : Controller
    {
        private IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public ActionResult PayInfo(int id)
        {
            var payment = paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return new HttpNotFoundResult();
            }

            var paymentModel = new PaymentModel()
            {
                Id = payment.Id,
                DueDateTime = payment.DueDateTime,
                TotalAmount = payment.TotalAmount,
                Paid = payment.Items.Sum(i => i.Paid)
            };

            return View(paymentModel);
        }

        [HttpPost]
        public ActionResult PayInfo(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = paymentService.GetPaymentById(model.Id);
                if (payment == null)
                {
                    return new HttpNotFoundResult();
                }

                var item = payment.Items.Select(
                    i => new
                    {
                        PayDate = i.PayDataTime,
                        PayAmount = i.Paid
                    });

                return Json(item);
            }

            return RedirectToAction(
                "PayInfo",
                new
                {
                    Id = model.Id
                });
        }

        [HttpPost]
        public ActionResult Pay(PayItemModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = paymentService.GetPaymentById(model.Id);
                if (payment == null)
                {
                    return new HttpNotFoundResult();
                }

                payment.Items.Add(new PayItem()
                {
                    PayDataTime = DateTime.Now,
                    Paid = model.PayAmount
                });

                paymentService.UpdatePayment(payment);
            }

            return RedirectToAction(
                "PayInfo",
                new
                {
                    Id = model.Id
                });
        }
    }
}