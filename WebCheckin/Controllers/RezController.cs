using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using WebCheckin.Models;

namespace WebCheckin.Controllers
{
    public class RezController : Controller
    {
        // GET: Rez
        public ActionResult Index()
        {
            
            using (ISession session = NHibernateSession.OpenSession())
            {
                var employees = session.Query<Reservations>().ToList();
                return View(employees);
            }
        }

        // GET: Rez/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rez/Create
        [HttpPost]
        public ActionResult Create(Reservations reservations)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(reservations);
                    transaction.Commit();
                }
            }
            SendEmail(reservations.Email,reservations.RezId);
            TempData["alertMessage"] = "A confirmation email has been sent to you. You can now Web Checkin!!";
            return RedirectToAction("Index");
        }

        // GET: Rez/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var reservations = session.Query<Reservations>().Single(emp => emp.RezId == id);
                return View(reservations);
            }
        }

        // POST: Rez/Edit/5
        [HttpPost]
        public ActionResult Edit(Reservations reservations)
        {
            
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(reservations);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            
        }

        // GET: Rez/Delete/5
        
        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var reservations = session.Query<Reservations>().Single(emp=> emp.RezId == id);
                return View(reservations);
            }
        }
        
        // POST: Rez/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            
                using (ISession session = NHibernateSession.OpenSession())
                {
                    Reservations reservations = session.Get<Reservations>(Convert.ToInt32(collection["Id"]));

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(reservations);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            
        }

        private void SendEmail(string to,int id)
        {
            string password = "2218396!"; //(ConfigurationManager.AppSettings["password"]);
            string from = "singlagaurav919@gmail.com"; //Replace this with your own correct Gmail Address
            
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "Reservation confirmed";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Hi your reservation has been conformed with Reservation Id "+id+ ". Click this link to perform Web Checkin http://localhost:61000/Home/Index";

            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();

            //Add the Creddentials- use your own email id and password
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, password);
            client.Port = 587; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer


            client.Send(mail);
            Response.Write("Message Sent...");
        }
    }
}
